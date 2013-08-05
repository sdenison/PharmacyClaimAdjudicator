using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class CoordinationOfBenefitsSegment
    {
        /// <summary>
        /// Segment Identification
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 111-AM</para>
        /// <para>Identifies the segment in the request and/or response</para>
        /// </remarks>
        [NcpdpFieldAttribute("111-AM")]
        public string SegmentIdentification { get; set; }

        /// <summary>
        /// Coordination of Benefits/Other Payemnts Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 337-4C</para> 
        /// <para>Count of other payment occurrences.</para>
        /// </remarks>
        [NcpdpFieldAttribute("337-4C")]
        public int CoordinationOfBenefitsOtherPaymentsCount { get; set; }

        /// <summary>
        /// List of Other Payers' repeating fields
        /// </summary>
        public List<OtherPayerContainer> OtherPayers { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties.
        /// </summary>
        /// <param name="s">NCPDP string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static CoordinationOfBenefitsSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new CoordinationOfBenefitsSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        /// <summary>
        /// Takes fields and assigns them to properties according to NCPDP rules.
        /// </summary>
        /// <param name="fields">Array of strings representing NCPDP fields.</param>
        public CoordinationOfBenefitsSegment(string[] fields)
        {
            OtherPayerContainer currentOtherPayer = null;
            OtherPayerAmountPaidContainer currentOtherPayerAmountPaid = null;
            OtherPayerPatientContainer currentOtherPayerPatient = null;
            BenefitStageContainer currentBenefitStage = null;
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
                    case "4C":
                        this.CoordinationOfBenefitsOtherPaymentsCount = int.Parse(ncpdpFieldValue);
                        break;
                    case "5C":
                        if (this.OtherPayers == null)
                            this.OtherPayers = new List<OtherPayerContainer>();
                        currentOtherPayer = new  OtherPayerContainer();
                        currentOtherPayer.OtherPayerCoverageType = ncpdpFieldValue;
                        //limit 9 per Coordination of Benefits Segment
                        this.OtherPayers.Add(currentOtherPayer);
                        break;
                    case "6C":
                        currentOtherPayer.OtherPayerIdQualifier = ncpdpFieldValue;
                        break;
                    case "7C":
                        currentOtherPayer.OtherPayerId = ncpdpFieldValue;
                        break;
                    case "E8":
                        currentOtherPayer.OtherPayerDate = ncpdpFieldValue;
                        break;
                    case "A7":
                        currentOtherPayer.InternalControlNumber = ncpdpFieldValue;
                        break;
                    case "HB":
                        currentOtherPayer.OtherPayerAmountPaidCount = int.Parse(ncpdpFieldValue);
                        currentOtherPayer.OtherPayerAmountPaids = new List<OtherPayerAmountPaidContainer>();
                        break;
                    case "HC":
                        if (currentOtherPayer.OtherPayerAmountPaids == null)
                            currentOtherPayer.OtherPayerAmountPaids = new List<OtherPayerAmountPaidContainer>();
                        currentOtherPayerAmountPaid = new OtherPayerAmountPaidContainer();
                        currentOtherPayerAmountPaid.OtherPayerAmountPaidQualifier = ncpdpFieldValue;
                        //limit 9 per Other Payer
                        currentOtherPayer.OtherPayerAmountPaids.Add(currentOtherPayerAmountPaid);
                        break;
                    case "DV":
                        currentOtherPayerAmountPaid.OtherPayerAmountPaid = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "5E":
                        currentOtherPayer.OtherPayerRejectCount = int.Parse(ncpdpFieldValue);
                        if (currentOtherPayer.OtherPayerRejectCodes == null)
                            currentOtherPayer.OtherPayerRejectCodes = new List<string>();
                        break;
                    case "6E":
                        if (currentOtherPayer.OtherPayerRejectCodes == null)
                            currentOtherPayer.OtherPayerRejectCodes = new List<string>();
                        //limit 5 per Other Payer
                        currentOtherPayer.OtherPayerRejectCodes.Add(ncpdpFieldValue);
                        break;
                    case "NR":
                        currentOtherPayer.OtherPayerPatientResponsibilityAmountCount = int.Parse(ncpdpFieldValue);
                        if (currentOtherPayer.OtherPayerPatientAmounts == null)
                            currentOtherPayer.OtherPayerPatientAmounts = new List<OtherPayerPatientContainer>();
                        break;
                    case "NP":
                        if (currentOtherPayer.OtherPayerPatientAmounts == null)
                            currentOtherPayer.OtherPayerPatientAmounts = new List<OtherPayerPatientContainer>();
                        currentOtherPayerPatient = new OtherPayerPatientContainer();
                        currentOtherPayerPatient.OtherPayerPatientResponsibilityAmountQualifier = ncpdpFieldValue;
                        //limit 25 per Other Payer
                        currentOtherPayer.OtherPayerPatientAmounts.Add(currentOtherPayerPatient);
                        break;
                    case "NQ":
                        currentOtherPayerPatient.OtherPayerPatientResponsibilityAmount = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "MU":
                        currentOtherPayer.BenefitStageCount = int.Parse(ncpdpFieldValue);
                        if (currentOtherPayer.BenefitStageAmounts == null)
                            currentOtherPayer.BenefitStageAmounts = new List<BenefitStageContainer>();
                        break;
                    case "MV":
                        if (currentOtherPayer.BenefitStageAmounts == null)
                            currentOtherPayer.BenefitStageAmounts = new List<BenefitStageContainer>();
                        currentBenefitStage = new BenefitStageContainer();
                        currentBenefitStage.BenefitStageQualifier = ncpdpFieldValue;
                        //limit 4 per Other Payer
                        currentOtherPayer.BenefitStageAmounts.Add(currentBenefitStage);
                        break;
                    case "MW":
                        currentBenefitStage.BenefitStageAmount = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    default:
                        break;
                }
            }
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
            /// Other Payer Date
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 443-E8</para>
            /// <para>
            /// Payment or denial date of the claim submitted to the other payer. 
            /// Used for coordination of benefits.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("443-E8")]
            public string OtherPayerDate { get; set; }

            /// <summary>
            /// Internal Control Number
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 993-A7</para>
            /// <para></para>
            /// </remarks>
            [NcpdpFieldAttribute("993-A7")]
            public string InternalControlNumber { get; set; }

            /// <summary>
            /// Other Payer Amount Paid Count
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 341-HB</para>
            /// <para>
            /// Number assigned by the processor to identify an adjudicated 
            /// claim when supplied in payer-to-payer coordination of benefits 
            /// only.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("341-HB")]
            public int OtherPayerAmountPaidCount { get; set; }

            /// <summary>
            /// List of Other Payer Amount Paid Records
            /// </summary>
            public List<OtherPayerAmountPaidContainer> OtherPayerAmountPaids { get; set; }

            /// <summary>
            /// Other Payer Reject Count
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 471-5E</para>
            /// <para>Count of ‘Other Payer Reject Code’ (472-6E) occurrences.</para>
            /// </remarks>
            [NcpdpFieldAttribute("471-5E")]
            public int OtherPayerRejectCount { get; set; }

            /// <summary>
            /// List of Payer Reject Codes
            /// </summary>
            public List<string> OtherPayerRejectCodes { get; set; }

            /// <summary>
            /// Other Payer Patient Responsibility Amount Count
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 353-NR</para>
            /// <para>
            /// Count of “Other Payer-Patient Responsibility Amount” (352-NQ) and 
            /// “Other Payer-Patient Responsibility Amount Qualifier” (351-NP) occurrences.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("353-NR")]
            public int OtherPayerPatientResponsibilityAmountCount { get; set; }

            /// <summary>
            /// List of Payer Patient Amounts
            /// </summary>
            public List<OtherPayerPatientContainer> OtherPayerPatientAmounts { get; set; }

            /// <summary>
            /// Benefit Stage Count
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 392-MU</para>
            /// <para>Count of ‘Benefit Stage Amount’ (394-MW) occurrences.</para>
            /// </remarks>
            [NcpdpFieldAttribute("392-MU")]
            public int BenefitStageCount { get; set; }

            /// <summary>
            /// List of Benefit Stage Amounts
            /// </summary>
            public List<BenefitStageContainer> BenefitStageAmounts { get; set; }
        }

        /// <summary>
        /// Held in a list under the Other Payer Container
        /// </summary>
        public class OtherPayerAmountPaidContainer
        {
            /// <summary>
            /// Other Payer Amount Paid Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 342-HC</para>
            /// <para>Code qualifying the ‘Other Payer Amount Paid’ (431-DV).</para>
            /// </remarks>
            [NcpdpFieldAttribute("342-HC")]
            public string OtherPayerAmountPaidQualifier { get; set; }

            /// <summary>
            /// Other Payer Amount Paid
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 431-DV</para>
            /// <para>
            /// Amount of any payment known by the pharmacy from other sources.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("431-DV")]
            public decimal OtherPayerAmountPaid { get; set; }
        }

        public class OtherPayerPatientContainer
        {
            /// <summary>
            /// Other Payer/Patient Responsibility Amount Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 351-NP</para>
            /// <para>
            /// Code qualifying the 'Other Payer-Patient Responsibility Amount 
            /// (352-NQ)'.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("351-NP")]
            public string OtherPayerPatientResponsibilityAmountQualifier { get; set; }

            /// <summary>
            /// Other Payer/Patient Responsibility Amount
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 352-NQ</para>
            /// <para>The patient’s cost share from a previous payer.</para>
            /// </remarks>
            [NcpdpFieldAttribute("352-NQ")]
            public decimal OtherPayerPatientResponsibilityAmount { get; set; }
        }

        public class BenefitStageContainer
        {
            /// <summary>
            /// Benefits Stage Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 393-MV</para>
            /// <para>Code qualifying the ’Benefit Stage Amount’ (394-MW).</para>
            /// </remarks>
            [NcpdpFieldAttribute("393-MV")]
            public string BenefitStageQualifier { get; set; }

            /// <summary>
            /// Benefit Stage Amount
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 394-MW</para>
            /// <para>
            /// The amount of claim allocated to the Medicare stage identified 
            /// by the ‘Benefit Stage Qualifier’ (393-MV)
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("394-MW")]
            public decimal BenefitStageAmount { get; set; }
        }
    }
}
