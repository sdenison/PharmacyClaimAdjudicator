using System;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class Drug : ReadOnlyBase<Drug>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> NdcProperty = RegisterProperty<string>(c => c.Ndc);
        public string Ndc
        {
            get { return GetProperty(NdcProperty); }
            private set { LoadProperty(NdcProperty, value); }
        }

        public static readonly PropertyInfo<string> BrandNameProperty = RegisterProperty<string>(p => p.BrandName);
        public string BrandName
        {
            get { return GetProperty(BrandNameProperty); }
            private set { LoadProperty(BrandNameProperty, value); }
        }

        public static readonly PropertyInfo<string> UpnProperty = RegisterProperty<string>(c => c.Upn);
        public string Upn
        {
            get { return GetProperty(UpnProperty); }
            private set { LoadProperty(UpnProperty, value); }
        }

        public static readonly PropertyInfo<string> VaClassProperty = RegisterProperty<string>(c => c.VaClass);
        public string VaClass
        {
            get { return ReadProperty(VaClassProperty); }
            private set { LoadProperty(VaClassProperty, value); }
        }

        public static readonly PropertyInfo<string> PkgTypeProperty = RegisterProperty<string>(c => c.PkgType);
        public string PkgType
        {
            get { return GetProperty(PkgTypeProperty); }
            private set { LoadProperty(PkgTypeProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        public static Drug GetByNdc(string ndc)
        {
            return DataPortal.Fetch<Drug>(ndc);
        }

        private Drug()
        { /* require use of factory methods */ }

        //internal Drug(Dal.DrugDto drugDto)
        //{ 
        //    //PopulateByDto(drugDto); 
        //}

        #endregion

        #region Data Access

        private void DataPortal_Fetch(string ndc)
        {
            
            

            //uncomment to use repository pattern
            //using (var ctx = PharmacyAdjudicator.Dal.DalFactory.GetManager())
            //{
            //    var dal = ctx.GetProvider<PharmacyAdjudicator.Dal.IDrugDal>();
            //    var data = dal.FetchByNdc(ndc);
            //    if (data == null)
            //        throw new Dal.DataNotFoundException("Ndc = " + ndc);
            //    PopulateByDto(data);
            //}


        }

        //private void PopulateByDto(PharmacyAdjudicator.Dal.DrugDto data)
        //{
        //    this.Ndc = data.Ndc;
        //    this.Upn = data.Upn;
        //    this.BrandName = data.BrandName;
        //    this.VaClass = data.VaClass;
        //    this.PkgType = data.PkgType;
        //}

        #endregion
    }
}
