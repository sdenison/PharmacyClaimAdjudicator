using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PharmacyAdjudicator.Library.Core.Group;
using Csla.Rules;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.GroupTests
{
    [TestClass]
    public class GroupEditTests
    {
        [TestInitialize()]
        public void Setup()
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            Csla.ApplicationContext.User = principal;

            //Using SQL Server script to recreate the database
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "Scripts\\recreate_database.bat";
            proc.StartInfo.RedirectStandardError = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
        }

        [TestMethod]
        public void Group_can_be_created_with_valid_client_id()
        {
            var newGroup = GroupEdit.NewGroup("ACME", "ACMEGROUP1");
            newGroup.Name = "This is the first group for ACME";
            newGroup = newGroup.Save();
            Assert.IsTrue(newGroup.GroupId.Equals("ACMEGROUP1"));
        }

        [TestMethod]
        public void Group_creation_should_fail_with_invalid_client_id()
        {
            GroupEdit newGroup;
            try
            {
                newGroup = GroupEdit.NewGroup("jkl;", "ACMEGROUP1");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetBaseException() is Library.DataNotFoundException);
                return;
            }
            Assert.Fail("Should have thrown excpetion");
        }

        [TestMethod]
        public void Group_can_be_created_and_read_from_storage()
        {
            var newGroup = GroupEdit.NewGroup("ACME", "ACMEGROUP1");
            newGroup.Name = "This is the first group for ACME";
            newGroup = newGroup.Save();
            Assert.IsTrue(newGroup.GroupId.Equals("ACMEGROUP1"));

            var savedGroup = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");
            Assert.IsNotNull(savedGroup);
            //Make sure default client assignment exists.
            Assert.IsTrue(savedGroup.ClientAssignments.Count == 1);
            Assert.IsTrue(savedGroup.ClientAssignments[0].ExpirationDate.Equals(new DateTime(9999, 12, 31))); 
        }

        [TestMethod]
        public void Can_set_expiration_date_on_client_assignment()
        {
            var newGroup = GroupEdit.NewGroup("ACME", "ACMEGROUP1");
            newGroup.Name = "This is the first group for ACME";
            newGroup = newGroup.Save();
            Assert.IsTrue(newGroup.GroupId.Equals("ACMEGROUP1"));

            var savedGroup = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");

            //Make sure default client assignment exists.
            Assert.IsTrue(savedGroup.ClientAssignments.Count == 1);
            Assert.IsTrue(savedGroup.ClientAssignments[0].ExpirationDate.Equals(new DateTime(9999, 12, 31)));

            savedGroup.ClientAssignments[0].ExpirationDate = new DateTime(2020, 1, 1);
            savedGroup = savedGroup.Save();

            var savedGroup2 = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");
            //Make sure default client assignment is still only one in length and expiration date is changed
            Assert.IsTrue(savedGroup2.ClientAssignments.Count == 1);
            Assert.IsTrue(savedGroup2.ClientAssignments[0].ExpirationDate.Equals(new DateTime(2020, 1, 1)));
        }

        [TestMethod]
        public void Can_have_gap_in_coverage_dates_for_group()
        {
            var newGroup = GroupEdit.NewGroup("ACME", "ACMEGROUP1");
            newGroup.Name = "This is the first group for ACME";
            newGroup = newGroup.Save();
            Assert.IsTrue(newGroup.GroupId.Equals("ACMEGROUP1"));

            var savedGroup = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");

            //Make sure default client assignment exists.
            Assert.IsTrue(savedGroup.ClientAssignments.Count == 1);
            Assert.IsTrue(savedGroup.ClientAssignments[0].ExpirationDate.Equals(new DateTime(9999, 12, 31)));

            savedGroup.ClientAssignments[0].ExpirationDate = new DateTime(2020, 1, 1);
            savedGroup.ClientAssignments.AddNewAssignment();
            //Creates a effective date gap of one year and a day
            var newAssignment = savedGroup.ClientAssignments[savedGroup.ClientAssignments.Count - 1];
            newAssignment.EffectiveDate = newAssignment.EffectiveDate.AddYears(1);
            savedGroup = savedGroup.Save();

            var savedGroup2 = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");
            //Make sure there are now 2 assignments
            Assert.IsTrue(savedGroup2.ClientAssignments.Count == 2);
            Assert.IsTrue(savedGroup2.ClientAssignments[0].ExpirationDate.Equals(new DateTime(2020, 1, 1)));

            //The second effective date should be a year and a day after the previous expiration date
            Assert.IsTrue(savedGroup2.ClientAssignments[0].ExpirationDate.AddDays(1).AddYears(1).Equals(savedGroup2.ClientAssignments[1].EffectiveDate));
        }

        [TestMethod]
        public void Adding_overlapping_effective_and_expiration_dates_not_allowed()
        {
            var newGroup = GroupEdit.NewGroup("ACME", "ACMEGROUP1");
            newGroup.Name = "This is the first group for ACME";
            newGroup = newGroup.Save();
            Assert.IsTrue(newGroup.GroupId.Equals("ACMEGROUP1"));

            var savedGroup = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");

            //Make sure default client assignment exists.
            Assert.IsTrue(savedGroup.ClientAssignments.Count == 1);
            Assert.IsTrue(savedGroup.ClientAssignments[0].ExpirationDate.Equals(new DateTime(9999, 12, 31)));

            savedGroup.ClientAssignments[0].ExpirationDate = new DateTime(2020, 1, 1);
            savedGroup.ClientAssignments.AddNewAssignment();
            //Creates a effective date one year previous.  This should overlap and break the rules. 
            var newAssignment = savedGroup.ClientAssignments[savedGroup.ClientAssignments.Count - 1];
            newAssignment.EffectiveDate = newAssignment.EffectiveDate.AddYears(-1);
            Assert.IsFalse(savedGroup.IsSavable);
            Assert.IsTrue(savedGroup.BrokenRulesCollection.Count == 1);
            Assert.IsTrue(savedGroup.BrokenRulesCollection[0].Description.Equals("Effective and expiration dates cannot overlap other records."));
        }

        [TestMethod]
        public void Effective_date_must_be_less_than_expiration_date_in_client_assignment()
        {
            var newGroup = GroupEdit.NewGroup("ACME", "ACMEGROUP1");
            newGroup.Name = "This is the first group for ACME";
            newGroup = newGroup.Save();
            Assert.IsTrue(newGroup.GroupId.Equals("ACMEGROUP1"));

            var savedGroup = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");

            //Make sure default client assignment exists.
            Assert.IsTrue(savedGroup.ClientAssignments.Count == 1);
            Assert.IsTrue(savedGroup.ClientAssignments[0].ExpirationDate.Equals(new DateTime(9999, 12, 31)));

            savedGroup.ClientAssignments[0].ExpirationDate = new DateTime(2020, 1, 1);
            savedGroup.ClientAssignments.AddNewAssignment();
            //Creates a valid effective day but sets the expiration date to one less.  should break rule.
            var newAssignment = savedGroup.ClientAssignments[savedGroup.ClientAssignments.Count - 1];
            newAssignment.EffectiveDate = newAssignment.EffectiveDate.AddYears(1);
            newAssignment.ExpirationDate = newAssignment.EffectiveDate.AddDays(-1);
            Assert.IsFalse(savedGroup.IsSavable);
            //Broken rule is done at child level
            Assert.IsTrue(savedGroup.ClientAssignments[1].BrokenRulesCollection.Count == 1);

            //Can get broken rule with linq query
            var allRules = BusinessRules.GetAllBrokenRules(savedGroup);
            var errors = (from r in allRules
                         where r.BrokenRules != null && r.BrokenRules.Any(x => x.Severity == RuleSeverity.Error)
                         select r.Object).ToList();
            //All broken rules should be 1
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void Group_can_be_reassigned_to_new_client()
        {
            var newGroup = GroupEdit.NewGroup("ACME", "ACMEGROUP1");
            newGroup.Name = "This is the first group for ACME";
            newGroup = newGroup.Save();
            Assert.IsTrue(newGroup.GroupId.Equals("ACMEGROUP1"));

            var savedGroup = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");

            //Make sure default client assignment exists.
            Assert.IsTrue(savedGroup.ClientAssignments.Count == 1);
            Assert.IsTrue(savedGroup.ClientAssignments[0].ExpirationDate.Equals(new DateTime(9999, 12, 31)));

            savedGroup.ClientAssignments[0].ExpirationDate = new DateTime(2020, 1, 1);
            savedGroup.ClientAssignments.AddNewAssignment();
            //Creates a effective date gap of one year and a day
            var newAssignment = savedGroup.ClientAssignments[savedGroup.ClientAssignments.Count - 1];
            newAssignment.EffectiveDate = newAssignment.EffectiveDate.AddYears(1);
            newAssignment.ClientId = "OB";
            savedGroup = savedGroup.Save();

            var savedGroup2 = GroupEdit.GetByClientIdGroupId("ACME", "ACMEGROUP1");
            //Make sure there are now 2 assignments
            Assert.IsTrue(savedGroup2.ClientAssignments.Count == 2);
            Assert.IsTrue(savedGroup2.ClientAssignments[0].ExpirationDate.Equals(new DateTime(2020, 1, 1)));

            //The second effective date should be a year and a day after the previous expiration date
            Assert.IsTrue(savedGroup2.ClientAssignments[0].ExpirationDate.AddDays(1).AddYears(1).Equals(savedGroup2.ClientAssignments[1].EffectiveDate));
        }
    }
}
