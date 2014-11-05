using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class ImplicationList : BusinessListBase<ImplicationList, Implication>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(ImplicationList), "Role");
        }

        #endregion

        #region Factory Methods

        public static ImplicationList NewImplicationList()
        {
            return DataPortal.Create<ImplicationList>();
        }

        public static ImplicationList GetByRuleId(Guid ruleId)
        {
            return DataPortal.Fetch<ImplicationList>(ruleId);
        }

        private ImplicationList()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(Guid ruleId)
        {
            RaiseListChangedEvents = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var implicationListData = (from ri in ctx.DbContext.RuleImplication 
                                      join i in ctx.DbContext.Implication on ri.ImplicationId equals i.ImplicationId
                                      orderby ri.Priority descending 
                                      select i);
                foreach (var implicationData in implicationListData)
                    this.Add(DataPortal.FetchChild<Implication>(implicationData));
            }
            RaiseListChangedEvents = true;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            //base.Child_Update();
        }

        #endregion
    }
}
