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

        private void Child_Fetch(Guid ruleId)
        {
            RaiseListChangedEvents = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var implicationListData = (from ri in ctx.DbContext.RuleImplication
                                           join i in ctx.DbContext.Implication on ri.ImplicationId equals i.ImplicationId
                                           where ri.RuleId == ruleId
                                           orderby ri.Priority descending
                                           select i);
                foreach (var implicationData in implicationListData)
                    this.Add(DataPortal.FetchChild<Implication>(implicationData));
            }
            RaiseListChangedEvents = true;
        }

        protected void Child_Update(Rule parent)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                foreach (var implication in DeletedList)
                {
                    var ruleImplicationToDelete = ctx.DbContext.RuleImplication.FirstOrDefault(ri => ri.RuleId == parent.RuleId && ri.ImplicationId == implication.ImplicationId);
                    if (ruleImplicationToDelete != null)
                        ctx.DbContext.RuleImplication.Remove(ruleImplicationToDelete);
                }

                var assignedImplications = new List<DataAccess.RuleImplication>();
                foreach (var implication in this)
                    if (implication.IsNew)
                        assignedImplications.Add(new DataAccess.RuleImplication() { RecordId = Utils.GuidHelper.GenerateComb(), ImplicationId = implication.ImplicationId, RuleId = parent.RuleId, Priority = "" });
                base.Child_Update();
                if (assignedImplications.Count > 0)
                    ctx.DbContext.RuleImplication.AddRange(assignedImplications);
            }
        }

        #endregion
    }
}
