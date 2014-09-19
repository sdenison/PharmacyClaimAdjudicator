using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Csla;

namespace PharmacyAdjudicator.TestLibrary.CoreTests
{
    [TestClass]
    public class AddressTests
    {
        [ClassInitialize]
        public static void Setup(TestContext ctx)
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

        [TestInitialize]
        public void TestInitialize()
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            Csla.ApplicationContext.User = principal;
        }

        [TestMethod]
        public void Address_can_be_created()
        {
            var address = DataPortal.CreateChild<Library.Core.Address>();
            Assert.IsNotNull(address);
            address.Address1 = "326 Scenic Meadow";
            address.Address2 = "Address_can_be_created_test()";
            address.City = "New Braunfels";
            address.State = "TX";
            address.Zip = "78130";
            DataPortal.UpdateChild(address);
        }

        [TestMethod]
        public void Address_can_be_created_for_patient_100()
        {
            var patient = Library.Core.Patient.GetByPatientId(100);
            Assert.IsNotNull(patient);

            var addressList = Library.Core.PatientAddressList.NewPatientAddressList(100);
            var patientPhysicalAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientPhysicalAddress.Address.Address1 = "326 Scenic Meadow";
            patientPhysicalAddress.Address.City = "New Braunfels";
            patientPhysicalAddress.Address.State = "TX";
            patientPhysicalAddress.Address.Zip = "78130";
            patientPhysicalAddress.AddressType = Library.Core.Enums.AddressType.Physical;

            var patientBusinessAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientBusinessAddress.Address.Address1 = "421 Torrey St.";
            patientBusinessAddress.Address.City = "New Braunfels";
            patientBusinessAddress.Address.State = "TX";
            patientBusinessAddress.Address.Zip = "78130";
            patientBusinessAddress.AddressType = Library.Core.Enums.AddressType.Billing;

            addressList.Add(patientPhysicalAddress);
            addressList.Add(patientBusinessAddress);
            addressList = addressList.Save();

            var addressListFromDb = Library.Core.PatientAddressList.GetByPatientId(100);
            Assert.IsTrue(addressListFromDb.Count == 2);

            var firstAddress = addressListFromDb[0];
            addressListFromDb.Remove(firstAddress);
            addressListFromDb = addressListFromDb.Save();

            Assert.IsTrue(addressListFromDb.Count == 1);

            var addressListAfterRemoval = Library.Core.PatientAddressList.GetByPatientId(100);
            //We should only have one address since we deleted one earlier.
            Assert.IsTrue(addressListAfterRemoval.Count == 1);
            Assert.IsTrue(addressListAfterRemoval[0].AddressType == Library.Core.Enums.AddressType.Billing);

            ////should be able to share one address as two different address types.
            //var businessAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            //businessAddress.AddressType = Library.Core.Enums.AddressType.Mailing;
            /////businessAddress.AddressData = addressListAfterRemoval[0].AddressData;
            //addressListAfterRemoval.Add(businessAddress);
            //addressListAfterRemoval = addressListAfterRemoval.Save();
            //Assert.IsTrue(addressListAfterRemoval.Count == 2);

            //var anotherDatabasePull = Library.Core.PatientAddressList.GetByPatientId(100);
            ////Both PatientAddresses in the list should be pointing to the same Address record
            //Assert.IsTrue(anotherDatabasePull[0].Address.Address1 == anotherDatabasePull[1].Address.Address1);
        }

        [TestMethod]
        public void Patient_owns_address_list()
        {
            const long PATIENT_ID = 22;

            var patient = Library.Core.Patient.GetByPatientId(PATIENT_ID);
            Assert.IsNotNull(patient);

            var addressList = patient.PatientAddresses;
            var patientPhysicalAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientPhysicalAddress.Address.Address1 = "326 Scenic Meadow";
            patientPhysicalAddress.Address.City = "New Braunfels";
            patientPhysicalAddress.Address.State = "TX";
            patientPhysicalAddress.Address.Zip = "78130";
            patientPhysicalAddress.AddressType = Library.Core.Enums.AddressType.Physical;

            var patientBusinessAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientBusinessAddress.Address.Address1 = "421 Torrey St.";
            patientBusinessAddress.Address.City = "New Braunfels";
            patientBusinessAddress.Address.State = "TX";
            patientBusinessAddress.Address.Zip = "78130";
            patientBusinessAddress.AddressType = Library.Core.Enums.AddressType.Billing;

            addressList.Add(patientPhysicalAddress);
            addressList.Add(patientBusinessAddress);
            patient = patient.Save();

            var addressListFromDb = Library.Core.PatientAddressList.GetByPatientId(PATIENT_ID);
            Assert.IsTrue(addressListFromDb.Count == 2);
        }

        [TestMethod]
        public void Address_can_be_edited()
        {
            const long PATIENT_ID = 62; 
            //Create addresses 
            var patient = Library.Core.Patient.GetByPatientId(PATIENT_ID);
            Assert.IsNotNull(patient);

            var addressList = patient.PatientAddresses;
            var patientPhysicalAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientPhysicalAddress.Address.Address1 = "326 Scenic Meadow";
            patientPhysicalAddress.Address.City = "New Braunfels";
            patientPhysicalAddress.Address.State = "TX";
            patientPhysicalAddress.Address.Zip = "78130";
            patientPhysicalAddress.AddressType = Library.Core.Enums.AddressType.Physical;

            var patientBusinessAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientBusinessAddress.Address.Address1 = "421 Torrey St.";
            patientBusinessAddress.Address.City = "New Braunfels";
            patientBusinessAddress.Address.State = "TX";
            patientBusinessAddress.Address.Zip = "78130";
            patientBusinessAddress.AddressType = Library.Core.Enums.AddressType.Billing;

            addressList.Add(patientPhysicalAddress);
            addressList.Add(patientBusinessAddress);
            patient = patient.Save();

            var addressListFromDb = Library.Core.PatientAddressList.GetByPatientId(PATIENT_ID);
            Assert.IsTrue(addressListFromDb.Count == 2);

            //Pull addresses from database
            patient = Library.Core.Patient.GetByPatientId(PATIENT_ID);
            Assert.IsTrue(patient.PatientAddresses[0].Address.Address1.Equals("326 Scenic Meadow"));
            patient.PatientAddresses[0].Address.Address1 = "123 Main St.";
            patient = patient.Save();

            //Re-pull patient from database
            var patient2 = Library.Core.Patient.GetByPatientId(PATIENT_ID);
            Assert.IsTrue(patient2.PatientAddresses[0].Address.Address1.Equals("123 Main St."));
        }

        [TestMethod]
        public void Address_can_be_removed()
        {
            const long PATIENT_ID = 61;
            //Create addresses
            var patient = Library.Core.Patient.GetByPatientId(PATIENT_ID); 
            Assert.IsNotNull(patient); 
            var addressList = patient.PatientAddresses;
            //var addressList = Library.Core.PatientAddressList.NewPatientAddressList(100);
            var patientPhysicalAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientPhysicalAddress.Address.Address1 = "326 Scenic Meadow";
            patientPhysicalAddress.Address.City = "New Braunfels";
            patientPhysicalAddress.Address.State = "TX";
            patientPhysicalAddress.Address.Zip = "78130";
            patientPhysicalAddress.AddressType = Library.Core.Enums.AddressType.Physical;

            var patientBusinessAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientBusinessAddress.Address.Address1 = "421 Torrey St.";
            patientBusinessAddress.Address.City = "New Braunfels";
            patientBusinessAddress.Address.State = "TX";
            patientBusinessAddress.Address.Zip = "78130";
            patientBusinessAddress.AddressType = Library.Core.Enums.AddressType.Billing;

            addressList.Add(patientPhysicalAddress);
            addressList.Add(patientBusinessAddress);
            patient = patient.Save();

            var addressListFromDb = Library.Core.PatientAddressList.GetByPatientId(PATIENT_ID);
            Assert.IsTrue(addressListFromDb.Count == 2);

            //Pull addresses from database
            patient = Library.Core.Patient.GetByPatientId(PATIENT_ID);
            Assert.IsTrue(patient.PatientAddresses[0].Address.Address1.Equals("326 Scenic Meadow"));
            patient.PatientAddresses.RemoveAt(0);
            patient = patient.Save();

            //Re-pull patient from database
            var patient2 = Library.Core.Patient.GetByPatientId(PATIENT_ID);
            //Should have only one address now.
            Assert.IsTrue(patient.PatientAddresses.Count == 1);
            Assert.IsTrue(patient2.PatientAddresses[0].Address.Address1.Equals("421 Torrey St."));
        }

        [TestMethod]
        public void Only_uniqe_address_types_are_allowed_in_address_list()
        {
            const long PATIENT_ID = 20;
            //Create addresses
            var patient = Library.Core.Patient.GetByPatientId(PATIENT_ID);
            Assert.IsNotNull(patient);
            var addressList = patient.PatientAddresses;
            //var addressList = Library.Core.PatientAddressList.NewPatientAddressList(100);
            var patientPhysicalAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientPhysicalAddress.Address.Address1 = "326 Scenic Meadow";
            patientPhysicalAddress.Address.City = "New Braunfels";
            patientPhysicalAddress.Address.State = "TX";
            patientPhysicalAddress.Address.Zip = "78130";
            patientPhysicalAddress.AddressType = Library.Core.Enums.AddressType.Physical;

            var patientBusinessAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientBusinessAddress.Address.Address1 = "421 Torrey St.";
            patientBusinessAddress.Address.City = "New Braunfels";
            patientBusinessAddress.Address.State = "TX";
            patientBusinessAddress.Address.Zip = "78130";
            patientBusinessAddress.AddressType = Library.Core.Enums.AddressType.Billing;

            var patientMailingAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            patientMailingAddress.Address.Address1 = "421 Torrey St.";
            patientMailingAddress.Address.City = "New Braunfels";
            patientMailingAddress.Address.State = "TX";
            patientMailingAddress.Address.Zip = "78130";
            patientMailingAddress.AddressType = Library.Core.Enums.AddressType.Mailing;

            addressList.Add(patientPhysicalAddress);
            addressList.Add(patientBusinessAddress);
            addressList.Add(patientMailingAddress);
            patient = patient.Save();

            var addressListFromDb = Library.Core.PatientAddressList.GetByPatientId(PATIENT_ID);
            Assert.IsTrue(addressListFromDb.Count == 3);

            //Pull addresses from database
            patient = Library.Core.Patient.GetByPatientId(PATIENT_ID);

            //Changing the address type to mailing should violate the rule we have set up.
            Assert.IsTrue(patient.PatientAddresses[0].AddressType == Library.Core.Enums.AddressType.Physical);
            patient.PatientAddresses[0].AddressType = Library.Core.Enums.AddressType.Mailing;
            patient.FirstName = "";
            var brokenRules = patient.BrokenRulesCollection;
            var brokenAddressRules = patient.BrokenAddressRules;
            Assert.IsTrue(brokenRules.Count > brokenAddressRules.Count);

            Assert.IsFalse(patient.IsSavable);
        }
    }
}
