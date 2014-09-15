using System;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class PatientAddress : BusinessBase<PatientAddress>
    {
        #region Business Methods

        public static readonly PropertyInfo<long> PatientIdProperty = RegisterProperty<long>(c => c.PatientId);
        public long PatientId
        {
            get { return GetProperty(PatientIdProperty); }
            private set { LoadProperty(PatientIdProperty, value); }
        }

        public static readonly PropertyInfo<Enums.AddressType> AddressTypeProperty = RegisterProperty<Enums.AddressType>(c => c.AddressType);
        public Enums.AddressType AddressType
        {
            get { return GetProperty(AddressTypeProperty); }
            set { SetProperty(AddressTypeProperty, value); }
        }

        public static readonly PropertyInfo<int> SlotProperty = RegisterProperty<int>(c => c.Slot);
        public int Slot
        {
            get { return GetProperty(SlotProperty); }
            set { SetProperty(SlotProperty, value); }
        }

        //public static readonly PropertyInfo<Address> AddressProperty = RegisterProperty<Address>(c => c.Address, RelationshipTypes.Child);
        //public Address Address
        //{
        //    get { return GetProperty(AddressProperty); }
        //    set { SetProperty(AddressProperty, value); }
        //}

        public static readonly PropertyInfo<Address> AddressProperty = RegisterProperty<Address>(c => c.Address, RelationshipTypes.Child);
        public Address Address
        {
            get { return GetProperty(AddressProperty); }
            private set { SetProperty(AddressProperty, value); }
        }

        private Guid _recordId;

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        public static PatientAddress NewAddress(long patientId)
        {
            return DataPortal.CreateChild<PatientAddress>(patientId);
        }

        private PatientAddress()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void Child_Create()
        {
            _recordId = Guid.NewGuid();
            this.Address = DataPortal.CreateChild<Address>();
            base.Child_Create();
        }

        [RunLocal]
        protected void Child_Create(long patientId)
        {
            this.PatientId = patientId;
            _recordId = Guid.NewGuid();
            this.Address = DataPortal.CreateChild<Address>();
            base.Child_Create();
        }

        private void Child_Fetch(DataAccess.PatientAddress addressData)
        {
            using(BypassPropertyChecks)
            {
                _recordId = addressData.RecordId;
                this.PatientId = addressData.PatientId;
                this.AddressType = (Enums.AddressType)Enum.Parse(typeof(Enums.AddressType), addressData.AddressTypeCode);
                this.Address = DataPortal.FetchChild<Address>(addressData.Address);
            }
        }

        private void Child_Insert(Patient parent)
        {
            AssertNewFact();
        }

        private void Child_Update(Patient parent)
        {
            RetractFact();
            this._recordId = Guid.NewGuid();
            AssertNewFact();
        }

        private void Child_DeleteSelf()
        {
            RetractFact();
        }

        private void AssertNewFact()
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    FieldManager.UpdateChildren(this);
                    var patientAddressData = CreateEntity();
                    ctx.DbContext.PatientAddress.Add(patientAddressData);
                }
            }
        }

        private void RetractFact()
        {
            using (BypassPropertyChecks)
            {
                var patientAddressData = CreateEntity();
                patientAddressData.Retraction = true;
                patientAddressData.OriginalFactRecordId = _recordId;
                _recordId = Guid.NewGuid();
                patientAddressData.RecordId = _recordId;
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    ctx.DbContext.PatientAddress.Add(patientAddressData);
                }
            }
        }

        private DataAccess.PatientAddress CreateEntity()
        {
            var addressData = new DataAccess.PatientAddress();
            addressData.RecordId = _recordId;
            addressData.PatientId = this.PatientId;
            addressData.AddressTypeCode = this.AddressType.ToString();
            addressData.AddressId = this.Address.AddressId;
            addressData.Retraction = false;
            addressData.RecordCreatedDateTime = DateTime.UtcNow;
            addressData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            addressData.Slot = this.Slot;
            return addressData;
        }

        #endregion
    }
}
