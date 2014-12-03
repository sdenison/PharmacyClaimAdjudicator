using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async static Task<PlanList> GetAllAsync()
        {
            return await DataPortal.FetchAsync<PlanList>();
        }

        public static PlanList GetAll()
        {
            return DataPortal.Fetch<PlanList>();
        }

        private PlanList()
        { /* Require use of factory methods */ }

        #endregion

        protected override PlanEdit AddNewCore()
        {
            //PlanEdit planEdit = PlanEdit.NewPlan("NEW-PLAN-ID");
            PlanEdit planEdit = DataPortal.CreateChild<PlanEdit>();
            //planEdit.PlanId = "NEW-PLAN-ID";
            Add(planEdit);
            return planEdit;
        }

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
                Child_Update();
                ctx.DbContext.SaveChanges();
                //try
                //{
                //}
                //catch (Exception ex)
                //{
                //    var x = ex;
                //}
            }
            this.RaiseListChangedEvents = rlce;
            //base.Child_Update();
        }

        #endregion
    }
}
