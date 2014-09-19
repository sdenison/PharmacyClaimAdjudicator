using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Threading;

using PharmacyAdjudicator.ModernUI;
using Csla;
using System.Threading.Tasks;
using Caliburn.Micro;
using Moq;
using PharmacyAdjudicator.ModernUI.Interface;
using PharmacyAdjudicator.Library.Core.Patient;

namespace PharmacyAdjudicator.ModernUITests.PatientTests
{
    /// <summary>
    /// Summary description for PatientEditViewModelTests
    /// </summary>
    [TestClass]
    public class PatientEditViewModelTests
    {
        private Mock<IEventAggregator> _eventAggregator;
        private Mock<IDialog> _dialog;

        public PatientEditViewModelTests()
        {
            //Using SQL Server script to recreate the database
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "Scripts\\recreate_database.bat";
            proc.StartInfo.RedirectStandardError = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
            _eventAggregator = new Mock<IEventAggregator>();
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
        public async Task Patient_view_model_can_be_created()
        {
            try
            {
                long patientId = 22;
                var patientVM = await ModernUI.Patient.PatientEditViewModel.BuildViewModelAsync(patientId, _eventAggregator.Object, _dialog.Object);
                var patientSecondFetch = PatientEdit.GetByPatientId(22);
                //Assert that the name from the database is now John.
                Assert.AreEqual(patientSecondFetch.FirstName, "John");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException() is Library.DataNotFoundException)
                {
                    Assert.Fail(ex.GetBaseException().Message);
                }
            }
        }
    }
}
