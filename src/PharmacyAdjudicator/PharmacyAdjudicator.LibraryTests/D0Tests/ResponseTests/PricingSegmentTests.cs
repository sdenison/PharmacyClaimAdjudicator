using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.D0.Response;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for PricingSegmentTests
    /// </summary>
    [TestClass]
    public class PricingSegmentTests
    {
        public PricingSegmentTests()
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
        public void TestPricing_34_5_2()
        {
            PricingSegment pricing = new PricingSegment();
            pricing.PatientPayAmount = 10;
            pricing.IngredientCostPaid = (decimal)55.7;
            pricing.DispensingFeePaid = 8;
            pricing.TaxExemptIndicator = "1";
            pricing.OtherAmountPaidCount = 1;

            PricingSegment.OtherAmountPaidContainer otherAmount = new PricingSegment.OtherAmountPaidContainer();
            otherAmount.OtherAmountPaidQualifier = "01";
            otherAmount.OtherAmountPaid = 15;
            pricing.OtherAmountPaids = new List<PricingSegment.OtherAmountPaidContainer>();
            pricing.OtherAmountPaids.Add(otherAmount);

            //Chagned TotalAmountPaid to calculated amount
            //pricing.TotalAmountPaid = (decimal)70.70;
            pricing.BasisOfReimbursementDetermination = Library.Core.Enums.BasisOfReimbursement.IngredientCostPaid;
            pricing.AmountAttributedToSalesTax = 2;
            pricing.AmountOfCopay = 8;
            pricing.FlatSalesTaxAmountPaid = 2;
            pricing.PatientSalesTaxAmount = 2;

            //507-F7 Dispensing Fee Paid is 10.00 in string, but 8.00 in grid.
            //Needed to change order of Flat Sales Tax Amount Paid.
            string expectedNcpdpString = "<1E><1C>AM23<1C>F51ØØ{<1C>F6557{<1C>F78Ø{<1C>AV1<1C>AW2Ø{<1C>J21<1C>J3Ø1<1C>J415Ø{<1C>F97Ø7{<1C>FM1<1C>FN2Ø{<1C>FI8Ø{<1C>EQ2Ø{";
            string ncpdpString = pricing.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestPricing_34_5_3()
        {
            PricingSegment pricing = new PricingSegment();
            pricing.PatientPayAmount = 15;
            pricing.IngredientCostPaid = (decimal)55.7;
            pricing.DispensingFeePaid = 10;
            pricing.TaxExemptIndicator = "1";

            //Changed TotalAmountPaid to calculated field
            //pricing.TotalAmountPaid = (decimal)70.70;
            pricing.BasisOfReimbursementDetermination = Library.Core.Enums.BasisOfReimbursement.IngredientCostPaid;
            //It's 8.00 but should be 15.00
            pricing.AmountOfCopay = 15;

            //Had to change Total Amount Paid from 70.70 to 50.70
            string expectedNcpdpString = "<1E><1C>AM23<1C>F515Ø{<1C>F6557{<1C>F71ØØ{<1C>AV1<1C>F95Ø7{<1C>FM1<1C>FI15Ø{";
            string ncpdpString = pricing.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestPricing_34_5_4()
        {
            PricingSegment pricing = new PricingSegment();
            pricing.PatientPayAmount = 10;
            pricing.IngredientCostPaid = (decimal)55.7;
            pricing.DispensingFeePaid = 5;
            pricing.TaxExemptIndicator = "1";
            //pricing.TotalAmountPaid = (decimal)50.70;
            pricing.BasisOfReimbursementDetermination = Library.Core.Enums.BasisOfReimbursement.IngredientCostPaid;

            string expectedNcpdpString = "<1E><1C>AM23<1C>F51ØØ{<1C>F6557{<1C>F75Ø{<1C>AV1<1C>F95Ø7{<1C>FM1";
            string ncpdpString = pricing.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestPricing_34_5_4_modified()
        {
            PricingSegment pricing = new PricingSegment();
            pricing.PatientPayAmount = 10;
            pricing.IngredientCostPaid = (decimal)55.7;
            pricing.DispensingFeePaid = 5;
            pricing.TaxExemptIndicator = "1";
            //pricing.TotalAmountPaid = 0;
            pricing.BasisOfReimbursementDetermination = Library.Core.Enums.BasisOfReimbursement.IngredientCostPaid;

            string expectedNcpdpString = "<1E><1C>AM23<1C>F51ØØ{<1C>F6557{<1C>F75Ø{<1C>AV1<1C>F95Ø7{<1C>FM1";
            string ncpdpString = pricing.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestPricing_34_10_1()
        {
            PricingSegment pricing = new PricingSegment();
            pricing.PatientPayAmount = 5;
            pricing.IngredientCostPaid = (decimal)14.20;
            pricing.DispensingFeePaid = 15;
            pricing.TaxExemptIndicator = "1";

            pricing.OtherAmountPaidCount = 1;
            PricingSegment.OtherAmountPaidContainer otherAmount = new PricingSegment.OtherAmountPaidContainer();
            otherAmount.OtherAmountPaidQualifier = "01";
            otherAmount.OtherAmountPaid = 5;
            pricing.OtherAmountPaids = new List<PricingSegment.OtherAmountPaidContainer>();
            pricing.OtherAmountPaids.Add(otherAmount);

            //pricing.TotalAmountPaid = (decimal)29.20;
            pricing.BasisOfReimbursementDetermination = Library.Core.Enums.BasisOfReimbursement.IngredientCostPaid;

            string expectedNcpdpString = "<1E><1C>AM23<1C>F55Ø{<1C>F6142{<1C>F715Ø{<1C>AV1<1C>J21<1C>J3Ø1<1C>J45Ø{<1C>F9292{<1C>FM1"; 
            string ncpdpString = pricing.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

    }
}
