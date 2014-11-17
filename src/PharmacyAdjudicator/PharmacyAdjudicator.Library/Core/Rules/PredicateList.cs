using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class PredicateList : BusinessListBase<PredicateList, Predicate>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(PredicateList), "Role");
        }

        #endregion

        #region Factory Methods

        //public static PredicateList NewPredicateList(AtomGroup parent)
        //{
        //    return DataPortal.Create<PredicateList>(parent);
        //}

        //public static PredicateList GetByAtomGroupId(int atomGroupId)
        //{
        //    return DataPortal.Fetch<PredicateList>(atomGroupId);
        //}

        private PredicateList()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access
        
        //private void DataPortal_Fetch(int atomGroupId)
        //{
        //    RaiseListChangedEvents = false;
        //    using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
        //    {
        //        var atomGroupItems = from a in ctx.DbContext.AtomGroupItems
        //                              where a.AtomGroupId == atomGroupId
        //                              orderby a.Priority
        //                              select a;
        //        foreach (var item in atomGroupItems)
        //            this.Add(new Predicate(item));
        //    }
        //    RaiseListChangedEvents = true;
        //}

        public IEnumerable<object> ToNxBre()
        {
            foreach (var item in this)
            {
                if (item.PredicateType == Predicate.PredicateTypeEnum.Atom)
                    yield return item.Atom.ToNxBre();
                else if (item.PredicateType == Predicate.PredicateTypeEnum.AtomGroup)
                    yield return item.AtomGroup.ToNxBre();
                else
                    throw new Exception ("PredicateList may only contain Atoms and AtomGroups for ToNxBre() return correct values.");
            }
        }

        internal void Add(AtomGroup parent, AtomGroup atomGroup)
        {
            this.Add(DataPortal.CreateChild<Predicate>(parent, atomGroup));
        }

        internal void Add(AtomGroup parent, Atom atom)
        {
            this.Add(DataPortal.CreateChild<Predicate>(parent, atom));
        }

        private AtomGroup _parent;

        //protected override void Child_Create()
        //{
        //    base.Child_Create();
        //}

        protected void Child_Create(AtomGroup parent)
        {
            //Every predicate list must be a child of an AtomGroup.
            //_atomGroupId = atomGroupId;
            _parent = parent;
        }

        //private void Child_Update(AtomGroup parent)
        //{
        //    _parent = parent;
        //    foreach (var item in this)
        //    {
        //        item.AtomGroupId = parent.AtomGroupId;
        //    }
            
        //}

        private void Child_Fetch(IEnumerable<DataAccess.AtomGroupItem> atomGroupItems)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            foreach (var atomGroupItem in atomGroupItems)
                Add(DataPortal.FetchChild<Predicate>(atomGroupItem));
            RaiseListChangedEvents = rlce;
        }

        protected override void DataPortal_Update()
        {
            //base.Child_Update();
            foreach (var item in this)
            {
                item.Save();
            }
        }

        #endregion
    }
}
