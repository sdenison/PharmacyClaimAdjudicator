using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for PatientSegmentTests
    /// </summary>
    [TestClass]
    public class PatientSegmentTests
    {
        public PatientSegmentTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestPatientToNcpdpString()
        {
            Library.D0.Response.PatientSegment patient = new Library.D0.Response.PatientSegment();
            patient.PatientFirstName = "LARRY";

            string expectedNcpdpString = "<1E><1C>AM29<1C>CALARRY"; 

            string ncpdpString = patient.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestPatientToNcpdpStringWithLast()
        {
            Library.D0.Response.PatientSegment patient = new Library.D0.Response.PatientSegment();
            patient.PatientFirstName = "LARRY";
            patient.PatientLastName = "PATTON";

            string expectedNcpdpString = "<1E><1C>AM29<1C>CALARRY<1C>CBPATTON";
            string ncpdpString = patient.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestPatientWithBirthDate()
        {
            Library.D0.Response.PatientSegment patient = new Library.D0.Response.PatientSegment();
            patient.PatientFirstName = "LARRY";
            patient.PatientLastName = "PATTON";
            patient.DateOfBirth = new DateTime(1975, 02, 19);

            string expectedNcpdpString = "<1E><1C>AM29<1C>CALARRY<1C>CBPATTON<1C>C41975Ø219";
            string ncpdpString = patient.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }
    }
}
