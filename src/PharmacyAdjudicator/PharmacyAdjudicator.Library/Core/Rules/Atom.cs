using System;
using System.Linq;
using Csla;
using System.Threading.Tasks;
using NxBRE.InferenceEngine.Rules;
using System.ComponentModel;

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
            set { SetProperty(PropertyProperty, value); OnPropertyChanged("ClrType"); }
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

        public static readonly PropertyInfo<string> ClassProperty = RegisterProperty<string>(c => c.Class);
        public string Class
        {
            get { return GetProperty(ClassProperty); }
            set 
            {
                //If class is changing then property should not be set
                if ((this.Class != value) && (!string.IsNullOrEmpty(this.Property)))
                    this.Property = "";
                SetProperty(ClassProperty, value); 
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

        //private int _RecordId;

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
            this.AtomId = Guid.NewGuid();
            base.Child_Create();
        }

        protected override void DataPortal_Create()
        {
            this.AtomId = Guid.NewGuid();
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
            this.AtomId = atomData.AtomId;
            this.Class = atomData.Class;
            this.Property = atomData.Property;
            this.Operation = atomData.Operation;

            //Converts Value to correct ClrType
            TypeConverter tc = TypeDescriptor.GetConverter(this.ClrType); 
            this.Value = tc.ConvertFromString(atomData.Value);
        }



        private DataAccess.AtomDetail CreateNewEntity()
        {
            var atomData = new DataAccess.AtomDetail();
            atomData.RecordId = Guid.NewGuid();
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
