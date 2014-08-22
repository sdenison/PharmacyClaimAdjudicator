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

            //should be able to share one address as two different address types.
            var businessAddress = Library.Core.PatientAddress.NewAddress(patient.PatientId);
            businessAddress.AddressType = Library.Core.Enums.AddressType.Mailing;
            businessAddress.Address = addressListAfterRemoval[0].Address;
            addressListAfterRemoval.Add(businessAddress);
            addressListAfterRemoval = addressListAfterRemoval.Save();
            Assert.IsTrue(addressListAfterRemoval.Count == 2);

            var anotherDatabasePull = Library.Core.PatientAddressList.GetByPatientId(100);
            //Both PatientAddresses in the list should be pointing to the same Address record
            Assert.IsTrue(anotherDatabasePull[0].Address.Address1 == anotherDatabasePull[1].Address.Address1);
        }

        [TestMethod]
        public void Patient_owns_address_list()
        {
            var patient = Library.Core.Patient.GetByPatientId(62);
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

            var addressListFromDb = Library.Core.PatientAddressList.GetByPatientId(100);
            Assert.IsTrue(addressListFromDb.Count == 2);
        }
    }
}
