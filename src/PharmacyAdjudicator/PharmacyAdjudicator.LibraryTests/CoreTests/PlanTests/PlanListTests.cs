using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Core.Plan;
using PharmacyAdjudicator.Library.Core.Rules;

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

        //[TestMethod]
        //public void Can_add_new_plan_to_list()
        //{
        //    var planId = "TEST-PLAN-2";
        //    var plans = PlanList.GetAll();
        //    var originalPlanCount = plans.Count;
        //    var newPlan = plans.AddNew(); //PlanEdit.lan(planId);
        //    newPlan.PlanId = planId;
        //    plans = plans.Save();
        //    var planToCheck = PlanEdit.GetByPlanId(planId);
        //    Assert.AreEqual(planToCheck.PlanId, planId);

        //    var plansAfterAddition = PlanList.GetAll();
        //    Assert.IsTrue(originalPlanCount < plansAfterAddition.Count);
        //}

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

        [TestMethod]
        public async Task Can_get_PlanList_async()
        {
            var plans = await PlanList.GetAllAsync();
            Assert.IsTrue(plans.Count > 0);
        }

        [TestMethod]
        public async Task Can_change_rule_in_implication()
        {
            var plans = await PlanList.GetAllAsync();
            var implication = plans[0].AssignedRules[0].Implications[0];
            var value = implication.Head.Value;
            Assert.IsTrue(value.ToString().Equals("5"));
            implication.Head.Value = 10;
            Assert.IsTrue(plans.IsDirty);
            plans = await plans.SaveAsync();
            var plansFromDb = await PlanList.GetAllAsync();
            var implicationFromDb = plansFromDb[0].AssignedRules[0].Implications[0];
            Assert.IsTrue(implicationFromDb.Head.Value.ToString().Equals("10"));
        }

        //[TestMethod]
        //public void Can_change_rule_in_implication()
        //{
        //    var plans = PlanList.GetAll();
        //    var implication = plans[0].AssignedRules[0].Implications[0];
        //    var value = implication.Head.Value;
        //    Assert.IsTrue(value.ToString().Equals("5"));
        //    implication.Head.Value = 10;
        //    Assert.IsTrue(plans.IsDirty);
        //    plans = plans.Save();
        //    var plansFromDb = PlanList.GetAll();
        //    var implicationFromDb = plansFromDb[0].AssignedRules[0].Implications[0];
        //    Assert.IsTrue(implicationFromDb.Head.Value.ToString().Equals("10"));
        //}

        [TestMethod]
        public void Can_add_an_atom_to_PlanList_PlanEdit_Rules_Implications_AtomGroup_Atom()
        {
            var plans = PlanList.GetAll();
            Assert.IsTrue(plans.IsDirty == false);


            var implication = plans[0].AssignedRules[4].Implications[0];
            //implication.Body.AddPredicate(Library.Core.Rules.Atom.NewAtom());
            //implication.Body.AddAtom();//.AddPredicate(Library.Core.Rules.Atom.NewAtom());

            var child = (AtomGroup)implication.Body.Children[0];

            //Testing AtomGroup add
            //NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator logicalOperator;
            //if (child.LogicalOperator ==  NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And)
            //   logicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
            //else
            //    logicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            //child.AddAtomGroup(logicalOperator);

            //Testing Atom add
            child.AddAtom();

            Assert.IsTrue(plans.IsDirty == true);

            //plans[0].AssignedRules[0].Implications[0].Body.AddPredicate(Library.Core.Rules.Atom.NewAtom());
            //Assert.IsTrue(plans.IsDirty == true);
        }
    }
}
