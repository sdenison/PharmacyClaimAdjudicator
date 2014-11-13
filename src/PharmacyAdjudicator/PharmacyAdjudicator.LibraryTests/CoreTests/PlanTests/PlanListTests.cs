﻿using System;
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
    }
}