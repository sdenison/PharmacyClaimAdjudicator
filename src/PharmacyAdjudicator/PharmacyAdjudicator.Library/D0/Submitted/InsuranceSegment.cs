using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    /// <summary>
    /// Insurance Segment data container
    /// </summary>
    public class InsuranceSegment
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
        /// Cardholder ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 302-C2</para>
        /// <para>
        /// Insurance ID assigned to the cardholder or identification 
        /// number used by the plan.
        /// </para>
        /// </remarks>
        [Required]
        [MaxLength(128)]
        [NcpdpFieldAttribute("302-C2")]
        public string CardholderId { get; set; }

        /// <summary>
        /// Cardholder First Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 312-CC</para>
        /// <para>Individual first name.</para>
        /// </remarks>
        [MaxLength(35)]
        [NcpdpFieldAttribute("312-CC")]
        public string CardholderFirstName { get; set; }

        /// <summary>
        /// Cardholder Last Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 313-CD</para>
        /// <para>Individual last name.</para>
        /// </remarks>
        [MaxLength(35)]
        [NcpdpFieldAttribute("313-CD")]
        public string CardholderLastName { get; set; }

        /// <summary>
        /// Home Plan
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 314-CE</para>
        /// <para>
        /// Code identifying the Blue Cross or Blue Shield plan ID which
        /// indicates where the member's coverate has been designated.
        /// Usually where the memeber lives or purchased their coverage.
        /// </para>
        /// </remarks>
        [MaxLength(3)]
        [NcpdpFieldAttribute("314-CE")]
        public string HomePlan { get; set; }

        /// <summary>
        /// Plan ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 524-FO</para>
        /// <para>
        /// Assigned by the processor to identify a set of parameters, benefit,
        /// or coverate criteria used to adjudicate a claim.
        /// </para>
        /// </remarks>
        [MaxLength(8)]
        [NcpdpFieldAttribute("524-FO")]
        public string PlanId { get; set; }

        /// <summary>
        /// Eligibility Clarification Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 309-C9</para>
        /// <para>
        /// Code indicating that the pharmacy is clarifying eligibility for a 
        /// patient.
        /// </para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("309-C9")]
        public string EligibilityClarificationCode { get; set; }

        /// <summary>
        /// Group ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 301-C1</para>
        /// <para>ID assigned to the cardholder group or employer group.</para>
        /// </remarks>
        [MaxLength(15)]
        [NcpdpFieldAttribute("301-C1")]
        public string GroupId { get; set; }

        /// <summary>
        /// Person Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 303-C3</para>
        /// <para>Code assigned to a specific person within a family.</para>
        /// </remarks>
        [MaxLength(3)]
        [NcpdpFieldAttribute("303-C3")]
        public string PersonCode { get; private set; }

        /// <summary>
        /// Pateint Relationship code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 306-C6</para>
        /// <para>Code indicating relationship of patient to cardholder.</para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("306-C6")]
        public string PatientRelationshipCode { get; set; }

        /// <summary>
        /// Other Payer Bin Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 990-MG</para>
        /// <para>
        /// The secondary, tertiary, etc. card issuer or bank ID number used 
        /// for network routing.
        /// </para>
        /// </remarks>
        [MaxLength(6)]
        [NcpdpFieldAttribute("990-MG")]
        public string OtherPayerBinNumber { get; set; }

        /// <summary>
        /// Other Payer Processor Control Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 991-MH</para>
        /// <para>
        /// A number that uniquely identifies the secondary, tertiary, etc. 
        /// payer to the processor.
        /// </para>
        /// </remarks>
        public string OtherPayerProcessorControlNumber { get; set; }

        /// <summary>
        /// Other Payer Cardholder ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 356-NU</para>
        /// <para>
        /// Cardholder ID for this member that is associated with the payer 
        /// noted.
        /// </para>
        /// </remarks>
        [MaxLength(20)]
        [NcpdpFieldAttribute("356-NU")]
        public string OtherPayerCardholderId { get; set; }

        /// <summary>
        /// Other Payer Group ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 992-MJ</para>
        /// <para>
        /// ID assigned to the cardholder groiup or employer group by the 
        /// secondary, tetiary, etc. payer.
        /// </para>
        /// </remarks>
        [MaxLength(15)]
        [NcpdpFieldAttribute("992-MJ")]
        public string OtherPayerGroupId { get; set; }

        /// <summary>
        /// Medigap ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 359-2A</para>
        /// <para>Patient's ID assigned by the Medigap Insurer.</para>
        /// </remarks>
        [MaxLength(20)]
        [NcpdpFieldAttribute("359-2A")]
        public string MedigapId { get; set; }

        /// <summary>
        /// Medicaid Indicator
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 360-2B</para>
        /// <para>
        /// Two character State Postal Code indicating the state where 
        /// Medicaid coverage exists.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("360-2B")]
        public string MedicaidIndicator { get; set; }

        /// <summary>
        /// Provider Accept Assignment Indicator
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 361-2D</para>
        /// <para>Code indicating whether the provider accepts assignement.</para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("361-2D")]
        public string ProviderAcceptAssignmentIndicator { get; set; }

        /// <summary>
        /// CMS Part D Defined Qualifeid Facility
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 997-G2</para>
        /// <para>
        /// Indicates that the patient resides in a facility that qualifies 
        /// for the CMS Part D benefit.
        /// </para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("997-G2")]
        public string CmsPartDDefinedQualifiedFacility { get; set; }

        /// <summary>
        /// Medicaid ID Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 115-N5</para>
        /// <para>A unique member identification number assigned by the Medicaid Agency</para>
        /// </remarks>
        [MaxLength(20)]
        [NcpdpFieldAttribute("115-N5")]
        public string MedicaidIdNumber { get; set; }

        /// <summary>
        /// Medicaid Agency Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 116-N6</para>
        /// <para>
        /// Number assigned by processor to identify the individual Medicaid 
        /// Agency or representative.
        /// </para>
        /// </remarks>
        [NcpdpField("116-N6")] 
        public string MedicaidAgencyNumber { get; set; } 

        /// <summary>
        /// Parses incoming NCPDP string into properties.
        /// </summary>
        /// <param name="s">NCPDP string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns>Insurance Segment</returns>
        public InsuranceSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new InsuranceSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public InsuranceSegment(string[] fields)
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
                    case "C2":
                        this.CardholderId = ncpdpFieldValue;
                        break;
                    case "CC":
                        this.CardholderFirstName = ncpdpFieldValue;
                        break;
                    case "CD":
                        this.CardholderLastName = ncpdpFieldValue;
                        break;
                    case "CE":
                        this.HomePlan = ncpdpFieldValue;
                        break;
                    case "FO":
                        this.HomePlan = ncpdpFieldValue;
                        break;
                    case "C9":
                        this.EligibilityClarificationCode = ncpdpFieldValue;
                        break;
                    case "C1":
                        this.GroupId = ncpdpFieldValue;
                        break;
                    case "C3":
                        this.PersonCode = ncpdpFieldValue;
                        break;
                    case "C6":
                        this.PatientRelationshipCode = ncpdpFieldValue;
                        break;
                    case "MG":
                        this.OtherPayerBinNumber = ncpdpFieldValue;
                        break;
                    case "MH":
                        this.OtherPayerProcessorControlNumber = ncpdpFieldValue;
                        break;
                    case "NU":
                        this.OtherPayerCardholderId = ncpdpFieldValue;
                        break;
                    case "MJ":
                        this.OtherPayerGroupId = ncpdpFieldValue;
                        break;
                    case "2A":
                        this.MedigapId = ncpdpFieldValue;
                        break;
                    case "2B":
                        this.MedicaidIndicator = ncpdpFieldValue;
                        break;
                    case "2D":
                        this.ProviderAcceptAssignmentIndicator = ncpdpFieldValue;
                        break;
                    case "G2":
                        this.CmsPartDDefinedQualifiedFacility = ncpdpFieldValue;
                        break;
                    case "N5":
                        this.MedicaidIdNumber = ncpdpFieldValue;
                        break;
                    case "N6":
                        this.MedicaidAgencyNumber = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
