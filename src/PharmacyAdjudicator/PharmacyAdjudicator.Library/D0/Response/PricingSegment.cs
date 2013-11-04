using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PharmacyAdjudicator.Library.D0.Response
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
        [NcpdpFieldAttribute("111-AM")]
        public string SegmentIdentification { get; set; }

        /// <summary>
        /// Patient Pay Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 505-F5</para>
        /// <para>
        /// Amount that is calculated by the processor and returned to the 
        /// pharmacy as the TOTAL amount to be paid by the patient to the 
        /// pharmacy; the patient’s total cost share, including copayments, 
        /// amounts applied to deductible, over maximum amounts, penalties, etc.
        /// </para>
        /// </remarks>
        [NcpdpField("505-F5")]
        public decimal? PatientPayAmount { get; set; }

        /// <summary>
        /// Ingredient Cost Paid
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 506-F6</para>
        /// <para>
        /// Drug ingredient cost paid included in the ‘Total Amount Paid’ (509-F9).
        /// </para>
        /// </remarks>
        [NcpdpField("506-F6")]
        public decimal? IngredientCostPaid { get; set; }

        /// <summary>
        /// Dispensing Fee Paid
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 507-F7</para>
        /// <para>
        /// Dispensing fee paid included in the ‘Total Amount Paid’ (5Ø9-F9).
        /// </para>
        /// </remarks>
        [NcpdpField("507-F7")]
        public decimal? DispensingFeePaid { get; set; }

        /// <summary>
        /// Tax Exempt Indicator
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 557-AV</para>
        /// <para>
        /// Code indicating the payer and/or the patient is exempt from taxes.
        /// </para>
        /// </remarks>
        [NcpdpField("557-AV")]
        public string TaxExemptIndicator { get; set; }

        /// <summary>
        /// Flat Sales Tax Amount Paid
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 558-AW</para>
        /// <para>
        /// Flat sales tax paid which is included in the ‘Total Amount Paid’ 
        /// (509-F9).
        /// </para>
        /// </remarks>
        [NcpdpField("558-AW")] 
        public decimal? FlatSalesTaxAmountPaid { get; set; }

        /// <summary>
        /// Percentage Sales Tax Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 559-AX</para>
        /// <para>
        /// Amount of percentage sales tax paid which is included in the ‘Total 
        /// Amount Paid’ (509-F9).
        /// </para> 
        /// </remarks>
        [NcpdpField("559-AX")]
        public decimal? PercentageSalesTaxAmount { get; set; }

        /// <summary>
        /// Percentage Sales Tax Rate Paid
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 560-AY</para>
        /// <para>
        /// Percentage sales tax rate used to calculate ‘Percentage Sales Tax 
        /// Amount Paid’ (559-AX).
        /// </para>
        /// </remarks>
        [NcpdpField("560-AY")]
        public decimal? PercentageSalesTaxRatePaid { get; set; }

        /// <summary>
        /// Percentage Sales Tax Basis Paid
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 561-AZ</para>
        /// <para>Code indicating the percentage sales tax paid basis.</para>
        /// </remarks>
        [NcpdpField("561-AZ")]
        public string PercentageSalesTaxBasisPaid { get; set; }

        /// <summary>
        /// Incentive Amount Paid
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 521-FL</para>
        /// <para>
        /// Amount represents the contractually agreed upon incentive fee paid 
        /// for specific services rendered. Amount is included in the 'Total 
        /// Amount Paid' (509-F9).
        /// </para>
        /// </remarks>
        [NcpdpField("521-FL")] 
        public decimal? IncentiveAmountPaid { get; set; }

        /// <summary>
        /// Professional Service Fee Paid
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 562-J1</para>
        /// <para>
        /// Amount representing the contractually agreed upon fee for 
        /// professional services rendered. This amount is included in 
        /// the ‘Total Amount Paid’ (509-F9).
        /// </para>
        /// </remarks>
        [NcpdpField("562-J1")]
        public decimal? ProfessionalServiceFeePaid { get; set; }

        /// <summary>
        /// Other Amount Paid Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 563-J2</para>
        /// <para>Count of the other amount paid occurrences.</para>
        /// </remarks>
        [NcpdpField("563-J2")]
        public int? OtherAmountPaidCount { get; set; }

        /// <summary>
        /// List of Other Amounts Paid
        /// </summary>
        public List<OtherAmountPaidContainer> OtherAmountPaids { get; set; }

        /// <summary>
        /// Other Payer Amount Recognized
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 566-J5</para>
        /// <para>
        /// Total amount recognized by the processor of any payment from another 
        /// source.
        /// </para>
        /// </remarks>
        [NcpdpField("566-J5")]
        public decimal? OtherPayerAmountRecognized { get; set; }

        /// <summary>
        /// Total Amount Paid
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 509-F9</para>
        /// <para>
        /// Total amount to be paid by the claims processor (i.e. pharmacy 
        /// receivable).  Represents a sum of: 
        /// 'Ingredient Cost Paid' (506-F6) 
        /// + 'Dispensing Fee Paid' (507-F7)
        /// + 'Flat Sales Tax Amount Paid' (558-AW)
        /// + 'Percentage Sales Tax Amount Paid' (559-AX)
        /// + 'Incentive Amount Paid' (521-FL)
        /// + 'Professional Service Fee Paid' (562-J1)
        /// + 'Other Amount Paid' (565-J4)
        /// - 'Patient Pay Amount' (505-F5)
        /// - 'Other Payer Amount Recognized' (566-J5)
        /// </para>
        /// </remarks>
        [Required]
        [NcpdpField("509-F9")]
        public decimal TotalAmountPaid { get; set; }

        /// <summary>
        /// Basis of Reimbursement Determination
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 522-FM</para>
        /// <para>
        /// Code identifying how the reimbursement amount was calculated for 
        /// 'Ingredient Cost Paid' (506-F6).
        /// </para>
        /// </remarks>
        [NcpdpField("522-FM")]
        public Core.Enums.BasisOfReimbursement BasisOfReimbursementDetermination { get; set; }

        /// <summary>
        /// Amount Attributed to Sales Tax
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 523-FN</para>
        /// <para>
        /// Amount To be collected from the patient that is included in 
        /// 'Patient Pay Amount' (505-F5) that is due to sales tax paid.
        /// </para>
        /// </remarks>
        [NcpdpField("523-FN")]
        public decimal? AmountAttributedToSalesTax { get; set; }

        /// <summary>
        /// Accumulated Deductible Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 512-FC</para>
        /// <para>
        /// Amount in dollars met by the patient/family in a deductible plan.
        /// </para>
        /// </remarks>
        [NcpdpField("512-FC")]
        public decimal? AccumulatedDeductibleAmount { get; set; }

        /// <summary>
        /// Remianing Deductible Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 513-FD</para> 
        /// <para>Amount not met by the patient/family in the deductible plan.</para>
        /// </remarks>
        [NcpdpField("513-FD")]
        public decimal? RemainingDeductibleAmount { get; set; }

        /// <summary>
        /// Remaining Benefit Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 514-FE</para>
        /// <para>
        /// Amount remaining in a patient/family plan with a periodic maximum benefit.
        /// </para>
        /// </remarks>
        [NcpdpField("514-FE")]
        public decimal? RemainingBenefitAmount { get; set; }

        /// <summary>
        /// Amount Applied To Periodic Deductible 
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 517-FH</para>
        /// <para>
        /// Amount to be collected from a patient that is included in 'Patient 
        /// Pay Amount' (505-F5) that is applied to a periodic deductible.
        /// </para>
        /// </remarks>
        [NcpdpField("517-FH")]
        public decimal? AmountAppliedToPeriodicDeductible { get; set; }

        /// <summary>
        /// Amount of Copay
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 518-FI</para>
        /// <para>
        /// Amount to be collected from the patient that is included in 'Patient 
        /// Pay Amount' (505-F5) that is due to a per prescription copay.
        /// </para>
        /// </remarks>
        [NcpdpField("518-FI")]
        public decimal? AmountOfCopay { get; set; }

        /// <summary>
        /// Amount Exceeding Periodic Benefit Maximum
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 520-FK</para>
        /// <para>
        /// Amount to be collected from the patient that is included in 'Patient 
        /// Pay Amount' (505-F5) that is due to the patient exceeding a periodic 
        /// benefit maximum.
        /// </para>
        /// </remarks>
        [NcpdpField("520-FK")]
        public decimal? AmountExceedingPeriodBenefitMaximum { get; set; }

        /// <summary>
        /// Baiss of Calculation Dispensing Fee
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 346-HH</para>
        /// <para>
        /// Code indicating how the reimbursement amount was calculated for 
        /// 'Dispensing Fee Paid' (507-F7).
        /// </para>
        /// </remarks>
        [NcpdpField("346-HH")]
        public string BasisOfCalculationDispensingFee { get; set; }

        /// <summary>
        /// Basis of Caluclation Copay
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 347-HJ</para>
        /// <para>
        /// Code indicating how the Copay reimbursement amount was calculated 
        /// for ‘Patient Pay Amount’ (5Ø5-F5).
        /// </para>
        /// </remarks>
        [NcpdpField("347-HJ")]
        public string BasisOfCalculationCopay { get; set; }

        /// <summary>
        /// Basis of Calculation Flat Sales Tax
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 348-HK</para>
        /// <para>
        /// Code indicating how the reimbursement amount was calculated for 
        /// 'Flat Sales Tax Amount Paid' (558-AW).
        /// </para>
        /// </remarks>
        [NcpdpField("348-HK")] 
        public string BasisOfCalculationFlatSalesTax { get; set; }

        /// <summary>
        /// Baiss of Calculation Percentage Sales Tax
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 349-HM</para>
        /// <para>
        /// Code indicating how the reimbursement amount was calculated for 
        /// 'Percentage Sales Tax Amount Paid' (559-AX).
        /// </para>
        /// </remarks>
        [NcpdpField("349-HM")]
        public string BasisOfCalculationPercentageSalesTax { get; set; }

        /// <summary>
        /// Amount Attributed to Processor Fee
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 571-NZ</para>
        /// <para>
        /// Amount to be collected from the patient that is included in Patient 
        /// Pay Amount (505-F5) that is due to the processing fee imposed by the 
        /// processor.
        /// </para>
        /// </remarks>
        [NcpdpField("571-NZ")]
        public decimal? AmountAttributedToProcessorFee { get; set; }

        /// <summary>
        /// Patient Sales Tax Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 575-EQ</para>
        /// <para>
        /// Patient sales tax responsibility.  This field is not a component of 
        /// the Patient Pay Amount (505-F5) formula.
        /// </para>
        /// </remarks>
        [NcpdpField("575-EQ")]
        public decimal? PatientSalesTaxAmount { get; set; }

        /// <summary>
        /// Plan Sales Tax Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 574-2Y</para>
        /// <para>
        /// Plan sales tax responsibility.  This field is not a component of the 
        /// Patient Pay Amount (505-F5) formula.
        /// </para>
        /// </remarks>
        [NcpdpField("574-2Y")]
        public decimal? PlanSalesTaxAmount { get; set; }

        /// <summary>
        /// Amount of Coinsurance
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 572-4U</para>
        /// <para>
        /// Amount to be collected from the patient that is included in 'Patient 
        /// Pay Amount' (505-F5) that is due to a per prescription coinsurance.
        /// </para>
        /// </remarks>
        [NcpdpField("572-4U")]
        public decimal? AmountOfCoinsurance { get; set; }

        /// <summary>
        /// Basis of Calculation - Coinsurance
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 573-4V</para>
        /// <para>
        /// Code indicating how the Coinsurance reimbursement amount was calculated 
        /// for 'Patient Pay Amount' (505-F5).
        /// </para>
        /// </remarks>
        [NcpdpField("573-4V")]
        public string BasisOfCalculationCoinsurance { get; set; }

        /// <summary>
        /// Benefit Stage Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 392-MU</para>
        /// <para>Count of 'Benefit Stage Amount' (394-MW) occurrences.</para>
        /// </remarks>
        [NcpdpField("392-MU")]
        public int? BenefitStageCount { get; set; }

        /// <summary>
        /// List of Benefit Stage Amounts
        /// </summary>
        public List<BenefitStageContainer> BenefitStages { get; set; }

        /// <summary>
        /// Estimated Generic Savings
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 577-G3</para>
        /// <para>
        /// The amount, not included in the Total Amount Paid (509-F9), that the 
        /// patient would have saved if they had chosen the generic drug instead 
        /// of the brand drug.
        /// </para>
        /// </remarks>
        [NcpdpField("577-G3")]
        public decimal? EstimatedGenericSavings { get; set; }

        /// <summary>
        /// Spending Account Amount Remaining
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 128-UC</para>
        /// <para>
        /// The balance from the patient's spending account after this transaction 
        /// was applied.
        /// </para>
        /// </remarks>
        [NcpdpField("128-UC")]
        public decimal? SpendingAccountAmountRemaining { get; set; }

        /// <summary>
        /// Health Plan Funded Assistance Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 129-UD</para>
        /// <para>
        /// The amount from the health plan-funded assistance account for the patient 
        /// that was applied to reduce Patient Pay Amount (505-F5).  This amount is
        /// used in Healthcare Reimbursement Account (HRA) benefits only.  This field
        /// is always a negative amount or zero.
        /// </para>
        /// </remarks>
        [NcpdpField("129-UD")]
        public decimal? HealthPlanFundedAssistanceAmount { get; set; }

        /// <summary>
        /// Amount Attributed to Provider Network Selection
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 133-UJ</para>
        /// <para>
        /// Amount to be collected from the patient that is included in 'Patient
        /// Pay Amount' (505-F5) that is due to the patient's provider network
        /// selection.
        /// </para>
        /// </remarks>
        [NcpdpField("133-UJ")]
        public decimal? AmountAttributedToProviderNetworkSelection { get; set; }

        /// <summary>
        /// Amount Attributed to Product Selection / Brand Drug
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 134-UK</para>
        /// <para>
        /// Amount to be collected from the patient that is included in 'Patient
        /// Pay Amount' (505-F5) that is due to the patient's select of a Brand
        /// product.
        /// </para>
        /// </remarks>
        [NcpdpField("134-UK")]
        public decimal? AmountAttributedToProductSelectionBrandDrug { get; set; }

        /// <summary>
        /// Amount Attributed to Product Selection / Non-Preferred Formulary Selection 
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 135-UM</para>
        /// <para>
        /// Amount to be collected from the patient that is included in 'Patient 
        /// Pay Amount' (505-F5) that is due to the patient's selection of a
        /// Non-Preferred Formulary product.
        /// </para>
        /// </remarks>
        [NcpdpField("135-UM")]
        public decimal? AmountAttributedToProductSelectionNonPreferredFormularySelection { get; set; }

        /// <summary>
        /// Amount Attributed to Product Selection / Brand Non-Preferred Formulary Selection
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 136-UN</para>
        /// <para>
        /// Amount to be collected from the patient that is included in 'Patient 
        /// Pay Amount' (505-F5) that is due to the patient's selection of a 
        /// Brand Non-Preferred Formulary product.
        /// </para>
        /// </remarks>
        [NcpdpField("136-UN")]
        public decimal? AmountAttributedToProductSelectionBrandNonPreferredFormularySelection { get; set; }

        /// <summary>
        /// Amount Attributed to Coverage Gap
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 137-UP</para>
        /// <para>
        /// Amount to be collected from the patient that is included in ‘Patient Pay Amount’ 
        /// (505-F5) that is due to the patient being in the coverage gap (for example 
        /// Medicare Part D Coverage Gap (donut hole)). A coverage gap is defined as the 
        /// period or amount during which the previous coverage ends and before an additional 
        /// coverage begins.
        /// </para>
        /// </remarks>
        [NcpdpField("137-UP")]
        public decimal? AmountAttributedToCoverageGap { get; set; }

        /// <summary>
        /// Ingredient Cost Contracted Reimbursable Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 148-U8</para>
        /// <para>
        /// Informational field used when Other Payer-Patient Responsibility Amount
        /// (352-NQ) or Patient Pay Amount (505-F5) is used for reimbursement.
        /// Amount is equal to contracted or reimbursable amount for product being
        /// dispensed.
        /// </para>
        /// </remarks>
        [NcpdpField("148-U8")]
        public decimal? IngredientCostContractedReimbursableAmount { get; set; }

        /// <summary>
        /// Dispensing Fee Contracted Reimbursable Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 149-U9</para>
        /// <para>
        /// Informational field used when Other Payer-Patient Responsibility Amount 
        /// (352-NQ) or Patient Pay Amount (505-F5) is used for reimbursement.  Amount 
        /// is equal to contracted or reimbursable dispensing fee for product being 
        /// dispensed.
        /// </para>
        /// </remarks>
        [NcpdpField("149-U9")]
        public decimal? DispensingFeeContractedReimbursableAmount { get; set; }

        public PricingSegment()
        {
            this.SegmentIdentification = "23";
        }

        public PricingSegment(Core.Transaction transaction)
        {
            this.SegmentIdentification = "23";

            ////Doing attribute binding instead
            //this.PatientPayAmount = transaction.PatientPayAmount;
            //this.IngredientCostPaid = transaction.IngredientCostPaid;
            //this.DispensingFeePaid = transaction.DispensingFeePaid;
            //this.TaxExemptIndicator = transaction.TaxExemptIndicator;
            //this.AmountOfCopay = transaction.Copay;

            ////TODO: implement other amount paid loop
            ////this.OtherAmountPaidCount = transaction.OtherAmountPaids.Count();
            ////foreach (var otherAmountPaid in transaction.OtherAmountPaids)
            ////    this.OtherAmountPaids.Add(new OtherAmountPaidContainer() { OtherAmountPaid = otherAmountPaid.AmountPaid, OtherAmountPaidQualifier = otherAmountPaid.Qualifier });
            //this.TotalAmountPaid = transaction.TotalAmountPaid;
            //this.BasisOfReimbursementDetermination = ((int)transaction.BasisOfReimbursement).ToString();

            BindTransaction(transaction);
        }



        private void BindTransaction(Core.Transaction transaction)
        {
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (Attribute.IsDefined(propertyInfo, typeof(NcpdpFieldAttribute)))
                {
                    var ncpdpField = (NcpdpFieldAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(NcpdpFieldAttribute));
                    // 111-AM is SegmentIdentification and should not be bound
                    if (!ncpdpField.NcpdpFieldName.Equals("111-AM"))
                    {
                        var value = ncpdpField.FindPropertyInObject(transaction);
                        if (value != null)
                        { 
                            propertyInfo.SetValue(this, value);
                        }
                    }
                }
                else if (Attribute.IsDefined(propertyInfo, typeof(NcpdpLoopAttribute)))
                { 

                }
            }
        }
        
        public string ToNcpdpString() 
        {
            StringBuilder returnValue = new StringBuilder();

            //Append properties to returnValue.
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.PatientPayAmount, this.PatientPayAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.IngredientCostPaid, this.IngredientCostPaid));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.DispensingFeePaid, this.DispensingFeePaid));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.TaxExemptIndicator, this.TaxExemptIndicator));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.FlatSalesTaxAmountPaid, this.FlatSalesTaxAmountPaid));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.PercentageSalesTaxAmount, this.PercentageSalesTaxAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.PercentageSalesTaxRatePaid, this.PercentageSalesTaxRatePaid));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PercentageSalesTaxBasisPaid, this.PercentageSalesTaxBasisPaid));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.IncentiveAmountPaid, this.IncentiveAmountPaid));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.ProfessionalServiceFeePaid, this.ProfessionalServiceFeePaid));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherAmountPaidCount, this.OtherAmountPaidCount.ToString()));
            if (this.OtherAmountPaids != null)
                foreach (var otherAmount in this.OtherAmountPaids)
                    returnValue.Append(otherAmount.ToNcpdpString());
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.OtherPayerAmountRecognized, this.OtherPayerAmountRecognized));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.TotalAmountPaid, this.TotalAmountPaid));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BasisOfReimbursementDetermination, Core.Enums.EnumConvert.ToString(this.BasisOfReimbursementDetermination)));
            //returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BasisOfReimbursementDetermination, this.BasisOfReimbursementDetermination.ToString()));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountAttributedToSalesTax, this.AmountAttributedToSalesTax));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AccumulatedDeductibleAmount, this.AccumulatedDeductibleAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.RemainingDeductibleAmount, this.RemainingDeductibleAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.RemainingBenefitAmount, this.RemainingBenefitAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountAppliedToPeriodicDeductible, this.AmountAppliedToPeriodicDeductible));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountOfCopay, this.AmountOfCopay));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountExceedingPeriodBenefitMaximum, this.AmountExceedingPeriodBenefitMaximum));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BasisOfCalculationDispensingFee, this.BasisOfCalculationDispensingFee));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BasisOfCalculationCopay, this.BasisOfCalculationCopay));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BasisOfCalculationFlatSalesTax, this.BasisOfCalculationFlatSalesTax));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BasisOfCalculationPercentageSalesTax, this.BasisOfCalculationPercentageSalesTax));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.PatientSalesTaxAmount, this.PatientSalesTaxAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.PlanSalesTaxAmount, this.PlanSalesTaxAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountOfCoinsurance, this.AmountOfCoinsurance));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BasisOfCalculationCoinsurance, this.BasisOfCalculationCoinsurance));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BenefitStageCount, this.BenefitStageCount.ToString()));
            if (this.BenefitStages != null)
                foreach (var benefitStage in this.BenefitStages)
                    returnValue.Append(benefitStage.ToNcpdpString());
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.EstimatedGenericSavings, this.EstimatedGenericSavings));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.SpendingAccountAmountRemaining, this.SpendingAccountAmountRemaining));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.HealthPlanFundedAssistanceAmount, this.HealthPlanFundedAssistanceAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountAttributedToProviderNetworkSelection, this.AmountAttributedToProviderNetworkSelection));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountAttributedToProductSelectionBrandDrug, this.AmountAttributedToProductSelectionBrandDrug));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountAttributedToProductSelectionNonPreferredFormularySelection, this.AmountAttributedToProductSelectionNonPreferredFormularySelection));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountAttributedToProductSelectionBrandNonPreferredFormularySelection, this.AmountAttributedToProductSelectionBrandNonPreferredFormularySelection));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.AmountAttributedToCoverageGap, this.AmountAttributedToCoverageGap));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.IngredientCostContractedReimbursableAmount, this.IngredientCostContractedReimbursableAmount));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.DispensingFeeContractedReimbursableAmount, this.DispensingFeeContractedReimbursableAmount));

            //Adds segment separator and identifier to beginning if the segment has data.
            if (returnValue.Length > 0)
            {
                returnValue.Insert(0, Utils.NcpdpString.ToNcpdpFieldString(() => this.SegmentIdentification, this.SegmentIdentification));
                returnValue.Insert(0, Utils.NcpdpString.SegmentSeparator);
            }

            return returnValue.ToString();
        }

        public class OtherAmountPaidContainer
        {
            /// <summary>
            /// Other Amount Paid Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 564-J3</para>
            /// <para>
            /// Code clarifying the value in the 'Other Amount Paid' (565-J4).
            /// </para>
            /// </remarks>
            [NcpdpField("564-J3")]
            public string OtherAmountPaidQualifier { get; set; }

            /// <summary>
            /// Other Amount Paid
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 565-J4</para>
            /// <para>
            /// Amount paid for additional costs claimed in 'Other Amount 
            /// Claimed Submitted' (480-H9).
            /// </para>
            /// </remarks>
            [NcpdpField("565-J4")]
            public decimal? OtherAmountPaid { get; set; }

            public string ToNcpdpString()
            {
                StringBuilder returnValue = new StringBuilder();

                //Append properties to returnValue.
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.OtherAmountPaidQualifier, this.OtherAmountPaidQualifier));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.OtherAmountPaid, this.OtherAmountPaid));
                return returnValue.ToString();
            }
        }

        public class BenefitStageContainer
        {
            /// <summary>
            /// Benefit Stage Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 393-MV</para>
            /// <para>Code qualifying the 'Benefit Stage Amount' (394-MW).</para>
            /// </remarks>
            [NcpdpField("393-MV")]
            public string BenefitStageQualifier { get; set; }

            /// <summary>
            /// Benefit Stage Amount
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 394-MW</para>
            /// <para>
            /// The amount of claim allocated to the Medicare stage identified by 
            /// the 'Benefit Stage Qualifier' (393-MV).
            /// </para>
            /// </remarks>
            [NcpdpField("394-MW")]
            public decimal? BenefitStageAmount { get; set; }

            public string ToNcpdpString()
            {
                StringBuilder returnValue = new StringBuilder();

                //Append properties to returnValue.
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.BenefitStageQualifier, this.BenefitStageQualifier));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldStringFromCurrency(() => this.BenefitStageAmount, this.BenefitStageAmount));
                return returnValue.ToString();
            }
        }
    }
}
