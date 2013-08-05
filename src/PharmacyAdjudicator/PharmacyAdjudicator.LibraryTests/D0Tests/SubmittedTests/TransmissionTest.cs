using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Collections.Generic;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.SubmittedTests
{
    [TestClass]
    public class TransmissionTest
    {

        [TestInitialize]
        public void InitializeRequestStrings() 
        {
        }

        public string PrepareNcpdpString(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace('Ø', '0');
            sb.Replace("<1E>", Library.Utils.NcpdpString.SegmentSeparator.ToString());
            sb.Replace("<1D>", Library.Utils.NcpdpString.GroupSeparator.ToString());
            sb.Replace("<1C>", Library.Utils.NcpdpString.FieldSeparator.ToString());
            return sb.ToString();
        }

        [TestMethod]
        public void TestBilling_34_5()
        {
            string _billingTransactionCodeB1 = "61ØØ66DØB1123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø91598765bbbbb<1E><1C>AMØ1<1C>C41962Ø615<1C>C51<1C>CAJOSEPH<1C>CBSMITH<1C>CM123 MAIN STREET<1C>CNMY TOWN<1C>COCO<1C>CP34567<1C>CQ2Ø14658923<1C>HNJSMITH@NCPDP.ORG<1E><1C>AMØ4<1C>C2987654321<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1C>E73ØØØØ<1C>D3Ø<1C>D53Ø<1C>D61<1C>D8Ø<1C>DE2ØØ7Ø915<1C>DF5<1C>DJ1<1C>NX1<1C>DK4<1C>C81<1C>DT1<1C>28EA<1E><1C>AMØ2<1C>EYØ5<1C>E93935933<1E><1C>AMØ3<1C>EZØ8<1C>DBØØG2345<1C>DRJONES<1C>PM2Ø13639572<1C>2EØ1<1C>DL1234566<1C>4EWRIGHT<1E><1C>AM11<1C>D9557{<1C>DC1ØØ{<1C>H71<1C>H8Ø1<1C>H915Ø{<1C>DQ867{<1C>DU8Ø7{<1C>DNØ3";
            string testString = PrepareNcpdpString(_billingTransactionCodeB1);
            Library.D0.Submitted.Transmission firstTransmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(firstTransmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(firstTransmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(firstTransmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(firstTransmission.TransactionHeader.TransactionCode, "B1");
            Assert.AreEqual(firstTransmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(firstTransmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(firstTransmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(firstTransmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(firstTransmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(firstTransmission.TransactionHeader.SoftwareVendorId, "98765bbbbb");

            //Patient Segment
            Assert.AreEqual(firstTransmission.Patient.SegmentIdentification, "01");
            Assert.AreEqual(firstTransmission.Patient.DateOfBirth, new DateTime(1962, 6, 15));
            Assert.AreEqual(firstTransmission.Patient.PatientGenderCode, "1");
            Assert.AreEqual(firstTransmission.Patient.PatientFirstName, "JOSEPH");
            Assert.AreEqual(firstTransmission.Patient.PatientLastName, "SMITH");
            Assert.AreEqual(firstTransmission.Patient.PatientStreetAddress, "123 MAIN STREET");
            Assert.AreEqual(firstTransmission.Patient.PatientCityAddress, "MY TOWN");
            Assert.AreEqual(firstTransmission.Patient.PatientState, "CO");
            Assert.AreEqual(firstTransmission.Patient.PatientZip, "34567");
            Assert.AreEqual(firstTransmission.Patient.PatientPhoneNumber, "2014658923");
            Assert.AreEqual(firstTransmission.Patient.PatientEmailAddress, "JSMITH@NCPDP.ORG");

            //Insurance Segment
            Assert.AreEqual(firstTransmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(firstTransmission.Insurance.CardholderId, "987654321");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = firstTransmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "00006094268");
            Assert.AreEqual(claimBilling.Claim.QuantityDispensed, 30);
            Assert.AreEqual(claimBilling.Claim.FillNumber, "0");
            Assert.AreEqual(claimBilling.Claim.DaysSupply, "30");
            Assert.AreEqual(claimBilling.Claim.CompoundCode, "1"); //Not a coumpound
            Assert.AreEqual(claimBilling.Claim.DispenseAsWrittenProductSelectionCode, "0");
            Assert.AreEqual(claimBilling.Claim.DatePrescriptionWritten, new DateTime(2007, 09, 15));
            Assert.AreEqual(claimBilling.Claim.NumberOfRefillsAuthorized, "5");
            Assert.AreEqual(claimBilling.Claim.PrescriptionOriginCode, "1");
            Assert.AreEqual(claimBilling.Claim.SubmissionClarificationCodeCount, 1);
            Assert.AreEqual(claimBilling.Claim.SubmissionClarificationCode[0], "4"); //Lost Prescription
            Assert.AreEqual(claimBilling.Claim.OtherCoverageCode, "1");
            Assert.AreEqual(claimBilling.Claim.SpecialPackagingIndicator, "1");
            Assert.AreEqual(claimBilling.Claim.UnitOfMeasure, "EA");

            //Pharmacy Segment
            Library.D0.Submitted.PharmacyProviderSegment pharmacyProvider = claimBilling.PharmacyProvider;
            Assert.AreEqual(pharmacyProvider.SegmentIdentification, "02");
            Assert.AreEqual(pharmacyProvider.ProviderIdQualifier, "05"); //NPI
            Assert.AreEqual(pharmacyProvider.ProviderId, "3935933");  //Actual and grid are in conflict.  Grid says 3935933111

            //Prescriber Segment
            Library.D0.Submitted.PrescriberSegment prescriber = claimBilling.Prescriber;
            Assert.AreEqual(prescriber.SegmentIdentification, "03");
            Assert.AreEqual(prescriber.PrescriberIdQualifier, "08");
            Assert.AreEqual(prescriber.PrescriberId, "00G2345");
            Assert.AreEqual(prescriber.PrescriberLastName, "JONES");
            Assert.AreEqual(prescriber.PrescriberPhoneNumber, "2013639572");
            Assert.AreEqual(prescriber.PrimaryCareProviderIdQualifier, "01"); //NPI
            Assert.AreEqual(prescriber.PrimaryCareProviderId, "1234566");  //Actual and grid are in conflict.  Grid says 1234566111
            Assert.AreEqual(prescriber.PrimaryCareProviderLastName, "WRIGHT"); //NPI

            //Pricing Segment
            Library.D0.Submitted.PricingSegment pricing = claimBilling.Pricing;
            Assert.AreEqual(pricing.SegmentIdentification, "11");
            Assert.AreEqual(pricing.IngredientCostSubmitted, (decimal)55.70);
            Assert.AreEqual(pricing.DispensingFeeSubmitted, (decimal)10.00);
            Assert.AreEqual(pricing.OtherAmountClaimedSubmittedCount, (int)1);
            Assert.AreEqual(pricing.OtherAmountClaimedSubmittedList[0].OtherAmountClaimedSubmittedQualifier, "01");
            Assert.AreEqual(pricing.OtherAmountClaimedSubmittedList[0].OtherAmountClaimedSubmitted, (decimal)15.00);
            Assert.AreEqual(pricing.UsualAndCustomaryCharge, (decimal)86.70);
            Assert.AreEqual(pricing.GrossAmountDue, (decimal)80.70);
            Assert.AreEqual(pricing.BasisOfCostDetermination, "03");
        }

        [TestMethod]
        public void TestBilling_34_5_1()
        {
            //AM04 in guide is missing "<"
            //Guide says <1C>DJ1>1C> but it should be <1C>DJ1<1C>
            string _billingTransactionCodeB1 = "484848DØB156789Ø12341Ø14563663bbbbbbbb2ØØ7Ø91598765bbbbb<1E><1C>AMØ4<1C>C2987654321<1E><1C>AMØ1<1C>C41962Ø615<1C>C51<1C>CAJOSEPH<1C>CBSMITH<1C>CM123 MAIN STREET<1C>CNMY TOWN<1C>COCO<1C>CP24567<1C>HNJSMITH@NCPDP.ORG<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1C>E73ØØØØ<1C>D3Ø<1C>D53Ø<1C>D61<1C>D8Ø<1C>DE2ØØ7Ø915<1C>DF5<1C>DJ1<1C>EX4689<1C>EW1<1E><1C>AMØ3<1C>EZØ8<1C>DBØØG2345<1C>DRJONES<1E><1C>AM11<1C>D9557{<1C>DC5Ø{<1C>DQ587{<1C>DU6Ø7{<1C>DNØ3";
            
            string testString = PrepareNcpdpString(_billingTransactionCodeB1);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "484848");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "B1");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "5678901234");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "98765bbbbb");

            //Patient Segment
            Assert.AreEqual(transmission.Patient.SegmentIdentification, "01");
            Assert.AreEqual(transmission.Patient.DateOfBirth, new DateTime(1962, 6, 15));
            Assert.AreEqual(transmission.Patient.PatientGenderCode, "1");
            Assert.AreEqual(transmission.Patient.PatientFirstName, "JOSEPH");
            Assert.AreEqual(transmission.Patient.PatientLastName, "SMITH");
            Assert.AreEqual(transmission.Patient.PatientStreetAddress, "123 MAIN STREET");
            Assert.AreEqual(transmission.Patient.PatientCityAddress, "MY TOWN");
            Assert.AreEqual(transmission.Patient.PatientState, "CO");
            Assert.AreEqual(transmission.Patient.PatientZip, "24567"); //Actual and grid are in conflict.  Actual 24567 and grid is 34567.
            Assert.AreEqual(transmission.Patient.PatientEmailAddress, "JSMITH@NCPDP.ORG");

            //Insurance Segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "987654321");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "00006094268");
            Assert.AreEqual(claimBilling.Claim.QuantityDispensed, 30);
            Assert.AreEqual(claimBilling.Claim.FillNumber, "0");
            Assert.AreEqual(claimBilling.Claim.DaysSupply, "30");
            Assert.AreEqual(claimBilling.Claim.CompoundCode, "1"); //Not a coumpound
            Assert.AreEqual(claimBilling.Claim.DispenseAsWrittenProductSelectionCode, "0");
            Assert.AreEqual(claimBilling.Claim.DatePrescriptionWritten, new DateTime(2007, 09, 15));
            Assert.AreEqual(claimBilling.Claim.NumberOfRefillsAuthorized, "5");
            Assert.AreEqual(claimBilling.Claim.PrescriptionOriginCode, "1");
            //These are the Intermediar Processing Override Codes
            Assert.AreEqual(claimBilling.Claim.IntermediaryAuthorizationId, "4689");
            Assert.AreEqual(claimBilling.Claim.IntermediaryAuthorizationTypeId, "1");

            //No Pharmacy Segment in this example.
            //Not sure why the pharmacy segemnt is needed with Service Provider ID in Transaction Header.

            //Prescriber Segment
            Library.D0.Submitted.PrescriberSegment prescriber = claimBilling.Prescriber;
            Assert.AreEqual(prescriber.SegmentIdentification, "03");
            Assert.AreEqual(prescriber.PrescriberIdQualifier, "08");
            Assert.AreEqual(prescriber.PrescriberId, "00G2345");
            Assert.AreEqual(prescriber.PrescriberLastName, "JONES");

            //Pricing Segment
            Library.D0.Submitted.PricingSegment pricing = claimBilling.Pricing;
            Assert.AreEqual(pricing.SegmentIdentification, "11");
            Assert.AreEqual(pricing.IngredientCostSubmitted, (decimal)55.70);
            Assert.AreEqual(pricing.DispensingFeeSubmitted, (decimal)5.00);
            Assert.AreEqual(pricing.OtherAmountClaimedSubmittedCount, (int)0);
            Assert.AreEqual(pricing.UsualAndCustomaryCharge, (decimal)58.70);
            Assert.AreEqual(pricing.GrossAmountDue, (decimal)60.70);
            Assert.AreEqual(pricing.BasisOfCostDetermination, "03");
        }

        [TestMethod]
        public void TestBilling_34_7()
        {
            string _billingTransactionCodeB1 = "61ØØ66DØB1123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø91598765bbbbb<1E><1C>AMØ4<1C>C2123456789<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1C>E73ØØØØ<1C>D3Ø<1C>D53Ø<1C>D61<1C>D8Ø<1C>DE2ØØ7Ø915<1C>DF5<1C>DJ1<1E><1C>AMØ8<1C>7E1<1C>E4DA<1C>E5MØ<1C>E61B<1C>8E11<1C>7E2<1C>E4LR<1C>E5PØ<1C>E61B<1C>8E11<1C>7E3<1C>E4TD<1C>E5MØ<1C>E61B<1C>8E11<1C>J9Ø3<1C>H617236Ø569Ø1<1E><1C>AM11<1C>D9557{<1C>DC1ØØ{<1C>H71<1C>H8Ø1<1C>H915Ø{<1C>DQ716E<1C>DU8Ø7{<1C>DNØ3";

            string testString = PrepareNcpdpString(_billingTransactionCodeB1);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "B1");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "98765bbbbb");

            //Insurance Segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "123456789");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "00006094268");
            Assert.AreEqual(claimBilling.Claim.QuantityDispensed, 30);
            Assert.AreEqual(claimBilling.Claim.FillNumber, "0");
            Assert.AreEqual(claimBilling.Claim.DaysSupply, "30");
            Assert.AreEqual(claimBilling.Claim.CompoundCode, "1"); //Not a coumpound
            Assert.AreEqual(claimBilling.Claim.DispenseAsWrittenProductSelectionCode, "0");
            Assert.AreEqual(claimBilling.Claim.DatePrescriptionWritten, new DateTime(2007, 09, 15));
            Assert.AreEqual(claimBilling.Claim.NumberOfRefillsAuthorized, "5");
            Assert.AreEqual(claimBilling.Claim.PrescriptionOriginCode, "1");

            //DUR/PPS Segment
            Library.D0.Submitted.DurSegment durSegment = claimBilling.Dur;
            Assert.AreEqual(durSegment.SegmentIdentification, "08");
            Library.D0.Submitted.DurSegment.DurContainer dur = durSegment.DurContainers[0];
            Assert.AreEqual(dur.DurPpsCodeCounter, 1);
            Assert.AreEqual(dur.ReasonForServiceCode, "DA");
            Assert.AreEqual(dur.ProfessionalServiceCode, "M0");
            Assert.AreEqual(dur.ResultOfServiceCode, "1B");
            Assert.AreEqual(dur.DurPpsLevelOfEffort, "11");

            //Next DUR 
            dur = durSegment.DurContainers[1];
            Assert.AreEqual(dur.DurPpsCodeCounter, 2);
            Assert.AreEqual(dur.ReasonForServiceCode, "LR");
            Assert.AreEqual(dur.ProfessionalServiceCode, "P0");
            Assert.AreEqual(dur.ResultOfServiceCode, "1B");
            Assert.AreEqual(dur.DurPpsLevelOfEffort, "11");

            //Next DUR 
            dur = durSegment.DurContainers[2];
            Assert.AreEqual(dur.DurPpsCodeCounter, 3);
            Assert.AreEqual(dur.ReasonForServiceCode, "TD");
            Assert.AreEqual(dur.ProfessionalServiceCode, "M0");
            Assert.AreEqual(dur.ResultOfServiceCode, "1B");
            Assert.AreEqual(dur.DurPpsLevelOfEffort, "11");
            Assert.AreEqual(dur.DurCoAgentIdQualifier, "03"); //NDC indicator
            Assert.AreEqual(dur.DurCoAgentId, "17236056901"); //NDC
        }

        [TestMethod]
        public void TestBilling_34_10()
        {
            string _billingTransactionCodeB1 = "61ØØ66DØB1123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø915bbbbbbbbbb<1E><1C>AMØ4<1C>C2123456789<1C>C96<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1ØØ<1C>D7Ø<1C>E712ØØØØ<1C>D31<1C>D53<1C>D62<1C>D8Ø<1C>DE2ØØ7Ø915<1C>DF5<1C>DJ1<1C>NX1<1C>DKØ<1C>C81<1C>DT1<1C>28ML<1C>E211<1E><1C>AMØ8<1C>7E1<1C>E4DD<1C>E5RØ<1C>E61B<1C>8E11<1C>J9Ø3<1C>H6Ø4ØØØØØØ216<1E><1C>AM1Ø<1C>EF11<1C>EG3<1C>ECØ3<1C>REØ3<1C>TE11845Ø139Ø1<1C>ED12ØØØ<1C>UEØ1<1C>REØ3<1C>TEØØ6Ø3148Ø49<1C>ED12ØØØØ<1C>EE84{<1C>UEØ1<1C>REØ3<1C>TE6Ø8Ø9Ø31Ø55<1C>ED24ØØØ<1C>EE46{<1C>UEØ1<1E><1C>AM11<1C>D9142{<1C>DC15Ø{<1C>H71<1C>H8Ø1<1C>H95Ø{<1C>DQ311E<1C>DU342{<1C>DNØ1";
            string testString = PrepareNcpdpString(_billingTransactionCodeB1);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "B1");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "bbbbbbbbbb");

            //Insurance Segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "123456789");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "00");  //00 used for multiingredient compounds
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "0"); //product service ID's will be in the compound segments
            Assert.AreEqual(claimBilling.Claim.QuantityDispensed, 120);
            Assert.AreEqual(claimBilling.Claim.FillNumber, "1");
            Assert.AreEqual(claimBilling.Claim.DaysSupply, "3");
            Assert.AreEqual(claimBilling.Claim.CompoundCode, "2"); //It is a coumpound
            Assert.AreEqual(claimBilling.Claim.DispenseAsWrittenProductSelectionCode, "0");
            Assert.AreEqual(claimBilling.Claim.DatePrescriptionWritten, new DateTime(2007, 09, 15));
            Assert.AreEqual(claimBilling.Claim.NumberOfRefillsAuthorized, "5");
            Assert.AreEqual(claimBilling.Claim.PrescriptionOriginCode, "1");
            Assert.AreEqual(claimBilling.Claim.OtherCoverageCode, "1");
            Assert.AreEqual(claimBilling.Claim.SpecialPackagingIndicator, "1");
            Assert.AreEqual(claimBilling.Claim.UnitOfMeasure, "ML");
            Assert.AreEqual(claimBilling.Claim.RouteOfAdministration, "11"); //Oral

            //DUR/PPS Segment
            Library.D0.Submitted.DurSegment durSegment = claimBilling.Dur;
            Assert.AreEqual(durSegment.SegmentIdentification, "08");
            Library.D0.Submitted.DurSegment.DurContainer dur = durSegment.DurContainers[0];
            Assert.AreEqual(dur.DurPpsCodeCounter, 1);
            Assert.AreEqual(dur.ReasonForServiceCode, "DD");
            Assert.AreEqual(dur.ProfessionalServiceCode, "R0");
            Assert.AreEqual(dur.ResultOfServiceCode, "1B");
            Assert.AreEqual(dur.DurPpsLevelOfEffort, "11");
            Assert.AreEqual(dur.DurCoAgentIdQualifier, "03");
            Assert.AreEqual(dur.DurCoAgentId, "04000000216");

            //Compound Segment
            Library.D0.Submitted.CompoundSegment compoundSegment = claimBilling.Compound;
            Assert.AreEqual(compoundSegment.SegmentIdentification, "10");
            Assert.AreEqual(compoundSegment.CompoundDosageFormDescriptionCode, "11"); //Solution
            Assert.AreEqual(compoundSegment.CompoundDispensingUnitFormIndicator, "3"); //Milliliters
            Assert.AreEqual(compoundSegment.CompoundIngredientComponentCount, 3);

            //First Ingredient
            Library.D0.Submitted.CompoundSegment.CompoundIngredient firstIngredient = compoundSegment.CompoundIngredients[0];
            Assert.AreEqual(firstIngredient.CompoundProductIdQualifier, "03"); //NDC Code
            Assert.AreEqual(firstIngredient.CompoundProductId, "11845013901"); //NDC
            Assert.AreEqual(firstIngredient.CompoundIngredientQuantity, 12);
            Assert.AreEqual(firstIngredient.CompoundIngredientDrugCost, null); //Intentionally left blank for rejected response example to designate an error.
            Assert.AreEqual(firstIngredient.CompoundIngredientBasisOfCostDetermination, "01"); 

            //Second Ingredient
            Library.D0.Submitted.CompoundSegment.CompoundIngredient secondIngredient = compoundSegment.CompoundIngredients[1];
            Assert.AreEqual(secondIngredient.CompoundProductIdQualifier, "03"); //NCD Code
            Assert.AreEqual(secondIngredient.CompoundProductId, "00603148049"); //NCD
            Assert.AreEqual(secondIngredient.CompoundIngredientQuantity, 120);
            Assert.AreEqual(secondIngredient.CompoundIngredientDrugCost, (decimal)8.40);
            Assert.AreEqual(secondIngredient.CompoundIngredientBasisOfCostDetermination, "01"); //AWP

            //Third Ingredient
            Library.D0.Submitted.CompoundSegment.CompoundIngredient thirdIngredient = compoundSegment.CompoundIngredients[2];
            Assert.AreEqual(thirdIngredient.CompoundProductIdQualifier, "03"); //NCD Code
            Assert.AreEqual(thirdIngredient.CompoundProductId, "60809031055"); //NCD  
            Assert.AreEqual(thirdIngredient.CompoundIngredientQuantity, 24);
            Assert.AreEqual(thirdIngredient.CompoundIngredientDrugCost, (decimal)4.60);
            Assert.AreEqual(thirdIngredient.CompoundIngredientBasisOfCostDetermination, "01"); //AWP

            //Pricing Segment
            Library.D0.Submitted.PricingSegment pricing = claimBilling.Pricing;
            Assert.AreEqual(pricing.SegmentIdentification, "11");
            Assert.AreEqual(pricing.IngredientCostSubmitted, (decimal)14.20);
            Assert.AreEqual(pricing.DispensingFeeSubmitted, (decimal)15.00);
            Assert.AreEqual(pricing.OtherAmountClaimedSubmittedCount, (int)1);
            Assert.AreEqual(pricing.OtherAmountClaimedSubmittedList[0].OtherAmountClaimedSubmittedQualifier, "01");
            Assert.AreEqual(pricing.OtherAmountClaimedSubmittedList[0].OtherAmountClaimedSubmitted, (decimal)5.00);
            Assert.AreEqual(pricing.UsualAndCustomaryCharge, (decimal)31.15);
            Assert.AreEqual(pricing.GrossAmountDue, (decimal)34.20);
            Assert.AreEqual(pricing.BasisOfCostDetermination, "01"); //AWP
        }

        [TestMethod]
        public void TestReversal_34_17()
        {
            string _reversalB2 = "61ØØ66DØB2123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø91598765bbbbb<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268";
            string testString = PrepareNcpdpString(_reversalB2);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "B2");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "98765bbbbb");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "00006094268");
        }

        [TestMethod]
        public void TestReversal_34_17_1()
        {
            string _reversalB2 = "61ØØ66DØB2123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø91598765bbbbb<1E><1C>AMØ4<1C>C2123456789<1C>C1MX468<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1E><1C>AMØ8<1C>7E1<1C>E4MS<1C>E5MØ<1C>E62A<1C>8E11";
            string testString = PrepareNcpdpString(_reversalB2);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "B2");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "98765bbbbb");

            //Insurance Segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "123456789");
            Assert.AreEqual(transmission.Insurance.GroupId, "MX468");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "00006094268");

            //DUR/PPS Segment
            Library.D0.Submitted.DurSegment durSegment = claimBilling.Dur;
            Assert.AreEqual(durSegment.SegmentIdentification, "08");
            Library.D0.Submitted.DurSegment.DurContainer dur = durSegment.DurContainers[0];
            Assert.AreEqual(dur.DurPpsCodeCounter, 1);
            Assert.AreEqual(dur.ReasonForServiceCode, "MS");
            Assert.AreEqual(dur.ProfessionalServiceCode, "M0");
            Assert.AreEqual(dur.ResultOfServiceCode, "2A");
            Assert.AreEqual(dur.DurPpsLevelOfEffort, "11");
        }

        
        [TestMethod]
        public void TestRebill_34_18()
        {
            //Rebills are reversal + claim all in one transmission
            string _rebillB3 = "61ØØ66DØB3123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø915bbbbbbbbbb<1E><1C>AMØ4<1C>C2123456789<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø3417821<1C>E73ØØØØ<1C>D3Ø<1C>D53Ø<1C>D61<1C>D8Ø<1C>DE2ØØ7Ø915<1C>DF5<1C>DJ1<1C>C81<1C>DT1<1C>28EA<1E><1C>AM11<1C>D9357F<1C>DC8Ø{<1C>DX5Ø{<1C>DQ528E<1C>DU437F<1C>DNØ3";

            string testString = PrepareNcpdpString(_rebillB3);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "B3");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "bbbbbbbbbb");

            //Insurance Segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "123456789");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "0000603417821");
            Assert.AreEqual(claimBilling.Claim.QuantityDispensed, 30);
            Assert.AreEqual(claimBilling.Claim.FillNumber, "0");
            Assert.AreEqual(claimBilling.Claim.DaysSupply, "30");
            Assert.AreEqual(claimBilling.Claim.CompoundCode, "1"); //Not a compound
            Assert.AreEqual(claimBilling.Claim.DispenseAsWrittenProductSelectionCode, "0");
            Assert.AreEqual(claimBilling.Claim.DatePrescriptionWritten, new DateTime(2007, 09, 15));
            Assert.AreEqual(claimBilling.Claim.NumberOfRefillsAuthorized, "5");
            Assert.AreEqual(claimBilling.Claim.PrescriptionOriginCode, "1");
            Assert.AreEqual(claimBilling.Claim.OtherCoverageCode, "1");
            Assert.AreEqual(claimBilling.Claim.SpecialPackagingIndicator, "1");
            Assert.AreEqual(claimBilling.Claim.UnitOfMeasure, "EA");

            //Pricing Segment
            Library.D0.Submitted.PricingSegment pricing = claimBilling.Pricing;
            Assert.AreEqual(pricing.SegmentIdentification, "11");
            Assert.AreEqual(pricing.IngredientCostSubmitted, (decimal)35.76);
            Assert.AreEqual(pricing.DispensingFeeSubmitted, (decimal)8.00);
            Assert.AreEqual(pricing.PatientPaidAmountSubmitted, (decimal)5.00);
            Assert.AreEqual(pricing.UsualAndCustomaryCharge, (decimal)52.85);
            Assert.AreEqual(pricing.GrossAmountDue, (decimal)43.76);
            Assert.AreEqual(pricing.BasisOfCostDetermination, "03"); //Direct
        }

        [TestMethod]
        public void TestPriorAuthAndBilling_34_19()
        {
            //Rebills are reversal + claim all in one transmission
            //Documentatin missing the segment separator before AM11 segment
            //Prescriber ID given D8 prefix whichis incorrect.  D8 is DAW Code.  Correct prefix is DB.
            //PB200915 needed to be updated to PB20070915
            string priorAuthP1 = "61ØØ66DØP1123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø915bbbbbbbbbb<1E><1C>AMØ4<1C>C2123456789<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1C>E73ØØØØ<1C>D3Ø<1C>D53Ø<1C>D61<1C>D81<1C>DE2ØØ7Ø915<1C>DF5<1C>DJ1<1C>C81<1C>DT1<1C>28EA<1E><1C>AMØ3<1C>EZØ8<1C>DBØØG2345<1C>1E1Ø<1C>DRJONES<1C>PM2Ø13639572<1C>2E1<1C>DL1234577<1C>H51Ø1<1C>4EHARRIS<1E><1C>AM11<1C>D9557{<1C>DC1ØØ{<1C>DX1ØØ{<1C>DQ725{<1C>DU657{<1C>DNØ3<1E><1C>AM12<1C>PA1<1C>PB2ØØ70915<1C>PC2ØØ8Ø914<1C>PDME<1C>PECAROLYN<1C>PFMILLER<1C>PG1234 WALNUT AVENUE<1C>PHDOVER<1C>PJDE<1C>PK21234<1C>PPPRIOR AUTHORIZATION SUPPORTING DOCUMENTATION";

            string testString = PrepareNcpdpString(priorAuthP1);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "P1");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "bbbbbbbbbb");

            //Insurance Segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "123456789");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "00006094268");
            Assert.AreEqual(claimBilling.Claim.QuantityDispensed, 30);
            Assert.AreEqual(claimBilling.Claim.FillNumber, "0");
            Assert.AreEqual(claimBilling.Claim.DaysSupply, "30");
            Assert.AreEqual(claimBilling.Claim.CompoundCode, "1"); //Not a compound
            Assert.AreEqual(claimBilling.Claim.DispenseAsWrittenProductSelectionCode, "1"); //Substution not allowed by prescriber
            Assert.AreEqual(claimBilling.Claim.DatePrescriptionWritten, new DateTime(2007, 09, 15));
            Assert.AreEqual(claimBilling.Claim.NumberOfRefillsAuthorized, "5");
            Assert.AreEqual(claimBilling.Claim.PrescriptionOriginCode, "1");
            Assert.AreEqual(claimBilling.Claim.OtherCoverageCode, "1");
            Assert.AreEqual(claimBilling.Claim.SpecialPackagingIndicator, "1");
            Assert.AreEqual(claimBilling.Claim.UnitOfMeasure, "EA");

            //Prescriber Segment
            Library.D0.Submitted.PrescriberSegment prescriber = claimBilling.Prescriber;
            Assert.AreEqual(prescriber.SegmentIdentification, "03");
            Assert.AreEqual(prescriber.PrescriberIdQualifier, "08"); //Actual and grid are in conflict.  Grid says 01 actual is 08.
            Assert.AreEqual(prescriber.PrescriberId, "00G2345"); //Actual and grid are in conflict.  Grid says 0012345111 but actual is 00G2345.
            Assert.AreEqual(prescriber.PrescriberLastName, "JONES");
            Assert.AreEqual(prescriber.PrescriberPhoneNumber, "2013639572");
            Assert.AreEqual(prescriber.PrimaryCareProviderIdQualifier, "1"); //NPI  Actual and grid are in conflict.  Grid says 01 and actual is just 1.
            Assert.AreEqual(prescriber.PrimaryCareProviderId, "1234577");  //Actual and grid are in conflict.  Grid says 1234566111
            Assert.AreEqual(prescriber.PrimaryCareProviderLastName, "HARRIS"); //NPI

            //Pricing Segment
            Library.D0.Submitted.PricingSegment pricing = claimBilling.Pricing;
            Assert.AreEqual(pricing.SegmentIdentification, "11");
            Assert.AreEqual(pricing.IngredientCostSubmitted, (decimal)55.70);
            Assert.AreEqual(pricing.DispensingFeeSubmitted, (decimal)10.00);
            Assert.AreEqual(pricing.PatientPaidAmountSubmitted, (decimal)10.00);
            Assert.AreEqual(pricing.UsualAndCustomaryCharge, (decimal)72.50);
            Assert.AreEqual(pricing.GrossAmountDue, (decimal)65.70);
            Assert.AreEqual(pricing.BasisOfCostDetermination, "03"); //Direct

            //Prior Authorization Segment
            Library.D0.Submitted.PriorAuthorizationRequestSegment priorAuth = claimBilling.PriorAuthorization;
            Assert.AreEqual(priorAuth.SegmentIdentification, "12");
            Assert.AreEqual(priorAuth.RequestType, "1");
            Assert.AreEqual(priorAuth.RequestPeriodDateBegin, "20070915");
            Assert.AreEqual(priorAuth.RequestPeriodDateEnd, "20080914");
            Assert.AreEqual(priorAuth.BasisOfRequest, "ME"); //Medical Exception
            Assert.AreEqual(priorAuth.AuthorizedRepresentativeFirstName, "CAROLYN");
            Assert.AreEqual(priorAuth.AuthorizedRepresentativeLastName, "MILLER");
            Assert.AreEqual(priorAuth.AuthorizedRepresentativeStreetAddress, "1234 WALNUT AVENUE");
            Assert.AreEqual(priorAuth.AuthorizedRepresentativeCityAddress, "DOVER");
            Assert.AreEqual(priorAuth.AuthorizedRepresentativeStateAddress, "DE");
            Assert.AreEqual(priorAuth.AuthorizedRepresentativeZipPostalCode, "21234");
            Assert.AreEqual(priorAuth.PriorAuthorizationSupportingDocumentation, "PRIOR AUTHORIZATION SUPPORTING DOCUMENTATION");
        }

        [TestMethod]
        public void TestPriorAuthReversal_34_20()
        {
            //On a Prior Auth Reversal there may be no claim submitted.  The Prior Auth Segment must be able to exist outside of the claim.

            string priorAuthReversal = "61ØØ66DØP2123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø915bbbbbbbbbb<1E><1C>AM12<1C>PA1<1C>PB2ØØ7Ø915<1C>PC2ØØ8Ø914<1C>PDME<1C>PY54321543215";

            string testString = PrepareNcpdpString(priorAuthReversal);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "P2");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "bbbbbbbbbb");

            //Prior Authorization
            //Since this is a reversal we'll look for the Prior Auth at the transmission level.
            Library.D0.Submitted.PriorAuthorizationRequestSegment priorAuth = transmission.PriorAuth;
            Assert.AreEqual(priorAuth.SegmentIdentification, "12");
            Assert.AreEqual(priorAuth.RequestType, "1");
            Assert.AreEqual(priorAuth.RequestPeriodDateBegin, "20070915");
            Assert.AreEqual(priorAuth.RequestPeriodDateEnd, "20080914"); //Actual and grid are in conflic.  Actual is 20080914 and grid says 20070914.
            Assert.AreEqual(priorAuth.BasisOfRequest, "ME");
            Assert.AreEqual(priorAuth.PriorAuthorizationNumberAssigned, "54321543215");
        }

        [TestMethod]
        public void TestPriorAuthInquiry_34_21()
        {
            //There's a claim segment sent but nothing about a claim in the grid.  It's not clear if an Prior Auth Inquiry should really apply to a claim or not.
            string priorAuthInquiry = "61ØØ66DØP3123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø915bbbbbbbbbb<1E><1C>AMØ4<1C>C2123456789<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1E><1C>AM12<1C>PA1<1C>PB2ØØ7Ø915<1C>PC2ØØ8Ø914<1C>PDME<1C>F39876545678";

            string testString = PrepareNcpdpString(priorAuthInquiry);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "P3");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "bbbbbbbbbb");

            //Insurance segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "123456789");

            //Prior Authorization
            //Since this is an inquiry we'll look for the Prior Auth at the transmission level.
            Library.D0.Submitted.PriorAuthorizationRequestSegment priorAuth;
            if (transmission.Claims == null)
                priorAuth = transmission.PriorAuth;
            else
                priorAuth = transmission.Claims[0].PriorAuthorization;

            Assert.AreEqual(priorAuth.SegmentIdentification, "12");
            Assert.AreEqual(priorAuth.RequestType, "1");
            Assert.AreEqual(priorAuth.RequestPeriodDateBegin, "20070915");
            Assert.AreEqual(priorAuth.RequestPeriodDateEnd, "20080914");
            Assert.AreEqual(priorAuth.BasisOfRequest, "ME");
            Assert.AreEqual(priorAuth.AuthorizationNumber, "9876545678");
        }

        [TestMethod]
        public void TestPriorAuthRequestOnly_34_22()
        {
            //Cardholder ID in Insurance Segment missing C2 prefix.
            string priorAuthInquiry = "61ØØ66DØP4123456789Ø1Ø14563663bbbbbbbb2ØØ7Ø915bbbbbbbbbb<1E><1C>AMØ4<1C>C2123456789<1D><1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ6Ø94268<1C>E73ØØØØ<1C>D53Ø<1C>D61<1C>D81<1C>DF5<1C>ET3ØØØØ<1C>DT1<1E><1C>AM12<1C>PA1<1C>PB2ØØ7Ø915<1C>PC2ØØ8Ø914<1C>PDME";

            string testString = PrepareNcpdpString(priorAuthInquiry);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "P4");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "01");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "bbbbbbbbbb");

            //Insurance Segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "123456789");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "00006094268");
            Assert.AreEqual(claimBilling.Claim.QuantityDispensed, 30);
            Assert.AreEqual(claimBilling.Claim.DaysSupply, "30");
            Assert.AreEqual(claimBilling.Claim.CompoundCode, "1"); //Not a compound
            Assert.AreEqual(claimBilling.Claim.DispenseAsWrittenProductSelectionCode, "1"); //Substution not allowed by prescriber
            Assert.AreEqual(claimBilling.Claim.NumberOfRefillsAuthorized, "5");
            Assert.AreEqual(claimBilling.Claim.QuantityPrescribed, (decimal)30.0);
            Assert.AreEqual(claimBilling.Claim.SpecialPackagingIndicator, "1");

            //Prior Authorization
            //Since this is an inquiry we'll look for the Prior Auth at the transmission level.
            Library.D0.Submitted.PriorAuthorizationRequestSegment priorAuth;
            if (transmission.Claims == null)
                priorAuth = transmission.PriorAuth;
            else
                priorAuth = transmission.Claims[0].PriorAuthorization;

            Assert.AreEqual(priorAuth.SegmentIdentification, "12");
            Assert.AreEqual(priorAuth.RequestType, "1");
            Assert.AreEqual(priorAuth.RequestPeriodDateBegin, "20070915");
            Assert.AreEqual(priorAuth.RequestPeriodDateEnd, "20080914");
            Assert.AreEqual(priorAuth.BasisOfRequest, "ME");
        }


        [TestMethod]
        public void TestBillingB1WithAdditionalDocumentation_34_31()
        {
            //Cardholder ID in Insurance Segment missing C2 prefix.
            //There was an extra 1 preceding the Software Vendor/Certification ID
            //Missing AM preceding "01"
            //Missing segment separator preceding claim segment
            //Field 4J had value 4O.  Changing this to 40 because according to the dictionary this should be numeric.
            //Patient First Name and Patient Last Name both missing field prefixes.  CA and CB were added to fields.
            //C8 other coverage changed from 2 to 1 to match grid.
            //Prescriber ID segment changed from D81123456 to DB1123456111 to match grid.
            string billingB1 = "61ØØ66DØB1123456789Ø1Ø44563663bbbbbbbb2ØØ7Ø91598765bbbbb<1E><1C>AMØ1<1C>C41962Ø615<1C>C51<1C>CAJOSEPH<1C>CBSMITH<1C>CM123 MAIN STREET<1C>CNMY TOWN<1C>COCO<1C>CP34567<1C>CQ2Ø14658923<1C>4X1<1C>CZ5ØZ123<1E><1C>AMØ4<1C>C2987654321A<1C>CCJOSEPH<1C>CDSMITH<1C>2ATXMEDICAID<1E><1C>AMØ7<1C>EM1<1C>D21234567<1C>E1Ø3<1C>D7ØØØØ9Ø11312<1C>E71<1C>D3Ø<1C>D53Ø<1C>D61<1C>D8Ø<1C>DE2ØØ3Ø5Ø1<1C>DF5<1C>DJ1<1C>NX1<1C>DK11<1C>C81<1C>DT1<1C>28EA<1E><1C>AMØ3<1C>EZØ1<1C>DB1123456111<1C>DRJONES<1C>PM2Ø13639572<1C>2E1<1C>DL1234566<1C>H51Ø1<1C>4EWRIGHT<1C>2JSALLY<1C>2K345 NOPLACE RD<1C>2MANYTOWN<1C>2NCO<1C>2P123456789<1E><1C>AM11<1C>D9557{<1C>DC1ØØ{<1C>DX1ØØ{<1C>H71<1C>H8Ø1<1C>H915Ø{<1C>DQ7ØØ{<1C>DU8Ø7{<1C>DNØ3<1E><1C>AM14<1C>2QØ11<1C>2V2ØØ7Ø915<1C>2U1<1C>2S4<1C>2R6<1C>2T2ØØ7Ø915<1C>2Z11<1C>4B1A<1C>4KJ292Ø<1C>4B1B<1C>4J40<1C>4B1C<1C>4J1<1C>4B4<1C>4KY<1C>4B5A<1C>4K1<1C>4B5B<1C>4K3<1C>4B8<1C>4KHEART INSTITUTE<1C>4B9<1C>4KHEARTSVILLE<1C>4b1Ø<1C>4KMO<1C>4B11<1C>4G2ØØ7Ø911<1C>4B12<1C>4KN";
            string testString = PrepareNcpdpString(billingB1);
            Library.D0.Submitted.Transmission transmission = new Library.D0.Submitted.Transmission(testString);

            Assert.IsNotNull(transmission, "It's not alive");

            //Transaction Header
            Assert.AreEqual(transmission.TransactionHeader.BinNumber, "610066");
            Assert.AreEqual(transmission.TransactionHeader.VersionNumber, "D0");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCode, "B1");
            Assert.AreEqual(transmission.TransactionHeader.ProcessorControlNumber, "1234567890");
            Assert.AreEqual(transmission.TransactionHeader.TransactionCount, 1);
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderIdQualifier, "04");
            Assert.AreEqual(transmission.TransactionHeader.ServiceProviderId, "4563663bbbbbbbb"); //Actual and grid are in conflict.  Grid says 4563663111bbbbb
            Assert.AreEqual(transmission.TransactionHeader.DateOfService, new DateTime(2007, 9, 15));
            Assert.AreEqual(transmission.TransactionHeader.SoftwareVendorId, "98765bbbbb");

            //Patient Segment
            Assert.AreEqual(transmission.Patient.SegmentIdentification, "01");
            Assert.AreEqual(transmission.Patient.DateOfBirth, new DateTime(1962, 6, 15));
            Assert.AreEqual(transmission.Patient.PatientGenderCode, "1");
            Assert.AreEqual(transmission.Patient.PatientFirstName, "JOSEPH");
            Assert.AreEqual(transmission.Patient.PatientLastName, "SMITH");
            Assert.AreEqual(transmission.Patient.PatientStreetAddress, "123 MAIN STREET");
            Assert.AreEqual(transmission.Patient.PatientCityAddress, "MY TOWN");
            Assert.AreEqual(transmission.Patient.PatientState, "CO");
            Assert.AreEqual(transmission.Patient.PatientZip, "34567");
            Assert.AreEqual(transmission.Patient.PatientPhoneNumber, "2014658923");
            Assert.AreEqual(transmission.Patient.PatientResidence, "1"); //Home
            Assert.AreEqual(transmission.Patient.EmployerId, "50Z123"); 

            //Insurance Segment
            Assert.AreEqual(transmission.Insurance.SegmentIdentification, "04");
            Assert.AreEqual(transmission.Insurance.CardholderId, "987654321A"); //Medicare Cardholder ID
            Assert.AreEqual(transmission.Insurance.CardholderFirstName, "JOSEPH");
            Assert.AreEqual(transmission.Insurance.CardholderLastName, "SMITH");
            Assert.AreEqual(transmission.Insurance.MedigapId, "TXMEDICAID");

            //Claim Segment
            Library.D0.Submitted.ClaimBilling claimBilling = transmission.Claims[0];
            Assert.AreEqual(claimBilling.Claim.SegmentIdentification, "07");
            Assert.AreEqual(claimBilling.Claim.PrescriptionReferenceNumberQualifier, "1");
            Assert.AreEqual(claimBilling.Claim.PrescriptionServiceReferenceNumber, "1234567");
            Assert.AreEqual(claimBilling.Claim.ProductServiceIdQualifier, "03");
            Assert.AreEqual(claimBilling.Claim.ProductServiceId, "00009011312");
            Assert.AreEqual(claimBilling.Claim.QuantityDispensed, (decimal)0.001);
            Assert.AreEqual(claimBilling.Claim.FillNumber, "0");
            Assert.AreEqual(claimBilling.Claim.DaysSupply, "30");
            Assert.AreEqual(claimBilling.Claim.CompoundCode, "1"); //Not a compound
            Assert.AreEqual(claimBilling.Claim.DispenseAsWrittenProductSelectionCode, "0"); //No product seletion indicated.
            Assert.AreEqual(claimBilling.Claim.NumberOfRefillsAuthorized, "5");
            Assert.AreEqual(claimBilling.Claim.PrescriptionOriginCode, "1"); 
            Assert.AreEqual(claimBilling.Claim.SubmissionClarificationCodeCount, 1); 
            Assert.AreEqual(claimBilling.Claim.SubmissionClarificationCode[0], "11");
            Assert.AreEqual(claimBilling.Claim.OtherCoverageCode, "1");
            Assert.AreEqual(claimBilling.Claim.SpecialPackagingIndicator, "1");
            Assert.AreEqual(claimBilling.Claim.UnitOfMeasure, "EA");

            //Prescriber Segment 
            Library.D0.Submitted.PrescriberSegment prescriber = claimBilling.Prescriber;
            Assert.AreEqual(prescriber.SegmentIdentification, "03");
            Assert.AreEqual(prescriber.PrescriberIdQualifier, "01");
            Assert.AreEqual(prescriber.PrescriberId, "1123456111");
            Assert.AreEqual(prescriber.PrescriberLastName, "JONES");
            Assert.AreEqual(prescriber.PrescriberPhoneNumber, "2013639572");
            Assert.AreEqual(prescriber.PrimaryCareProviderIdQualifier, "1"); //NPI
            Assert.AreEqual(prescriber.PrimaryCareProviderId, "1234566");  //Changed from 1234566111 to 1234566 to pass test
            Assert.AreEqual(prescriber.PrimaryCareProviderLastName, "WRIGHT"); //NPI
            Assert.AreEqual(prescriber.PrescriberFirstName, "SALLY");
            Assert.AreEqual(prescriber.PrescriberStreetAddress, "345 NOPLACE RD");
            Assert.AreEqual(prescriber.PrescriberCityAddress, "ANYTOWN"); 
            Assert.AreEqual(prescriber.PrescriberStateProvinceAddress, "CO");
            Assert.AreEqual(prescriber.PrescriberZipPostalCode, "123456789");

            //Pricing Segment
            Library.D0.Submitted.PricingSegment pricing = claimBilling.Pricing;
            Assert.AreEqual(pricing.SegmentIdentification, "11");
            Assert.AreEqual(pricing.IngredientCostSubmitted, (decimal)55.70);
            Assert.AreEqual(pricing.DispensingFeeSubmitted, (decimal)10);
            Assert.AreEqual(pricing.PatientPaidAmountSubmitted, (decimal)10);
            Assert.AreEqual(pricing.OtherAmountClaimedSubmittedCount, 1);

            Library.D0.Submitted.PricingSegment.OtherAmountClaimedSubmittedContainer otherAmountSubmitted = pricing.OtherAmountClaimedSubmittedList[0];
            Assert.AreEqual(otherAmountSubmitted.OtherAmountClaimedSubmittedQualifier, "01");
            Assert.AreEqual(otherAmountSubmitted.OtherAmountClaimedSubmitted, (decimal)15);

            Assert.AreEqual(pricing.UsualAndCustomaryCharge, (decimal)70);
            Assert.AreEqual(pricing.GrossAmountDue, (decimal)80.7);
            Assert.AreEqual(pricing.BasisOfCostDetermination, "03"); //Direct

            //Additional Documentation Segment
            Library.D0.Submitted.AdditionalDocumentationSegment additionalDocumentation = claimBilling.AdditionalDocumentation;
            Assert.AreEqual(additionalDocumentation.SegmentIdentification, "14");
            Assert.AreEqual(additionalDocumentation.AdditionalDocumentationTypeId, "011");
            Assert.AreEqual(additionalDocumentation.RequestPeriodBeginDate, new DateTime(2007, 09, 15));
            Assert.AreEqual(additionalDocumentation.RequestStatus, "1");
            Assert.AreEqual(additionalDocumentation.LengthOfNeedQualifier, "4");
            Assert.AreEqual(additionalDocumentation.LengthOfNeed, 6);
            Assert.AreEqual(additionalDocumentation.PrescriberSupplierDateSigned, new DateTime(2007, 09, 15));
            Assert.AreEqual(additionalDocumentation.QuestionCount, 11);
            List<Library.D0.Submitted.AdditionalDocumentationSegment.Question> questions = additionalDocumentation.Questions;
            Assert.AreEqual(questions.Count, additionalDocumentation.QuestionCount);
            Assert.AreEqual(questions[2].QuestionNumberLetter, "1C"); //What drugs are prescribed (frequence per day).            
            Assert.AreEqual(questions[2].QuestionNumericResponse, 1); //What drugs are prescribed (frequence per day).            
            Assert.AreEqual(questions[10].QuestionNumberLetter, "12");
            Assert.AreEqual(questions[10].QuestionAlphanumericResponse, "N"); //No
        }
    }
}
