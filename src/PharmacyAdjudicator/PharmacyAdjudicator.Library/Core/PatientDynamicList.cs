using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class PatientDynamicList :
      DynamicListBase<Patient>
    {
        #region Business Methods

        //protected override PatientDynamicList AddNewCore()
        //{
        //    var item = Patient.NewDynamicRoot();
        //    Add(item);
        //    return item;
        //}

        #endregion

        #region  Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(PatientDynamicList), "Role");
            //AuthorizationRules.AllowEdit(typeof(PatientDynamicList), "Role");
        }

        #endregion

        #region  Factory Methods

        public static PatientDynamicList NewDynamicRootList()
        {
            return DataPortal.Create<PatientDynamicList>();
        }

        public static PatientDynamicList GetBySearchObject(PatientSearchCriteria criteria)
        {
            return DataPortal.Fetch<PatientDynamicList>(criteria);
        }

        private PatientDynamicList()
        {
            // require use of factory methods
            AllowNew = true;
        }

        #endregion

        #region  Data Access

        //private void DataPortal_Fetch()
        //{
        //    // TODO: load values
        //    RaiseListChangedEvents = false;
        //    object listData = null;
        //    foreach (var item in (List<object>)listData)
        //        Add(DynamicRoot.GetDynamicRoot(item));
        //    RaiseListChangedEvents = true;
        //}

        private void DataPortal_Fetch(PatientSearchCriteria criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Entity Framework is great, but sometimes SQL is better.
                StringBuilder select = new StringBuilder("select p.* from patientfacts p ");
                StringBuilder where = new StringBuilder();
                List<object> parameters = new List<object>();

                if (!String.IsNullOrWhiteSpace(criteria.PatientFirstName))
                {
                    AddToWhere(where, "p.firstname like @firstname");
                    parameters.Add(new SqlParameter("@firstname", criteria.PatientFirstName));
                }

                if (!String.IsNullOrWhiteSpace(criteria.PatientLastName))
                {
                    AddToWhere(where, "p.lastname like @lastname");
                    parameters.Add(new SqlParameter("@lastname", criteria.PatientLastName));
                }

                if (!String.IsNullOrWhiteSpace(criteria.CardholderId))
                {
                    AddToWhere(where, "p.lastname like @cardholderid");
                    parameters.Add(new SqlParameter("@cardholderid", criteria.CardholderId));
                }

                if (!String.IsNullOrWhiteSpace(criteria.GroupId))
                {
                    select.Append("inner join patientgroups pg on p.patientid = pg.patientid ");
                    AddToWhere(where, "pg.groupid like @groupid");
                    parameters.Add(new SqlParameter("@groupid", criteria.GroupId));
                }

                AddToWhere(where, "p.recordid = (select max(p2.recordid) from patientfacts p2 " +
                    "where p2.patientid = p.patientid " +
                    "and p2.retraction = 0 " +
                    "and not exists (select 1 from patientfacts p3 " +
                        "where p3.patientid = p2.patientid " +
                        "and p3.retraction = 1 " +
                        "and p3.OriginalFactRecordId = p2.recordid))");

                var results = (ctx.DbContext.Database.SqlQuery<DataAccess.PatientFact>(select.ToString() + where.ToString(), parameters.ToArray()));

                var rlce = this.RaiseListChangedEvents;
                this.RaiseListChangedEvents = false;
                foreach (var p in results)
                {
                    Add(new Patient(p));
                }
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

        #endregion
    }
}