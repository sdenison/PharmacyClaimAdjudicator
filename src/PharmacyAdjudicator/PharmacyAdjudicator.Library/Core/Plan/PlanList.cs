using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Plan
{
    [Serializable]
    public class PlanList : BusinessListBase<PlanList, PlanEdit>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(PlanList), "Role");
        }

        #endregion

        #region Factory Methods

        //public static PlanList NewPlanList()
        //{
        //    return DataPortal.Create<PlanList>();
        //}

        //public static PlanList GetEditableRootList(int id)
        //{
        //    return DataPortal.Fetch<PlanList>(id);
        //}

        //public PlanEdit AddPlan()
        //{
        //    var newPlan = DataPortal.CreateChild<PlanEdit>();
        //    this.Add(newPlan);
        //    return newPlan;
        //}

        public static PlanList GetAll()
        {
            return DataPortal.Fetch<PlanList>();
        }

        private PlanList()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch()
        {
            //Gets all plans ordered by name
            RaiseListChangedEvents = false;
            //IsReadOnly = false;
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
            //IsReadOnly = true;
        }

        //[Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            var rlce = this.RaiseListChangedEvents;
            this.RaiseListChangedEvents = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                Child_Update(null);
                ctx.DbContext.SaveChanges();
            }
            this.RaiseListChangedEvents = rlce;
            //base.Child_Update();
        }

        #endregion
    }
}
