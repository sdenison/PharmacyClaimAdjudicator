using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NxBRE.InferenceEngine.IO;
using System.Collections.Generic;

namespace PharmacyAdjudicator.Library.Core.Plan
{
    [Serializable]
    public class PlanEdit : BusinessBase<PlanEdit>, IRuleBaseAdapter
    {
        #region Business Methods

        public IList<NxBRE.InferenceEngine.Rules.Implication> Implications
        {
            get
            {
                var implications = new List<NxBRE.InferenceEngine.Rules.Implication>();
                foreach (var rule in this.AssignedRules)
                    foreach (var implication in rule.Implications)
                        implications.Add(implication.ToNxBre());
                return implications;
            }
            set { }
        }

        public IList<NxBRE.InferenceEngine.Rules.Query> Queries
        {
            get
            {
                return new List<NxBRE.InferenceEngine.Rules.Query>();
            }
            set { }
        }

        public IList<NxBRE.InferenceEngine.Rules.Fact> Facts
        {
            get
            {
                var defaults = new List<NxBRE.InferenceEngine.Rules.Fact>();
                foreach (var rule in this.AssignedRules)
                {
                    //var atom = Library.Core.Rules.Atom.NewAtom();
                    //atom.Class = "Transaction";
                    //atom.Property = "Default " + rule.RuleType;
                    //atom.Value = rule.DefaultValue;
                    //defaults.Add(new NxBRE.InferenceEngine.Rules.Fact(atom.ToNxBre()));
                    var defaultValue = new NxBRE.InferenceEngine.Rules.Individual(rule.DefaultValue);
                    var atom = new NxBRE.InferenceEngine.Rules.Atom("Default" + rule.RuleType, defaultValue);
                    var fact = new NxBRE.InferenceEngine.Rules.Fact("Default " + rule.RuleType, atom);
                    defaults.Add(fact);
                }
                return defaults;
            }
            set { }
        }

        private NxBRE.InferenceEngine.IO.IBinder _binder;
        public NxBRE.InferenceEngine.IO.IBinder Binder
        {
            set { _binder = value; }
        }

        public string Direction
        {
            get { return "forward"; }
            set { throw new NotImplementedException(); }
        }
        
        public string Label
        {
            get { return this.Name; }
            set { }
        }

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

        public static readonly PropertyInfo<Guid> PlanInternalIdProperty = RegisterProperty<Guid>(c => c.PlanInternalId);
        [Display(Name="Plan Internal ID")]
        public Guid PlanInternalId
        {
            get { return GetProperty(PlanInternalIdProperty); }
            private set { SetProperty(PlanInternalIdProperty, value); }
        }

        public static readonly PropertyInfo<Guid> RecordIdProperty = RegisterProperty<Guid>(c => c.RecordId);
        public Guid RecordId
        {
            get { return GetProperty(RecordIdProperty); }
            private set { LoadProperty(RecordIdProperty, value); }
        }

        public static readonly PropertyInfo<Rules.RuleList> AssignedRulesProperty = RegisterProperty<Rules.RuleList>(r => r.AssignedRules, RelationshipTypes.Child);
        public Rules.RuleList AssignedRules
        {
            get
            {
                if (!FieldManager.FieldExists(AssignedRulesProperty))
                    LoadProperty(AssignedRulesProperty, DataPortal.FetchChild<Rules.RuleList>(this));
                return GetProperty(AssignedRulesProperty);
            }
            private set { SetProperty(AssignedRulesProperty, value); }
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

        public static PlanEdit NewPlan(string planId)
        {
            return DataPortal.Create<PlanEdit>(planId);
        }

        public static PlanEdit GetPlanByPlanId(string planId)
        {
            return DataPortal.Fetch<PlanEdit>(planId);
        }

        public static void DeletePlan(string planId)
        {
            DataPortal.Delete<PlanEdit>(planId);
        }

        private PlanEdit()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        protected void DataPortal_Create(string planId)
        {
            this.PlanId = planId;
            this.RecordId = Guid.NewGuid();
            this.PlanInternalId = Guid.NewGuid();
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                DataAccess.Plan newPlan = new DataAccess.Plan();
                newPlan.RecordCreatedDateTime = DateTime.Now;
                newPlan.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                newPlan.PlanInternalId = this.PlanInternalId;
                ctx.DbContext.Plan.Add(newPlan);
                ctx.DbContext.SaveChanges();
            }

            //Adds rule for every inferrable property on Transaction.
            //Rules are all assigned a default default and no rules have implication.
            var rules = this.AssignedRules;
            foreach (var ruleTypeToAdd in Rules.RuleTypes.GetInferrableProperties())
            {
                var ruleToAdd = Rules.Rule.NewRule();
                ruleToAdd.RuleType = ruleTypeToAdd;
                this.AssignedRules.Add(ruleToAdd);
            }

            base.Child_Create();
        }

        private void CreateMissingRulesWithDefaults()
        {
            
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
            throw new NotImplementedException("DataPortal_Fetch(CriteriaByPlanIdCompareDatetime criteria) not implemented.");
        }

        private void DataPortal_Fetch(string planId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var planData = (from p in ctx.DbContext.PlanDetail
                                where p.PlanId.Equals(planId)
                                && p.Retraction == false
                                && !ctx.DbContext.PlanDetail.Any(p2 => p2.Retraction == true && p2.OriginalFactRecordId == p.RecordId)
                                select p).FirstOrDefault();

                if (planData != null)
                    using (BypassPropertyChecks)
                    {
                        PopulateByRow(planData);
                        //Eagerly load the assigned rules
                        LoadProperty(AssignedRulesProperty, DataPortal.FetchChild<Rules.RuleList>(this)); 
                    }
                else 
                    throw new DataNotFoundException("PlanId = " + planId);
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var planData = CreateNewEntity();
                ctx.DbContext.PlanDetail.Add(planData);
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                if (this.IsSelfDirty)
                {
                    //Adds an entry in the database retracting the current data.
                    RetractFact();
                    //Adds an entry in the database asserting the current data.
                    //Need to return this because rowid will be changed when savechanges is called
                    var newPlanData = AssertNewFact();
                    PopulateByRow(newPlanData);
                }
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            RetractFact();
        }

        private void DataPortal_Delete(Guid planId)
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

        private void RetractFact()
        {
            using (BypassPropertyChecks)
            {
                //Add a record with Retraction = true and OriginalFactRecordId = currentRecordId
                var originalRecordId = this.RecordId;
                var planData = CreateNewEntity();
                planData.Retraction = true;
                planData.OriginalFactRecordId = originalRecordId;
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    ctx.DbContext.PlanDetail.Add(planData);
                }
            }
        }

        private DataAccess.PlanDetail AssertNewFact()
        {
            var planData = CreateNewEntity();
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                ctx.DbContext.PlanDetail.Add(planData);
            }
            return planData;
        }

        private void Child_Fetch(DataAccess.PlanDetail planData)
        {
            using (BypassPropertyChecks)
            { 
                PopulateByRow(planData);
                LoadProperty(AssignedRulesProperty, DataPortal.FetchChild<Rules.RuleList>(this));
            }
        }

        private void PopulateByRow(DataAccess.PlanDetail planData)
        {
            this.RecordId = planData.RecordId;
            this.PlanInternalId = planData.PlanInternalId;
            this.PlanId = planData.PlanId;
            this.Name = planData.Name;
        }

        private DataAccess.PlanDetail CreateNewEntity()
        {
            var planData = new DataAccess.PlanDetail();
            planData.RecordId = this.RecordId;
            planData.PlanInternalId = this.PlanInternalId;
            planData.PlanId = this.PlanId;
            planData.Name = this.Name;
            planData.Retraction = false;
            planData.RecordCreatedDateTime = DateTime.Now;
            planData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return planData;
        }

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
