using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Csla.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmacyAdjudicator.ModernUI.Interface;

namespace PharmacyAdjudicator.ModernUITests.PlanTests
{
    [TestClass]
    public class PlanListViewModelTests
    {
        private Mock<IDialog> _dialog = new Mock<IDialog>();

        public PlanListViewModelTests()
        {
            //Using SQL Server script to recreate the database
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "Scripts\\recreate_database.bat";
            proc.StartInfo.RedirectStandardError = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
        }

        [TestInitialize]
        public void InitializeTest()
        {

            var principal = new System.Security.Principal.GenericPrincipal(
               new System.Security.Principal.GenericIdentity("Test"),
               new string[] { "RuleManager" });
            //var principal = new System.Security.Principal.GenericPrincipal(
            //    new System.Security.Principal.GenericIdentity("Test"),
            //    new string[] { "PatientViewer" });
            Csla.ApplicationContext.User = principal;
        }
        

        [TestMethod]
        public void Can_get_ViewModel_with_plans()
        {
            var plans = new PharmacyAdjudicator.ModernUI.Plan.PlanListViewModel(_dialog.Object);
            
            Assert.IsTrue(plans.Model.Count > 0);
        }

        [TestMethod]
        public void Can_add_plan_to_PlanListViewModel()
        {
            var plans = new PharmacyAdjudicator.ModernUI.Plan.PlanListViewModel(_dialog.Object);
            var originalPlanCount = plans.Model.Count;

            plans.AddPlan();
            plans.SelectedPlan.Name = "ADDED TEST PLAN";



            var manageObjectLifetime = plans.ManageObjectLifetime;
            var savable = plans.Model as ISavable;

            //plans.Model.ApplyEdit();
            //plans.Model.Save();


           
            

            //plans.SaveAsync
            //plans.SavePlans(); //doesn't work
            var task = plans.SavePlansAsync(); //.Wait(); //doesn't work
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
                {
                    var plansFromDatabase = new PharmacyAdjudicator.ModernUI.Plan.PlanListViewModel(_dialog.Object);
                    Assert.IsTrue(originalPlanCount < plansFromDatabase.Model.Count);
                });
            //plans.Save(new object(), new Csla.Xaml.ExecuteEventArgs());
            //plans.Save();
            //plans.Save(new object(), new CslaContrib.Caliburn.Micro.ExecuteEventArgs()); //doesn't work either

            //var plansFromDatabase = new PharmacyAdjudicator.ModernUI.Plan.PlanListViewModel(_dialog.Object);
            //Assert.IsTrue(originalPlanCount < plansFromDatabase.Model.Count);
        }

        [TestMethod]
        public async Task Can_save_plan_async()
        {
            var plans = new PharmacyAdjudicator.ModernUI.Plan.PlanListViewModel(_dialog.Object);
            var originalPlanCount = plans.Model.Count;

            plans.AddPlan();
            plans.SelectedPlan.Name = "ADDED TEST PLAN";
            await plans.SavePlansAsync();
            //plans.Save();
            var plansFromDatabase = new PharmacyAdjudicator.ModernUI.Plan.PlanListViewModel(_dialog.Object);
            Assert.IsTrue(originalPlanCount < plansFromDatabase.Model.Count);
        }

    
    }
}
