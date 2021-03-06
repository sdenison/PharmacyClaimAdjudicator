//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PharmacyAdjudicator.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plan
    {
        public Plan()
        {
            this.PlanFacts = new HashSet<PlanDetail>();
            this.GroupPlan = new HashSet<GroupPlan>();
            this.PlanRule = new HashSet<PlanRule>();
        }
    
        public System.Guid PlanInternalId { get; set; }
        public System.DateTime RecordCreatedDateTime { get; set; }
        public string RecordCreatedUser { get; set; }
    
        public virtual ICollection<PlanDetail> PlanFacts { get; set; }
        public virtual ICollection<GroupPlan> GroupPlan { get; set; }
        public virtual ICollection<PlanRule> PlanRule { get; set; }
    }
}
