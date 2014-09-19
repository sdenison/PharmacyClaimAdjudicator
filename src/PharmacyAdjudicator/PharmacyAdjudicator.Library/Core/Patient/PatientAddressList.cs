using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Patient
{
    [Serializable]
    public class PatientAddressList : BusinessListBase<PatientAddressList, PatientAddress>
    {
        private long _patientId;

        #region Authorization Rules

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            //Requires that the user be in the RuleManager role to create, edit or delete an Patient object
            Csla.Rules.BusinessRules.AddRule(typeof(PatientAddressList), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
            Csla.Rules.BusinessRules.AddRule(typeof(PatientAddressList), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(PatientAddressList), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(PatientAddressList), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "RuleManager", "Manager", "Admin"));
        }

        #endregion

        #region Factory Methods

        public static PatientAddressList NewPatientAddressList(long patientId)
        {
            return DataPortal.Create<PatientAddressList>(patientId);
        }

        public static PatientAddressList GetByPatientId(long patientId)
        {
            return DataPortal.Fetch<PatientAddressList>(patientId);
        }

        private PatientAddressList()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access



        private void DataPortal_Create(long patientId)
        {
            _patientId = patientId;
        }

        public void Add(PatientAddress patientAddress)
        {
            if (this.ContainsAddressType(patientAddress))
                throw new InvalidOperationException("Cannot add address to list with an address type that already exists.");
            else
            {
                patientAddress.Slot = this.Count + 1;
                base.Add(patientAddress);
            }
        }

        public void Remove(PatientAddress patientAddress)
        {
            var slotsToAdjust = (from p in this where p.Slot > patientAddress.Slot select p);
            foreach (var patient in slotsToAdjust)
                patient.Slot--;
            base.Remove(patientAddress);
        }

        public bool ContainsAddressType(PatientAddress patientAddress)
        {
            foreach (var address in this)
                if (address.AddressType == patientAddress.AddressType)
                    return true;
            return false;
        }

        public bool ContainsAddressType(Library.Core.Enums.AddressType addressType)
        {
            foreach (var address in this)
                if (address.AddressType == addressType)
                    return true;
            return false;
        }

        private void Child_Fetch(long patientId)
        {
            Fetch(patientId);
        }

        private void DataPortal_Fetch(long patientId)
        {
            Fetch(patientId);
        }

        private void Fetch(long patientId)
        {
            RaiseListChangedEvents = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Return all addresses that have not been deleted.
                var addresses = (from pa in ctx.DbContext.PatientAddress
                                 where pa.PatientId == patientId
                                 && pa.Retraction == false
                                 && !ctx.DbContext.PatientAddress.Any(retractionPatientAddress =>
                                        retractionPatientAddress.Retraction == true
                                        && retractionPatientAddress.OriginalFactRecordId == pa.RecordId)
                                 orderby pa.Slot
                                 select pa);
                foreach (var addressData in addresses)
                    this.Add(DataPortal.FetchChild<PatientAddress>(addressData));
            }
            RaiseListChangedEvents = true;
        }

        protected void Child_Update()
        {
            SaveChanges();
        }

        protected override void DataPortal_Update()
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;

                //Delete items that have been removed.
                foreach (var item in this.DeletedList)
                    DataPortal.UpdateChild(item);

                foreach (var item in this)
                    if (item.IsSavable)
                        DataPortal.UpdateChild(item);

                if(!IsChild)
                    ctx.DbContext.SaveChanges();
                RaiseListChangedEvents = rlce;
            }
        }

        #endregion
    }
}
