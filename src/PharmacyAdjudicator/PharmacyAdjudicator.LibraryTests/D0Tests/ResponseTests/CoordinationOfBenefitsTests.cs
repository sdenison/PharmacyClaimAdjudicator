using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.D0.Response;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for CoordinationOfBenefitsTests
    /// </summary>
    [TestClass]
    public class CoordinationOfBenefitsTests
    {
        public CoordinationOfBenefitsTests()
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
        public void CoordinationOfBenefit_34_6_1()
        {
            CoordinationOfBenefitsSegment cobSegment = new CoordinationOfBenefitsSegment();
            cobSegment.OtherPayers = new List<CoordinationOfBenefitsSegment.OtherPayerContainer>();

            cobSegment.OtherPayerIdCount = 2;

            CoordinationOfBenefitsSegment.OtherPayerContainer otherPayer = new CoordinationOfBenefitsSegment.OtherPayerContainer();
            otherPayer.OtherPayerCoverageType = "01";
            otherPayer.OtherPayerIdQualifier = "03";
            otherPayer.OtherPayerId = "999999";
            otherPayer.OtherPayerCardholderId = "998877665";
            cobSegment.OtherPayers.Add(otherPayer);

            otherPayer = new CoordinationOfBenefitsSegment.OtherPayerContainer();
            otherPayer.OtherPayerCoverageType = "02";
            otherPayer.OtherPayerIdQualifier = "01";
            otherPayer.OtherPayerId = "123456";
            cobSegment.OtherPayers.Add(otherPayer);

            string expectedNcpdpString = "<1E><1C>AM28<1C>NT2<1C>5CØ1<1C>6CØ3<1C>7C999999<1C>NU998877665<1C>5CØ2<1C>6CØ1<1C>7C123456";
            string ncpdpString = cobSegment.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }
    }
}
