using System;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    /// <summary>
    /// Predicate can either be an Atom or AtomGroup
    /// </summary>
    [Serializable]
    public class Predicate : BusinessBase<Predicate>
    {
        #region Business Methods

        public static readonly PropertyInfo<Guid> AtomGroupIdProperty = RegisterProperty<Guid>(c => c.AtomGroupId);
        public Guid AtomGroupId
        {
            get { return GetProperty(AtomGroupIdProperty); }
            private set { LoadProperty(AtomGroupIdProperty, value); }
        }

        public static readonly PropertyInfo<Atom> AtomProperty = RegisterProperty<Atom>(c => c.Atom);
        public Atom Atom
        {
            get { return GetProperty(AtomProperty); }
            set { SetProperty(AtomProperty, value); }
        }

        public static readonly PropertyInfo<AtomGroup> AtomGroupProperty = RegisterProperty<AtomGroup>(c => c.AtomGroup);
        public AtomGroup AtomGroup
        {
            get { return GetProperty(AtomGroupProperty); }
            set { SetProperty(AtomGroupProperty, value); }
        }

        public object PredicateValue
        {
            get
            {
                if (PredicateType == PredicateTypeEnum.Atom)
                    return Atom;
                if (PredicateType == PredicateTypeEnum.AtomGroup)
                    return AtomGroup;
                throw new Exception("Predicate must be an Atom or AtomGroup to retrieve PredicateValue");
            }
        }

        public PredicateTypeEnum PredicateType
        {
            get
            {
                if (Atom != null)
                    return PredicateTypeEnum.Atom;
                if (AtomGroup != null)
                    return PredicateTypeEnum.AtomGroup;
                throw new Exception("PredicateType has unknown value");
            }
            private set { throw new Exception("Not supported");  }
        }

        public static readonly PropertyInfo<int> PriorityProperty = RegisterProperty<int>(c => c.Priority);
        public int Priority
        {
            get { return GetProperty(PriorityProperty); }
            set { SetProperty(PriorityProperty, value); }
        }

        private Guid _RecordId;

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

        internal static Predicate NewPredicate(AtomGroup parent)
        {
            return DataPortal.Create<Predicate>(parent);
        }

        internal static Predicate GetByRecordId(Guid recordId)
        {
            return DataPortal.Fetch<Predicate>(recordId);
        }

        internal static void DeleteByRecordId(Guid recordId)
        {
            DataPortal.Delete<Predicate>(recordId);
        }

        internal Predicate(DataAccess.AtomGroupItem atomGroupItemData)
        {
            using (BypassPropertyChecks)
                PopulateByEntity(atomGroupItemData);
        }

        private Predicate()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected void DataPortal_Create(AtomGroup parent)
        {
            this._RecordId = Utils.GuidHelper.GenerateComb();
            this.AtomGroupId = parent.AtomGroupId;
            base.DataPortal_Create();
        }

        private void Child_Create(AtomGroup parent, PredicateTypeEnum predicateType)
        {
            using (BypassPropertyChecks)
            {
                this._RecordId = Utils.GuidHelper.GenerateComb();
                if (predicateType == PredicateTypeEnum.Atom)
                    this.Atom = DataPortal.CreateChild<Atom>();
                else
                    this.AtomGroup = DataPortal.CreateChild<AtomGroup>();
                this.AtomGroupId = parent.AtomGroupId;
            }
            base.Child_Create();
        }

        private void Child_Fetch(DataAccess.AtomGroupItem atomGroupItemData)
        {
            using (BypassPropertyChecks)
            {
                PopulateByEntity(atomGroupItemData);
            }
        }

        private void Child_Insert(AtomGroup parent)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupItemData = CreateEntity();
                atomGroupItemData.AtomGroupId = parent.AtomGroupId;
                FieldManager.UpdateChildren(this);
                ctx.DbContext.AtomGroupItem.Add(atomGroupItemData);
            }
        }

        private void Child_DeleteSelf()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupItemData = CreateEntity();
                atomGroupItemData.Retraction = true;
                atomGroupItemData.OriginalFactRecordId = _RecordId;
                _RecordId = Utils.GuidHelper.GenerateComb();
                ctx.DbContext.AtomGroupItem.Add(atomGroupItemData);
            }
        }

        private void Child_Update(AtomGroup parent)
        {
            if ((this.Atom != null) && (this.Atom.IsSavable))
                this.Atom = this.Atom.Save();
            if ((this.AtomGroup != null) && (this.AtomGroup.IsSavable))
                this.AtomGroup = this.AtomGroup.Save();
            //Save the children!
        }

        private void DataPortal_Fetch(Guid recordId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupItemData = (from a in ctx.DbContext.AtomGroupItem
                                        where a.RecordId == recordId
                                        select a).FirstOrDefault();
                if (atomGroupItemData == null)
                    throw new DataNotFoundException("RecordId = " + recordId.ToString());
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(atomGroupItemData);
                }
            }
        }

        private void PopulateByEntity(DataAccess.AtomGroupItem atomGroupItemData)
        {
            _RecordId = atomGroupItemData.RecordId;
            this.AtomGroupId = atomGroupItemData.AtomGroupId;
            if (atomGroupItemData.AtomId.HasValue)
            {
                if (atomGroupItemData.AtomId.HasValue)
                {
                    this.Atom = DataPortal.FetchChild<Atom>(atomGroupItemData.AtomId.Value);
                }
            }
            else if (atomGroupItemData.ContainedAtomGroupId.HasValue)
            {
                if (atomGroupItemData.ContainedAtomGroupId.HasValue)
                {
                    this.AtomGroup = DataPortal.FetchChild<AtomGroup>(atomGroupItemData.ContainedAtomGroupId.Value);
                }
            }
        }

        private DataAccess.AtomGroupItem CreateEntity()
        {
            var atomGroupItemData = new DataAccess.AtomGroupItem();
            atomGroupItemData.RecordId = this._RecordId;
            if (this.PredicateType == PredicateTypeEnum.Atom)
            {
                atomGroupItemData.AtomId = this.Atom.AtomId;
            }
            else
            {
                atomGroupItemData.ContainedAtomGroupId = this.AtomGroup.AtomGroupId;
            }
            atomGroupItemData.Priority = this.Priority;
            atomGroupItemData.AtomGroupId = this.AtomGroupId;
            atomGroupItemData.Retraction = false;
            atomGroupItemData.RecordCreatedDateTime = DateTime.Now;
            atomGroupItemData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return atomGroupItemData;
        }

        public enum PredicateTypeEnum
        {
            AtomGroup,
            Atom
        }

        #endregion
    }
}
