using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Response
{
    public class CoordinationOfBenefitsSegment
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
        /// Other Payer ID Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 355-NT</para>
        /// <para>Count of other payers with payment responsibility.</para>
        /// </remarks>
        [NcpdpField("355-NT")]
        public int? OtherPayerIdCount { get; set; }

        /// <summary>
        /// List of Other Payer Containers
        /// </summary>
        public List<OtherPayerContainer> OtherPayers { get; set; }

        public CoordinationOfBenefitsSegment()
        {
            this.SegmentIdentification = "28";
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();

            //Append properties to returnValue.
            if (this.OtherPayerIdCount != null && this.OtherPayerIdCount > 0)
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerIdCount, this.OtherPayerIdCount.ToString()));
            foreach (var otherPayer in this.OtherPayers)
                returnValue.Append(otherPayer.ToNcpdpString());

            //Adds segment separator and identifier to beginning if the segment has data.
            if (returnValue.Length > 0)
            {
                returnValue.Insert(0, Utils.NcpdpString.ToNcpdpFieldString(() => this.SegmentIdentification, this.SegmentIdentification));
                returnValue.Insert(0, Utils.NcpdpString.SegmentSeparator);
            }
            return returnValue.ToString();
        }

        public class OtherPayerContainer
        {
            /// <summary>
            /// Other Payer Coverage Type
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 338-5C</para>
            /// <para>
            /// Code identifying the type of ‘Other Payer ID’ (34Ø-7C).
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("338-5C")]
            public string OtherPayerCoverageType { get; set; }

            /// <summary>
            /// Other Payer ID Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 339-6C</para>
            /// <para>Code qualifying the 'Other Payer ID' (340-7C).</para>
            /// </remarks>
            [NcpdpFieldAttribute("339-6C")]
            public string OtherPayerIdQualifier { get; set; }

            /// <summary>
            /// Other Payer ID
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 340-7C</para>
            /// <para>ID assigned to the payer.</para>
            /// </remarks>
            [NcpdpFieldAttribute("340-7C")]
            public string OtherPayerId { get; set; }

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
            [NcpdpField("991-MH")]
            public string OtherPayerProcessorControlNumber { get; set; }

            /// <summary>
            /// Other Payer Cardholder ID
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 356-NU</para>
            /// <para>
            /// Cardholder ID for this member that is associated with the Payer 
            /// noted.
            /// </para>
            /// </remarks>
            [NcpdpField("356-NU")]
            public string OtherPayerCardholderId { get; set; }

            /// <summary>
            /// Other Payer Group ID
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 992-MJ</para>
            /// <para>
            /// ID assigned to the cardholder or group or employer group by the 
            /// secondary, tertiary, etc. payer.
            /// </para>
            /// </remarks>
            [NcpdpField("992-MJ")]
            public string OtherPayerGroupId { get; set; }

            /// <summary>
            /// Other Payer Person Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 142-UV</para>
            /// <para>
            /// Code assigned by the other payer to a specific person within a 
            /// family.
            /// </para>
            /// </remarks>
            [NcpdpField("142-UV")]
            public string OtherPayerPersonCode { get; set; }

            /// <summary>
            /// Other Payer Help Desk Phone Number
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 127-UB</para>
            /// <para>Phone number of the other payer's help desk.</para>
            /// </remarks>
            [NcpdpField("127-UB")]
            public string OtherPayerHelpDeskPhoneNumber { get; set; }

            /// <summary>
            /// Other Payer Patient Relationship Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 143-UW</para>
            /// <para>Code assigned by the other payer to indicate the 
            /// relationship of patient to cardholder.
            /// </para>
            /// </remarks>
            [NcpdpField("143-UW")]
            public string OtherPayerPatientRelationshipCode { get; set; }

            /// <summary>
            /// Other Payer Benefit Effective Date
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 144-UX</para>
            /// <para>
            /// Other Payer's effective date of the patient's benefit.
            /// </para>
            /// </remarks>
            [NcpdpField("144-UX")]
            public DateTime? OtherPayerBenefitEffectiveDate { get; set; }

            /// <summary>
            /// Other Payer Benefit Termination Date
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 145-UY</para>
            /// <para>
            /// Other Payer's termination date of the patient's beneift.
            /// </para>
            /// </remarks>
            [NcpdpField("145-UY")]
            public DateTime? OtherPayerBenefitTerminationDate { get; set; }

            public string ToNcpdpString()
            {
                StringBuilder returnValue = new StringBuilder();

                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerCoverageType, this.OtherPayerCoverageType));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerIdQualifier, this.OtherPayerIdQualifier));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerId, this.OtherPayerId));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerProcessorControlNumber, this.OtherPayerProcessorControlNumber));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerCardholderId, this.OtherPayerCardholderId));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerGroupId, this.OtherPayerGroupId));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerPersonCode, this.OtherPayerPersonCode));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerHelpDeskPhoneNumber, this.OtherPayerHelpDeskPhoneNumber));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerPatientRelationshipCode, this.OtherPayerPatientRelationshipCode));
                if (this.OtherPayerBenefitEffectiveDate != null)
                    returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerBenefitEffectiveDate, this.OtherPayerBenefitEffectiveDate.Value.ToString("yyyyMMdd")));
                if (this.OtherPayerBenefitTerminationDate != null)
                    returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPayerBenefitTerminationDate, this.OtherPayerBenefitTerminationDate.Value.ToString("yyyyMMdd")));

                return returnValue.ToString();
            }
        }
    }
}
