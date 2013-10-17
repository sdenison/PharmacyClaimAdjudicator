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
        }

        [TestMethod]
        public void TestClaimResponse_34_5_2()
        {
            ClaimSegment claim = new ClaimSegment();

            claim.PrescriptionReferenceNumberQualifier = "1";
            claim.PrescriptionServiceReferenceNumber = "1234567";

            //Preferred product count should come from the count of the preferred product list
            //claim.PreferredProductCount = 1;
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

            //Preferred product count should come from the count of the list
            //claim.PreferredProductCount = 1;
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

            //Preferred product count should come from the list
            // claim.PreferredProductCount = 1;
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
