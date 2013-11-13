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
            this.AtomGroupItems = new HashSet<AtomGroupItems>();
            this.Implications = new HashSet<Implication>();
            this.AtomFacts = new HashSet<AtomFact>();
        }
    
        public int AtomId { get; set; }
        public System.DateTime RecordCreatedDateTime { get; set; }
        public string RecordCreatedUser { get; set; }
    
        public virtual ICollection<AtomGroupItems> AtomGroupItems { get; set; }
        public virtual ICollection<Implication> Implications { get; set; }
        public virtual ICollection<AtomFact> AtomFacts { get; set; }
    }
}
