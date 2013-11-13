using System;
using System.Collections.Generic;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class AtomList :
      BusinessListBase<AtomList, Atom>
    {
        #region Factory Methods

        public static AtomList NewAtomList()
        {
            return DataPortal.CreateChild<AtomList>();
        }

        internal static AtomList GetEditableChildList(
          object childData)
        {
            return DataPortal.FetchChild<AtomList>(childData);
        }

        private AtomList()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;
            //foreach (var child in (IList<object>)childData)
            //    this.Add(EditableChild.GetEditableChild(child));
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
