using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class ClaimSegment
    {
        /// <summary>
        /// Segment Identification
        /// </summary>
        /// <remarks>
        /// <para>>NCPDP 111-AM</para>
        /// <para>Identifies the segment in the request and/or response</para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("111-AM")]
        public string SegmentIdentification { get; set; }

        /// <summary>
        /// Prescription Reference Number Qualifier
        /// <remarks>
        /// <para>NCPDP 455-EM</para>
        /// <para>Indicates the type of billing submitted.</para>
        /// </remarks> 
        /// </summary>
        [Required]
        [MaxLength(1)]
        [NcpdpFieldAttribute("455-EM")]
        public string PrescriptionReferenceNumberQualifier { get; set; }

        /// <summary>
        /// Prescription Service Reference Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 402-D2</para>
        /// <para>
        /// Reference number assigned by the provider for the dispensed 
        /// drug/product and/or service provided.
        /// </para>
        /// </remarks>
        [Required]
        [MaxLength(12)]
        [NcpdpFieldAttribute("402-D2")]
        public string PrescriptionServiceReferenceNumber { get; set; }

        /// <summary>
        /// Product/Service ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 436-E1</para>
        /// <para>
        /// Code qualifying the value in 'Product/Service ID' (407-D7).
        /// </para>
        /// </remarks>
        [Required]
        [MaxLength(1)]
        [NcpdpFieldAttribute("436-E1")]
        public string ProductServiceIdQualifier { get; set; }

        /// <summary>
        /// Product Service Id
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 407-D7</para>
        /// <para>ID of the product dispensed or service provided.</para>
        /// </remarks>
        [Required]
        [MaxLength(19)]
        [NcpdpFieldAttribute("407-D7")]
        public string ProductServiceId { get; set; }

        /// <summary>
        /// Associated Prescription/Service Reference Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 456-EN</para>
        /// <para>
        /// Related 'Prescription/Service Reference Number' (402-D2) to which
        /// the service is associated.
        /// </para>
        /// </remarks>
        [Required]
        [MaxLength(12)]
        [NcpdpFieldAttribute("456-EN")]
        public string AssociatedPrescriptionServiceReferenceNumber { get; set; }

        /// <summary>
        /// Associated Prescription/Service Date
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 457-EP</para>
        /// <para>
        /// Date of the Associated Prescription/Service Reference Number' (456-EN).
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("457-EP")]
        public DateTime? AssociatedPrescriptionServiceDate { get; set; }

        /// <summary>
        /// Procedure Modified Code Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 458-SE</para>
        /// <para>count of the 'Procedure Modifier Code' (459-ER) occurrences.</para>
        /// </remarks>
        [NcpdpFieldAttribute("458-SE")]
        public int ProcedureModifierCodeCount { get; set; }

        /// <summary>
        /// Procedure Modifier Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 459-ER</para>
        /// <para>
        /// Identifies special circumstances related to the performance 
        /// of the service.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("459-ER")]
        public List<string> ProcedureModifierCode { get; set; }

        /// <summary>
        /// Quantity Dispensed
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 442-E7</para> 
        /// <para>Quantity dispensed expressed in metric decimal units.</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("442-E7")]
        public decimal QuantityDispensed { get; set; }

        /// <summary>
        /// Fill Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 403-D3</para>
        /// <para>
        /// The code indicating whether the prescription is an original or a 
        /// refill.
        /// </para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("403-D3")]
        public string FillNumber { get; set; }

        /// <summary>
        /// Days Supply
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 405-D5</para> 
        /// <para>Estimated number of days the prescription will last.</para>
        /// </remarks>
        [Required]
        [MaxLength(3)]
        [NcpdpFieldAttribute("405-D5")]
        public string DaysSupply { get; set; }

        /// <summary>
        /// Compound Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 406-D6</para>
        /// <para>
        /// Code indicating whether or not the prescription is a compound.
        /// </para>
        /// </remarks>
        [Required]
        [MaxLength(1)]
        [NcpdpFieldAttribute("406-D6")]
        public string CompoundCode { get; set; }

        /// <summary>
        /// Dispense As Written Production Selection code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 408-D8</para>
        /// <para>
        /// Code indicating whether or not the prescriber's instructions
        /// regarding generic substution were followed.
        /// </para>
        /// </remarks>
        [Required]
        [MaxLength(1)]
        [NcpdpFieldAttribute("408-D8")]
        public string DispenseAsWrittenProductSelectionCode { get; set; }

        /// <summary>
        /// Date Prescription Written
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 414-DE</para>
        /// <para>Date prescription was written.</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("414-DE")]
        public DateTime DatePrescriptionWritten { get; set; }

        /// <summary>
        /// Number Of Refills Authorized
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 415-DF</para>
        /// <para>Number of refills authorized by the prescriber.</para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("415-DF")]
        public string NumberOfRefillsAuthorized { get; set; }

        /// <summary>
        /// Prescription Origin Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 419-DJ</para>
        /// <para>Code indicating the origin of the prescription.</para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("419-DJ")]
        public string PrescriptionOriginCode { get; set; }

        /// <summary>
        /// Submission Clarification Code Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 354-NX</para>
        /// <para>
        /// Count of the 'Submission Clarification Code' (420-DK) occurrences.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("354-NX")]
        public int SubmissionClarificationCodeCount { get; set; }

        /// <summary>
        /// Submission Clarification Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 420-DK</para>
        /// <para>
        /// Code indicating that the pharmacist is clarifying the submission.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("420-DK")]
        public List<string> SubmissionClarificationCode { get; set; }

        /// <summary>
        /// Quantity Prescribed
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 460-ET</para>
        /// <para>Not used or defined.</para>
        /// </remarks>
        [NcpdpFieldAttribute("460-ET")]
        public decimal? QuantityPrescribed { get; set; }

        /// <summary>
        /// Other Coverage Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 308-C8</para>
        /// <para>
        /// Code indicating whether or not the patient has other insurance coverage.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("308-C8")]
        public string OtherCoverageCode { get; set; }

        /// <summary>
        /// Special Packaging Indicator
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 429-DT</para>
        /// <para>Code indicating the type of dispensing dose.</para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("439-DT")]
        public string SpecialPackagingIndicator { get; set; }

        /// <summary>
        /// Originally Prescribed Product/Service ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 453-EJ</para>
        /// <para>
        /// Code qualifying the value in 'Originally Prescribed Product/Service 
        /// Code' (Field 445-EA).
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("453-EJ")]
        public string OriginallyPrescribedProductServiceIdQualifier { get; set; }

        /// <summary>
        /// Originally Prescribed Product/Service Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 445-EA</para>
        /// <para>Code of the initially prescribed product or service.</para>
        /// </remarks>
        [MaxLength(19)]
        [NcpdpFieldAttribute("445-EA")]
        public string OriginallyPrescribedProductServiceCode { get; set; }

        /// <summary>
        /// Originally Prescribed Quantity
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 446-EB</para>
        /// <para>
        /// Product initially prescribed amount expressed in metric decimal 
        /// units.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("446-EB")]
        public decimal OriginallyPrescribedQuantity { get; set; }

        /// <summary>
        /// Alternate ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 330-CW</para>
        /// <para>
        /// Person identifier to be used for controlled product reporting.
        /// Identifier may be that of the patient or the person picking up
        /// the prescription as required by the governing body.
        /// </para>
        /// </remarks>
        [MaxLength(20)]
        [NcpdpFieldAttribute("330-CW")]
        public string AlternateId { get; set; }

        /// <summary>
        /// Scheduled Prescription ID Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 454-EK</para>
        /// <para>The serial number of the prescription blank/form.</para>
        /// </remarks>
        [MaxLength(12)]
        [NcpdpFieldAttribute("454-EK")]
        public string ScheduledPrescriptionIdNumber { get; set; }

        /// <summary>
        /// Unit Of Measure
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 600-28</para>
        /// <para>NCPDP standard product billing codes.</para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("600-28")]
        public string UnitOfMeasure { get; set; }

        /// <summary>
        /// Level Of Service
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 418-DI</para>
        /// <para>
        /// Coding indicating the type of service the provider rendered.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("418-DI")]
        public string LevelOfService { get; set; }

        /// <summary>
        /// Prior Authorization Type Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 461-EU</para>
        /// <para>
        /// Code clarifying the 'Prior Authorization Number Submitted' (462-EV)
        /// or benefit/plan exemption.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("461-EU")]
        public string PriorAuthorizationTypeCode { get; set; }

        /// <summary>
        /// Prior Authroization Number Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 462-EV</para>
        /// <para>
        /// Number submitted by the provider to identify the prior authorization.
        /// </para>
        /// </remarks>
        [MaxLength(11)]
        [NcpdpFieldAttribute("462-EV")]
        public string PriorAuthorizationNumberSubmitted { get; set; }

        /// <summary>
        /// Intermediary Authorization Type ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 463-EW</para>
        /// <para>
        /// Value indicating that authorization occurred for intermediary 
        /// processing.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("463-EW")]
        public string IntermediaryAuthorizationTypeId { get; set; }

        /// <summary>
        /// Intermediary Authorization ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 464-EX</para>
        /// <para>
        /// Value indicating intermediary authorization occurred for 
        /// intemediary processing.
        /// </para>
        /// </remarks>
        [MaxLength(11)]
        [NcpdpFieldAttribute("464-EX")]
        public string IntermediaryAuthorizationId { get; set; }

        /// <summary>
        /// Dispensing Status
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 343-HD</para>
        /// <para>
        /// Code indicating the quantity dispensed is a partial fill or the 
        /// completion of a partial fill.  Used only in situations where 
        /// inventory shortages do not allow the full quantity to be dispensed.
        /// </para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("343-HD")]
        public string DispensingStatus { get; set; }

        /// <summary>
        /// Quantity Intended To Be Dispensed
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 344-HF</para> 
        /// <para>
        /// Metric decimal quantity of medication that would be dispensed on 
        /// original filling if inventory were available.  Used in association 
        /// with a 'P' or 'C' in 'Dispensing Status' (343-HD).
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("344-HF")]
        public decimal? QuantityIntendedToBeDispensed { get; set; }

        /// <summary>
        /// Days Supply Intended To Be Dispensed
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 345-HG</para>
        /// <para>
        /// Days supply for metric decimal quantity of medication that would 
        /// be dispensed on original dispensing if inventory were available.  
        /// Used in association with a 'P' or 'C' in 'Dispensing Status' 
        /// (343-HD).</para>
        /// </remarks>
        [NcpdpFieldAttribute("345-HG")]
        public int? DaysSupplyIntendedToBeDispensed { get; set; }

        /// <summary>
        /// Delay Reason Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 357-NV</para>
        /// <para>
        /// Code to specify the reason that submission of the transactions has 
        /// been delayed.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("357-NV")]
        public string DelayReasonCode { get; set; }

        /// <summary>
        /// Transaction Reference Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 880-K5</para>
        /// <para>
        /// A reference number assigned by the provider to each of the data 
        /// records in the batch or real-time transactions. The purpose of this 
        /// number is to facilitate the process of matching the transaction 
        /// response to the transaction. The transaction reference number 
        /// assigned should be returned in the response.
        /// Not used.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("880-K5")]
        public string TransactionReferenceNumber { get; set; }

        /// <summary>
        /// Patient Assignment Indicator (Direct Member Reimbursement Indicator
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 391-MT</para>
        /// <para>
        /// Code to indicate a pateint's choice on assignment of benefits.
        /// </para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("391-MT")]
        public string PatientAssignmentIndicator { get; set; }

        /// <summary>
        /// Route of Administration
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 995-E2</para>
        /// <para>
        /// This is an override to the “default” route referenced for the 
        /// product. For a multi-ingredient compound, it is the route of the 
        /// complete compound mixture.
        /// </para>
        /// </remarks>
        [MaxLength(11)]
        [NcpdpFieldAttribute("995-E2")]
        public string RouteOfAdministration { get; set; }

        /// <summary>
        /// Compound Type
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 996-G1</para>
        /// <para>
        /// Clarifies the type of compound.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("996-G1")]
        public string CompoundType { get; set; }

        /// <summary>
        /// Medicaid Subrogation Internal Control Number/Transaction Control Number (ICN/TCN)
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 114-N4</para>
        /// <para>Claim number assigned by the Medicaid Agancy. Not used.</para>
        /// </remarks>
        [MaxLength(20)]
        [NcpdpFieldAttribute("114-N4")]
        public string MedicaidSubrogationInternalControlNumber { get; set; }

        /// <summary>
        /// Pharmacy Service Type
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 147-U7</para>
        /// <para></para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("147-U7")]
        public string PharmacyServiceType { get; set; }

        /// <summary>
        /// Public constructor for use by storage ;)
        /// </summary>
        public ClaimSegment()
        {
        }

        /// <summary>
        /// Parses incoming NCPDP string into properties
        /// </summary>
        /// <param name="s">NCPPD string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static ClaimSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new ClaimSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public ClaimSegment(string[] fields)
        {
            foreach (string field in fields)
            {
                //Skips blank fields
                if (string.IsNullOrEmpty(field))
                    continue;
                string ncpdpField = field.Substring(0, 2).ToUpper();
                string ncpdpFieldValue = field.Substring(2).Trim();
                switch (ncpdpField)
                {
                    case "AM":
                        if (string.IsNullOrEmpty(this.SegmentIdentification) == false)
                            throw new InvalidIncomingLineException("Segment Identification already set.  Line is probably missing a segment separator.  " + fields.ToString());
                        this.SegmentIdentification = ncpdpFieldValue;
                        break;
                    case "EM":
                        this.PrescriptionReferenceNumberQualifier = ncpdpFieldValue;
                        break;
                    case "D2":
                        this.PrescriptionServiceReferenceNumber = ncpdpFieldValue;
                        break;
                    case "E1":
                        this.ProductServiceIdQualifier = ncpdpFieldValue;
                        break;
                    case "D7":
                        this.ProductServiceId = ncpdpFieldValue;
                        break;
                    case "EN":
                        this.AssociatedPrescriptionServiceReferenceNumber = ncpdpFieldValue;
                        break;
                    case "EP":
                        this.AssociatedPrescriptionServiceDate = DateTime.ParseExact(ncpdpFieldValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "SE":
                        this.ProcedureModifierCodeCount = int.Parse(ncpdpFieldValue);
                        break;
                    case "ER":
                        if (this.ProcedureModifierCode == null)
                            this.ProcedureModifierCode = new List<string>();
                        this.ProcedureModifierCode.Add(ncpdpFieldValue);
                        break;
                    case "E7":
                        this.QuantityDispensed = decimal.Parse(ncpdpFieldValue) / 1000;
                        break;
                    case "D3":
                        this.FillNumber = ncpdpFieldValue;
                        break;
                    case "D5":
                        this.DaysSupply = ncpdpFieldValue;
                        break;
                    case "D6":
                        this.CompoundCode = ncpdpFieldValue;
                        break;
                    case "D8":
                        this.DispenseAsWrittenProductSelectionCode = ncpdpFieldValue;
                        break;
                    case "DE":
                        this.DatePrescriptionWritten = DateTime.ParseExact(ncpdpFieldValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "DF":
                        this.NumberOfRefillsAuthorized = ncpdpFieldValue;
                        break;
                    case "DJ":
                        this.PrescriptionOriginCode = ncpdpFieldValue;
                        break;
                    case "NX":
                        this.SubmissionClarificationCodeCount = int.Parse(ncpdpFieldValue);
                        break;
                    case "DK":
                        if (this.SubmissionClarificationCode == null)
                            this.SubmissionClarificationCode = new List<string>();
                        this.SubmissionClarificationCode.Add(ncpdpFieldValue);
                        break;
                    case "ET":
                        this.QuantityPrescribed = decimal.Parse(ncpdpFieldValue) / 1000;
                        break;
                    case "C8":
                        this.OtherCoverageCode = ncpdpFieldValue;
                        break;
                    case "DT":
                        this.SpecialPackagingIndicator = ncpdpFieldValue;
                        break;
                    case "EJ":
                        this.OriginallyPrescribedProductServiceIdQualifier = ncpdpFieldValue;
                        break;
                    case "EA":
                        this.OriginallyPrescribedProductServiceCode = ncpdpFieldValue;
                        break;
                    case "EB":
                        this.OriginallyPrescribedQuantity = decimal.Parse(ncpdpFieldValue) / 1000;
                        break;
                    case "CW":
                        this.ScheduledPrescriptionIdNumber = ncpdpFieldValue;
                        break;
                    case "EK":
                        this.ScheduledPrescriptionIdNumber = ncpdpFieldValue;
                        break;
                    case "28":
                        this.UnitOfMeasure = ncpdpFieldValue;
                        break;
                    case "DI":
                        this.LevelOfService = ncpdpFieldValue;
                        break;
                    case "EU":
                        this.PriorAuthorizationTypeCode = ncpdpFieldValue;
                        break;
                    case "EV":
                        this.PriorAuthorizationNumberSubmitted = ncpdpFieldValue;
                        break;
                    case "EW":
                        this.IntermediaryAuthorizationTypeId = ncpdpFieldValue;
                        break;
                    case "EX":
                        this.IntermediaryAuthorizationId = ncpdpFieldValue;
                        break;
                    case "HD":
                        this.DispensingStatus = ncpdpFieldValue;
                        break;
                    case "HF":
                        this.QuantityIntendedToBeDispensed = decimal.Parse(ncpdpFieldValue) / 1000;
                        break;
                    case "HG":
                        this.DaysSupplyIntendedToBeDispensed = int.Parse(ncpdpFieldValue);
                        break;
                    case "NV":
                        this.DelayReasonCode = ncpdpFieldValue;
                        break;
                    case "K5":
                        this.TransactionReferenceNumber = ncpdpFieldValue;
                        break;
                    case "MT":
                        this.PatientAssignmentIndicator = ncpdpFieldValue;
                        break;
                    case "E2":
                        this.RouteOfAdministration = ncpdpFieldValue;
                        break;
                    case "G1":
                        this.CompoundType = ncpdpFieldValue;
                        break;
                    case "N4":
                        this.MedicaidSubrogationInternalControlNumber = ncpdpFieldValue;
                        break;
                    case "U7":
                        this.PharmacyServiceType = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }

            //Checks that the number of Procedure Modifier Segments match Procedure Modifier Count sent.
            if (this.ProcedureModifierCodeCount > 0)
                if (this.ProcedureModifierCodeCount != this.ProcedureModifierCode.Count)
                    throw new Exception("Procedure Modifier Code Count does not equal number of Procedure Modifier Segments.");
            //Checks that the number of Submission Clarification Codes matches the Submission Clarification Code Count.
            if (this.SubmissionClarificationCodeCount > 0)
                if (this.SubmissionClarificationCodeCount != this.SubmissionClarificationCode.Count)
                    throw new Exception("Submission Clarification Code Count does not equal the number of Submission Clarification Codes.");
        }
    }
}
