using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Response
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
        /// Preferred Product Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 551-9F</para>
        /// <para>Count of preferred product occurences.</para>
        /// </remarks>
        [NcpdpField("551-9F")]
        public int? PreferredProductCount { get; set; }

        /// <summary>
        /// List of perferred products
        /// </summary>
        public List<PreferredProductContainer> PreferredProducts { get; set; }

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

        public ClaimSegment()
        {
            this.SegmentIdentification = "22";
        }

        //Provided to transform a submitted ClaimSegment into a repsonse ClaimSegment
        public ClaimSegment(Submitted.ClaimSegment submittedClaim)
        {
            this.SegmentIdentification = "22";
            this.PrescriptionReferenceNumberQualifier = submittedClaim.PrescriptionReferenceNumberQualifier;
            this.PrescriptionServiceReferenceNumber = submittedClaim.PrescriptionServiceReferenceNumber;
            this.MedicaidSubrogationInternalControlNumber = submittedClaim.MedicaidSubrogationInternalControlNumber;
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();

            //Append properties to returnValue.
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PrescriptionReferenceNumberQualifier, this.PrescriptionReferenceNumberQualifier));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PrescriptionServiceReferenceNumber, this.PrescriptionServiceReferenceNumber));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PreferredProductCount, this.PreferredProductCount.ToString()));

            if (this.PreferredProductCount > 0 && this.PreferredProducts != null)
                foreach (var product in this.PreferredProducts)
                    returnValue.Append(product.ToNcpdpString());
            
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.MedicaidSubrogationInternalControlNumber, this.MedicaidSubrogationInternalControlNumber));

            //Adds segment separator and identifier to beginning if the segment has data.
            if (returnValue.Length > 0)
            {
                returnValue.Insert(0, Utils.NcpdpString.ToNcpdpFieldString(() => this.SegmentIdentification, this.SegmentIdentification));
                returnValue.Insert(0, Utils.NcpdpString.SegmentSeparator);
            }
            return returnValue.ToString();
        }

        public class PreferredProductContainer
        {
            /// <summary>
            /// Preferred Product ID Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 552-AP</para>
            /// <para>
            /// Code qualifying the type of product ID submitted in ‘Preferred 
            /// Product ID’ (553-AR).
            /// </para>
            /// </remarks>
            [NcpdpField("552-AP")]
            public string PreferredProductIdQualifier { get; set; }

            /// <summary>
            /// Preferred Product ID
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 553-AR</para>
            /// <para>Alternate product recommended by the plan.</para>
            /// </remarks>
            [NcpdpField("553-AR")]
            public string PreferredProductId { get; set; }

            /// <summary>
            /// Preferred Product Incentive
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 554-AS</para>
            /// <para>
            /// Amount of pharmacy incentive available for substitution of 
            /// preferred product.
            /// </para>
            /// </remarks>
            [NcpdpField("554-AS")]
            public decimal? PreferredProductIncentive { get; set; }

            /// <summary>
            /// Preferred Product Cost Share Incentive
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 555-AT</para>
            /// <para>
            /// Amount of patient’s copay/cost-share incentive for preferred 
            /// product.
            /// </para>
            /// </remarks>
            [NcpdpField("555-AT")]
            public decimal? PreferredProductCostShareIncentive { get; set; }

            /// <summary>
            /// Preferred Product Description
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 556-AU</para>
            /// <para>Free text message.</para>
            /// </remarks>
            [NcpdpField("556-AU")]
            public string PreferredProductDescription { get; set; }

            public string ToNcpdpString()
            {
                StringBuilder returnValue = new StringBuilder();
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PreferredProductIdQualifier, this.PreferredProductIdQualifier));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PreferredProductId, this.PreferredProductId));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PreferredProductIncentive, this.PreferredProductIncentive, true, 2));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PreferredProductCostShareIncentive , this.PreferredProductCostShareIncentive, true, 2));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PreferredProductDescription, this.PreferredProductDescription));
                return returnValue.ToString();
            }
        }
    }
}
