using System;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class Claim : BusinessBase<Claim>
    {
        #region Business Methods


        public static readonly PropertyInfo<TransactionList> TransactionsProperty = RegisterProperty<TransactionList>(p => p.Transactions, RelationshipTypes.PrivateField);
        private TransactionList _Transactions;
        public TransactionList Transactions
        {
            get { return GetProperty(TransactionsProperty, _Transactions); }
            set { SetProperty(TransactionsProperty, ref _Transactions, value); }
        }

        // example with private backing field
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, RelationshipTypes.PrivateField);
        private int _Id = IdProperty.DefaultValue;
        public int Id
        {
            get { return GetProperty(IdProperty, _Id); }
            set { SetProperty(IdProperty, ref _Id, value); }
        }

        // example with managed backing field
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
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

        public static Claim NewEditableRoot()
        {
            return DataPortal.Create<Claim>();
        }

        public static Claim GetEditableRoot(int id)
        {
            return DataPortal.Fetch<Claim>(id);
        }

        public static void DeleteEditableRoot(int id)
        {
            DataPortal.Delete<Claim>(id);
        }

        private Claim()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(int criteria)
        {
            // TODO: load values
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            // TODO: insert values
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // TODO: update values
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.Id);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int criteria)
        {
            // TODO: delete values
        }

        #endregion
    }
}
