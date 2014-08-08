using System;
using System.Collections.Generic;
using Csla;
using System.Linq;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class PatientHistory :
      ReadOnlyListBase<PatientHistory, Patient>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(PatientHistory), "Role");
        }

        #endregion

        #region Factory Methods

        public static PatientHistory GetByPatientId(int patientId)
        {
            return DataPortal.Fetch<PatientHistory>(patientId);
        }

        private PatientHistory()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(int patientId)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            using (var ctx = new DataAccess.PharmacyClaimAdjudicatorEntities())
            {
                var patientFacts = (from p in ctx.PatientFact
                                   where p.PatientId == patientId
                                   orderby p.RecordId
                                   select p);
                if (patientFacts == null)
                    throw new DataNotFoundException("PatientId = patientId");
                foreach (var data in patientFacts)
                    Add(DataPortal.FetchChild<Patient>(data));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
