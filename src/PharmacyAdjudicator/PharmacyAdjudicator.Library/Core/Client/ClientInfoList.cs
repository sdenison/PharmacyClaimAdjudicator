using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Client
{
    [Serializable]
    public class ClientInfoList : ReadOnlyListBase<ClientInfoList, ClientInfo>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ClientInfoList), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
        }

        #endregion

        #region Factory Methods

        public static ClientInfoList GetAllClients()
        {
            return DataPortal.Fetch<ClientInfoList>();
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