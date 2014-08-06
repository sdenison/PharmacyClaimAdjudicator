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
    
    public partial class PatientGroup
    {
        public long RecordId { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public bool Retraction { get; set; }
        public Nullable<long> OriginalFactRecordId { get; set; }
        public System.DateTime RecordCreatedDateTime { get; set; }
        public string RecordCreatedUser { get; set; }
        public long PatientId { get; set; }
        public string GroupId { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
