using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Response
{
    /// <summary>
    /// Insurance Response Segment
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
        /// Network Reimbursement ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 545-2F</para>
        /// <para>
        /// Field defined by the processor. It identifies the network, for the 
        /// covered member, used to calculate the reimbursement to the pharmacy.
        /// </para>
        /// </remarks>
        [MaxLength(10)]
        [NcpdpField("545-2F")]
        public string NetworkReimbursementId { get; set; }

        /// <summary>
        /// Payer ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 568-J7</para>
        /// <para>Code indicating the type of payer ID.</para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpField("568-J7")]
        public string PayerIdQualifier { get; set; }

        /// <summary>
        /// Payer ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 569-J8</para> 
        /// <para>ID of the payer.</para>
        /// </remarks>
        [MaxLength(10)]
        [NcpdpField("569-J8")]
        public string PayerId { get; set; }

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

        public InsuranceSegment()
        {
            this.SegmentIdentification = "25";
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();

            //Append properties to returnValue.
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.GroupId, this.GroupId));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PlanId, this.PlanId));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.NetworkReimbursementId, this.NetworkReimbursementId));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PayerIdQualifier, this.PayerIdQualifier));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PayerId, this.PayerId));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.MedicaidIdNumber, this.MedicaidIdNumber));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.MedicaidAgencyNumber, this.MedicaidAgencyNumber));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.CardholderId, this.CardholderId));

            //Adds segment separator and identifier to beginning if the segment has data.
            if (returnValue.Length > 0)
            {
                returnValue.Insert(0, Utils.NcpdpString.ToNcpdpFieldString(() => this.SegmentIdentification, this.SegmentIdentification));
                returnValue.Insert(0, Utils.NcpdpString.SegmentSeparator);
            }

            return returnValue.ToString();
        }
    }
}
