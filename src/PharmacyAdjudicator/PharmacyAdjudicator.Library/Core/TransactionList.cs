using System;
using System.Collections.Generic;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class TransactionList :
      BusinessListBase<TransactionList, Transaction>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(TransactionList), "Role");
        }

        #endregion

        #region Factory Methods

        public static TransactionList NewEditableRootList()
        {
            return DataPortal.Create<TransactionList>();
        }

        public static TransactionList GetEditableRootList(int id)
        {
            return DataPortal.Fetch<TransactionList>(id);
        }

        private TransactionList()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(int criteria)
        {
            //RaiseListChangedEvents = false;
            //// TODO: load values into memory
            //object childData = null;
            //foreach (var item in (List<object>)childData)
            //    this.Add(Transaction.GetEditableChild(childData));
            //RaiseListChangedEvents = true;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // TODO: open database, update values
            //base.Child_Update();
        }

        #endregion
    }
}
