using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Core;
using System.Threading.Tasks;
using PharmacyAdjudicator.Library.Core.Patient;

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
            var patient = PatientEdit.GetByRecordId(1000);
            Assert.AreEqual(patient.FirstName, "GARY");
        }

        [TestMethod]
        public void GetPatientRecordByPatientId()
        {
            var patient = PatientEdit.GetByPatientId(100);
            Assert.AreEqual(patient.DateOfBirth.Value, new DateTime(1999, 07, 05));
        }

        [TestMethod]
        public void UpdatePatientNameWorks()
        {
            var patient = PatientEdit.GetByPatientId(100);
            patient.FirstName = "Johnny";
            patient.Save();
            var patient2 = PatientEdit.GetByPatientId(100);
            Assert.AreEqual(patient2.FirstName, "Johnny"); 
        }

        [TestMethod]
        public void GetPatient()
        {
            try
            {
                var patient = PatientEdit.GetByPatientId(22);
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
            var patient = PatientEdit.GetByPatientId(22);
            Assert.AreEqual(patient.FirstName, "Joe");
            patient.Delete();
            patient.Save();
            try
            {
                patient = PatientEdit.GetByPatientId(22);
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
            PatientEdit patient = null;
            patient = PatientEdit.GetByPatientId(22);

            Assert.IsNotNull(patient, "Patient object should not be null.");
        }

        [TestMethod]
        public void FindPatientAsOfDate()
        {
            PatientEdit patient = PatientEdit.GetByPatientIdCompareDate(100, new DateTime(2013, 05, 01));
            //Patient should have original cardholder ID of 123456789
            Assert.AreEqual(patient.CardholderId, "123456789");
        }

        [TestMethod]
        public void AddAPatient()
        {
            PatientEdit newPatient = PatientEdit.NewPatient();
            newPatient.FirstName = "Tony";
            newPatient.LastName = "Parker";
            newPatient.CardholderId = "456123789";
            newPatient.DateOfBirth = new DateTime(1982, 05, 17);
            long newPatientId = newPatient.PatientId;

            foreach (var brokenRule in newPatient.BrokenRulesCollection)
            {
                string x = brokenRule.Description;
            } 
            newPatient.Save();

            try
            {
                PatientEdit testGetPatient = PatientEdit.GetByPatientId(newPatientId);
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
            Task<PatientEdit> patTask = PatientEdit.GetByPatientIdAsync(100);

            string x = "this should be happening while getting the patient happens";
            Assert.IsNotNull(x);

            PatientEdit pat = patTask.Result;
        }

        [TestMethod]
        public void SavePatientAsync()
        {
            var patient = PatientEdit.GetByPatientId(100);
            patient.FirstName = "Thisshouldchange";
            Task<PatientEdit> patTask = patient.SaveAsync();
            var patient2 = patTask.Result;
        }

        [TestMethod]
        public async Task SavePatientAsync2()
        {
            var patient = PatientEdit.GetByPatientId(100);
            patient.FirstName = "Thisshouldchange";
            var patient2 = await patient.SaveAsync();
            Assert.IsTrue(patient2.FirstName.Equals("Thisshouldchange"));

        }
    }
}
