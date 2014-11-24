using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Caliburn.Micro;
using PharmacyAdjudicator.ModernUI.Interface;

namespace PharmacyAdjudicator.ModernUITests.PlanTests
{
    /// <summary>
    /// Summary description for PlanWorkspaceViewModelTests
    /// </summary>
    [TestClass]
    public class PlanWorkspaceViewModelTests
    {
        private Mock<IEventAggregator> _eventAggregator;
        private Mock<IDialog> _dialog;
        //private Mock<IWindowManager>

        public PlanWorkspaceViewModelTests()
        {
            _eventAggregator = new Mock<IEventAggregator>();
            _dialog = new Mock<IDialog>();
        }

        [ClassInitialize]
        public static void Setup(TestContext ctx)
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

        //[TestMethod]
        //public void Plans_property_should_have_items()
        //{
        //    var planViewModel = new ModernUI.Plan.PlanWorkspaceViewModel(_dialog.Object, _eventAggregator.Object);
        //    Assert.IsTrue(planViewModel.Plans.Count > 0);
        //}

        //[TestMethod]
        //public void FilteredPlans_property_should_have_less_items_than_Plans()
        //{
        //    var planViewModel = new ModernUI.Plan.PlanWorkspaceViewModel(_dialog.Object, _eventAggregator.Object);
        //    planViewModel.PlanFilter = "adsf";
        //    Assert.IsTrue(planViewModel.FilteredPlans.Count == 0);
        //    planViewModel.PlanFilter = "plan";
        //    Assert.IsTrue(planViewModel.FilteredPlans.Count > 0);
        //}
    }
}
