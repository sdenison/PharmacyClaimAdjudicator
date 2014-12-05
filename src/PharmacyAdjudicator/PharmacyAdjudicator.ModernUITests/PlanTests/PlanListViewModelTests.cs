using System;
using System.Threading;
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
            var i = 0;
            while (plans.IsBusy) 
            { 
                Thread.Sleep(500); /*Do nothing while waiting for the object to load. */
                i++;
                if (i > 100)
                {
                    Assert.IsTrue(false, "first plans.IsBusy timed out");
                }
            }
            Assert.IsTrue(plans.Model.Count > 0);
        }

        [TestMethod]
        public void Can_save_plan_async()
        {
            var plans = new PharmacyAdjudicator.ModernUI.Plan.PlanListViewModel(_dialog.Object);
            //Wait until plans.IsBusy is false before further evaluations.
            int i = 0;
            while (plans.IsBusy) 
            { 
                Thread.Sleep(500);  
                i++; 
                if (i > 100)
                {
                    Assert.IsTrue(false, "first plans.IsBusy timed out");
                }
            }
            Console.WriteLine("Counted to " + i.ToString() + " while waiting for IsBusy");
            Assert.IsTrue(i > 0);

            int originalPlanCount = 0;
            originalPlanCount = plans.Model.Count;

            plans.AddPlan();
            plans.SelectedPlan.Name = "ADDED TEST PLAN";
            var task = plans.Save();
            i = 0;
            while (plans.IsBusy) 
            { 
                Thread.Sleep(500); 
                i++; 
                if (i > 100)
                {
                    Assert.IsTrue(false, "second plans.IsBusy timed out");
                }
            }
            var plansFromDatabase = new PharmacyAdjudicator.ModernUI.Plan.PlanListViewModel(_dialog.Object);
            while (plansFromDatabase.IsBusy) { Thread.Sleep(500); }
            Assert.IsTrue(originalPlanCount < plansFromDatabase.Model.Count);
        }
    
    }
}
