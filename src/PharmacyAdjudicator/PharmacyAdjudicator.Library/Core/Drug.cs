using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class Drug : ReadOnlyBase<Drug>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> NdcProperty = RegisterProperty<string>(c => c.Ndc);
        [Fact]
        public string Ndc
        {
            get { return GetProperty(NdcProperty); }
            private set { LoadProperty(NdcProperty, value); }
        }

        public static readonly PropertyInfo<string> BrandNameProperty = RegisterProperty<string>(p => p.BrandName);
        [Fact]
        public string BrandName
        {
            get { return GetProperty(BrandNameProperty); }
            private set { LoadProperty(BrandNameProperty, value); }
        }

        public static readonly PropertyInfo<string> UpnProperty = RegisterProperty<string>(c => c.Upn);
        [Fact]
        public string Upn
        {
            get { return GetProperty(UpnProperty); }
            private set { LoadProperty(UpnProperty, value); }
        }

        public static readonly PropertyInfo<string> VaClassProperty = RegisterProperty<string>(c => c.VaClass);
        [Fact]
        public string  VaClass
        {
            get { return ReadProperty(VaClassProperty); }
            private set { LoadProperty(VaClassProperty, value); }
        }

        public static readonly PropertyInfo<string> PkgTypeProperty = RegisterProperty<string>(c => c.PkgType);
        [Fact]
        public string PkgType
        {
            get { return GetProperty(PkgTypeProperty); }
            private set { LoadProperty(PkgTypeProperty, value); }
        }

        public static readonly PropertyInfo<string> DosageFormProperty = RegisterProperty<string>(c => c.DosageForm);
        [Fact]
        public string DosageForm
        {
            get { return GetProperty(DosageFormProperty); }
            private set { LoadProperty(DosageFormProperty, value); }
        }

        public static readonly PropertyInfo<string> ScheduleProperty = RegisterProperty<string>(c => c.Schedule);
        public string Schedule
        {
            get { return GetProperty(ScheduleProperty); }
            private set { LoadProperty(ScheduleProperty, value); }
        }

        public static readonly PropertyInfo<bool> OtcProperty = RegisterProperty<bool>(c => c.Otc);
        public bool Otc
        {
            get { return GetProperty(OtcProperty); }
            private set { LoadProperty(OtcProperty, value); }
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

        public async Task<Drug> GetByNdcAsync(string ndc)
        {
            return await DataPortal.FetchAsync<Drug>(ndc);
        }

#if !SILVERLIGHT && !NET_FX
        public static Drug GetByNdc(string ndc)
        {
            return DataPortal.Fetch<Drug>(ndc);
        }
#endif

        private Drug()
        { /* require use of factory methods */ }

        internal Drug(DataAccess.VaDrug vaDrug)
        {
            PopulateByEntity(vaDrug);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(string ndc)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var drug = (from d in ctx.DbContext.VaDrugs
                            where d.NdfNdc == ndc
                            select d).FirstOrDefault();
                if (drug == null)
                    throw new DataNotFoundException("ndc = " + ndc);
                PopulateByEntity(drug);
            }
        }

        private void PopulateByEntity(PharmacyAdjudicator.DataAccess.VaDrug vaDrug)
        {
            this.Ndc = vaDrug.NdfNdc;
            this.Upn = vaDrug.Upn == null ? string.Empty : vaDrug.Upn;
            this.BrandName = vaDrug.VaProduct;
            this.VaClass = vaDrug.VaClass;
            this.PkgType = vaDrug.PkgType;
            this.DosageForm = vaDrug.DoseForm;
            this.Schedule = vaDrug.Csfs;
            this.Otc = ConvertVaOtc(vaDrug.RxOtc);
        }

        private bool ConvertVaOtc(string otcString)
        {
            if (otcString.StartsWith("O"))
                return true;
            else
                return false;
        }

        #endregion
    }
}
