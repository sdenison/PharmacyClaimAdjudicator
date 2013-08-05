using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Core;

namespace PharmacyAdjudicator.TestLibrary.CoreTests
{
    [TestClass]
    public class PatientTests
    {
        [TestInitialize()]
        public void Setup()
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
            proc.StartInfo.RedirectStandardError  = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
        }

        [TestMethod]
        public void GetPatientRecord()
        {
            var patient = Library.Core.Patient.GetByRecordId(1000);
            Assert.AreEqual(patient.FirstName, "GARY");
        }

        [TestMethod]
        public void GetPatientRecordByPatientId()
        {
            var patient = Library.Core.Patient.GetByPatientId(100);
            Assert.AreEqual(patient.DateOfBirth.Value, new DateTime(1999, 07, 05));
        }

        [TestMethod]
        public void UpdatePatientNameWorks()
        {
            var patient = Library.Core.Patient.GetByPatientId(100);
            patient.FirstName = "Johnny";
            patient.Save();
            var patient2 = Library.Core.Patient.GetByPatientId(100);
            Assert.AreEqual(patient2.FirstName, "Johnny"); 
        }

        [TestMethod]
        public void GetPatient()
        {
            try
            {
                var patient = Library.Core.Patient.GetByPatientId(22);
                Assert.AreEqual(patient.FirstName, "Joe");
                patient.FirstName = "John";
                patient = patient.Save();
                Assert.AreEqual(patient.FirstName, "John");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException() is Library.DataNotFoundException)
                {
                    Assert.Fail(ex.GetBaseException().Message);
                }
            }
        }

        [TestMethod]
        public void RetractFactWhenOnlyOneFactExists()
        {
            var patient = Library.Core.Patient.GetByPatientId(22);
            Assert.AreEqual(patient.FirstName, "Joe");
            patient.Delete();
            patient.Save();
            try
            {
                patient = Library.Core.Patient.GetByPatientId(22);
            }
            catch (Csla.DataPortalException ex)
            {
                if (ex.GetBaseException() is Library.DataNotFoundException)
                {
                    Assert.AreEqual(ex.GetBaseException().Message, "PatientId = 22");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestFindPatient()
        {
            Library.Core.Patient patient = null;
            patient = Library.Core.Patient.GetByPatientId(22);

            Assert.IsNotNull(patient, "Patient object should not be null.");
        }

        [TestMethod]
        public void FindPatientAsOfDate()
        {
            Library.Core.Patient patient = Library.Core.Patient.GetByPatientIdCompareDate(100, new DateTime(2013, 05, 01));
            //Patient should have original cardholder ID of 123456789
            Assert.AreEqual(patient.CardholderId, "123456789");
        }

        [TestMethod]
        public void AddAPatient()
        {
            Library.Core.Patient newPatient = Library.Core.Patient.NewPatient();
            newPatient.FirstName = "Tony";
            newPatient.LastName = "Parker";
            newPatient.CardholderId = "456123789";
            newPatient.DateOfBirth = new DateTime(1982, 05, 17);
            int newPatientId = newPatient.PatientId;

            foreach (var brokenRule in newPatient.BrokenRulesCollection)
            {
                string x = brokenRule.Description;
            } 
            newPatient.Save();

            try
            {
                Library.Core.Patient testGetPatient = Library.Core.Patient.GetByPatientId(newPatientId);
                Assert.AreEqual(testGetPatient.FirstName, "Tony");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException() is Library.DataNotFoundException)
                {
                    throw ex;
                }
                else
                    throw ex;
            }
        }

        [TestMethod]
        public void GetPatientAsync()
        {
            System.Threading.Tasks.Task<Patient> patTask = Patient.GetByPatientIdAsync(100);

            string x = "this should be happening while getting the patient happens";

            Patient pat = patTask.Result;
        }

        public async void TestGetPatientAsync()
        {
            return;
        }


    }
}
