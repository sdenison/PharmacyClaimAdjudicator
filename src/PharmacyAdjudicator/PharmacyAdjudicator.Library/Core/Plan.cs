using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NxBRE.InferenceEngine.IO;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class Plan : BusinessBase<Plan>//, IRuleBaseAdapter
    {
        #region Business Methods

        public static readonly PropertyInfo<string> PlanIdProperty = RegisterProperty<string>(c => c.PlanId);
        [Display(Name="Plan ID")]
        public string PlanId
        {
            get { return GetProperty(PlanIdProperty); }
            set { SetProperty(PlanIdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        [Display(Name="Plan Name")]
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<long> RecordIdProperty = RegisterProperty<long>(c => c.RecordId);
        public long RecordId
        {
            get { return GetProperty(RecordIdProperty); }
            private set { LoadProperty(RecordIdProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        public static Plan NewPlan(string planId)
        {
            return DataPortal.Create<Plan>(planId);
        }

        public static Plan GetPlanByPlanId(string planId)
        {
            return DataPortal.Fetch<Plan>(planId);
        }

        public static void DeletePlan(string planId)
        {
            DataPortal.Delete<Plan>(planId);
        }

        private Plan()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        protected void DataPortal_Create(string planId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                DataAccess.Plan newPlan = new DataAccess.Plan();
                //newPlan.PlanId = planId;
                newPlan.RecordCreatedDateTime = DateTime.Now;
                newPlan.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                ctx.DbContext.Plan.Add(newPlan);
                ctx.DbContext.SaveChanges();
                this.PlanId = planId;
            }
            base.DataPortal_Create();
        }

        [Serializable]
        private class CriteriaByPlanIdCompareDatetime : CriteriaBase<CriteriaByPlanIdCompareDatetime>
        {
            public static readonly PropertyInfo<string> PlanIdProperty = RegisterProperty<string>(c => c.PlanId);
            public string PlanId
            {
                get { return ReadProperty(PlanIdProperty); }
                set { LoadProperty(PlanIdProperty, value); }
            }

            public static readonly PropertyInfo<DateTime> RecordCompareDatetimeProperty = RegisterProperty<DateTime>(c => c.RecordCompareDatetime);
            public DateTime RecordCompareDatetime
            {
                get { return ReadProperty(RecordCompareDatetimeProperty); }
                set { LoadProperty(RecordCompareDatetimeProperty, value); }
            }
        }

        private void DataPortal_Fetch(CriteriaByPlanIdCompareDatetime criteria)
        {
            //using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            //{
            //    DataAccess.PlanFact planData;
            //    //Gets the plan record active on the compare date/time passed in
            //    planData = (from p in ctx.DbContext.PlanFact
            //                   where
            //                   p.PlanId == criteria.PlanId
            //                   && p.RecordId == (from p2 in ctx.DbContext.PlanFact
            //                                     where p2.PlanId == criteria.PlanId
            //                                     && p2.Retraction == false
            //                                     && p2.RecordCreatedDateTime < criteria.RecordCompareDatetime
            //                                     && !ctx.DbContext.PlanFact.Any(p3 => p3.PlanId == criteria.PlanId
            //                                         && p3.Retraction == true
            //                                         && p3.OriginalFactRecordId == p2.RecordId
            //                                         && p3.RecordCreatedDateTime < criteria.RecordCompareDatetime)
            //                                     select p2.RecordId).Max()
            //                   select p).FirstOrDefault();
            //    if (planData != null)
            //        using (BypassPropertyChecks)
            //            PopulateByRow(planData);
            //    else
            //        throw new DataNotFoundException("PlanId = " + criteria.PlanId);
            //}
        }

        private void DataPortal_Fetch(string planId)
        {
            //using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            //{
            //    DataAccess.PlanFact planData;
            //    planData = (from p in ctx.DbContext.PlanFact
            //                where
            //                p.PlanId == planId
            //                && p.RecordId == (from p2 in ctx.DbContext.PlanFact
            //                                  where p2.PlanId == planId
            //                                  && p2.Retraction == false
            //                                  && ctx.DbContext.PlanFact.Any(p3 => p3.PlanId == planId
            //                                  && p3.Retraction == true
            //                                  && p3.OriginalFactRecordId == p2.RecordId)
            //                                  select p2.RecordId).Max()
            //                select p).FirstOrDefault();
            //    if (planData != null)
            //        using (BypassPropertyChecks)
            //            PopulateByRow(planData);
            //    else
            //        throw new DataNotFoundException("PlanId = " + planId);
            //}
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var planData = CreateNewEntity();
                ctx.DbContext.PlanDetail.Add(planData);
                ctx.DbContext.SaveChanges();
                //this.RecordId = planData.RecordId;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var planData = CreateNewEntity();
                ctx.DbContext.PlanDetail.Add(planData);
                ctx.DbContext.SaveChanges();
                //this.RecordId = planData.RecordId;
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.PlanId);
        }

        private void DataPortal_Delete(string planId)
        {
            using (BypassPropertyChecks)
            {
                var planData = CreateNewEntity();
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    planData.Retraction = true;
                    planData.OriginalFactRecordId = this.RecordId;
                    ctx.DbContext.PlanDetail.Add(planData);
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        private void PopulateByRow(DataAccess.PlanDetail planData)
        {
            //this.PlanId = planData.PlanId;
            //this.RecordId = planData.RecordId;
        }

        private DataAccess.PlanDetail CreateNewEntity()
        {
            var planData = new DataAccess.PlanDetail();
            //planData.PlanId = this.PlanId;
            planData.Retraction = false;
            planData.RecordCreatedDateTime = DateTime.Now;
            planData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return planData;
        }

        #endregion
    }
}
