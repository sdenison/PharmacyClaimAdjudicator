using System;
using System.Linq;
using Csla;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core.Client
{
    [Serializable]
    public class ClientInfo : ReadOnlyBase<ClientInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
        public string ClientId
        {
            get { return GetProperty(ClientIdProperty); }
            private set { LoadProperty(ClientIdProperty, value); }
        }

        public static readonly PropertyInfo<Guid> ClientInternalIdProperty = RegisterProperty<Guid>(c => c.ClientInternalId);
        public Guid ClientInternalId
        {
            get { return GetProperty(ClientInternalIdProperty); }
            private set { LoadProperty(ClientInternalIdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        #endregion

        #region Business Rules

        private static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ClientInfo), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
        }

        #endregion


        #region Factory Methods

        public static async Task<ClientInfo> GetByClientIdAsync (string clientId)
        {
            return await DataPortal.FetchAsync<ClientInfo>(clientId);
        }

        public static ClientInfo GetByClientId (string clientId)
        {
            return DataPortal.Fetch<ClientInfo>(clientId);
        }

        public static async Task<ClientInfo> GetByClientInternalIdAsync(Guid clientInternalId)
        {
            return await DataPortal.FetchAsync<ClientInfo>(clientInternalId);
        }

        public static ClientInfo GetByClientInternalId (Guid clientInternalId)
        {
            return DataPortal.Fetch<ClientInfo>(clientInternalId);
        }

        private ClientInfo()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(string clientId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var clientData = (from c in ctx.DbContext.ClientDetail
                                   where c.ClientId == clientId
                                   && c.Retraction == false
                                   && !ctx.DbContext.ClientDetail.Any(c2 => c2.ClientInternalId == c.ClientInternalId && c2.Retraction == true && c2.OriginalFactRecordId == c.RecordId)
                                   select c).ToList();
                if (clientData.Count == 0)
                    throw new DataNotFoundException("ClientId = " + clientId);
                if (clientData.Count > 1)
                    throw new DataNotUniqueException("ClientId = " + clientId);
                PopulateByRow(clientData[0]);
            }
        }

        private void DataPortal_Fetch(Guid clientInternalId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var clientData = (from c in ctx.DbContext.ClientDetail
                                  where c.ClientInternalId == clientInternalId
                                  && c.Retraction == false
                                  && !ctx.DbContext.ClientDetail.Any(c2 => c2.Retraction == true && c2.OriginalFactRecordId == c.RecordId)
                                  select c).ToList();
                if (clientData.Count == 0)
                    throw new DataNotFoundException("ClientInternalId = " + clientInternalId.ToString());
                if (clientData.Count > 1)
                    throw new DataNotUniqueException("ClientInternalId = " + clientInternalId.ToString());
                PopulateByRow(clientData[0]);
            }
        }

        private void Child_Fetch(DataAccess.ClientDetail clientData)
        {
            PopulateByRow(clientData);
        }

        public void PopulateByRow(DataAccess.ClientDetail clientData)
        {
            this.ClientId = clientData.ClientId;
            this.Name = clientData.Name;
            this.ClientInternalId = clientData.ClientInternalId;
        }

        #endregion
    }
}
