using System;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class OtherAmountClaimedSubmitted : BusinessBase<OtherAmountClaimedSubmitted>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> QualifierProperty = RegisterProperty<string>(c => c.Qualifier);
        [NcpdpField("479-H8")]
        public string Qualifier
        {
            get { return GetProperty(QualifierProperty); }
            set { SetProperty(QualifierProperty, value); }
        }

        public static readonly PropertyInfo<decimal> OtherAmountClaimedProperty = RegisterProperty<decimal>(c => c.OtherAmountClaimed);
        [NcpdpField("480-H9")]
        public decimal OtherAmountClaimed
        {
            get { return GetProperty(OtherAmountClaimedProperty); }
            set { SetProperty(OtherAmountClaimedProperty, value); }
        }

        #endregion

        #region Factory Methods

        internal static OtherAmountClaimedSubmitted NewOtherAmount()
        {
            return DataPortal.CreateChild<OtherAmountClaimedSubmitted>();
        }

        internal static OtherAmountClaimedSubmitted GetOtherAmount(object childData)
        {
            return DataPortal.FetchChild<OtherAmountClaimedSubmitted>(childData);
        }

        private OtherAmountClaimedSubmitted()
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
