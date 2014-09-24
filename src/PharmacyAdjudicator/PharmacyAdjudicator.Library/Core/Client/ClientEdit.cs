using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Client
{
    [Serializable]
    public class ClientEdit : BusinessBase<ClientEdit>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        [Required]
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
        [Required]
        public string ClientId
        {
            get { return GetProperty(ClientIdProperty); }
            private set { SetProperty(ClientIdProperty, value); }
        }

        private Guid _ClientInternalId;
        private Guid ClientInternalId
        {
            get { return _ClientInternalId; }
            set { _ClientInternalId = value; }
        }

        private Guid _RecordId;
        private Guid RecordId
        {
            get { return _RecordId; }
            set { _RecordId = value; }
        }

        public static bool Exists(string clientId)
        {
            return ClientExistsCommand.Execute(clientId);
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            //Requires that the user be in the RuleManager role to create, edit or delete an Client object
            Csla.Rules.BusinessRules.AddRule(typeof(ClientEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
            Csla.Rules.BusinessRules.AddRule(typeof(ClientEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(ClientEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(ClientEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "RuleManager", "Manager", "Admin"));
        }

        #endregion

        #region Factory Methods

        #if !WINDOWS_PHONE
        public async static Task<ClientEdit> GetByClientIdAsync(string clientId)
        {
            return await DataPortal.FetchAsync<ClientEdit>(new CriteriaByClientId() { ClientId = clientId });
        }

        #endif

#if !SILVERLIGHT && !NETFX_CORE
        public static ClientEdit NewClient(string clientId)
        {
            return DataPortal.Create<ClientEdit>(clientId);
        }

        public static ClientEdit GetByClientId(string clientId)
        {
            return DataPortal.Fetch<ClientEdit>(new CriteriaByClientId() { ClientId = clientId });
        }

        public static void DeleteByClientId(string clientId)
        {
            DataPortal.Delete<ClientEdit>(clientId);
        }

        private ClientEdit()
        { /* Require use of factory methods */ }
#endif

        #endregion

        #region Data Access

        [Serializable]
        private class CriteriaByClientId : CriteriaBase<CriteriaByClientId>
        {
            public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
            public string ClientId
            {
                get { return ReadProperty(ClientIdProperty); }
                set { LoadProperty(ClientIdProperty, value);  }
            }
        }

        [RunLocal]
        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
        }

        protected void DataPortal_Create(string clientId)
        {
            if (ClientEdit.Exists(clientId))
                throw new DataAlreadyExistsException("ClientId = " + clientId);
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                DataAccess.Client newClient = new DataAccess.Client();
                this.ClientInternalId = Guid.NewGuid();
                newClient.ClientInternalId = this.ClientInternalId;
                newClient.RecordCreatedDateTime = DateTime.Now;
                newClient.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                ctx.DbContext.Client.Add(newClient);
                ctx.DbContext.SaveChanges();
                this.ClientId = clientId;
            }
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(CriteriaByClientId criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var patientData = (from c in ctx.DbContext.ClientDetail
                                   where c.ClientId == criteria.ClientId
                                   && c.Retraction == false
                                   && !ctx.DbContext.ClientDetail.Any(c2 => c2.ClientInternalId == c.ClientInternalId && c2.Retraction == true && c2.OriginalFactRecordId == c.RecordId)
                                   select c).ToList();
                if (patientData.Count == 0)
                    throw new DataNotFoundException("ClientId = " + criteria.ClientId);
                if (patientData.Count > 1)
                    throw new DataNotUniqueException("ClientId = " + criteria.ClientId);
                PopuldateByRow(patientData[0]);
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            { 
                AssertNewFact();
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                RetractFact();
                AssertNewFact();
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            RetractFact();
            DataPortal_Delete(this.ClientId);
        }

        private void DataPortal_Delete(string criteria)
        {
            throw new NotImplementedException();
        }

        private DataAccess.ClientDetail AssertNewFact()
        {
            var clientData = CreateNewEntity();
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                ctx.DbContext.ClientDetail.Add(clientData);
            }
            return clientData;
        }

        private void RetractFact()
        {
            using (BypassPropertyChecks)
            {
                var originalRecordId = this.RecordId;
                var clientData = CreateNewEntity();
                clientData.Retraction = true;
                clientData.OriginalFactRecordId = originalRecordId;
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    ctx.DbContext.ClientDetail.Add(clientData);
                }
            }
        }

        private DataAccess.ClientDetail CreateNewEntity()
        {
            var clientData = new DataAccess.ClientDetail();
            clientData.RecordId = Guid.NewGuid();
            clientData.ClientId = this.ClientId;
            clientData.Name = this.Name;
            clientData.ClientInternalId = this.ClientInternalId;
            clientData.Retraction = false;
            clientData.RecordCreatedDateTime = DateTime.Now;
            clientData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return clientData;
        }

        private void PopuldateByRow(DataAccess.ClientDetail clientData)
        {
            using (BypassPropertyChecks)
            {
                this.RecordId = clientData.RecordId;
                this.ClientId = clientData.ClientId;
                this.Name = clientData.Name;
                this.ClientInternalId = clientData.ClientInternalId;
            }
        }

        #endregion
    }
}
