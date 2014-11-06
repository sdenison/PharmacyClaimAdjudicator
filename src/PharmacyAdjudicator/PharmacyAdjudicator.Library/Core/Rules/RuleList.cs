using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class RuleList :
      BusinessListBase<RuleList, Rule>
    {
        #region Factory Methods

        internal static RuleList NewRuleList()
        {
            return DataPortal.CreateChild<RuleList>();
        }

        //internal static RuleList GetEditableChildList(DataAccess.Rule rules)
        //{
        //    return DataPortal.FetchChild<RuleList>(childData);
        //}

        private RuleList()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(Plan parent)
        {
            RaiseListChangedEvents = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var ruleListData = from rl in ctx.DbContext.PlanRule
            }
            //foreach (var child in (IList<object>)childData)
            //    this.Add(EditableChild.GetEditableChild(child));
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
