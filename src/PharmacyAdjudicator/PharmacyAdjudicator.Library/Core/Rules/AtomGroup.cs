using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class AtomGroup : BusinessBase<AtomGroup>, IPredicate, IEnumerable<AtomGroup>
    {
        #region Business Methods

        public static readonly PropertyInfo<NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator> LogicalOperatorProperty = RegisterProperty<NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator>(c => c.LogicalOperator);
        public NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator LogicalOperator
        {
            get { return GetProperty(LogicalOperatorProperty); }
            set { SetProperty(LogicalOperatorProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<Guid> AtomGroupIdProperty = RegisterProperty<Guid>(c => c.AtomGroupId);
        public Guid AtomGroupId
        {
            get { return GetProperty(AtomGroupIdProperty); }
            set { SetProperty(AtomGroupIdProperty, value); }
        }

        public IEnumerable<object> TopLevel 
        {
            get
            {
                //FirstGeneration must be a AtomGroup since we're using it as top level for Implication
                yield return this;
            }
            
        }

        public static readonly PropertyInfo<PredicateList> ChildrenProperty = RegisterProperty<PredicateList>(c => c.Children, RelationshipTypes.Child); // | RelationshipTypes.LazyLoad);
        public PredicateList Children
        {
            get
            {
                if (!FieldManager.FieldExists(ChildrenProperty))
                    Children = DataPortal.FetchChild<PredicateList>(this);
                return GetProperty(ChildrenProperty);
            }
            private set
            {
                LoadProperty(ChildrenProperty, value);
                OnPropertyChanged(ChildrenProperty);
            }
        }

        /// <summary>
        /// Will map to ViewModel.AddNewCriteriaItem
        /// </summary>
        /// <param name="predicate"></param>
        public void AddPredicate(Atom predicate)
        {
            //this.Predicates.Add(this, predicate);
            predicate.MarkAsChild();
            this.Children.Add(predicate);
            //this.Children.Add(DataPortal.CreateChild<Atom>());
            MarkDirty();

        }

        public Atom AddAtom()
        {
            var atomToAdd = DataPortal.CreateChild<Atom>();
            this.Children.Add(atomToAdd);
            OnPropertyChanged(ChildrenProperty);
            MarkDirty();
            return atomToAdd;
        }

        //public Atom AddAtom(object param)
        //{
        //    return AddAtom();
        //}

        /// <summary>
        /// Will map to ViewModel.AddNewCriteriaGroup
        /// </summary>
        /// <param name="predicate"></param>
        public void AddPredicate(AtomGroup predicate)
        {
            if (this.LogicalOperator == predicate.LogicalOperator)
                throw new Exception("Child logical operator cannot be the same as parent logical operator.");
            predicate.MarkAsChild();
            this.Children.Add(predicate);
            MarkDirty();
        }

        public AtomGroup AddAtomGroup(NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator logicalOperator)
        {
            //Logical operator needs to be passed in so child AtomGroups don't have the same operator as parent.
            var child = DataPortal.CreateChild<AtomGroup>();
            //var child = new AtomGroup();
            child.LogicalOperator = logicalOperator;
            this.Children.Add(child);
            MarkDirty();
            return child;
        }

        public AtomGroup AddAtomGroup()
        {
            //Logical operator needs to be passed in so child AtomGroups don't have the same operator as parent.
            var child = DataPortal.CreateChild<AtomGroup>();
            if (this.LogicalOperator == NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And)
                child.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
            else
                child.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            this.Children.Add(child);
            MarkDirty();
            return child;
        }

        public List<string> ComplexFactsUsed()
        {
            List<string> contains = new List<string>();
            foreach (var child in this.Children)
            {
                if (child is Atom)
                {
                    var predicate = (Atom)child;
                    if (!contains.Contains(predicate.Class))
                        contains.Add(predicate.Class);
                }
                if (child is AtomGroup)
                {
                    var predicate = (AtomGroup)child;
                    foreach (var complexFactUsed in predicate.ComplexFactsUsed())
                        if (!contains.Contains(complexFactUsed))
                            contains.Add(complexFactUsed);
                }
            }
            return contains;
        }

        public NxBRE.InferenceEngine.Rules.AtomGroup ToNxBre()
        {
            List<object> predicates = new List<object>();
            foreach (var child in Children)
            {
                if (child is Atom)
                {
                    var predicate = (Atom)child;
                    predicates.Add(predicate.ToNxBre());
                }
                if (child is AtomGroup)
                {
                    var predicate = (AtomGroup)child;
                    predicates.Add(predicate.ToNxBre());
                }
            }
            return new NxBRE.InferenceEngine.Rules.AtomGroup(this.LogicalOperator, predicates.ToArray()); 
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

        public static AtomGroup NewAtomGroup()
        {
            //var ag = DataPortal.Create<AtomGroup>();
            //ag.Children.CollectionChanged += (obj, evn) => { ag.MarkDirty(); };
            //return ag;

            return DataPortal.Create<AtomGroup>();
        }

        public static AtomGroup GetById(Guid id)
        {
            //var ag = DataPortal.Fetch<AtomGroup>(id);
            //ag.Children.CollectionChanged += (obj, evn) => { ag.MarkDirty(); };
            //return ag;

            return DataPortal.Fetch<AtomGroup>(id);
        }

        public static void DeleteById(Guid id)
        {
            DataPortal.Delete<AtomGroup>(id);
        }

        private AtomGroup()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void Child_Create()
        {
            this.AtomGroupId = Utils.GuidHelper.GenerateComb();
        }

        protected void Child_Update(Implication parent)
        {
            //base.DataPortal_Update();
            Child_Update();
        }

        protected void Child_Update(Predicate parent)
        {
            //base.DataPortal_Update();
            Child_Update();
        }

        [RunLocal]
        protected override void DataPortal_Create()
        {
            this.AtomGroupId = Utils.GuidHelper.GenerateComb();
            //this.Children = PredicateList.NewPredicateList(this);
            //this.Children = DataPortal.CreateChild<PredicateList>();
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(Guid atomGroupId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = (from a in ctx.DbContext.AtomGroup
                                     where a.AtomGroupId == atomGroupId
                                     select a).FirstOrDefault();

                if (atomGroupData == null)
                    throw new DataNotFoundException("AtomGroupId = " + atomGroupId.ToString());
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(atomGroupData);
                    //var predicateDataList = from p in ctx.DbContext.AtomGroupItem
                    //                    where p.AtomGroupId == atomGroupId
                    //                    orderby p.Priority
                    //                    select p;

                    //foreach (var predicateData in predicateDataList)
                    //{
                    //    if (predicateData.AtomId != null)
                    //    {
                    //        //Children.Add(DataPortal.Fetch<Atom>(predicateData.AtomId));
                    //        Children.Add(DataPortal.FetchChild<Atom>(predicateData.AtomId));
                    //    }
                    //    else
                    //    {
                    //        //Children.Add(DataPortal.Fetch<AtomGroup>(predicateData.ContainedAtomGroupId));
                    //        Children.Add(DataPortal.FetchChild<AtomGroup>(predicateData.ContainedAtomGroupId));
                    //    }
                    //}
                }
            }
        }

        private void Child_Fetch(Guid atomGroupId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = (from a in ctx.DbContext.AtomGroup
                                     where a.AtomGroupId == atomGroupId
                                     select a).FirstOrDefault();

                if (atomGroupData == null)
                    throw new DataNotFoundException("AtomGroupId = " + atomGroupId.ToString());
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(atomGroupData);
                    //var children = this.Children; //called to so we can eagerly load the children
                }
            }
        }

        private void Child_Fetch(Guid atomGroupId, IBusinessBase parent)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = (from a in ctx.DbContext.AtomGroup
                                     where a.AtomGroupId == atomGroupId
                                     select a).FirstOrDefault();
                if (atomGroupData == null)
                    throw new DataNotFoundException("AtomGroupId = " + atomGroupId.ToString());
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(atomGroupData);
                    this.SetParent(parent);
                    //var predicateDataList = from p in ctx.DbContext.AtomGroupItem
                    //                        where p.AtomGroupId == atomGroupId
                    //                        orderby p.Priority
                    //                        select p;
                    //if (Children == null)
                    //    Children = PredicateList.NewPredicateList(this);
                    //foreach (var predicateData in predicateDataList)
                    //{
                    //    //Load child data
                    //    if (predicateData.AtomId != null)
                    //    {
                    //        //this.SetParent(parent);
                    //        Children.Add(DataPortal.FetchChild<Atom>(predicateData.AtomId));
                    //    }
                    //    else
                    //    {
                    //        //this.SetParent(parent);
                    //        var atomGroupToAdd = DataPortal.FetchChild<AtomGroup>(predicateData.ContainedAtomGroupId, this);
                    //        atomGroupToAdd.PropertyChanged += (o, a) => { this.MarkDirty(); };
                    //        Children.Add(atomGroupToAdd);
                    //        //Children.Add(DataPortal.FetchChild<AtomGroup>(predicateData.ContainedAtomGroupId, this));
                    //    }
                    //}
                }
            }



            //DataPortal_Fetch(atomGroupId);
            MarkAsChild();
        }

        //private void SaveChildren()
        //{
        //    foreach (var child in Children)
        //    {
        //        var savableChild = (IBusinessBase)child;
        //        if (savableChild.IsSavable)
        //        {
        //            savableChild.Save();
        //        }
        //    }
        //}

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = CreateEntity();
                ctx.DbContext.AtomGroup.Add(atomGroupData);

                FieldManager.UpdateChildren(this);
                //foreach (var child in this.Children)
                //{
                //    var atomGroupItem = new DataAccess.AtomGroupItem();
                //    atomGroupItem.RecordId = Utils.GuidHelper.GenerateComb();
                //    atomGroupItem.AtomGroupId = this.AtomGroupId;
                //    var atom = (Atom) child;
                //    if (atom != null)
                //        atomGroupItem.AtomId = atom.AtomId;
                //    else
                //    {
                //        var atomGroup = (AtomGroup)child;
                //        if (atomGroup != null)
                //            atomGroupItem.ContainedAtomGroupId = atomGroup.AtomGroupId;
                //        else
                //            throw new Exception("AtomGroup.child must be of type Atom or AtomGroup");
                //    }
                //    atomGroupItem.Priority = 0;
                //    ctx.DbContext.AtomGroupItem.Add(atomGroupItem);
                //}
                ctx.DbContext.SaveChanges();
            }
        }



        protected void Child_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = CreateEntity();
                ctx.DbContext.AtomGroup.Add(atomGroupData);
                //SaveChildren();
                FieldManager.UpdateChildren(this);
                //ctx.DbContext.SaveChanges();
                //this.AtomGroupId = atomGroupData.AtomGroupId;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                Child_Update();
                ctx.DbContext.SaveChanges();
                //var atomGroupData = (from a in ctx.DbContext.AtomGroup
                //                     where a.AtomGroupId == this.AtomGroupId
                //                     select a).FirstOrDefault();
                //atomGroupData.LogicalOperator = this.LogicalOperator.ToString();
                //atomGroupData.Name = this.Name;
                ////SaveChildren();
                //FieldManager.UpdateChildren(this);
                //ctx.DbContext.SaveChanges();
            }
        }

        protected void Child_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = (from a in ctx.DbContext.AtomGroup
                                     where a.AtomGroupId == this.AtomGroupId
                                     select a).FirstOrDefault();
                atomGroupData.LogicalOperator = this.LogicalOperator.ToString();
                atomGroupData.Name = this.Name;
                //SaveChildren();
                FieldManager.UpdateChildren(this);
                //ctx.DbContext.SaveChanges();
            }
        }

        //protected void Child_Update(Implication parent)
        //{
        //    using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
        //    {
        //        var atomGroupData = (from a in ctx.DbContext.AtomGroup
        //                             where a.AtomGroupId == this.AtomGroupId
        //                             select a).FirstOrDefault();
        //        atomGroupData.LogicalOperator = this.LogicalOperator.ToString();
        //        atomGroupData.Name = this.Name;
        //        //SaveChildren();
        //        FieldManager.UpdateChildren(this);
        //        //ctx.DbContext.SaveChanges();
        //    }
        //}

        protected override void DataPortal_DeleteSelf()
        {
            //DataPortal_Delete(this.Id);
        }

        private void DataPortal_Delete(Guid criteria)
        {
            // TODO: delete values
        }

        private void PopulateByEntity(DataAccess.AtomGroup atomGroupData)
        {
            this.AtomGroupId = atomGroupData.AtomGroupId;
            this.Name = atomGroupData.Name;
            this.LogicalOperator = (atomGroupData.LogicalOperator.ToUpper().Equals("AND")) ? NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And : NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
        }

        private DataAccess.AtomGroup CreateEntity()
        {
            var atomGroupData = new DataAccess.AtomGroup();
            atomGroupData.AtomGroupId = this.AtomGroupId;
            atomGroupData.LogicalOperator = this.LogicalOperator.ToString();
            atomGroupData.Name = this.Name;

            return atomGroupData;
        }

        #endregion

        IEnumerator<AtomGroup> IEnumerable<AtomGroup>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            //throw new NotImplementedException();
            
            //return new List<AtomGroup>();
            //var x = new ReadOnlyCollection<AtomGroup>(
            //    new AtomGroup[]
            //    {
            //        this
            //    });
            var returnValue = new List<AtomGroup>();
            returnValue.Add(this);
            return returnValue.GetEnumerator();
        }
    }
}
