using System;
using System.Collections.Generic;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class ClaimList :
      BusinessListBase<ClaimList, Claim>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(ClaimList), "Role");
        }

        #endregion

        #region Factory Methods

        public static ClaimList NewEditableRootList()
        {
            return DataPortal.Create<ClaimList>();
        }

        public static ClaimList GetEditableRootList(int id)
        {
            return DataPortal.Fetch<ClaimList>(id);
        }

        private ClaimList()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(int criteria)
        {
            //RaiseListChangedEvents = false;
            //// TODO: load values into memory
            //object childData = null;
            //foreach (var item in (List<object>)childData)
            //    this.Add(Claim.GetEditableRoot(childData));
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
