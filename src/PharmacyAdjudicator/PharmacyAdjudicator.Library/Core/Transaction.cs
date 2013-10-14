using System;
using Csla;

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

        public static readonly PropertyInfo<decimal> CopayProperty = RegisterProperty<decimal>(c => c.Copay);
        [Inferrable]
        public decimal Copay
        {
            get { return GetProperty(CopayProperty); }
            set { SetProperty(CopayProperty, value); }
        }

        public static readonly PropertyInfo<bool> FormularyProperty = RegisterProperty<bool>(c => c.Formulary);
        [Inferrable]
        [Fact]
        public bool Formulary
        {
            get { return GetProperty(FormularyProperty); }
            set { SetProperty(FormularyProperty, value); }
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
            this.Id = Guid.NewGuid().ToString();
            this.Drug = drug;
            this.Formulary = false;
            MarkOld();
        }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
            Id = Guid.NewGuid().ToString();
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
