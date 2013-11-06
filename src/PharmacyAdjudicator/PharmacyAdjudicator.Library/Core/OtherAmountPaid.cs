using System;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class OtherAmountPaid : BusinessBase<OtherAmountPaid>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> QualifierProperty = RegisterProperty<string>(c => c.Qualifier);
        [NcpdpField("564-J3")]
        public string Qualifier
        {
            get { return GetProperty(QualifierProperty); }
            set { SetProperty(QualifierProperty, value); }
        }

        public static readonly PropertyInfo<decimal> OtherAmountClaimedProperty = RegisterProperty<decimal>(c => c.OtherAmountClaimed);
        [NcpdpField("565-J4")]
        public decimal OtherAmountClaimed
        {
            get { return GetProperty(OtherAmountClaimedProperty); }
            set { SetProperty(OtherAmountClaimedProperty, value); }
        }

        #endregion

        #region Factory Methods

        internal static OtherAmountPaid NewOtherAmount()
        {
            return DataPortal.CreateChild<OtherAmountPaid>();
        }

        internal static OtherAmountPaid GetOtherAmount(object childData)
        {
            return DataPortal.FetchChild<OtherAmountPaid>(childData);
        }

        private OtherAmountPaid()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        

        private void Child_Fetch(object childData)
        {
            // TODO: load values
        }

        private void Child_Insert(object parent)
        {
            // TODO: insert values
        }

        private void Child_Update(object parent)
        {
            // TODO: update values
        }

        private void Child_DeleteSelf(object parent)
        {
            // TODO: delete values
        }

        #endregion
    }
}
