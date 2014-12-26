using System;
using System.Reflection;
using Csla;
using PharmacyAdjudicator.Library.Core.Patient;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class Transaction : BusinessBase<Transaction>
    {
        #region Business Methods

        [ComplexFact]
        public Drug Drug { get; private set; }
        //public Patient Patient { get; private set; }
        //public Pharmacy Pharmacy { get; private set; }

        public static readonly PropertyInfo<decimal> IngredientCostSubmittedProperty = RegisterProperty<decimal>(c => c.IngredientCostSubmitted);
        [Fact]
        [NcpdpField("409-D9")]
        public decimal IngredientCostSubmitted
        {
            get { return GetProperty(IngredientCostSubmittedProperty); }
            set { SetProperty(IngredientCostSubmittedProperty, value); }
        }

        public static readonly PropertyInfo<decimal> AmountOfCopayProperty = RegisterProperty<decimal>(c => c.AmountOfCopay);
        [Inferrable]
        [NcpdpField("518-FI")]
        public decimal AmountOfCopay
        {
            get { return GetProperty(AmountOfCopayProperty); }
            set { SetProperty(AmountOfCopayProperty, value); }
        }

        public static readonly PropertyInfo<bool> FormularyProperty = RegisterProperty<bool>(c => c.Formulary);
        [Inferrable]
        [Fact]
        public bool Formulary
        {
            get { return GetProperty(FormularyProperty); }
            set { SetProperty(FormularyProperty, value); }
        }

        public static readonly PropertyInfo<Enums.ResponseStatus> ResponseStatusProperty = RegisterProperty<Enums.ResponseStatus>(c => c.ResponseStatus);
        [NcpdpField("111-AM")]
        [Inferrable]
        public Enums.ResponseStatus ResponseStatus
        {
            get { return GetProperty(ResponseStatusProperty); }
            set { SetProperty(ResponseStatusProperty, value); }
        }

        public static readonly PropertyInfo<string> AuthorizationNumberProperty = RegisterProperty<string>(c => c.AuthorizationNumber);
        [NcpdpField("112-AN")]
        public string AuthorizationNumber
        {
            get { return GetProperty(AuthorizationNumberProperty); }
            private set { LoadProperty(AuthorizationNumberProperty, value); }
        }

        public static readonly PropertyInfo<string> PrescriptionNumberIdQualifierProperty = RegisterProperty<string>(c => c.PrescriptionNumberIdQualifier);
        [NcpdpField("455-EM")]
        public string PrescriptionNumberIdQualifier
        {
            get { return GetProperty(PrescriptionNumberIdQualifierProperty); }
            set { SetProperty(PrescriptionNumberIdQualifierProperty, value); }
        }

        public static readonly PropertyInfo<string> PrescriptionNumberProperty = RegisterProperty<string>(c => c.PrescriptionNumber);
        [NcpdpField("402-D2")]
        public string PrescriptionNumber
        {
            get { return GetProperty(PrescriptionNumberProperty); }
            set { SetProperty(PrescriptionNumberProperty, value); }
        }

        [NcpdpField("505-F5")]
        public decimal PatientPayAmount
        {
            get
            {
                return this.AmountOfCopay + this.PatientPaySalesTaxAmount;
            }
            private set { }
        }

        [NcpdpField("523-FN")]
        public decimal AmountAttributedToSalesTax
        {
            get
            {
                return this.FlatSalesTaxAmountPaid + this.PercentageSalesTaxAmountPaid;
            }
        }

        public static readonly PropertyInfo<decimal> PercentageSalesTaxAmountPaidProperty = RegisterProperty<decimal>(c => c.PercentageSalesTaxAmountPaid);
        [NcpdpField("559-AX")]
        [Inferrable]
        public decimal PercentageSalesTaxAmountPaid
        {
            get { return GetProperty(PercentageSalesTaxAmountPaidProperty); }
            set { SetProperty(PercentageSalesTaxAmountPaidProperty, value); }
        }

        public static readonly PropertyInfo<decimal> PatientPaySalesTaxAmountProperty = RegisterProperty<decimal>(c => c.PatientPaySalesTaxAmount);
        [NcpdpField("575-EQ")]
        [Inferrable]
        [Fact]
        public decimal PatientPaySalesTaxAmount
        {
            get { return GetProperty(PatientPaySalesTaxAmountProperty); }
            set { SetProperty(PatientPaySalesTaxAmountProperty, value); }
        }

        public static readonly PropertyInfo<decimal> FlatSalesTaxAmountPaidProperty = RegisterProperty<decimal>(c => c.FlatSalesTaxAmountPaid);
        [NcpdpField("558-AW")]
        [Inferrable]
        [Fact]
        public decimal FlatSalesTaxAmountPaid
        {
            get { return GetProperty(FlatSalesTaxAmountPaidProperty); }
            set { SetProperty(FlatSalesTaxAmountPaidProperty, value); }
        }

        public static readonly PropertyInfo<decimal> IngredientCostPaidProperty = RegisterProperty<decimal>(c => c.IngredientCostPaid);
        [NcpdpField("506-F6")]
        [Inferrable]
        public decimal IngredientCostPaid
        {
            get { return GetProperty(IngredientCostPaidProperty); }
            set { SetProperty(IngredientCostPaidProperty, value); }
        }

        public static readonly PropertyInfo<decimal> DispensingFeePaidProperty = RegisterProperty<decimal>(c => c.DispensingFeePaid);
        [NcpdpField("507-F7")]
        [Inferrable]
        [Fact]
        public decimal DispensingFeePaid
        {
            get { return GetProperty(DispensingFeePaidProperty); }
            set { SetProperty(DispensingFeePaidProperty, value); }
        }

        public static readonly PropertyInfo<Enums.TaxExemptIndicator> TaxExemptIndicatorProperty = RegisterProperty<Enums.TaxExemptIndicator>(c => c.TaxExemptIndicator);
        [NcpdpField("557-AV")]
        [Inferrable]
        public Enums.TaxExemptIndicator TaxExemptIndicator
        {
            get { return GetProperty(TaxExemptIndicatorProperty); }
            set 
            { 
                int intVal;
                if (int.TryParse(value.ToString(), out intVal))
                    SetProperty(TaxExemptIndicatorProperty, (Enums.TaxExemptIndicator) intVal); 
                else
                    SetProperty(TaxExemptIndicatorProperty, Enums.TaxExemptConverter.Parse(value.ToString()));
                //SetProperty(TaxExemptIndicatorProperty, Enums.TaxExemptConverter.Parse(value)); 
            }
        }

        public static readonly PropertyInfo<decimal> TotalAmountPaidProperty = RegisterProperty<decimal>(c => c.TotalAmountPaid);
        [NcpdpField("509-F9")]
        [Inferrable]
        public decimal TotalAmountPaid
        {
            get { return GetProperty(TotalAmountPaidProperty); }
            set { SetProperty(TotalAmountPaidProperty, value); }
        }

        public static readonly PropertyInfo<Enums.BasisOfReimbursement> BasisOfReimbursementProperty = RegisterProperty<Enums.BasisOfReimbursement>(c => c.BasisOfReimbursement);
        [NcpdpField("522-FM")]
        [Inferrable]
        [Fact]
        public Enums.BasisOfReimbursement BasisOfReimbursement
        {
            get { return GetProperty(BasisOfReimbursementProperty); }
            set { SetProperty(BasisOfReimbursementProperty, value); }
        }

        [NcpdpLoop("OtherAmountClaimSubmitted")]
        public OtherAmountClaimedSubmittedList OtherAmountsClaimed
        {
            get;
            set;
        }

        [NcpdpLoop("OtherAmountPaid")]
        public OtherAmountPaidList OtherAmountsPaid
        {
            get;
            set;
        }

        public static readonly PropertyInfo<string> IdProperty = RegisterProperty<string>(c => c.Id);
        public string Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        public static Transaction NewTransaction(Drug drug)
        {

            return new Transaction(drug);
            //return DataPortal.Create<Transaction>(patient, drug, pharmacy);
        }

        public static Transaction GetEditableRoot(int id)
        {
            return DataPortal.Fetch<Transaction>(id);
        }

        public static void DeleteEditableRoot(int id)
        {
            DataPortal.Delete<Transaction>(id);
        }

        private Transaction()
        { /* Require use of factory methods */ }

        public Transaction(Drug drug)
        {
            this.Id = Utils.GuidHelper.GenerateComb().ToString();
            this.Drug = drug;
            this.Formulary = false;
            MarkOld();
        }

        public Transaction(Drug drug, PatientEdit patient, D0.Submitted.ClaimBilling claim)
        {
            this.Id = Utils.GuidHelper.GenerateComb().ToString();
            this.AuthorizationNumber = Transaction.AssignNewAuthorizationNumber();
            this.Drug = drug;
            this.Formulary = false;
            this.OtherAmountsClaimed = OtherAmountClaimedSubmittedList.NewOtherAmountList();
            this.OtherAmountsPaid = OtherAmountPaidList.NewOtherAmountList();
            BindClaimBilling(claim);
        }

        public static string AssignNewAuthorizationNumber()
        {
            return "123456789123456789";
        }

        private void BindClaimBilling(D0.Submitted.ClaimBilling claim)
        {
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (Attribute.IsDefined(propertyInfo, typeof(NcpdpFieldAttribute)))
                {
                    var ncpdpField = (NcpdpFieldAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(NcpdpFieldAttribute));
                    // 111-AM is SegmentIdentification and should not be bound
                    if (!ncpdpField.NcpdpFieldName.Equals("111-AM"))
                    {
                        var value = ncpdpField.FindPropertyInObject(claim);
                        if (value != null)
                            propertyInfo.SetValue(this, value);
                    }
                }
                else if (Attribute.IsDefined(propertyInfo, typeof(NcpdpLoopAttribute)))
                {
                    //TODO: Add way to handle NCPDP Loops rather than creating list items and assigning values with known types
                    var ncpdpField = (NcpdpLoopAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(NcpdpLoopAttribute));
                    if (ncpdpField.NcpdpFieldName.Equals("OtherAmountClaimSubmitted"))
                    {
                        foreach(var otherAmt in claim.Pricing.OtherAmountClaimedSubmittedList)
                        {
                            var transOtherAmt = OtherAmountClaimedSubmitted.NewOtherAmount();
                            transOtherAmt.Qualifier = otherAmt.OtherAmountClaimedSubmittedQualifier;
                            transOtherAmt.OtherAmountClaimed = otherAmt.OtherAmountClaimedSubmitted;
                            this.OtherAmountsClaimed.Add(transOtherAmt);
                        }

                        //For now just accept all other amounts
                        foreach(var otherAmt in claim.Pricing.OtherAmountClaimedSubmittedList)
                        {
                            var otherAmtPaid = OtherAmountPaid.NewOtherAmount();
                            otherAmtPaid.Qualifier = otherAmt.OtherAmountClaimedSubmittedQualifier;
                            otherAmtPaid.OtherAmountClaimed = otherAmt.OtherAmountClaimedSubmitted;
                            this.OtherAmountsPaid.Add(otherAmtPaid);
                        }
                        
                    }
                }
            }
        }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
            Id = Utils.GuidHelper.GenerateComb().ToString();
        }

        private void DataPortal_Fetch(int criteria)
        {
            // TODO: load values
        }

        protected override void DataPortal_Insert()
        {
            // TODO: insert values
        }

        protected override void DataPortal_Update()
        {
            // TODO: update values
        }

        protected override void DataPortal_DeleteSelf()
        {
            //DataPortal_Delete(this.Id);
        }

        private void DataPortal_Delete(int criteria)
        {
            // TODO: delete values
        }

        #endregion

    }
}
