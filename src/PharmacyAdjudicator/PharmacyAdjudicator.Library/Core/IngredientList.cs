using System;
using System.Collections.Generic;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class IngredientList :
      BusinessListBase<IngredientList, Ingredient>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(IngredientList), "Role");
        }

        #endregion

        #region Factory Methods

        public static IngredientList NewEditableRootList()
        {
            return DataPortal.Create<IngredientList>();
        }

        public static IngredientList GetEditableRootList(int id)
        {
            return DataPortal.Fetch<IngredientList>(id);
        }

        private IngredientList()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(int criteria)
        {
            //RaiseListChangedEvents = false;
            //// TODO: load values into memory
            //object childData = null;
            //foreach (var item in (List<object>)childData)
            //    this.Add(EditableChild.GetEditableChild(childData));
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
