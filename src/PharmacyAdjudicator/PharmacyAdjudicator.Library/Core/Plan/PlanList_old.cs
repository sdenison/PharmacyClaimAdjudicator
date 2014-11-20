using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Plan
{
    [Serializable]
    public class PlanList_old : ReadOnlyListBase<PlanList_old, PlanEdit>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(PlanList), "Role");
        }

        #endregion

        #region Factory Methods

        //public static PlanList GetReadOnlyList(string filter)
        //{
        //    return DataPortal.Fetch<PlanList>(filter);
        //}

        public static PlanList_old GetAll()
        {
            return DataPortal.Fetch<PlanList_old>();
        }

        private PlanList_old()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        //private void DataPortal_Fetch(string criteria)
        //{
        //    RaiseListChangedEvents = false;
        //    IsReadOnly = false;
        //    // TODO: load values
        //    object objectData = null;
        //    foreach (var child in (List<object>)objectData)
        //        Add(ReadOnlyChild.GetReadOnlyChild(child));
        //    IsReadOnly = true;
        //    RaiseListChangedEvents = true;
        //}

        private void DataPortal_Fetch()
        {
            //Gets all plans ordered by name
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var plansData = from pd in ctx.DbContext.PlanDetail
                                where pd.Retraction == false
                                && !ctx.DbContext.PlanDetail.Any(pd2 => pd2.Retraction == true && pd2.OriginalFactRecordId == pd.RecordId)
                                orderby pd.Name
                                select pd;
                foreach (var planData in plansData)
                    Add(DataPortal.FetchChild<PlanEdit>(planData));
            }
            RaiseListChangedEvents = true;
            IsReadOnly = true;
        }

        #endregion
    }
}
