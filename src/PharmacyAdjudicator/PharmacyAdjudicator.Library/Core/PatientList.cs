using System;
using System.Collections.Generic;
using Csla;
using System.Linq;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class PatientList : BusinessListBase<PatientList, Patient>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(PatientList), "Role");
            //Csla.Rules.BusinessRules.AddRule(typeof(PatientList), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager")); 
        }

        #endregion

        #region Factory Methods

        public static PatientList NewPatientList()
        {
            return DataPortal.Create<PatientList>();
        }

        public static PatientList GetByLastName(string lastName)
        {
            return DataPortal.Fetch<PatientList>(lastName);
        }

        public static PatientList GetAll()
        {
            return DataPortal.Fetch<PatientList>();
        }

        private PatientList()
        { /* Require use of factory methods */ }

        #endregion

#if !WINDOWS_PHONE
        //public async static System.Threading.Tasks.Task<PatientList> GetPatientList()
        //{
        //    //return await DataPortal.FetchAsync<PatientList>();
        //}
#endif

        #region Data Access

        private void DataPortal_Fetch(string lastName)
        { 
            using (var ctx = new DataAccess.PharmacyClaimAdjudicatorEntities())
            {
                var data = (from p in ctx.PatientFacts
                           where p.LastName.StartsWith(lastName)
                           && p.RecordId == (from p2 in ctx.PatientFacts
                                            where p2.PatientId == p.PatientId
                                            && p2.Retraction == false
                                            && !ctx.PatientFacts.Any(p3 => p3.PatientId == p2.PatientId
                                                                     && p3.Retraction == true
                                                                     && p3.OriginalFactRecordId == p2.RecordId)
                                            select p2.RecordId).Max()
                           orderby p.LastName
                           select p);
                RaiseListChangedEvents = false;
                foreach (var item in data)
                {
                    this.Add(DataPortal.FetchChild<Patient>(item));
                }
                RaiseListChangedEvents = true;
            }
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = new DataAccess.PharmacyClaimAdjudicatorEntities())
            {
                var data = (from p in ctx.PatientFacts
                            where p.RecordId == (from p2 in ctx.PatientFacts
                                              where p2.PatientId == p.PatientId
                                              && p2.Retraction == false
                                              && !ctx.PatientFacts.Any(p3 => p3.PatientId == p2.PatientId
                                                                       && p3.Retraction == true
                                                                       && p3.OriginalFactRecordId == p2.RecordId)
                                              select p2.RecordId).Max()
                            orderby p.PatientId
                            select p);
                RaiseListChangedEvents = false;
                foreach (var item in data)
                {
                    this.Add(DataPortal.FetchChild<Patient>(item));
                }
                RaiseListChangedEvents = true;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;
            //using (var ctx = //PharmacyAdjudicator.Dal.DalFactory.GetManager())
            //    Child_Update();
            //this.RaiseListChangedEvents = true;
        }

        #endregion
    }
}
