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
    
    public partial class Implication
    {
        public Implication()
        {
            this.RuleImplications = new HashSet<RuleImplication>();
        }
    
        public System.Guid ImplicationId { get; set; }
        public System.Guid AtomGroupId { get; set; }
        public System.Guid DeductionAtomId { get; set; }
        public string Label { get; set; }
    
        public virtual ICollection<RuleImplication> RuleImplications { get; set; }
        public virtual AtomGroup AtomGroup { get; set; }
        public virtual Atom Atom { get; set; }
    }
}
