using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class PricingSegment
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
        [NcpdpField("111-AM")]
        public string SegmentIdentification { get; set; }

        /// <summary>
        /// Ingredient Cost Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 409-D9</para>
        /// <para>
        /// Submitted product component cost of the dispensed prescription.  
        /// This amount is included in the 'Gross Amount Due' (430-DU).
        /// </para>
        /// </remarks>
        [Required]
        [NcpdpField("409-D9")]
        public decimal? IngredientCostSubmitted { get; set; }

        /// <summary>
        /// Dispensing Fee Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 412-DC</para>
        /// <para>
        /// Dispensing fee submitted by the pharmacy.  This amount is included 
        /// in the 'Gross Amount Due' (430-DU).
        /// </para>
        /// </remarks>
        [NcpdpField("430-DU")]
        public decimal? DispensingFeeSubmitted { get; set; }

        /// <summary>
        /// Professional Service Fee Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 477-BE</para>
        /// <para>
        /// Amount submitted by the provider for professional services rendered.
        /// </para>
        /// </remarks>
        [NcpdpField("477-BE")] 
        public decimal? ProfessionalServiceFeeSubmitted { get; set; }

        /// <summary>
        /// Patient Paid Amount Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 433-DX</para>
        /// <para>
        /// Amount the pharmacy received from the patient for the prescription 
        /// dispensed.
        /// </para>
        /// </remarks>
        [NcpdpField("433-DX")]
        public decimal? PatientPaidAmountSubmitted { get; set; }

        /// <summary>
        /// Incentive Amount Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 438-E3</para>
        /// <para>
        /// Amount represents a fee that is submitted by the pharmacy for
        /// contractually agreed upon services.  This amount is included in the
        /// 'Gross Amount Due' (430-DU).
        /// </para>
        /// </remarks>
        [NcpdpField("438-E3")]
        public decimal? IncentiveAmountSubmitted { get; set; }

        /// <summary>
        /// Other Amount Claimed Submitted Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 478-H7</para>
        /// <para>
        /// Count of other amount claimed submitted occurrences.
        /// </para>
        /// </remarks>
        [NcpdpField("478-H7")]
        public int OtherAmountClaimedSubmittedCount { get; set; }

        /// <summary>
        /// Other Amount Submitted List
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 479-H8 and 480-H8</para>
        /// <para>Repeating fields held in a list object</para>
        /// </remarks>
        [NcpdpLoop("OtherAmountClaimSubmitted")]
        public List<OtherAmountClaimedSubmittedContainer> OtherAmountClaimedSubmittedList { get; set; }

        /// <summary>
        /// Flat Sales Tax Amount Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 481-HA</para>
        /// <para>
        /// Flat sales tax submitted for prescription.  This amount is included 
        /// in the 'Gross Amount Due' (430-DU).
        /// </para>
        /// </remarks>
        [NcpdpField("481-HA")]
        public decimal? FlatSalesTaxAmountSubmitted { get; set; }

        /// <summary>
        /// Percentage Sales Tax Rate Amount Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 482-GE</para>
        /// <para>Percentage sales tax submitted</para>
        /// </remarks>
        [NcpdpField("482-GE")]
        public decimal? PercentageSalesTaxRateAmountSubmitted { get; set; }

        /// <summary>
        /// Percentage Sales Tax Rate Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 483-HE</para>
        /// <para>
        /// Percentage sales tax rate used to calculate 'Percentage Sales Tax 
        /// Amount Submitted' (482-GE).
        /// </para>
        /// </remarks>
        [NcpdpField("483-HE")]
        public decimal? PercentageSalesTaxRateSubmitted { get; set; }

        /// <summary>
        /// Percentage Sales Tax Basis Submitted
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 484-JE</para>
        /// <para>Code indicating the basis for precentage sales tax.</para>
        /// </remarks>
        [NcpdpField("484-JE")]
        public string PercentageSalesTaxBasisSubmitted { get; set; }

        /// <summary>
        /// Usual and Customary Charge 
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 426-DQ</para>
        /// <para>
        /// Amount charged cast customers for the prescription exclusive of 
        /// sales tax or other amounts claimed.
        /// </para>
        /// </remarks>
        [NcpdpField("426-DQ")]
        public decimal? UsualAndCustomaryCharge { get; set; }

        /// <summary>
        /// Gross Amount Due
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 430-DU</para>
        /// <para>
        /// Total price claimed from all sources. For prescription claim 
        /// request, field represents a sum of
        /// ‘Ingredient Cost Submitted’ (409-D9), 
        /// ‘Dispensing Fee Submitted’ (412-DC), 
        /// ‘Flat Sales Tax Amount Submitted’ (481-HA), 
        /// ‘Percentage Sales Tax Amount Submitted’ (482-GE), 
        /// ‘Incentive Amount Submitted’ (438-E3), 
        /// ‘Other Amount Claimed’ (48Ø-H9).
        /// 
        /// For service claim request, field represents a sum of 
        /// ‘Professional Services Fee Submitted’ (477-BE), 
        /// ‘Flat Sales Tax Amount Submitted’ (481-HA), 
        /// ‘Percentage Sales Tax Amount Submitted’ (482-GE), 
        /// ‘Other Amount Claimed’ (480-H9).
        /// </para>
        /// </remarks>
        [Required]
        [NcpdpField("430-DU")]
        public decimal? GrossAmountDue { get; set; }

        /// <summary>
        /// Basis of Cost Determination
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 423-DN</para>
        /// <para>
        /// Code indicating the method by which 'Ingredient Cost Submitted' 
        /// (Field 409-D9) was calculated.
        /// </para>
        /// </remarks>
        [NcpdpField("423-DN")]
        public string BasisOfCostDetermination { get; set; }

        /// <summary>
        /// Medcaid Paid Amount 
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 113-N3</para>
        /// <para>
        /// Amount paid by the Medicaid Agency.
        /// </para>
        /// </remarks>
        [NcpdpField("113-N3")] 
        public decimal? MedicaidPaidAmount { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties
        /// </summary>
        /// <param name="s">NCPPD string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static PricingSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new PricingSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public PricingSegment(string[] fields)
        {
            OtherAmountClaimedSubmittedContainer currentOtherAmountClaimSubmitted = null;
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
                    case "D9":
                        this.IngredientCostSubmitted = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "DC":
                        this.DispensingFeeSubmitted = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "BE":
                        this.ProfessionalServiceFeeSubmitted = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "DX":
                        this.PatientPaidAmountSubmitted = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "E3":
                        this.IncentiveAmountSubmitted = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "H7":
                        this.OtherAmountClaimedSubmittedCount = int.Parse(ncpdpFieldValue);
                        break;
                    case "H8":
                        //Creates a list of other amount claimed submitted objects and adds one to the list.
                        if (this.OtherAmountClaimedSubmittedList == null)
                            this.OtherAmountClaimedSubmittedList = new List<OtherAmountClaimedSubmittedContainer>();
                        currentOtherAmountClaimSubmitted = new OtherAmountClaimedSubmittedContainer();
                        currentOtherAmountClaimSubmitted.OtherAmountClaimedSubmittedQualifier = ncpdpFieldValue;
                        this.OtherAmountClaimedSubmittedList.Add(currentOtherAmountClaimSubmitted);
                        break;
                    case "H9":
                        if (currentOtherAmountClaimSubmitted == null)
                            throw new Exception("H9 field sent before H8 field on line = " + fields.ToString());
                        currentOtherAmountClaimSubmitted.OtherAmountClaimedSubmitted = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "HA":
                        this.FlatSalesTaxAmountSubmitted = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "GE":
                        this.PercentageSalesTaxRateAmountSubmitted = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "HE":
                        this.PercentageSalesTaxRateSubmitted = Utils.Overpunch.Parse(ncpdpFieldValue) / 1000;
                        break;
                    case "JE":
                        this.PercentageSalesTaxBasisSubmitted = ncpdpFieldValue;
                        break;
                    case "DQ":
                        this.UsualAndCustomaryCharge = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "DU":
                        this.GrossAmountDue = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "DN":
                        this.BasisOfCostDetermination = ncpdpFieldValue;
                        break;
                    case "N3":
                        this.MedicaidPaidAmount = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    default:
                        break;
                }
            }
        }

        public class OtherAmountClaimedSubmittedContainer
        {
            /// <summary>
            /// Other Amount Claimed Submitted Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 479-H8</para>
            /// <para>
            /// Code identifying the additional incurred cost claimed in 'Other 
            /// Amount Claimed Submitted' (480-H9).
            /// </para>
            /// </remarks>
            [NcpdpField("479-H8")] 
            public string OtherAmountClaimedSubmittedQualifier { get; set; }

            /// <summary>
            /// Other Amount Claimed Submitted
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 480-H9</para>
            /// <para>
            /// Amount representing the additional incurred costs for a 
            /// dispensed prescription or service.
            /// </para>
            /// </remarks>
            [NcpdpField("480-H9")] 
            public decimal OtherAmountClaimedSubmitted { get; set; }
        }
    }
}
