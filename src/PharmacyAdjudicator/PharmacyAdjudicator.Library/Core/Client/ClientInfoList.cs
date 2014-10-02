using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Client
{
    [Serializable]
    public class ClientInfoList : ReadOnlyListBase<ClientInfoList, ClientInfo>
    {
        private static ClientInfoList _allClientsCache = null;

        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ClientInfoList), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
        }

        #endregion

        #region Factory Methods

        public static async Task<ClientInfoList> GetAllClientsAsync()
        {
            if (_allClientsCache != null)
                return _allClientsCache;
            _allClientsCache = await DataPortal.FetchAsync<ClientInfoList>();
            return _allClientsCache;
            //return await DataPortal.FetchAsync<ClientInfoList>();
        }

        public static ClientInfoList GetAllClients()
        {
            if (_allClientsCache != null)
                return _allClientsCache;
            _allClientsCache = DataPortal.Fetch<ClientInfoList>();
            return _allClientsCache;

            //return DataPortal.Fetch<ClientInfoList>();
        }

        private ClientInfoList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch()
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var clientDataList = (from c in ctx.DbContext.ClientDetail
                                  where c.Retraction == false
                                  //&& c.ClientDetail1 != null
                                  && !ctx.DbContext.ClientDetail.Any(c2 => c2.Retraction == true && c2.OriginalFactRecordId == c.RecordId)
                                  orderby c.ClientId
                                  select c);
                foreach (var clientData in clientDataList)
                    Add(DataPortal.FetchChild<ClientInfo>(clientData));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
