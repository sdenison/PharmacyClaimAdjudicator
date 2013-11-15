using System;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class Predicate : BusinessBase<Predicate>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> AtomGroupIdProperty = RegisterProperty<int>(c => c.AtomGroupId);
        public int AtomGroupId
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

        public PredicateTypeEnum PredicateType
        {
            get
            {
                if (Atom != null)
                    return PredicateTypeEnum.Atom;
                else if (AtomGroup != null)
                    return PredicateTypeEnum.AtomGroup;
                else
                    return PredicateTypeEnum.NotSet;
            }
            private set { throw new Exception("Not supported");  }
        }

        public static readonly PropertyInfo<int> PriorityProperty = RegisterProperty<int>(c => c.Priority);
        public int Priority
        {
            get { return GetProperty(PriorityProperty); }
            set { SetProperty(PriorityProperty, value); }
        }

        private int _RecordId;

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

        internal static Predicate GetByRecordId(int recordId)
        {
            return DataPortal.Fetch<Predicate>(recordId);
        }

        internal static void DeleteByRecordId(int recordId)
        {
            DataPortal.Delete<Predicate>(recordId);
        }

        internal Predicate(DataAccess.AtomGroupItems atomGroupItemData)
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
            this.AtomGroupId = parent.AtomGroupId;
            base.DataPortal_Create();
        }

        private void Child_Create(AtomGroup parent)
        {
            using (BypassPropertyChecks)
            {
                this.AtomGroupId = parent.AtomGroupId;
            }
            base.Child_Create();
        }

        private void Child_Create(AtomGroup parent, AtomGroup item)
        {
            this.AtomGroupId = parent.AtomGroupId;
            this.AtomGroup = item;
            base.Child_Create();
        }

        private void Child_Create(AtomGroup parent, Atom item)
        {
            this.AtomGroupId = parent.AtomGroupId;
            this.Atom = item;
            base.Child_Create();
        }

        //private void Child_Fetch(int recordId)
        //{
        //    using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
        //    {
        //        var atomGroupItemData = (from a in ctx.DbContext.AtomGroupItems
        //                                 where a.RecordId == recordId
        //                                 select a).FirstOrDefault();
        //        if (atomGroupItemData == null)
        //            throw new DataNotFoundException("RecordId = " + recordId.ToString());
        //        using (BypassPropertyChecks)
        //        {
        //            PopulateByEntity(atomGroupItemData);
        //        }
        //    }
        //}

        private void Child_Fetch(DataAccess.AtomGroupItems atomGroupItemData)
        {
            using (BypassPropertyChecks)
            {
                PopulateByEntity(atomGroupItemData);
            }
        }

        private void Child_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupItemData = CreateEntity();
                ctx.DbContext.AtomGroupItems.Add(atomGroupItemData);
                ctx.DbContext.SaveChanges();
                UpdateChildren();
                _RecordId = atomGroupItemData.RecordId;
            }
        }

        private void Child_Update()
        {
            UpdateChildren();
        }

        private void DataPortal_Fetch(int recordId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupItemData = (from a in ctx.DbContext.AtomGroupItems
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

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupItemData = CreateEntity();
                ctx.DbContext.AtomGroupItems.Add(atomGroupItemData);
                ctx.DbContext.SaveChanges();
                UpdateChildren();
                _RecordId = atomGroupItemData.RecordId;
            }
        }

        private void UpdateChildren()
        {
            if (this.Atom != null)
                this.Atom.Save();
            if (this.AtomGroup != null)
                this.AtomGroup.Save();
        }

        private void PopulateByEntity(DataAccess.AtomGroupItems atomGroupItemData)
        {
            _RecordId = atomGroupItemData.RecordId;
            if (atomGroupItemData.AtomId.HasValue)
            {
                if (atomGroupItemData.AtomId.Value > 0)
                {
                    this.Atom = Core.Rules.Atom.GetByAtomId(atomGroupItemData.AtomId.Value);
                }
            }
            else if (atomGroupItemData.ContainedAtomGroupId.HasValue)
            {
                if (atomGroupItemData.ContainedAtomGroupId.Value > 0)
                {
                    this.AtomGroup = Core.Rules.AtomGroup.GetById(atomGroupItemData.ContainedAtomGroupId.Value);
                }
            }
        }

        private DataAccess.AtomGroupItems CreateEntity()
        {
            var atomGroupItemData = new DataAccess.AtomGroupItems();
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
            return atomGroupItemData;
        }

        public enum PredicateTypeEnum
        {
            AtomGroup,
            Atom,
            NotSet
        }

        #endregion
    }
}
