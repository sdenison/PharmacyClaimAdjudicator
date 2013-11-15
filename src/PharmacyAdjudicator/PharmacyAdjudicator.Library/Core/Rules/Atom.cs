using System;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class Atom : BusinessBase<Atom>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> ValueProperty = RegisterProperty<string>(c => c.Value);
        public string Value
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

        public static readonly PropertyInfo<int> AtomIdProperty = RegisterProperty<int>(c => c.AtomId);
        public int AtomId
        {
            get { return GetProperty(AtomIdProperty); }
            set { SetProperty(AtomIdProperty, value); }
        }

        private int _RecordId;

        public NxBRE.InferenceEngine.Rules.Atom GetInferenceEngineAtom()
        {
            return new NxBRE.InferenceEngine.Rules.Atom(this.Property, new NxBRE.InferenceEngine.Rules.Variable(this.Class), new NxBRE.InferenceEngine.Rules.Individual(this.Value));
        }

        public NxBRE.InferenceEngine.Rules.Atom ToNxBre()
        {
            return new NxBRE.InferenceEngine.Rules.Atom(this.Property, new NxBRE.InferenceEngine.Rules.Variable(this.Class), new NxBRE.InferenceEngine.Rules.Individual(this.Value));
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

        public static Atom NewAtom()
        {
            return DataPortal.Create<Atom>();
        }

        public static Atom GetByAtomId(int atomId)
        {
            return DataPortal.Fetch<Atom>(atomId);
        }

        public static void DeleteByAtomId(int atomId)
        {
            DataPortal.Delete<Atom>(atomId);
        }

        private Atom()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        protected override void DataPortal_Create()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var identityAtom = new DataAccess.Atom();
                identityAtom.RecordCreatedDateTime = DateTime.Now;
                identityAtom.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                ctx.DbContext.Atoms.Add(identityAtom);
                ctx.DbContext.SaveChanges();
                this.AtomId = identityAtom.AtomId;
            }
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(int atomId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Pull the most recent version of this atom
                var atomData = (from a in ctx.DbContext.AtomFacts
                                where a.AtomId == atomId
                                && a.RecordId == (from a2 in ctx.DbContext.AtomFacts
                                                      where a2.AtomId == atomId
                                                      && a2.Retraction == false
                                                      && !ctx.DbContext.AtomFacts.Any(a3 => a3.AtomId == atomId
                                                      && a3.Retraction == true
                                                      && a3.OriginalFactRecordId == a2.RecordId)
                                                  select a2.RecordId).Max()
                                select a).FirstOrDefault();
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
                ctx.DbContext.AtomFacts.Add(atomData);
                ctx.DbContext.SaveChanges();
                _RecordId = atomData.RecordId;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomData = CreateNewEntity();
                ctx.DbContext.AtomFacts.Add(atomData);
                ctx.DbContext.SaveChanges();
                _RecordId = atomData.RecordId;
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.AtomId);
        }

        private void DataPortal_Delete(int criteria)
        {
            using(BypassPropertyChecks)
            {
                var atomData = CreateNewEntity();
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    atomData.Retraction = true;
                    atomData.OriginalFactRecordId = _RecordId;
                    ctx.DbContext.AtomFacts.Add(atomData);
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        private void PopulateByEntity(DataAccess.AtomFact atomData)
        {
            this.AtomId = atomData.AtomId;
            this.Value = atomData.Value;
            this.Class = atomData.Class;
            this.Property = atomData.Property;
            this.Operation = atomData.Operation;
            _RecordId = atomData.RecordId;
        }

        private DataAccess.AtomFact CreateNewEntity()
        {
            var atomData = new DataAccess.AtomFact();
            atomData.AtomId = this.AtomId;
            atomData.Value = this.Value;
            atomData.Class = this.Class;
            atomData.Property = this.Property;
            atomData.Operation = this.Operation;
            atomData.RecordCreatedDateTime = DateTime.Now;
            atomData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return atomData;
        }

        #endregion
    }
}
