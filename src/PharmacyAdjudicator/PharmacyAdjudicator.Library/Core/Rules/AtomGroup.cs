using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class AtomGroup : BusinessBase<AtomGroup>
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

        public static readonly PropertyInfo<ObservableCollection<object>> ChildrenProperty = RegisterProperty<ObservableCollection<object>>(c => c.Children, RelationshipTypes.PrivateField);
        private ObservableCollection<object> _children = new ObservableCollection<object>(); 
        public ObservableCollection<object> Children
        {
            get { return GetProperty(ChildrenProperty, _children); }
            private set { _children = value; }
        }

        /// <summary>
        /// Will map to ViewModel.AddNewCriteriaItem
        /// </summary>
        /// <param name="predicate"></param>
        public void AddPredicate(Atom predicate)
        {
            //this.Predicates.Add(this, predicate);
            this.Children.Add(predicate);
        }

        /// <summary>
        /// Will map to ViewModel.AddNewCriteriaGroup
        /// </summary>
        /// <param name="predicate"></param>
        public void AddPredicate(AtomGroup predicate)
        {
            if (this.LogicalOperator == predicate.LogicalOperator)
                throw new Exception("Child logical operator cannot be the same as parent logical operator.");
            this.Children.Add(predicate);
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
            //return new NxBRE.InferenceEngine.Rules.AtomGroup(this.LogicalOperator, this.Predicates.ToNxBre().ToArray());

            List<object> predicates = new List<object>();
            foreach (var child in Children)
            {
                if (child is Atom)
                {
                    var predicate = (Atom) child;
                    predicates.Add(predicate.ToNxBre());
                }
                if (child is AtomGroup)
                {
                    var predicate = (AtomGroup) child;
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
            return DataPortal.Create<AtomGroup>();
        }

        public static AtomGroup GetById(Guid id)
        {
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

        protected void Child_Update(Implication parent)
        {
            base.DataPortal_Update();
        }

        protected void Child_Update(Predicate parent)
        {
            base.DataPortal_Update();
        }

        [RunLocal]
        protected override void DataPortal_Create()
        {
            this.AtomGroupId = Guid.NewGuid();
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
                    var predicateDataList = from p in ctx.DbContext.AtomGroupItem
                                        where p.AtomGroupId == atomGroupId
                                        orderby p.Priority
                                        select p;

                    foreach (var predicateData in predicateDataList)
                    {
                        if (predicateData.AtomId != null)
                        {
                            Children.Add(DataPortal.Fetch<Atom>(predicateData.AtomId));
                        }
                        else
                        {
                            Children.Add(DataPortal.Fetch<AtomGroup>(predicateData.ContainedAtomGroupId));
                        }
                    }
                }
            }
        }

        private void SaveChildren()
        {
            foreach (var child in Children)
            {
                var savableChild = (IBusinessBase)child;
                if (savableChild.IsSavable)
                {
                    savableChild.Save();
                }
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = CreateEntity();
                ctx.DbContext.AtomGroup.Add(atomGroupData);
                SaveChildren();
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
                //this.AtomGroupId = atomGroupData.AtomGroupId;
            }
        }

        protected void Child_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = CreateEntity();
                ctx.DbContext.AtomGroup.Add(atomGroupData);
                SaveChildren();
                FieldManager.UpdateChildren(this);
                //ctx.DbContext.SaveChanges();
                //this.AtomGroupId = atomGroupData.AtomGroupId;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = (from a in ctx.DbContext.AtomGroup
                                     where a.AtomGroupId == this.AtomGroupId
                                     select a).FirstOrDefault();
                atomGroupData.LogicalOperator = this.LogicalOperator.ToString();
                atomGroupData.Name = this.Name;
                SaveChildren();
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
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
                SaveChildren();
                FieldManager.UpdateChildren(this);
                //ctx.DbContext.SaveChanges();
            }
        }

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
    }
}
