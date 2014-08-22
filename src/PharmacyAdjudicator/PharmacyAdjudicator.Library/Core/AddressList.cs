using System;
using System.Collections.Generic;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class AddressList : BusinessListBase<AddressList, Address>
    {
        #region Factory Methods

        internal static AddressList NewEditableChildList()
        {
            return DataPortal.CreateChild<AddressList>();
        }

        internal static AddressList GetEditableChildList(object childData)
        {
            return DataPortal.FetchChild<AddressList>(childData);
        }

        private AddressList()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(IEnumerable<DataAccess.Address> addresses)
        {
            RaiseListChangedEvents = false;
            foreach (var addressData in addresses)
                this.Add(DataPortal.FetchChild<Address>(addressData));
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
