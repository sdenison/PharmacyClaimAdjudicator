using System;
using System.Collections.Generic;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class OtherAmountPaidList :
      BusinessListBase<OtherAmountPaidList, OtherAmountPaid>
    {
        #region Factory Methods

        internal static OtherAmountPaidList NewOtherAmountList()
        {
            return DataPortal.CreateChild<OtherAmountPaidList>();
        }

        internal static OtherAmountPaidList GetEditableChildList(
          object childData)
        {
            return DataPortal.FetchChild<OtherAmountPaidList>(childData);
        }

        private OtherAmountPaidList()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;
            foreach (var child in (IList<object>)childData)
                this.Add(OtherAmountPaid.GetOtherAmount(child));
            RaiseListChangedEvents = true;
        }

        #endregion

    }
}
