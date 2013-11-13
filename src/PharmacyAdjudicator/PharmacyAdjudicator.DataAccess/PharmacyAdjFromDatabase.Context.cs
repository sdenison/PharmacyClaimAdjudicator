﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PharmacyClaimAdjudicatorEntities : DbContext
    {
        public PharmacyClaimAdjudicatorEntities()
            : base("name=PharmacyClaimAdjudicatorEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupFact> GroupFacts { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PatientFact> PatientFacts { get; set; }
        public DbSet<PatientGroup> PatientGroups { get; set; }
        public DbSet<VaDrug> VaDrugs { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanFact> PlanFacts { get; set; }
        public DbSet<Atom> Atoms { get; set; }
        public DbSet<AtomGroup> AtomGroups { get; set; }
        public DbSet<AtomGroupItems> AtomGroupItems { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<PlanRules> PlanRules { get; set; }
        public DbSet<Implication> Implications { get; set; }
        public DbSet<RuleImplication> RuleImplications { get; set; }
        public DbSet<AtomFact> AtomFacts { get; set; }
    }
}
