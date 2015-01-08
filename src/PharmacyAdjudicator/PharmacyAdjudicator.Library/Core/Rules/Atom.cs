using System;
using System.Linq;
using Csla;
using System.Threading.Tasks;
using NxBRE.InferenceEngine.Rules;
using System.ComponentModel;
using System.Collections.Generic;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class Atom : BusinessBase<Atom>, IPredicate
    {
        #region Business Methods

        public static readonly PropertyInfo<object> ValueProperty = RegisterProperty<object>(c => c.Value);
        public object Value
        {
            get { return GetProperty(ValueProperty); }
            set { SetProperty(ValueProperty, value); }
        }

        public static readonly PropertyInfo<string> PropertyProperty = RegisterProperty<string>(c => c.Property);
        public string Property
        {
            get { return GetProperty(PropertyProperty); }
            set 
            { 
                SetProperty(PropertyProperty, value);
                this.Value = DefaultDefault();
                OnPropertyChanged("ClrType"); 
                OnPropertyChanged("ClrTypeString"); 
            }
        }

        /// <summary>
        /// Provides a default value for the default property based on the ruleType's type.  Very meta.
        /// </summary>
        /// <param name="ruleType"></param>
        /// <returns></returns>
        private object DefaultDefault()
        {
            if ((string.IsNullOrEmpty(this.Class)) || (string.IsNullOrEmpty(this.Property)))
                return "";
            Type type = Type.GetType("PharmacyAdjudicator.Library.Core." + this.Class);
            //If we don't have enough information to get the type of the atom then return NotSet by default.
            if (type == null)
                return "";
            var pi = type.GetProperty(this.Property);
            if (pi == null)
                return "";
            if (pi.PropertyType.IsEnum)
                return default(int);// "Enum";
            if (pi.PropertyType.Equals(typeof(decimal)))
                return 0.0;
            if (pi.PropertyType.Equals(typeof(string)))
                return "";
            if (pi.PropertyType.Equals(typeof(int)))
                return 0;
            return 0;
        }

        public Type ClrType
        {
            get 
            {
                if ((string.IsNullOrEmpty(this.Class)) || (string.IsNullOrEmpty(this.Property)))
                    return typeof(string);
                Type type = Type.GetType("PharmacyAdjudicator.Library.Core." + this.Class);
                //If we don't have enough information to get the type of the atom then return string by default.
                var pi = type.GetProperty(this.Property);
                return pi.PropertyType;
            }
            private set { }
        }

        public string ClrTypeString
        {
            get
            {
                if ((string.IsNullOrEmpty(this.Class)) || (string.IsNullOrEmpty(this.Property)))
                    return "NotSet";
                Type type = Type.GetType("PharmacyAdjudicator.Library.Core." + this.Class);
                //If we don't have enough information to get the type of the atom then return NotSet by default.
                if (type == null) 
                    return "NotSet"; 
                var pi = type.GetProperty(this.Property);
                if (pi == null) 
                    return "NotSet"; 
                if (pi.PropertyType.IsEnum) 
                    return "Enum";
                return pi.PropertyType.Name;
            }
        }

        public static readonly PropertyInfo<string> ClassProperty = RegisterProperty<string>(c => c.Class);
        public string Class
        {
            get { return GetProperty(ClassProperty); }
            set 
            {
                //If class is changing then property should not be set
                if ((this.Class != value) && (!string.IsNullOrEmpty(this.Property)))
                {
                    this.Property = "";
                    OnPropertyChanged("Property");
                }
                SetProperty(ClassProperty, value);
                OnPropertyChanged("AllowedProperties");
            }
        }

        public List<string> AllowedClasses
        {
            get { return RuleTypes.GetTypes(); }
        }

        //Returns all properties of the selected class.
        public List<string> AllowedProperties
        {
            get
            {
                //if we can't identify the class return an empty list.
                var classType = Type.GetType("PharmacyAdjudicator.Library.Core." + this.Class);
                if (classType == null)
                    return new List<string>();
                return RuleTypes.GetFactProperties(classType);
            }
        }

        public static readonly PropertyInfo<string> OperationProperty = RegisterProperty<string>(c => c.Operation);
        public string Operation
        {
            get { return GetProperty(OperationProperty); }
            set { SetProperty(OperationProperty, value); }
        } 

        public static readonly PropertyInfo<Guid> AtomIdProperty = RegisterProperty<Guid>(c => c.AtomId);
        public Guid AtomId
        {
            get { return GetProperty(AtomIdProperty); }
            set { SetProperty(AtomIdProperty, value); }
        }

        public NxBRE.InferenceEngine.Rules.Atom GetInferenceEngineAtom()
        {
            string valueWithOperation;
            if (this.Operation == "")
                //value to object
                valueWithOperation = this.Value.ToString();
            else
                valueWithOperation = string.Format("{0}({1})", this.Operation, this.Value);
            return new NxBRE.InferenceEngine.Rules.Atom(this.Property, new NxBRE.InferenceEngine.Rules.Variable(this.Class), new NxBRE.InferenceEngine.Rules.Individual(valueWithOperation));
        }

        public NxBRE.InferenceEngine.Rules.Atom ToNxBre()
        {
            //Only supporting nxbre://operator 
            if (this.Operation != "")
            {
                //value to object
                //var predicate = new Function(Function.FunctionResolutionType.NxBRE, this.Value, null, string.Format("{0}({1})", this.Operation, this.Value), this.Value);
                var predicate = new Function(Function.FunctionResolutionType.NxBRE, this.Value.ToString(), null, string.Format("{0}({1})", this.Operation, this.Value), this.Value.ToString());
                return new NxBRE.InferenceEngine.Rules.Atom(this.Property, new NxBRE.InferenceEngine.Rules.Variable(this.Class), predicate); 
            }
            else
            {
                return new NxBRE.InferenceEngine.Rules.Atom(this.Property, new NxBRE.InferenceEngine.Rules.Variable(this.Class), new NxBRE.InferenceEngine.Rules.Individual(this.Value));//ealueWithOperation));
            }
        }

        private Guid _RecordId;
        private Guid RecordId
        {
            get { return _RecordId; }
            set { _RecordId = value; }
        }

        public void MarkAsChild()
        {
            base.MarkAsChild();
        }

        //public void RemoveAtom(Atom atomToRemove)
        //{
        //    var x = "this worked";
        //}

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

        public async static Task<Atom> NewAtomAsync()
        {
            return await DataPortal.CreateAsync<Atom>();
        }

        public async static Task<Atom> GetByAtomIdAsync(Guid atomId)
        {
            return await DataPortal.FetchAsync<Atom>(atomId);
        }

        public static Atom NewAtom()
        {
            return DataPortal.Create<Atom>();
        }

        public static Atom GetByAtomId(Guid atomId)
        {
            return DataPortal.Fetch<Atom>(atomId);
        }

        public static void DeleteByAtomId(Guid atomId)
        {
            DataPortal.Delete<Atom>(atomId);
        }

        private Atom()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void Child_Create()
        {
            this.AtomId = Utils.GuidHelper.GenerateComb();
            //this.AtomId = Guid.
            base.Child_Create();
        }

        protected override void DataPortal_Create()
        {
            this.AtomId = Utils.GuidHelper.GenerateComb();
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(Guid atomId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomData = (from a in ctx.DbContext.AtomDetail
                                where a.AtomId == atomId
                                && a.Retraction == false
                                && !ctx.DbContext.AtomDetail.Any(a2 => a2.Retraction == true && a2.OriginalFactRecordId == a.RecordId)
                                select a).FirstOrDefault();
                if (atomData == null)
                    throw new DataNotFoundException("AtomId = " + atomId);
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(atomData);
                }
            }
        }

        private void Child_Fetch(Guid atomId)
        {
            DataPortal_Fetch(atomId);
            MarkAsChild();
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Add identity record
                var atomIdentityData = new DataAccess.Atom();
                atomIdentityData.AtomId = this.AtomId;
                atomIdentityData.RecordCreatedDateTime = DateTime.Now;
                atomIdentityData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                ctx.DbContext.Atom.Add(atomIdentityData);
                //Add detail record
                AssertNewFact();
                ctx.DbContext.SaveChanges();
            }
        }

        protected void Child_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Add identity record
                var atomIdentityData = new DataAccess.Atom();
                atomIdentityData.AtomId = this.AtomId;
                atomIdentityData.RecordCreatedDateTime = DateTime.Now;
                atomIdentityData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                ctx.DbContext.Atom.Add(atomIdentityData);
                //Add AtomDetail
                AssertNewFact();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                RetractFact();
                AssertNewFact();
                ctx.DbContext.SaveChanges();
            }
        }

        protected void Child_Update()
        {
            RetractFact();
            AssertNewFact();
        }

        protected void Child_DeleteSelf()
        {
            //RetractFact();
            //Using deletion of link in AtomGroupItem instead.
        }

        protected override void DataPortal_DeleteSelf()
        {
            using(BypassPropertyChecks)
            {
                RetractFact();
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        private void DataPortal_Delete(Guid criteria)
        {
            using(BypassPropertyChecks)
            {
                RetractFact();
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        private void Child_Delete(Guid criteria)
        {
            using (BypassPropertyChecks)
            {
                RetractFact();
            }
        }

        private void RetractFact()
        {
            using (BypassPropertyChecks)
            {
                var atomData = CreateNewEntity();
                atomData.Retraction = true;
                atomData.OriginalFactRecordId = this.RecordId;
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    ctx.DbContext.AtomDetail.Add(atomData);
                }
            }
        }

        private void AssertNewFact()
        {
            using (BypassPropertyChecks)
            {
                var atomData = CreateNewEntity();
                this.RecordId = atomData.RecordId;
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    ctx.DbContext.AtomDetail.Add(atomData);
                }
            }
        }

        private void PopulateByEntity(DataAccess.AtomDetail atomData)
        {
            this.RecordId = atomData.RecordId;
            //this.AtomId = atomData.AtomId;
            SetProperty(AtomIdProperty, atomData.AtomId);
            //this.Class = atomData.Class;
            SetProperty(ClassProperty, atomData.Class);
            //this.Property = atomData.Property;
            SetProperty(PropertyProperty, atomData.Property);
            //this.Operation = atomData.Operation;
            SetProperty(OperationProperty, atomData.Operation);

            //Converts Value to correct ClrType
            TypeConverter tc = TypeDescriptor.GetConverter(this.ClrType); 
            //this.Value = tc.ConvertFromString(atomData.Value);
            SetProperty(ValueProperty, tc.ConvertFromString(atomData.Value));
        }



        private DataAccess.AtomDetail CreateNewEntity()
        {
            var atomData = new DataAccess.AtomDetail();
            atomData.RecordId = Utils.GuidHelper.GenerateComb();
            atomData.AtomId = this.AtomId;
            atomData.Value = this.Value != null ? this.Value.ToString() : "";// this.Value.ToString();
            atomData.Class = this.Class;
            atomData.Property = this.Property;
            atomData.Operation = this.Operation;
            atomData.Retraction = false;
            atomData.RecordCreatedDateTime = DateTime.Now;
            atomData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return atomData;
        }

        #endregion
    }
}
