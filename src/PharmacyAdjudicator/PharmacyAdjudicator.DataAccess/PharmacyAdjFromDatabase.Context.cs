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
    }
}
