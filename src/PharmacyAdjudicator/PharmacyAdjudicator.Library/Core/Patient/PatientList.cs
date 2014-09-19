using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PharmacyAdjudicator.Library.Core.Patient
{
    [Serializable]
    public class PatientList : BusinessListBase<PatientList, PatientEdit>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(PatientList), "Role");
            //Csla.Rules.BusinessRules.AddRule(typeof(PatientList), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager")); 
        }

        #endregion

        public static async Task<PatientList> GetBySearchObjectAsync(PatientSearchCriteria criteria)
        {
            return await DataPortal.FetchAsync<PatientList>(criteria);
        }
        public static async Task<PatientList> GetAllAsync()
        {
            return await DataPortal.FetchAsync<PatientList>();
        }

#if !SILVERLIGHT
        #region Factory Methods

        public static PatientList NewPatientList()
        {
            return DataPortal.Create<PatientList>();
        }

        public static PatientList GetByLastName(string lastName)
        {
            return DataPortal.Fetch<PatientList>(lastName);
        }

        public static PatientList GetBySearchObject(PatientSearchCriteria criteria)
        {
            return DataPortal.Fetch<PatientList>(criteria);
        }

        public static PatientList GetAll()
        {
            return DataPortal.Fetch<PatientList>();
        }

        private PatientList()
        { /* Require use of factory methods */ }

        #endregion
#endif

#if !WINDOWS_PHONE
        //public async static System.Threading.Tasks.Task<PatientList> GetPatientList()
        //{
        //    //return await DataPortal.FetchAsync<PatientList>();
        //}
#endif

#if !SILVERLIGHT
        #region Data Access

        private void DataPortal_Fetch(string lastName)
        { 
            using (var ctx = new DataAccess.PharmacyClaimAdjudicatorEntities())
            {
                var data = (from p in ctx.PatientFact
                           where p.LastName.StartsWith(lastName)
                           && p.RecordId == (from p2 in ctx.PatientFact
                                            where p2.PatientId == p.PatientId
                                            && p2.Retraction == false
                                            && !ctx.PatientFact.Any(p3 => p3.PatientId == p2.PatientId
                                                                     && p3.Retraction == true
                                                                     && p3.OriginalFactRecordId == p2.RecordId)
                                            select p2.RecordId).Max()
                           orderby p.LastName
                           select p);
                RaiseListChangedEvents = false;
                foreach (var item in data)
                {
                    this.Add(DataPortal.FetchChild<PatientEdit>(item));
                }
                RaiseListChangedEvents = true;
            }
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = new DataAccess.PharmacyClaimAdjudicatorEntities())
            {
                var data = (from p in ctx.PatientFact
                            where p.RecordId == (from p2 in ctx.PatientFact
                                              where p2.PatientId == p.PatientId
                                              && p2.Retraction == false
                                              && !ctx.PatientFact.Any(p3 => p3.PatientId == p2.PatientId
                                                                       && p3.Retraction == true
                                                                       && p3.OriginalFactRecordId == p2.RecordId)
                                              select p2.RecordId).Max()
                            orderby p.PatientId
                            select p);
                RaiseListChangedEvents = false;
                foreach (var item in data)
                {
                    this.Add(DataPortal.FetchChild<PatientEdit>(item));
                }
                RaiseListChangedEvents = true;
            }
        }

        private void DataPortal_Fetch(PatientSearchCriteria criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Entity Framework is great, but sometimes SQL is better.
                StringBuilder select = new StringBuilder("select p.* from patientfact p ");
                StringBuilder where = new StringBuilder();
                List<object> parameters = new List<object>();

                if (!String.IsNullOrWhiteSpace(criteria.PatientFirstName))
                {
                    AddToWhere(where, "p.firstname like @firstname + '%'");
                    parameters.Add(new SqlParameter("@firstname", criteria.PatientFirstName));
                }

                if (!String.IsNullOrWhiteSpace(criteria.PatientLastName))
                {
                    AddToWhere(where, "p.lastname like @lastname + '%'");
                    parameters.Add(new SqlParameter("@lastname", criteria.PatientLastName));
                }

                if (!String.IsNullOrWhiteSpace(criteria.CardholderId))
                {
                    AddToWhere(where, "p.lastname like @cardholderid");
                    parameters.Add(new SqlParameter("@cardholderid", criteria.CardholderId));
                }

                if (!String.IsNullOrWhiteSpace(criteria.GroupId))
                {
                    select.Append("inner join patientgroup pg on p.patientid = pg.patientid ");
                    AddToWhere(where, "pg.groupid like @groupid");
                    parameters.Add(new SqlParameter("@groupid", criteria.GroupId));
                }

                AddToWhere(where, "p.recordid = (select max(p2.recordid) from patientfact p2 " +
                    "where p2.patientid = p.patientid " +
                    "and p2.retraction = 0 " +
                    "and not exists (select 1 from patientfact p3 " +
                        "where p3.patientid = p2.patientid " +
                        "and p3.retraction = 1 " +
                        "and p3.OriginalFactRecordId = p2.recordid))");

                var results = (ctx.DbContext.Database.SqlQuery<DataAccess.PatientFact>(select.ToString() + where.ToString(), parameters.ToArray()));

                var rlce = this.RaiseListChangedEvents;
                this.RaiseListChangedEvents = false;

                foreach (var p in results)
                {
                    Add(DataPortal.FetchChild<PatientEdit>(p));
                    //Add(new Patient(p));
                }

                this.RaiseListChangedEvents = rlce;
            }
        }

        private void DataPortal_Fetch2(PatientSearchCriteria criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {

                //Entity Framework is great, but sometimes SQL is better.
                //string select = @"select p.* from patientfacts";
                //StringBuilder where = new StringBuilder();
                //List<object> parameters = new List<object>();

                //if (!String.IsNullOrWhiteSpace(criteria.PatientFirstName))
                //{
                //    AddToWhere(where, "firstname like @firstname");
                //    parameters.Add(new SqlParameter("@firstname", criteria.PatientFirstName));
                //}

                //if (!String.IsNullOrWhiteSpace(criteria.PatientLastName))
                //{
                //    AddToWhere(where, "lastname like @lastname");
                //    parameters.Add(new SqlParameter("@lastname", criteria.PatientLastName));
                //}


                IQueryable<DataAccess.PatientFact> query = (from  p in ctx.DbContext.PatientFact 
                                                           select p);

                //Add criteria to the where
                if (!String.IsNullOrWhiteSpace(criteria.PatientFirstName))
                    query = query.Where(x => x.FirstName.StartsWith(criteria.PatientFirstName.Trim()));

                if (!String.IsNullOrWhiteSpace(criteria.PatientLastName))
                    query = query.Where(x => x.LastName.StartsWith(criteria.PatientLastName.Trim()));

                if (!String.IsNullOrWhiteSpace(criteria.CardholderId))
                    query = query.Where(x => x.CardholderId.StartsWith(criteria.CardholderId.Trim()));

                if (!String.IsNullOrWhiteSpace(criteria.GroupId))
                {
                    IQueryable<DataAccess.PatientGroup> groupQuery = (from pg in ctx.DbContext.PatientGroup
                                                                 select pg);
                    //groupQuery = groupQuery.Where(g => g.GroupId.)
                    //query = query.Where(x => x.)
                    
                }

                var patientData = query.Select(q => q.PatientId).Distinct();//.ToList(); //from p in ctx.DbContext.PatientFacts)

                var data = (from p in ctx.DbContext.PatientFact
                            where p.RecordId == (from p2 in ctx.DbContext.PatientFact
                                                 where p2.PatientId == p.PatientId
                                                 && p2.Retraction == false
                                                 && !ctx.DbContext.PatientFact.Any(p3 => p3.PatientId == p2.PatientId
                                                                          && p3.Retraction == true
                                                                          && p3.OriginalFactRecordId == p2.RecordId)
                                                 select p2.RecordId).Max()
                            && patientData.Contains(p.PatientId)
                            orderby p.PatientId
                            select p);


                var rlce = this.RaiseListChangedEvents;
                this.RaiseListChangedEvents = false;

                foreach (var p in data)
                {
                    Add(DataPortal.FetchChild<PatientEdit>(p));
                    //Add(DataPortal.Fetch<Patient>(p));
                    //Add(new Patient(p));
                }


                //foreach (var p in patientData)
                //{
                //    Add(DataPortal.FetchChild<Patient>(p));
                //}


                this.RaiseListChangedEvents = rlce;
            }
        }

        private void AddToWhere(StringBuilder clause, string predicate)
        {
            if (!string.IsNullOrEmpty(clause.ToString()))
                clause.Append(" and ");
            else
                clause.Append(" where ");
            clause.Append(predicate + " ");
        }

        protected override void DataPortal_Update()
        {
            var rlce = this.RaiseListChangedEvents;
            this.RaiseListChangedEvents = false;
            Child_Update(null);
            this.RaiseListChangedEvents = rlce;

            //var oldRLCE = this.RaiseListChangedEvents;
            //this.RaiseListChangedEvents = false;
            //try
            //{
            //    foreach (var child in DeletedList)
            //        DataPortal.UpdateChild(child, null);
            //    DeletedList.Clear();

            //    foreach (var child in this)
            //        if (child.IsDirty) DataPortal.UpdateChild(child, null);
            //}
            //finally
            //{
            //    this.RaiseListChangedEvents = oldRLCE;
            //}
        }

        #endregion
#endif
    }

}
