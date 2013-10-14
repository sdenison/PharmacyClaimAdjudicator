using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.D0.Response;


namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for TransmissionTests
    /// </summary>
    [TestClass]
    public class TransmissionTests
    {
        public TransmissionTests()
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
        public void TestBilling_34_5_3()
        {
            string _submitted = "61ØØ66DØB1123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø91598765bbbbb<1E><1C>AMØ1<1C>C41962Ø615<1C>C51<1C>CAJOSEPH<1C>CBSMITH<1C>CM123 MAIN STREET<1C>CNMY TOWN<1C>COCO<1C>CP34567<1C>CQ2Ø14658923<1C>HNJSMITH@NCPDP.ORG<1E><1C>AMØ4<1C>C2987654321<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1C>E73ØØØØ<1C>D3Ø<1C>D53Ø<1C>D61<1C>D8Ø<1C>DE2ØØ7Ø915<1C>DF5<1C>DJ1<1C>NX1<1C>DK4<1C>C81<1C>DT1<1C>28EA<1E><1C>AMØ2<1C>EYØ5<1C>E93935933<1E><1C>AMØ3<1C>EZØ8<1C>DBØØG2345<1C>DRJONES<1C>PM2Ø13639572<1C>2EØ1<1C>DL1234566<1C>4EWRIGHT<1E><1C>AM11<1C>D9557{<1C>DC1ØØ{<1C>H71<1C>H8Ø1<1C>H915Ø{<1C>DQ867{<1C>DU8Ø7{<1C>DNØ3";
            string testString = NcpdpHelper.FromHumanReadableToNcpdp(_submitted);
            Library.D0.Submitted.Transmission submittedTransmission = new Library.D0.Submitted.Transmission(testString);
            Assert.IsNotNull(submittedTransmission, "It's not alive");

            Library.D0.Response.Transmission responseTransmission = null;

            //Response will be accepted and copay should be $15.  

            responseTransmission = Library.D0.TransmissionProcessor.Process(submittedTransmission);

            string expectedResponse = "DØB11AØ14563663bbbbbbbb2ØØ7Ø915<1E><1C>AM2Ø<1C>F4TRANSMISSION MESSAGE TEXT<1D><1E><1C>AM21<1C>ANP<1C>F3123456789123456789<1C>UF1<1C>UHØ1<1C>FQTRANSACTION MESSAGE TEXT<1C>7FØ3<1C>8F6Ø2357Ø862<1E><1C>AM22<1C>EM1<1C>D21234567<1C>9F1<1C>APØ3<1C>AR17236Ø569Ø1<1E><1C>AM23<1C>F51ØØ{<1C>F6557{<1C>F71ØØ{<1C>AV1<1C>J21<1C>J3Ø1<1C>J415Ø{<1C>F97Ø7{<1C>FM1<1C>FN2Ø{<1C>FI8Ø{<1C>AW2Ø{<1C>EQ2Ø{";

            //Assert.AreEqual(responseTransmission.

            Assert.IsNotNull(responseTransmission);
        }

    }
}
