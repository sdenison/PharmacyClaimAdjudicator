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
    
    public partial class Patient
    {
        public Patient()
        {
            this.PatientGroups = new HashSet<PatientGroup>();
            this.PatientFacts = new HashSet<PatientDetail>();
            this.PatientAddress = new HashSet<PatientAddress>();
        }
    
        public long PatientId { get; set; }
        public System.DateTime RecordCreatedDateTime { get; set; }
        public string RecordCreatedUser { get; set; }
    
        public virtual ICollection<PatientGroup> PatientGroups { get; set; }
        public virtual ICollection<PatientDetail> PatientFacts { get; set; }
        public virtual ICollection<PatientAddress> PatientAddress { get; set; }
    }
}
