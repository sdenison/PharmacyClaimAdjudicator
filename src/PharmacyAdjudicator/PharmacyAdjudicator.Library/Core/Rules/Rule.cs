using System;
using Csla;
using System.Linq;
using NxBRE.InferenceEngine.IO;
using System.Data.Entity;
using System.ComponentModel;

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
                {
                    SetProperty(RuleTypeProperty, value);
                    this.DefaultValue = DefaultDefault();
                }
                else
                    throw new ArgumentException("RuleType must be an inferrable attribute of Transaction.");
            }
        }

        ///// <summary>
        ///// Provides a default value for the default property based on the ruleType's type.  Very meta.
        ///// </summary>
        ///// <param name="ruleType"></param>
        ///// <returns></returns>
        //private string DefaultDefault()
        //{
        //    var pi = typeof(Transaction).GetProperty(RuleType);
        //    if (pi.PropertyType.Equals(typeof(Boolean)))
        //        return "false";
        //    if (pi.PropertyType.Equals(typeof(Decimal)))
        //        return "0";
        //    if (pi.PropertyType.Equals(typeof(Enums.ResponseStatus)))
        //        return Enums.ResponseStatus.Captured.ToString();
        //    if (pi.PropertyType.Equals(typeof(Enums.BasisOfReimbursement)))
        //        return Enums.BasisOfReimbursement.NotSpecified.ToString();
        //    if (pi.PropertyType.Equals(typeof(Enums.TaxExemptIndicator)))
        //        return Enums.TaxExemptIndicator.NotSpecified.ToString();
        //    throw new ArgumentException("Unknown ruleType = " + RuleType);
        //}

        public Type ClrType()
        {
            if (string.IsNullOrEmpty(this.RuleType))
                throw new Exception("Cannot get ClrType while RuleType is not set");
            var pi = typeof(Transaction).GetProperty(this.RuleType);
            return pi.PropertyType;
        }

        /// <summary>
        /// Provides a default value for the default property based on the ruleType's type.  Very meta.
        /// </summary>
        /// <param name="ruleType"></param>
        /// <returns></returns>
        private object DefaultDefault()
        {
            var pi = typeof(Transaction).GetProperty(RuleType);
            if (pi.PropertyType.Equals(typeof(Boolean)))
                return false; 
            if (pi.PropertyType.Equals(typeof(Decimal)))
                return 0;
            if (pi.PropertyType.Equals(typeof(Enums.ResponseStatus)))
                return Enums.ResponseStatus.Captured;
            if (pi.PropertyType.Equals(typeof(Enums.BasisOfReimbursement)))
                return Enums.BasisOfReimbursement.NotSpecified;
            if (pi.PropertyType.Equals(typeof(Enums.TaxExemptIndicator)))
                return Enums.TaxExemptIndicator.NotSpecified;
            throw new ArgumentException("Unknown ruleType = " + RuleType);
        }
        /// <summary>
        /// This is the default value of field the rule applies to
        /// </summary>
        public static readonly PropertyInfo<object> DefaultValueProperty = RegisterProperty<object>(c => c.DefaultValue);
        public object DefaultValue
        {
            get { return GetProperty(DefaultValueProperty); }
            set
            {
                var pi = typeof(Transaction).GetProperty(RuleType);
                if (pi.PropertyType == value.GetType())
                    SetProperty(DefaultValueProperty, value);
                else if (pi.PropertyType.Equals(typeof(Decimal)))
                {
                    if ((value.GetType() == typeof(int)) || (value.GetType() == typeof(long)))
                        SetProperty(DefaultValueProperty, value);
                }
                else if (pi.PropertyType != value.GetType())
                {
                    throw new ArgumentException("Default value cannot be set to " + value.ToString() + " when Rule has RuleType of " + RuleType + ".");
                }

                //if (pi.PropertyType.Equals(typeof(Boolean)))
                //{
                //    bool boolValue;
                //    if (bool.TryParse(value, out boolValue))
                //        SetProperty(DefaultValueProperty, value);
                //    else
                //        throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
                //}
                //else if (pi.PropertyType.Equals(typeof(Decimal)))
                //{
                //    decimal decimalValue;
                //    if (decimal.TryParse(value, out decimalValue))
                //        SetProperty(DefaultValueProperty, value);
                //    else
                //        throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
                //}
                //else if (pi.PropertyType.Equals(typeof(Enums.ResponseStatus)))
                //{
                //    //already throws argument exception 
                //    Enums.ResponseStatus rs = (Enums.ResponseStatus)Enum.Parse(typeof(Enums.ResponseStatus), value);
                //    if (Enum.IsDefined(typeof(Enums.ResponseStatus), rs))
                //        SetProperty(DefaultValueProperty, value);
                //    else
                //        throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
                //}
                //else if (pi.PropertyType.Equals(typeof(Enums.BasisOfReimbursement)))
                //{
                //    //already throws argument exception 
                //    Enums.BasisOfReimbursement bor = (Enums.BasisOfReimbursement)Enum.Parse(typeof(Enums.BasisOfReimbursement), value);
                //    if (Enum.IsDefined(typeof(Enums.BasisOfReimbursement), bor))
                //        SetProperty(DefaultValueProperty, value);
                //    else
                //        throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
                //}
                //else if (pi.PropertyType.Equals(typeof(Enums.TaxExemptIndicator)))
                //{
                //    //already throws argument exception 
                //    Enums.TaxExemptIndicator tei = (Enums.TaxExemptIndicator)Enum.Parse(typeof(Enums.TaxExemptIndicator), value);
                //    if (Enum.IsDefined(typeof(Enums.TaxExemptIndicator), tei))
                //        SetProperty(DefaultValueProperty, value);
                //    else
                //        throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
                //}
                //else
                //{
                //    throw new ArgumentException("DefaultValue does not impliment conversion for " + RuleType);
                //}

                //SetProperty(DefaultValueProperty, value); 
            }
        }

        ///// <summary>
        ///// This is the default value of field the rule applies to
        ///// </summary>
        //public static readonly PropertyInfo<string> DefaultValueProperty = RegisterProperty<string>(c => c.DefaultValue);
        //public string DefaultValue
        //{
        //    get { return GetProperty(DefaultValueProperty); }
        //    set
        //    {
        //        var pi = typeof(Transaction).GetProperty(RuleType);
        //        if (pi.PropertyType.Equals(typeof(Boolean)))
        //        {
        //            bool boolValue;
        //            if (bool.TryParse(value, out boolValue))
        //                SetProperty(DefaultValueProperty, value);
        //            else
        //                throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
        //        }
        //        else if (pi.PropertyType.Equals(typeof(Decimal)))
        //        {
        //            decimal decimalValue;
        //            if (decimal.TryParse(value, out decimalValue))
        //                SetProperty(DefaultValueProperty, value);
        //            else
        //                throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
        //        }
        //        else if (pi.PropertyType.Equals(typeof(Enums.ResponseStatus)))
        //        {
        //            //already throws argument exception 
        //            Enums.ResponseStatus rs = (Enums.ResponseStatus)Enum.Parse(typeof(Enums.ResponseStatus), value);
        //            if (Enum.IsDefined(typeof(Enums.ResponseStatus), rs))
        //                SetProperty(DefaultValueProperty, value);
        //            else
        //                throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
        //        }
        //        else if (pi.PropertyType.Equals(typeof(Enums.BasisOfReimbursement)))
        //        {
        //            //already throws argument exception 
        //            Enums.BasisOfReimbursement bor = (Enums.BasisOfReimbursement)Enum.Parse(typeof(Enums.BasisOfReimbursement), value);
        //            if (Enum.IsDefined(typeof(Enums.BasisOfReimbursement), bor))
        //                SetProperty(DefaultValueProperty, value);
        //            else
        //                throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
        //        }
        //        else if (pi.PropertyType.Equals(typeof(Enums.TaxExemptIndicator)))
        //        {
        //            //already throws argument exception 
        //            Enums.TaxExemptIndicator tei = (Enums.TaxExemptIndicator)Enum.Parse(typeof(Enums.TaxExemptIndicator), value);
        //            if (Enum.IsDefined(typeof(Enums.TaxExemptIndicator), tei))
        //                SetProperty(DefaultValueProperty, value);
        //            else
        //                throw new ArgumentException("Default value cannot be set to " + value + " when Rule has RuleType of " + RuleType + ".");
        //        }
        //        else
        //        {
        //            throw new ArgumentException("DefaultValue does not impliment conversion for " + RuleType);
        //        }

        //        //SetProperty(DefaultValueProperty, value); 
        //    }
        //}

        public static readonly PropertyInfo<ImplicationList> ImplicationsProperty = RegisterProperty<ImplicationList>(c => c.Implications, RelationshipTypes.Child | RelationshipTypes.LazyLoad);
        public ImplicationList Implications
        {
            get
            {
                if (!FieldManager.FieldExists(ImplicationsProperty))
                {
                    LoadProperty(ImplicationsProperty, DataPortal.FetchChild<ImplicationList>(this.RuleId));
                    //Implications = ImplicationList.GetByRuleId(this.RuleId);
                }
                return GetProperty(ImplicationsProperty);
            }
            private set
            {
                //LoadProperty(ImplicationsProperty, value);
                SetProperty(ImplicationsProperty, value);
                OnPropertyChanged(ImplicationsProperty);
            }
        }

        public void AddImplication(Implication implication)
        {
            Implications.Add(implication);
            OnPropertyChanged(ImplicationsProperty);
            this.CheckPropertyRules(ImplicationsProperty);
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

            BusinessRules.AddRule(new ImplicationsAssignedToRuleMustMatchTypes(ImplicationsProperty));
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
            //return DataPortal.Create<Rule>();
            return DataPortal.CreateChild<Rule>();
        }

        public static Rule GetByRuleId(Guid ruleId)
        {
            //return DataPortal.Fetch<Rule>(ruleId);
            return DataPortal.FetchChild<Rule>(ruleId);
        }

        public static void DeleteRule(Guid ruleId)
        {
            //DataPortal.Delete<Rule>(ruleId);
            DataPortal.Delete<Rule>(ruleId);
        }

        private Rule()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        //[RunLocal]
        //protected override void DataPortal_Create()
        //{
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //    this.RuleId = Guid.NewGuid();
        //    base.DataPortal_Create();
        //}

        [RunLocal]
        protected override void Child_Create()
        {
            this.RuleId = Guid.NewGuid();
            base.Child_Create();
        }

        //private void DataPortal_Fetch(Guid criteria)
        //{
        //    using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
        //    {
        //        var ruleData = ctx.DbContext.Rule.FirstOrDefault(r => r.RuleId == criteria);
        //        if (ruleData == null)
        //            throw new DataNotFoundException("RuleId = " + ruleData);
        //        using (BypassPropertyChecks)
        //        {
        //            PopulateByEntity(ruleData);
        //        }
        //    }
        //}

        private void Child_Fetch(Guid criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var ruleData = ctx.DbContext.Rule.FirstOrDefault(r => r.RuleId == criteria);
                if (ruleData == null)
                    throw new DataNotFoundException("RuleId = " + ruleData);
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(ruleData);
                    LoadProperty(ImplicationsProperty, DataPortal.FetchChild<ImplicationList>(this.RuleId));
                }
            }
        }

        private void Child_Fetch(DataAccess.Rule ruleData)
        {
            PopulateByEntity(ruleData);
            LoadProperty(ImplicationsProperty, DataPortal.FetchChild<ImplicationList>(this.RuleId));
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var ruleData = CreateNewEntity();
                ctx.DbContext.Rule.Add(ruleData);
                FieldManager.UpdateChildren(this);
                //UpdateAssignedImplications();
                ctx.DbContext.SaveChanges();
            }
        }

        protected void Child_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var ruleData = CreateNewEntity();
                ctx.DbContext.Rule.Add(ruleData);
                FieldManager.UpdateChildren(this);
                //UpdateAssignedImplications();
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
                FieldManager.UpdateChildren(this);
                //UpdateAssignedImplications();
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
                FieldManager.UpdateChildren(this);
                //UpdateAssignedImplications();
            }
        }

        private void UpdateAssignedImplications()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                foreach (var assignedImplication in this.Implications)
                {
                    var implicationLinkData = ctx.DbContext.RuleImplication.FirstOrDefault(ri => ri.RuleId == this.RuleId && ri.ImplicationId == assignedImplication.ImplicationId);
                    if (implicationLinkData == null)
                    {
                        implicationLinkData = new DataAccess.RuleImplication() { RecordId = Guid.NewGuid(), RuleId = this.RuleId, ImplicationId = assignedImplication.ImplicationId, Priority = "60" };
                        ctx.DbContext.RuleImplication.Add(implicationLinkData);
                    }
                }
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
            //this.DefaultValue = ruleData.DefaultValue;

            Type type = Type.GetType("PharmacyAdjudicator.Library.Core.Transaction");
            var pi = type.GetProperty(this.RuleType);
            var propertyType = pi.PropertyType;
            TypeConverter tc = TypeDescriptor.GetConverter(propertyType);
            this.DefaultValue = tc.ConvertFromString(ruleData.DefaultValue);
        }

        private DataAccess.Rule CreateNewEntity()
        {
            var ruleData = new DataAccess.Rule();
            ruleData.RuleId = this.RuleId;
            ruleData.RuleType = this.RuleType;
            ruleData.DefaultValue = this.DefaultValue.ToString();
            return ruleData;
        }

        #endregion
    }
}