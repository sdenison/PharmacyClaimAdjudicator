using System;
using System.Linq;
using Csla;
using System.Threading.Tasks;
using NxBRE.InferenceEngine.Rules;
using System.ComponentModel;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class Atom : BusinessBase<Atom>
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
            set { SetProperty(PropertyProperty, value); }
        }

        public static readonly PropertyInfo<string> ClassProperty = RegisterProperty<string>(c => c.Class);
        public string Class
        {
            get { return GetProperty(ClassProperty); }
            set { SetProperty(ClassProperty, value); }
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

        //public NxBRE.InferenceEngine.Rules.Fact ToNxBreFact()
        //{
        //    return new NxBRE.InferenceEngine.Rules.Fact()
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
                var atomData = ctx.DbContext.Atom.FirstOrDefault(a => a.AtomId == atomId);
                if (atomData == null)
                    throw new DataNotFoundException("AtomId = " + atomId);
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(atomData);
                }
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomData = CreateNewEntity();
                ctx.DbContext.Atom.Add(atomData);
                ctx.DbContext.SaveChanges();
            }
        }

        protected void Child_Insert(object parent)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomData = CreateNewEntity();
                ctx.DbContext.Atom.Add(atomData);
            }
        }

        protected override void DataPortal_Update()
        {
            throw new NotSupportedException("Updates to an Atom are not allowed.");
        }

        protected void Child_Update()
        {
            throw new NotSupportedException("Updates to an Atom are not allowed.");
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.AtomId);
        }

        private void DataPortal_Delete(Guid criteria)
        {
            using(BypassPropertyChecks)
            {
                var atomData = CreateNewEntity();
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    atomData.IsDeleted = true;
                    atomData.RecordDeleteUser = Csla.ApplicationContext.User.Identity.Name;
                    atomData.RecordDeleteDate = DateTime.Now;
                    ctx.DbContext.Atom.Add(atomData);
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        private void Child_Delete(Guid criteria)
        {
            using (BypassPropertyChecks)
            {
                var atomData = CreateNewEntity();
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    atomData.IsDeleted = true;
                    atomData.RecordDeleteUser = Csla.ApplicationContext.User.Identity.Name;
                    atomData.RecordDeleteDate = DateTime.Now;
                    ctx.DbContext.Atom.Add(atomData);
                }
            }
        }

        private void PopulateByEntity(DataAccess.Atom atomData)
        {
            this.AtomId = atomData.AtomId;
            this.Value = atomData.Value;
            this.Class = atomData.Class;
            this.Property = atomData.Property;
            this.Operation = atomData.Operation;

            //var valueType = new System.Reflection.PropertyInfo();
            Type type = Type.GetType("PharmacyAdjudicator.Library.Core." + this.Class);
            var pi = type.GetProperty(this.Property);
            var propertyType = pi.PropertyType;
            TypeConverter tc = TypeDescriptor.GetConverter(propertyType);
            this.Value = tc.ConvertFromString(atomData.Value);
        }

        private DataAccess.Atom CreateNewEntity()
        {
            var atomData = new DataAccess.Atom();
            atomData.AtomId = this.AtomId;
            //atomData.Value = this.Value;
            atomData.Value = this.Value.ToString();
            atomData.Class = this.Class;
            atomData.Property = this.Property;
            atomData.Operation = this.Operation;
            atomData.IsDeleted = false;
            atomData.RecordCreatedDateTime = DateTime.Now;
            atomData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return atomData;
        }

        #endregion
    }
}
