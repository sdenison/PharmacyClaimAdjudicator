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
    
    public partial class PlanDetail
    {
        public System.Guid RecordId { get; set; }
        public string PlanId { get; set; }
        public string Name { get; set; }
        public bool Retraction { get; set; }
        public long OriginalFactRecordId { get; set; }
        public System.DateTime RecordCreatedDateTime { get; set; }
        public string RecordCreatedUser { get; set; }
        public System.Guid PlanInternalId { get; set; }
    }
}
