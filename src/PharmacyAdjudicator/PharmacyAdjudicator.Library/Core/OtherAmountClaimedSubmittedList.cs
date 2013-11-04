using System;
using System.Collections.Generic;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class OtherAmountClaimedSubmittedList :
      BusinessListBase<OtherAmountClaimedSubmittedList, OtherAmountClaimedSubmitted>
    {
        #region Factory Methods

        internal static OtherAmountClaimedSubmittedList NewOtherAmountList()
        {
            return DataPortal.CreateChild<OtherAmountClaimedSubmittedList>();
        }

        internal static OtherAmountClaimedSubmittedList GetEditableChildList(
          object childData)
        {
            return DataPortal.FetchChild<OtherAmountClaimedSubmittedList>(childData);
        }

        private OtherAmountClaimedSubmittedList()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;
            foreach (var child in (IList<object>)childData)
                this.Add(OtherAmountClaimedSubmitted.GetOtherAmount(child));
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
