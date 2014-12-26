using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyAdjudicator.Library.Core.Group
{
    [Serializable]
    public class ClientAssignment : BusinessBase<ClientAssignment>
    {
        #region Business Methods

        //public static readonly PropertyInfo<Client.ClientInfo> ClientProperty = RegisterProperty<Client.ClientInfo>(c => c.Client, RelationshipTypes.PrivateField);
        //// [NotUndoable, NonSerialized]
        //private Client.ClientInfo _client = ClientProperty.DefaultValue;
        //public Client.ClientInfo Client
        //{
        //    get 
        //    {
        //        //return GetProperty(ClientProperty, _client);
        //        return Clients.Where(c => c.ClientId == _client.ClientId).FirstOrDefault();
        //    }
        //    set 
        //    { 
        //        SetProperty(ClientProperty, ref _client, value); 
        //    }
        //}

        public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
        public string ClientId
        {
            get { return GetProperty(ClientIdProperty); }
            set { SetProperty(ClientIdProperty, value); }
        }

        //public static readonly PropertyInfo<Client.ClientInfo> ClientProperty = RegisterProperty<Client.ClientInfo>(c => c.Client);
        //public Client.ClientInfo Client
        //{
        //    get 
        //    { 
        //        return GetProperty(ClientProperty); 
        //        //Clients.Where(c => c.ClientId == )
        //    }
        //    set 
        //    { 
        //        SetProperty(ClientProperty, value); 
        //    }
        //}

        //public IEnumerable<Client.ClientInfo> Clients
        //{
        //    get
        //    {
        //        return Library.Core.Client.ClientInfoList.GetAllClients();
        //        //return Enum.GetValues(typeof(Enums.AddressType)).Cast<Enums.AddressType>();
        //    }
        //}

        private List<string> _clients = null;
        public IEnumerable<string> Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new List<string>();
                    foreach (var client in Library.Core.Client.ClientInfoList.GetAllClients())
                        _clients.Add(client.ClientId);
                }
                return _clients;
                //return Enum.GetValues(typeof(Enums.AddressType)).Cast<Enums.AddressType>();
            }
        }

        //public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
        //[Required]
        //public string ClientId
        //{
        //    get { return GetProperty(ClientIdProperty); }
        //    set 
        //    {
        //        if (value != this.ClientId)
        //            this.ClientInternalId = Client.ClientInfo.GetByClientId(value).ClientInternalId;
        //        SetProperty(ClientIdProperty, value); 
        //    }
        //}

        //public static readonly PropertyInfo<Guid> ClientInternalIdProperty = RegisterProperty<Guid>(c => c.ClientInternalId);
        //private Guid ClientInternalId
        //{
        //    get { return GetProperty(ClientInternalIdProperty); }
        //    set { LoadProperty(ClientInternalIdProperty, value); }
        //}

        public static readonly PropertyInfo<Guid> GroupInternalIdProperty = RegisterProperty<Guid>(c => c.GroupInternalId);
        public Guid GroupInternalId
        {
            get { return GetProperty(GroupInternalIdProperty); }
            private set { LoadProperty(GroupInternalIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EffectiveDateProperty = RegisterProperty<DateTime>(c => c.EffectiveDate);
        public DateTime EffectiveDate
        {
            get { return GetProperty(EffectiveDateProperty); }
            set { SetProperty(EffectiveDateProperty, value.Date); }
        }

        public static readonly PropertyInfo<DateTime> ExpirationDateProperty = RegisterProperty<DateTime>(c => c.ExpirationDate);
        public DateTime ExpirationDate
        {
            get { return GetProperty(ExpirationDateProperty); }
            set { SetProperty(ExpirationDateProperty, value.Date); }
        }

        public static readonly PropertyInfo<Guid> RecordIdProperty = RegisterProperty<Guid>(c => c.RecordId);
        /// <summary>
        /// Set to internal so ClientAssignmentsCannotOverlap rule can access the property.
        /// </summary>
        internal Guid RecordId
        {
            get { return GetProperty(RecordIdProperty); }
            private set { LoadProperty(RecordIdProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
            BusinessRules.AddRule(new ExpirationCannotBeLessThanEffectiveDateRule(EffectiveDateProperty));
            BusinessRules.AddRule(new ExpirationCannotBeLessThanEffectiveDateRule(ExpirationDateProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            Csla.Rules.BusinessRules.AddRule(typeof(ClientAssignment), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
            Csla.Rules.BusinessRules.AddRule(typeof(ClientAssignment), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(ClientAssignment), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(ClientAssignment), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "RuleManager", "Manager", "Admin"));
        }

        #endregion

        #region Factory Methods

        internal static ClientAssignment NewAssignment(string clientId, DateTime effectiveDate, DateTime expirationDate)
        {
            return DataPortal.CreateChild<ClientAssignment>(new CriteriaByClientGroupEffExp() { ClientId = clientId, EffectiveDate = effectiveDate, ExpirationDate = expirationDate });
        }

        //internal static ClientAssignment GetEditableChild(object childData)
        //{
        //    return DataPortal.FetchChild<ClientAssignment>(childData);
        //}

        private ClientAssignment()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [Serializable]
        protected class CriteriaByClientGroupEffExp : CriteriaBase<CriteriaByClientGroupEffExp>
        {
            public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
            public string ClientId
            {
                get { return ReadProperty(ClientIdProperty); }
                set { LoadProperty(ClientIdProperty, value); }
            }

            //public static readonly PropertyInfo<string> GroupIdProperty = RegisterProperty<string>(c => c.GroupId);
            //public string GroupId
            //{
            //    get { return ReadProperty(GroupIdProperty); }
            //    set { LoadProperty(GroupIdProperty, value); }
            //}

            public static readonly PropertyInfo<DateTime> EffectiveDateProperty = RegisterProperty<DateTime>(c => c.EffectiveDate);
            public DateTime EffectiveDate
            {
                get { return ReadProperty(EffectiveDateProperty); }
                set { LoadProperty(EffectiveDateProperty, value); }
            }

            public static readonly PropertyInfo<DateTime> ExpirationDateProperty = RegisterProperty<DateTime>(c => c.ExpirationDate);
            public DateTime ExpirationDate
            {
                get { return ReadProperty(ExpirationDateProperty); }
                set { LoadProperty(ExpirationDateProperty, value); }
            }
        }

        protected void Child_Create(CriteriaByClientGroupEffExp criteria)
        {
            //if (string.IsNullOrEmpty(criteria.ClientId))
            //var client = Client.ClientInfo.GetByClientId(criteria.ClientId);
            //this.ClientInternalId = client.ClientInternalId;


            //this.ClientId = criteria.ClientId;

            //worked
            //this.Client = Library.Core.Client.ClientInfo.GetByClientId(criteria.ClientId);

            this.ClientId = criteria.ClientId;




            this.EffectiveDate = criteria.EffectiveDate.Date;
            this.ExpirationDate = criteria.ExpirationDate.Date;
            this.RecordId = Utils.GuidHelper.GenerateComb();
            base.Child_Create();
        }

        //private void Child_Fetch(DataAccess.ClientGroup clientGroupData)
        private void Child_Fetch(ClientAssignmentDto clientGroupData)
        {
            PopulateByRow(clientGroupData);
        }

        private void Child_Insert(GroupEdit parent)
        {
            //var client = Client.ClientInfo.GetByClientId(this.ClientId);
            //this.ClientInternalId = client.ClientInternalId;
            this.GroupInternalId = parent.GroupInternalId;
            AssertNewFact();
        }

        private void Child_Update(GroupEdit parent)
        {
            //var client = Client.ClientInfo.GetByClientId(this.ClientId);
            //this.ClientInternalId = client.ClientInternalId;
            RetractFact();
            this.RecordId = Utils.GuidHelper.GenerateComb();
            AssertNewFact();
        }

        private void Child_DeleteSelf(object parent)
        {
            RetractFact();
        }

        private void RetractFact()
        {
            var originalRecordId = this.RecordId;
            var clientGroupData = CreateNewEntity();
            clientGroupData.Retraction = true;
            clientGroupData.OriginalFactRecordId = originalRecordId;
            clientGroupData.RecordId = Utils.GuidHelper.GenerateComb();
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                ctx.DbContext.ClientGroup.Add(clientGroupData);
            }
        }

        private DataAccess.ClientGroup AssertNewFact()
        {
            var clientGroupData = CreateNewEntity();
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                ctx.DbContext.ClientGroup.Add(clientGroupData);
            }
            return clientGroupData;
        }

        private DataAccess.ClientGroup CreateNewEntity()
        {
            var clientGroup = new DataAccess.ClientGroup();
            clientGroup.RecordId = this.RecordId;



            //clientGroup.ClientInternalId = this.ClientInternalId;
            var client = Library.Core.Client.ClientInfo.GetByClientId(this.ClientId);
            clientGroup.ClientInternalId = client.ClientInternalId;

            //Worked
            //clientGroup.ClientInternalId = this.Client.ClientInternalId;



            clientGroup.GroupInternalId = this.GroupInternalId;
            clientGroup.EffectiveDate = this.EffectiveDate;
            clientGroup.ExpirationDate = this.ExpirationDate;
            clientGroup.Retraction = false;
            clientGroup.RecordCreatedDateTime = DateTime.Now;
            clientGroup.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return clientGroup;
        }

        //private void PopulateByRow(DataAccess.ClientGroup clientGroupData)
        //needed a dto because of clientid clientinternalid relationship
        private void PopulateByRow(ClientAssignmentDto clientGroupData)
        {
            using (BypassPropertyChecks)
            {
                this.RecordId = clientGroupData.RecordId;



                //this.ClientInternalId = clientGroupData.ClientInternalId;
                //this.Client = Library.Core.Client.ClientInfo.GetByClientInternalId(clientGroupData.ClientInternalId);
                this.ClientId = clientGroupData.ClientId;




                //var client = Client.ClientInfo.GetByClientInternalId(ClientInternalId);
                //this.ClientId = client.ClientId;


                //this.ClientId = clientGroupData.ClientId;




                this.GroupInternalId = clientGroupData.GroupInternalId;
                this.EffectiveDate = clientGroupData.EffectiveDate;
                this.ExpirationDate = clientGroupData.ExpirationDate;
            }
        }

        #endregion
    }

    internal class ClientAssignmentDto
    {
        public Guid RecordId { get; set; }
        public Guid ClientInternalId { get; set; }
        public string ClientId { get; set; }
        public Guid GroupInternalId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
