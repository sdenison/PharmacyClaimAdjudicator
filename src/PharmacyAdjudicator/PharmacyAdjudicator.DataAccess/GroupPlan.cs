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
    
    public partial class GroupPlan
    {
        public GroupPlan()
        {
            this.GroupPlan1 = new HashSet<GroupPlan>();
        }
    
        public System.Guid RecordId { get; set; }
        public System.Guid GroupInternalId { get; set; }
        public System.Guid PlanInternalId { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public bool Retraction { get; set; }
        public Nullable<System.Guid> OriginalFactRecordId { get; set; }
        public System.DateTime RecordCreateDateTime { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual ICollection<GroupPlan> GroupPlan1 { get; set; }
        public virtual GroupPlan GroupPlan2 { get; set; }
    }
}
