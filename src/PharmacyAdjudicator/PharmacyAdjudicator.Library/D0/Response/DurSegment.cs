using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Response
{
    public class DurSegment
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
        /// List of DURs
        /// </summary>
        public List<DurContainer> DurContainers;

        public DurSegment()
        {
            this.SegmentIdentification = "24";
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();
            foreach (var dur in DurContainers)
                returnValue.Append(dur.ToNcpdpString());
            //Adds segment separator and identifier to beginning if the segment has data.
            if (returnValue.Length > 0)
            {
                returnValue.Insert(0, Utils.NcpdpString.ToNcpdpFieldString(() => this.SegmentIdentification, this.SegmentIdentification));
                returnValue.Insert(0, Utils.NcpdpString.SegmentSeparator);
            }
            return returnValue.ToString();
        }

        /// <summary>
        /// Container that holds DUR record
        /// </summary>
        public class DurContainer
        {
            /// <summary>
            /// DUR/PPS Response Code Counter
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 567-J6</para>
            /// <para>
            /// Counter number for each DUR/PPS response set/logical grouping.
            /// </para>
            /// </remarks>
            [NcpdpField("567-J6")]
            public string DurPpsResponseCodeCounter { get; set; }

            /// <summary>
            /// Reasons For Service Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 439-E4</para>
            /// <para>
            /// Code identifying teh type of utilization conflict detected by the 
            /// prescriber or the pharmacist or the reason for the pharmacist's 
            /// professional service.
            /// </para>
            /// </remarks>
            [NcpdpField("439-E4")]
            public string ReasonForServiceCode { get; set; }

            /// <summary>
            /// Clinical Significance Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 538-FS</para>
            /// <para>
            /// Code identifying the significance or severity level of a clinical 
            /// event as contained in the originating database.
            /// </para>
            /// </remarks>
            [NcpdpField("538-FS")]
            public string ClinicalSignificanceCode { get; set; }

            /// <summary>
            /// Other Pharmacy Indicator
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 529-FT</para>
            /// <para>
            /// Code indicating the pharmacy responsible for the previous event 
            /// involved in the DUR conflict.
            /// </para>
            /// </remarks>
            [NcpdpField("529-FT")]
            public string OtherPharmacyIndicator { get; set; }

            /// <summary>
            /// Previous Date of Fill
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 530-FU</para>
            /// <para>Date prescription was previously filled.</para>
            /// </remarks>
            [NcpdpField("530-FU")]
            public DateTime? PreviousDateOfFill { get; set; }

            /// <summary>
            /// Quantity of Previous Fill
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 531-FV</para>
            /// <para>
            /// Amount expressed in metric decimal units of the conflicting 
            /// agent that was previously filled.
            /// </para>
            /// </remarks>
            [NcpdpField("531-FV")]
            public decimal? QuantityOfPreviousFill { get; set; }

            /// <summary>
            /// Database Indicator
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 532-FW</para>
            /// <para>
            /// Code identifying the source of drug information used for DUR 
            /// processing or to define the database used for identifying the 
            /// product.
            /// </para>
            /// </remarks>
            [NcpdpField("532-FW")]
            public string DatabaseIndicator { get; set; }

            /// <summary>
            /// Other Prescriber Indicator
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 533-FX</para>
            /// <para>
            /// Code comparing the prescriber of the current prescription to 
            /// the prescriber of the previously filled conflicting prescription.
            /// </para>
            /// </remarks>
            [NcpdpField("533-FX")]
            public string OtherPrescriberIndicator { get; set; }

            /// <summary>
            /// DUR Free Text Message
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 544-FY</para>
            /// <para>
            /// Text that provides additional detail regarding a DUR conflict.
            /// </para>
            /// </remarks>
            [NcpdpField("544-FY")]
            public string DurFreeTextMessage { get; set; }

            /// <summary>
            /// DUR Additional Text
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 570-NS</para>
            /// <para>
            /// Descriptive information that further defines the referenced DUR 
            /// alert.
            /// </para>
            /// </remarks>
            [NcpdpField("570-NS")]
            public string DurAdditionalText { get; set; }

            public string ToNcpdpString()
            {
                StringBuilder returnValue = new StringBuilder();
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.DurPpsResponseCodeCounter, this.DurPpsResponseCodeCounter));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.ReasonForServiceCode, this.ReasonForServiceCode));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.ClinicalSignificanceCode, this.ClinicalSignificanceCode));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPharmacyIndicator, this.OtherPharmacyIndicator));
                if (this.PreviousDateOfFill != null)
                    returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PreviousDateOfFill, this.PreviousDateOfFill.Value.ToString("yyyyMMdd")));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.QuantityOfPreviousFill, this.QuantityOfPreviousFill.ToString()));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.DatabaseIndicator, this.DatabaseIndicator));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherPrescriberIndicator, this.OtherPrescriberIndicator));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.DurFreeTextMessage, this.DurFreeTextMessage));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.DurAdditionalText, this.DurAdditionalText));
                return returnValue.ToString();
            }
        }

    }
}
