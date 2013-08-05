using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.D0.Response;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for ClaimSegment
    /// </summary>
    [TestClass]
    public class ClaimSegmentTests
    {
        public ClaimSegmentTests()
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
        public void TestClaimResponse_34_5_2()
        {
            ClaimSegment claim = new ClaimSegment();

            claim.PrescriptionReferenceNumberQualifier = "1";
            claim.PrescriptionServiceReferenceNumber = "1234567";

            claim.PreferredProductCount = 1;
            claim.PreferredProducts = new List<ClaimSegment.PreferredProductContainer>();
            ClaimSegment.PreferredProductContainer preferredProduct = new ClaimSegment.PreferredProductContainer();
            claim.PreferredProducts.Add(preferredProduct);

            preferredProduct.PreferredProductIdQualifier = "03";
            preferredProduct.PreferredProductId = "17236056901";

            string expectedNcpdpString = "<1E><1C>AM22<1C>EM1<1C>D21234567<1C>9F1<1C>APØ3<1C>AR17236Ø569Ø1";
            string ncpdpString = claim.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestClaimResponse_34_5_3()
        {
            ClaimSegment claim = new ClaimSegment();

            claim.PrescriptionReferenceNumberQualifier = "1";
            claim.PrescriptionServiceReferenceNumber = "1234567";

            claim.PreferredProductCount = 1;
            claim.PreferredProducts = new List<ClaimSegment.PreferredProductContainer>();
            ClaimSegment.PreferredProductContainer preferredProduct = new ClaimSegment.PreferredProductContainer();
            claim.PreferredProducts.Add(preferredProduct);

            preferredProduct.PreferredProductIdQualifier = "03";
            preferredProduct.PreferredProductId = "17236056901";

            string expectedNcpdpString = "<1E><1C>AM22<1C>EM1<1C>D21234567<1C>9F1<1C>APØ3<1C>AR17236Ø569Ø1";
            string ncpdpString = claim.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestClaimResponse_34_5_4()
        {
            ClaimSegment claim = new ClaimSegment();

            claim.PrescriptionReferenceNumberQualifier = "1";
            claim.PrescriptionServiceReferenceNumber = "1234567";

            string expectedNcpdpString = "<1E><1C>AM22<1C>EM1<1C>D21234567";
            string ncpdpString = claim.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestClaimResponse_34_5_6()
        {
            ClaimSegment claim = new ClaimSegment();

            claim.PrescriptionReferenceNumberQualifier = "1";
            claim.PrescriptionServiceReferenceNumber = "1234567";

            claim.PreferredProductCount = 1;
            claim.PreferredProducts = new List<ClaimSegment.PreferredProductContainer>();
            ClaimSegment.PreferredProductContainer preferredProduct = new ClaimSegment.PreferredProductContainer();
            claim.PreferredProducts.Add(preferredProduct);

            preferredProduct.PreferredProductIdQualifier = "03";
            preferredProduct.PreferredProductId = "17236056901";
            preferredProduct.PreferredProductIncentive = 10;

            string expectedNcpdpString = "<1E><1C>AM22<1C>EM1<1C>D21234567<1C>9F1<1C>APØ3<1C>AR17236Ø569Ø1<1C>AS1ØØ{";
            string ncpdpString = claim.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }
    }
}
