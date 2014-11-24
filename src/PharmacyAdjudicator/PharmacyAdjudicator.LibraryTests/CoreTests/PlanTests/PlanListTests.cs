using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PharmacyAdjudicator.Library.Core.Plan;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.PlanTests
{
    [TestClass]
    public class PlanListTests
    {
        [ClassInitialize()]
        public static void Setup(TestContext testContext)
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            //var principal = new System.Security.Principal.GenericPrincipal(
            //    new System.Security.Principal.GenericIdentity("Test"),
            //    new string[] { "PatientViewer" });
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
        public void Should_be_able_to_retrieve_plan_list()
        {
            var plans = PlanList.GetAll();
            Assert.IsTrue(plans.Count > 0);
        }

        [TestMethod]
        public void Can_add_new_plan_to_list()
        {
            var planId = "TEST-PLAN-2";
            var plans = PlanList.GetAll();
            var newPlan = plans.AddNew(); //PlanEdit.lan(planId);
            newPlan.PlanId = planId;
            plans = plans.Save();
            var planToCheck = PlanEdit.GetPlanByPlanId(planId);
            Assert.AreEqual(planToCheck.PlanId, planId);
        }

        [TestMethod]
        public void Plan_IsDirty_should_be_false_after_GetAll()
        {
            var plans = PlanList.GetAll();
            var planCanSave = plans.IsSavable;
            var dirty = plans.IsDirty;
            Assert.IsFalse(dirty);
        }

        [TestMethod]
        public void Plan_IsDirty_should_be_true_after_calling_AddPlan()
        {
            var plans = PlanList.GetAll();
            Assert.IsFalse(plans.IsDirty);
            plans.AddNew();
            Assert.IsTrue(plans.IsDirty);
        }
    }
}
