using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using PharmacyAdjudicator.Library.Core.Patient;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class Address : BusinessBase<Address>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(c => c.Address1);
        [Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Address1 must be at least one character long")]
        public string Address1
        {
            get { return GetProperty(Address1Property); }
            set { SetProperty(Address1Property, value); }
        }

        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(c => c.Address2);
        public string Address2
        {
            get { return GetProperty(Address2Property); }
            set { SetProperty(Address2Property, value); }
        }

        public static readonly PropertyInfo<string> Address3Property = RegisterProperty<string>(c => c.Address3);
        public string Address3
        {
            get { return GetProperty(Address3Property); }
            set { SetProperty(Address3Property, value); }
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);
        [Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "City must be at least one character long")]
        public string City
        {
            get { return GetProperty(CityProperty); }
            set { SetProperty(CityProperty, value); }
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters long")]
        public string State
        {
            get { return GetProperty(StateProperty); }
            set { SetProperty(StateProperty, value); }
        }

        public static readonly PropertyInfo<string> ZipProperty = RegisterProperty<string>(c => c.Zip);
        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Zip must be 5 to 10 characters long")]
        public string Zip
        {
            get { return GetProperty(ZipProperty); }
            set { SetProperty(ZipProperty, value); }
        }

        public static readonly PropertyInfo<Guid> AddressIdProperty = RegisterProperty<Guid>(c => c.AddressId);
        public Guid AddressId
        {
            get { return GetProperty(AddressIdProperty); }
            private set { LoadProperty(AddressIdProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
            base.AddBusinessRules();
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        //internal static Address NewAddress()
        //{
        //    return DataPortal.CreateChild<Address>();
        //}

        internal static Address GetAddress(DataAccess.Address addressData)
        {
            return DataPortal.Fetch<Address>(addressData);
        }

        private Address()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        public void CheckAllRules()
        {
            var propertyNames = BusinessRules.CheckRules();
            foreach (var name in propertyNames)
                OnPropertyChanged(name);
        }

        protected override void Child_Create()
        {
            this.AddressId = Utils.GuidHelper.GenerateComb();
            base.Child_Create();
        }

        private void Child_Fetch(DataAccess.Address addressData)
        {
            using(BypassPropertyChecks)
                PopulateByEntity(addressData);
        }

        private void Child_Insert(PatientAddress parent)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var addressData = CreateNewEntity();
                ctx.DbContext.Address.Add(addressData);
            }
        }

        private void Child_Update(PatientAddress parent)
        {
            //Address table is insert only so an update is really a table insert.
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                this.AddressId = Utils.GuidHelper.GenerateComb();
                var addressData = CreateNewEntity();
                ctx.DbContext.Address.Add(addressData);
            }
        }

        private void Child_DeleteSelf(object parent)
        {
            throw new NotImplementedException("Library.Core.Address is not allowed to delete.");
        }

        private DataAccess.Address CreateNewEntity()
        {
            var addressData = new DataAccess.Address();
            addressData.AddressId = this.AddressId;
            addressData.Address1 = this.Address1;
            addressData.Address2 = this.Address2;
            addressData.Address3 = this.Address3;
            addressData.City = this.City;
            addressData.State = this.State;
            addressData.Zip = this.Zip;
            addressData.RecordCreatedDateTime = DateTime.UtcNow;
            addressData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return addressData;
        }

        private void PopulateByEntity(DataAccess.Address addressData)
        {
            this.AddressId = addressData.AddressId;
            this.Address1 = addressData.Address1;
            this.Address2 = addressData.Address2;
            this.Address3 = addressData.Address3;
            this.City = addressData.City;
            this.State = addressData.State;
            this.Zip = addressData.Zip;
        }

        #endregion
    }
}
