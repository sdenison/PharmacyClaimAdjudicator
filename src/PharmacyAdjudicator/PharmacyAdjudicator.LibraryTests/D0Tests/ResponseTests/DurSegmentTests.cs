using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.D0.Response;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for DurSegmentTests
    /// </summary>
    [TestClass]
    public class DurSegmentTests
    {
        public DurSegmentTests()
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
        public void TestDur_34_7_2()
        {
            DurSegment durSeg = new DurSegment();
            durSeg.DurContainers = new List<DurSegment.DurContainer>();

            DurSegment.DurContainer dur = new DurSegment.DurContainer();
            dur.DurPpsResponseCodeCounter = "1";
            dur.ReasonForServiceCode = "LD";
            dur.DatabaseIndicator = "5";
            dur.DurFreeTextMessage = "MIN DAILY DOSE=2 EA/DAY";
            dur.DurAdditionalText = "RENAL IMPAIRMENT MAY JUSTIFY LOW DOSE";
            durSeg.DurContainers.Add(dur);

            dur = new DurSegment.DurContainer();
            dur.DurPpsResponseCodeCounter = "2";
            dur.ReasonForServiceCode = "MC";
            dur.ClinicalSignificanceCode = "3";
            dur.DatabaseIndicator = "5";
            dur.DurFreeTextMessage = "BRONCHIAL ASTHMA";
            durSeg.DurContainers.Add(dur);

            dur = new DurSegment.DurContainer();
            dur.DurPpsResponseCodeCounter = "3";
            dur.ReasonForServiceCode = "ER";
            dur.OtherPharmacyIndicator = "3";
            dur.PreviousDateOfFill = new DateTime(2007, 09, 01);
            dur.QuantityOfPreviousFill = 30;
            dur.OtherPrescriberIndicator = "1";
            dur.DurFreeTextMessage = "RX IS 10 DAYS EARLY";
            durSeg.DurContainers.Add(dur);

            dur = new DurSegment.DurContainer();
            dur.DurPpsResponseCodeCounter = "4";
            dur.ReasonForServiceCode = "TD";
            dur.OtherPharmacyIndicator = "3";
            dur.PreviousDateOfFill = new DateTime(2007, 09, 13);
            dur.QuantityOfPreviousFill = 90;
            dur.DatabaseIndicator = "5";
            dur.OtherPrescriberIndicator = "2";
            dur.DurFreeTextMessage = "IBUPROFEN";
            durSeg.DurContainers.Add(dur);

            string expectedNcpdpString = "<1E><1C>AM24<1C>J61<1C>E4LD<1C>FW5<1C>FYMIN DAILY DOSE=2 EA/DAY<1C>NSRENAL IMPAIRMENT MAY JUSTIFY LOW DOSE<1C>J62<1C>E4MC<1C>FS3<1C>FW5<1C>FYBRONCHIAL ASTHMA<1C>J63<1C>E4ER<1C>FT3<1C>FU2ØØ7Ø9Ø1<1C>FV3Ø<1C>FX1<1C>FYRX IS 1Ø DAYS EARLY<1C>J64<1C>E4TD<1C>FT3<1C>FU2ØØ7Ø913<1C>FV9Ø<1C>FW5<1C>FX2<1C>FYIBUPROFEN";
            string ncpdpString = durSeg.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestDur_34_10_2()
        {
            DurSegment durSeg = new DurSegment();
            durSeg.DurContainers = new List<DurSegment.DurContainer>();

            DurSegment.DurContainer dur = new DurSegment.DurContainer();
            dur.DurPpsResponseCodeCounter = "1";
            dur.ReasonForServiceCode = "HD";
            dur.DatabaseIndicator = "5";
            dur.DurFreeTextMessage = "MAXDOSE=6/DAY";
            durSeg.DurContainers.Add(dur);

            string expectedNcpdpString = "<1E><1C>AM24<1C>J61<1C>E4HD<1C>FW5<1C>FYMAXDOSE=6/DAY";
            string ncpdpString = durSeg.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }
    }
}
