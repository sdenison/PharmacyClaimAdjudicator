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
    
    public partial class Atom
    {
        public Atom()
        {
            this.AtomGroupItems = new HashSet<AtomGroupItem>();
            this.Implication = new HashSet<Implication>();
            this.AtomDetail = new HashSet<AtomDetail>();
        }
    
        public System.Guid AtomId { get; set; }
        public System.DateTime RecordCreatedDateTime { get; set; }
        public string RecordCreatedUser { get; set; }
    
        public virtual ICollection<AtomGroupItem> AtomGroupItems { get; set; }
        public virtual ICollection<Implication> Implication { get; set; }
        public virtual ICollection<AtomDetail> AtomDetail { get; set; }
    }
}
