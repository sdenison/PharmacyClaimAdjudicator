using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Core;
using PharmacyAdjudicator.Library.Core.Patient;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.PatientTests
{
    [TestClass]
    public class PatientHistoryTests
    {
        [TestInitialize()]
        public void Setup()
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            Csla.ApplicationContext.User = principal;
            //DalMock.MockDb.Refresh();

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
        public void PatientHistoryShouldContainMultiplePateints()
        {
            PatientHistory patientRecords = PatientHistory.GetByPatientId(100);
            Assert.IsTrue(patientRecords.Count > 5);
        }
    }
}
