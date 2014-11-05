using System;
using Csla;
using System.Linq;
using NxBRE.InferenceEngine.IO;
using System.Data.Entity;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class Rule : BusinessBase<Rule>
    {
        #region Business Methods

        /// <summary>
        /// Rule type is restricted to inferrable types from Transaction
        /// </summary>
        public static readonly PropertyInfo<string> RuleTypeProperty = RegisterProperty<string>(c => c.RuleType);
        public string RuleType
        {
            get { return GetProperty(RuleTypeProperty); }
            set
            {
                var propertyList = RuleTypes.GetInferrableProperties();
                if (propertyList.Contains(value))
                    SetProperty(RuleTypeProperty, value);
                else
                    throw new ArgumentException("RuleType must be an inferrable attribute of Transaction.");
            }
        }

        /// <summary>
        /// This is the default value of field the rule applies to
        /// </summary>
        public static readonly PropertyInfo<string> DefaultValueProperty = RegisterProperty<string>(c => c.DefaultValue);
        public string DefaultValue
        {
            get { return GetProperty(DefaultValueProperty); }
            set
            {

                var pi = typeof(Transaction).GetProperty(RuleType);
                if (pi.PropertyType.Equals(typeof(Boolean)))
                {
                    bool boolValue;
                    if (bool.TryParse(value, out boolValue))
                        SetProperty(DefaultValueProperty, value);
                    else
                        throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
                }
                else if (pi.PropertyType.Equals(typeof(Decimal)))
                {
                    decimal decimalValue;
                    if (decimal.TryParse(value, out decimalValue))
                        SetProperty(DefaultValueProperty, value);
                    else
                        throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
                }
                else if (pi.PropertyType.Equals(typeof(Enums.ResponseStatus)))
                {
                    //already throws argument exception 
                    Enums.ResponseStatus rs = (Enums.ResponseStatus)Enum.Parse(typeof(Enums.ResponseStatus), value);
                    if (Enum.IsDefined(typeof(Enums.ResponseStatus), rs))
                        SetProperty(DefaultValueProperty, value);
                    else
                        throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
                }
                else
                {
                    throw new ArgumentException("DefaultValue does not impliment conversion for " + RuleType);
                }

                //SetProperty(DefaultValueProperty, value); 
            }
        }

        public static readonly PropertyInfo<ImplicationList> ImplicationsProperty = RegisterProperty<ImplicationList>(c => c.Implications, RelationshipTypes.Child | RelationshipTypes.LazyLoad);
        public ImplicationList Implications
        {
            get
            {
                if (!FieldManager.FieldExists(ImplicationsProperty))
                {
                    Implications = ImplicationList.GetByRuleId(this.RuleId);
                }
                return GetProperty(ImplicationsProperty);
            }
            private set
            {
                LoadProperty(ImplicationsProperty, value);
                OnPropertyChanged(ImplicationsProperty);
            }
        }

        public static readonly PropertyInfo<Guid> RuleIdProperty = RegisterProperty<Guid>(c => c.RuleId);
        public Guid RuleId
        {
            get { return GetProperty(RuleIdProperty); }
            private set { LoadProperty(RuleIdProperty, value); }
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

        public static Rule NewRule()
        {
            return DataPortal.Create<Rule>();
        }

        public static Rule GetByRuleId(int id)
        {
            return DataPortal.Fetch<Rule>(id);
        }

        public static void DeleteRule(Guid ruleId)
        {
            DataPortal.Delete<Rule>(ruleId);
        }

        private Rule()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            this.RuleId = Guid.NewGuid();
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(Guid criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var ruleData = ctx.DbContext.Rule.FirstOrDefault(r => r.RuleId == criteria);
                if (ruleData == null)
                    throw new DataNotFoundException("RuleId = " + ruleData);
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(ruleData);
                }
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var ruleData = CreateNewEntity();
                ctx.DbContext.Rule.Add(ruleData);
                FieldManager.UpdateChildren();
                ctx.DbContext.SaveChanges();
            }
        }

        protected void Child_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var ruleData = CreateNewEntity();
                ctx.DbContext.Rule.Add(ruleData);
                FieldManager.UpdateChildren();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                if (this.IsSelfDirty)
                {
                    var ruleData = CreateNewEntity();
                    ctx.DbContext.Entry(ruleData).State = EntityState.Modified;
                }
                FieldManager.UpdateChildren();
                ctx.DbContext.SaveChanges();
            }
        }

        protected void Child_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                if (this.IsSelfDirty)
                {
                    var ruleData = CreateNewEntity();
                    ctx.DbContext.Entry(ruleData).State = EntityState.Modified;
                }
                FieldManager.UpdateChildren();
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.RuleId);
        }

        private void DataPortal_Delete(Guid criteria)
        {
            // TODO: delete values
        }

        private void PopulateByEntity(DataAccess.Rule ruleData)
        {
            this.RuleId = ruleData.RuleId;
            this.RuleType = ruleData.RuleType;
            this.DefaultValue = ruleData.DefaultValue;
        }

        private DataAccess.Rule CreateNewEntity()
        {
            var ruleData = new DataAccess.Rule();
            ruleData.RuleId = this.RuleId;
            ruleData.RuleType = this.RuleType;
            ruleData.DefaultValue = this.DefaultValue;
            return ruleData;
        }

        #endregion
    }
}