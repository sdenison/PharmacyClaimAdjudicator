using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Core;

namespace PharmacyAdjudicator.LibraryTests.CoreTests
{
    [TestClass]
    public class PatientListTest
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
        public void PatientListCountIsGreaterThanOne()
        {
            PatientList patients = PatientList.GetAll();
            foreach (var patient in patients)
            {
                var x = patient.FirstName;
            }
            Assert.IsTrue(patients.Count > 1, "Patient count not greater than one");
        }

        [TestMethod]
        public void ThirdPatientNameUpdatedToValue()
        {
            PatientList patients = PatientList.GetAll();
            Patient patient = patients[2];

            Assert.IsTrue(patient.FirstName.Equals("Richard"), "First name not equal to Joe");

            patient.FirstName = "John";
            patients = patients.Save();

            Assert.IsTrue(patient.FirstName.Equals("John"), "First name not updated to John");
        }

        [TestMethod]
        public void InsertNewItem()
        {
            PatientList patients = PatientList.GetAll();
            Patient patient = Patient.NewPatient();
            foreach (var brokenRule in patient.BrokenRulesCollection)
            {
                var description = brokenRule.ToString();
                var severity = brokenRule.Severity;
            }
            
            patient.FirstName = "Johnny";
            patient.LastName = "Cash";
            patient.CardholderId = "666666666";
            patient.DateOfBirth = new DateTime(1932, 02, 26);
            patient.Save();

            int countBeforeAdd = patients.Count;
            patients = PatientList.GetAll();
            Assert.IsTrue(patients.Count == countBeforeAdd + 1, "Patient not added");
        }


        [TestMethod]
        public void ThirdPatientNameUpdatedToSameName()
        {
            PatientList patients = PatientList.GetAll();
            Patient patient = patients[2];

            Assert.IsTrue(patient.FirstName.Equals("Richard"), "First name not equal to Joe");

            patient.FirstName = "Joe";
            //Save doesn't throw exception in newer CSLA .NET versions.
            patients.Save();

            Assert.IsTrue(patient.FirstName.Equals("Joe"), "First name not updated to John");
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void Individual_patient_from_list_cannot_use_its_own_save_method()
        {
            PatientList patients = PatientList.GetAll();
            Patient patient = patients[2];

            Assert.IsTrue(patient.FirstName.Equals("Richard"), "First name not equal to Joe");

            patient.FirstName = "Joe";
            //Will throw exception because patient is a child object.
            patient.Save();

            Assert.IsTrue(patient.FirstName.Equals("Joe"), "First name not updated to John");
        }

        [TestMethod]
        public void Can_use_search_criteria_to_return_PatientList()
        {
            var criteria = new Library.Core.PatientSearchCriteria();
            criteria.PatientLastName = "Smith";
            var matchingPatients = Library.Core.PatientList.GetBySearchObject(criteria);

            Assert.IsNotNull(matchingPatients);
            Assert.IsTrue(matchingPatients.Count > 0, "Expecting more than one result while searching for last name Smith");
        }

        [TestMethod]
        public void Can_search_for_patients_by_groupid()
        {
            var criteria = new Library.Core.PatientSearchCriteria();
            criteria.GroupId = "Group1";
            var matchingPatients = Library.Core.PatientList.GetBySearchObject(criteria);

            Assert.IsNotNull(matchingPatients);
            Assert.IsTrue(matchingPatients.Count > 0, "Expecting at least one result for Group1");
        }
    }
}
