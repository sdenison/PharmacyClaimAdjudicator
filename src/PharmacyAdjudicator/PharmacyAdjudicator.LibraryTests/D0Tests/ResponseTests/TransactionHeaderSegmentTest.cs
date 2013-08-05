using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for TransactionHeaderSegmentTest
    /// </summary>
    [TestClass]
    public class TransactionHeaderSegmentTest
    {
        public TransactionHeaderSegmentTest()
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
        public void TestHeader_34_5_1()
        {
            PharmacyAdjudicator.Library.D0.Response.TransactionHeaderSegment ths = new PharmacyAdjudicator.Library.D0.Response.TransactionHeaderSegment();
            ths.VersionNumber = "D0";
            ths.TransactionCode = "B1";
            ths.TransactionCount = 1;
            ths.HeaderResponseStatus = "A";
            ths.ServiceProviderIdQualifier = "01";
            ths.ServiceProviderId = "4563663111bbbbb";
            ths.DateOfService = new DateTime(2007, 09, 15);

            string expectedNcpdpString = "DØB11AØ14563663111bbbbb2ØØ7Ø915";
            string ncpdpString = ths.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestHeader_34_5_1_version2()
        {
            string testSubmittedString = NcpdpHelper.FromHumanReadableToNcpdp("61ØØ66DØB1123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø91598765bbbbb");
            PharmacyAdjudicator.Library.D0.Submitted.TransactionHeaderSegment submittedThs = new PharmacyAdjudicator.Library.D0.Submitted.TransactionHeaderSegment(testSubmittedString);
            PharmacyAdjudicator.Library.D0.Response.TransactionHeaderSegment responseThs = new PharmacyAdjudicator.Library.D0.Response.TransactionHeaderSegment(submittedThs);
            responseThs.HeaderResponseStatus = "A";

            string expectedNcpdpString = "DØB11AØ14563663bbbbbbbb2ØØ7Ø915";
            string ncpdpString = responseThs.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }
    }
}
