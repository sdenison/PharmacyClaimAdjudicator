using System;
using System.Collections.Generic;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class DrugList : ReadOnlyListBase<DrugList, Drug>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(DrugList), "Role");
        }

        #endregion

        #region Factory Methods

        public static DrugList GetAll()
        {
            return DataPortal.Fetch<DrugList>();
        }

        public static DrugList GetByBrandName(string brandName)
        {
            return DataPortal.Fetch<DrugList>(brandName);
        }

        private DrugList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(string brandName)
        {
            //RaiseListChangedEvents = false;
            //IsReadOnly = false;

            //using (var ctx = PharmacyAdjudicator.Dal.DalFactory.GetManager())
            //{
            //    var dal = ctx.GetProvider<PharmacyAdjudicator.Dal.IDrugDal>();
            //    var data = dal.FetchByBrandName(brandName);
            //    foreach (var drugDto in data)
            //    {
            //        Add(new Drug(drugDto));
            //    }
            //}

            //IsReadOnly = true;
            //RaiseListChangedEvents = true;
        }

        #endregion
    }
}
