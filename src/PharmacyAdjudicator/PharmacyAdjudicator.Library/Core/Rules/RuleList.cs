using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using PharmacyAdjudicator.Library.Core.Plan;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class RuleList : BusinessListBase<RuleList, Rule>
    {
        #region Factory Methods

        internal static RuleList NewRuleList()
        {
            return DataPortal.CreateChild<RuleList>();
        }

        private RuleList()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(PlanEdit parent)
        {
            RaiseListChangedEvents = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var ruleListData = from pr in ctx.DbContext.PlanRule
                                   join r in ctx.DbContext.Rule on pr.RuleId equals r.RuleId
                                   where pr.Retraction == false
                                   && !ctx.DbContext.PlanRule.Any(pr2 => pr2.Retraction == true && pr2.OriginalFactRecordId == pr.OriginalFactRecordId)
                                   && pr.PlanInternalId == parent.PlanInternalId
                                   orderby r.RuleType
                                   select r;
                foreach (var ruleData in ruleListData)
                    DataPortal.FetchChild<Rule>(ruleData);
            }
            RaiseListChangedEvents = true;
        }

        protected void Child_Update(PlanEdit parent)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                foreach (var deletedRule in DeletedList)
                {
                    var planRuleData = (from pr in ctx.DbContext.PlanRule
                                        where pr.RuleId == deletedRule.RuleId
                                        && pr.PlanInternalId == parent.PlanInternalId
                                        && pr.Retraction == false
                                        && !ctx.DbContext.PlanRule.Any(pr2 => pr2.Retraction == true && pr2.OriginalFactRecordId == pr.RecordId)
                                        select pr).FirstOrDefault();
                    planRuleData.Retraction = true;
                    planRuleData.OriginalFactRecordId = planRuleData.RecordId;
                    planRuleData.RecordId = Guid.NewGuid();
                    ctx.DbContext.PlanRule.Add(planRuleData);
                }
                foreach (var newRule in this)
                {
                    if (newRule.IsNew)
                    {
                        var planRuleData = CreateEntity(parent, newRule);
                        ctx.DbContext.PlanRule.Add(planRuleData);
                    }
                    //DataPortal.UpdateChild(newRule);
                }
                base.Child_Update();
            }
        }

        private DataAccess.PlanRule CreateEntity(PlanEdit plan, Rule rule)
        {
            var entity = new DataAccess.PlanRule();
            entity.RecordId = Guid.NewGuid();
            entity.RuleId = rule.RuleId;
            entity.PlanInternalId = plan.PlanInternalId;
            entity.Retraction = false;
            entity.RecordCreatedDateTime = DateTime.Now;
            entity.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return entity;
        }

        #endregion
    }
}
