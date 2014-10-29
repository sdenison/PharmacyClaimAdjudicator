using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.D0.Response;

using NxBRE.InferenceEngine.IO;


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

        [TestInitialize()]
        public void Setup()
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            Csla.ApplicationContext.User = principal;
        }

        [TestMethod]
        public void TestBilling_34_5_2()
        {
            string _submitted = "61ØØ66DØB1123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø91598765bbbbb<1E><1C>AMØ1<1C>C41962Ø615<1C>C51<1C>CAJOSEPH<1C>CBSMITH<1C>CM123 MAIN STREET<1C>CNMY TOWN<1C>COCO<1C>CP34567<1C>CQ2Ø14658923<1C>HNJSMITH@NCPDP.ORG<1E><1C>AMØ4<1C>C2987654321<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1C>E73ØØØØ<1C>D3Ø<1C>D53Ø<1C>D61<1C>D8Ø<1C>DE2ØØ7Ø915<1C>DF5<1C>DJ1<1C>NX1<1C>DK4<1C>C81<1C>DT1<1C>28EA<1E><1C>AMØ2<1C>EYØ5<1C>E93935933<1E><1C>AMØ3<1C>EZØ8<1C>DBØØG2345<1C>DRJONES<1C>PM2Ø13639572<1C>2EØ1<1C>DL1234566<1C>4EWRIGHT<1E><1C>AM11<1C>D9557{<1C>DC1ØØ{<1C>H71<1C>H8Ø1<1C>H915Ø{<1C>DQ867{<1C>DU8Ø7{<1C>DNØ3";
            string testString = NcpdpHelper.FromHumanReadableToNcpdp(_submitted);
            Library.D0.Submitted.Transmission submittedTransmission = new Library.D0.Submitted.Transmission(testString);
            var submittedClaim = submittedTransmission.Claims[0];
            Assert.AreEqual(submittedTransmission.TransactionHeader.DateOfService, new DateTime(2007, 09, 15));
            Assert.AreEqual(submittedTransmission.Claims[0].Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(submittedTransmission.Claims[0].Claim.DaysSupply, "30");
            Assert.AreEqual(submittedTransmission.Claims[0].Claim.ProductServiceId, "00006094268");
            Assert.AreEqual(submittedTransmission.Claims[0].Pricing.IngredientCostSubmitted, (decimal)55.70);
            Assert.AreEqual(submittedTransmission.Claims[0].Pricing.DispensingFeeSubmitted, (decimal)10.00);
            Assert.AreEqual(submittedTransmission.Claims[0].Pricing.OtherAmountClaimedSubmittedCount, 1);
            Assert.AreEqual(submittedTransmission.Claims[0].Pricing.OtherAmountClaimedSubmittedList[0].OtherAmountClaimedSubmitted, (decimal)15.00); //delivery fee
            Assert.AreEqual(submittedTransmission.Claims[0].Pricing.GrossAmountDue, (decimal)80.70);
            Assert.AreEqual(submittedTransmission.Claims[0].Pricing.BasisOfCostDetermination, "03"); //03 = Direct

            Library.D0.Response.Transmission responseTransmission = null;

            //Response will be accepted and copay should be $15.  
            //Rules should be coming from the patient -> group -> plan, but for now we're going to experiment with ruleml files.
            IRuleBaseAdapter rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\CliniorilShouldHave8copayAndMaxFeeOf8.ruleml", System.IO.FileAccess.Read);
            responseTransmission = Library.D0.TransmissionProcessor.Process(submittedTransmission, rules);

            Assert.AreEqual(responseTransmission.Claims.Count, 1);
            var responseClaim = responseTransmission.Claims[0];
            Assert.AreEqual(responseClaim.Claim.PrescriptionReferenceNumberQualifier, submittedClaim.Claim.PrescriptionReferenceNumberQualifier);
            Assert.AreEqual(responseClaim.Claim.PrescriptionServiceReferenceNumber, submittedClaim.Claim.PrescriptionServiceReferenceNumber);
            Assert.AreEqual(responseTransmission.Claims[0].Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(responseTransmission.Claims[0].Claim.PrescriptionReferenceNumberQualifier, submittedTransmission.Claims[0].Claim.PrescriptionReferenceNumberQualifier);
            Assert.AreEqual(responseClaim.Pricing.IngredientCostPaid, (decimal)55.70);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.IngredientCostPaid, submittedTransmission.Claims[0].Pricing.IngredientCostSubmitted);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.TotalAmountPaid, (decimal)70.70);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.BasisOfReimbursementDetermination, Library.Core.Enums.BasisOfReimbursement.IngredientCostPaid);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.DispensingFeePaid, (decimal)8);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.PatientSalesTaxAmount, (decimal)2);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.AmountOfCopay, (decimal)8);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.PatientPayAmount, (decimal)10);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.AmountAttributedToSalesTax, (decimal)2);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.PatientSalesTaxAmount, (decimal)2);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.FlatSalesTaxAmountPaid, (decimal)2);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.OtherAmountPaidCount, (int)1);
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.OtherAmountPaids[0].OtherAmountPaidQualifier, "01");
            Assert.AreEqual(responseTransmission.Claims[0].Pricing.OtherAmountPaids[0].OtherAmountPaid, (decimal)15.0);

            Assert.AreEqual(responseTransmission.Claims[0].Pricing.TaxExemptIndicator, "1");


            //string expectedResponse = "DØB11AØ14563663bbbbbbbb2ØØ7Ø915<1E><1C>AM2Ø<1C>F4TRANSMISSION MESSAGE TEXT<1D><1E><1C>AM21<1C>ANP<1C>F3123456789123456789<1C>UF1<1C>UHØ1<1C>FQTRANSACTION MESSAGE TEXT<1C>7FØ3<1C>8F6Ø2357Ø862<1E><1C>AM22<1C>EM1<1C>D21234567<1C>9F1<1C>APØ3<1C>AR17236Ø569Ø1<1E><1C>AM23<1C>F51ØØ{<1C>F6557{<1C>F71ØØ{<1C>AV1<1C>J21<1C>J3Ø1<1C>J415Ø{<1C>F97Ø7{<1C>FM1<1C>FN2Ø{<1C>FI8Ø{<1C>AW2Ø{<1C>EQ2Ø{";

            //Without default message
            string expectedResponse = "DØB11AØ14563663bbbbbbbb2ØØ7Ø915<1E><1C>AM21<1C>ANP<1C>F3123456789123456789<1C>7FØ3<1C>8F83Ø5158129<1E><1C>AM22<1C>EM1<1C>D21234567<1E><1C>AM23<1C>F51ØØ{<1C>F6557{<1C>F78Ø{<1C>AV1<1C>AW2Ø{<1C>AX{<1C>J21<1C>J3Ø1<1C>J415Ø{<1C>F97Ø7{<1C>FM1<1C>FN2Ø{<1C>FI8Ø{<1C>EQ2Ø{";
            string actualResponse = responseTransmission.ToNcpdpString();
            string humanReadableResponse = NcpdpHelper.FromNcpdpToHumanReadable(actualResponse);

            Assert.AreEqual(expectedResponse, humanReadableResponse);
        }

    }
}
