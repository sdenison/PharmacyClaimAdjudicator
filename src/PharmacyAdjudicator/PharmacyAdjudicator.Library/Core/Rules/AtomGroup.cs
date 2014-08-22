using System;
using System.Collections.Generic;
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

        public static readonly PropertyInfo<PredicateList> PredicatesProperty = RegisterProperty<PredicateList>(c => c.Predicates, RelationshipTypes.Child);
        public PredicateList Predicates
        {
            get 
            {
                if (!(FieldManager.FieldExists(PredicatesProperty)))
                    LoadProperty(PredicatesProperty, DataPortal.FetchChild<PredicateList>(this.AtomGroupId));
                return GetProperty(PredicatesProperty); 
            }
            private set { SetProperty(PredicatesProperty, value); }
        }

        public void AddPredicate(Atom predicate)
        {
            this.Predicates.Add(this, predicate);
        }

        public void AddPredicate(AtomGroup predicate)
        {
            if (this.LogicalOperator == predicate.LogicalOperator)
                throw new Exception("Child logical operator cannot be the same as parent logical operator.");
            this.Predicates.Add(this, predicate);
        }

        public List<string> ComplexFactsUsed()
        {
            List<string> contains = new List<string>();
            foreach (var predicate in this.Predicates)
            {
                if (predicate.PredicateType == Predicate.PredicateTypeEnum.Atom)
                {
                    if (!contains.Contains(predicate.Atom.Class))
                        contains.Add(predicate.Atom.Class);
                }
                else if (predicate.PredicateType == Predicate.PredicateTypeEnum.AtomGroup)
                {
                    foreach (var complexFactUsed in predicate.AtomGroup.ComplexFactsUsed())
                        if (!contains.Contains(complexFactUsed))
                            contains.Add(complexFactUsed); 
                }
            }
            return contains;
        }

        public NxBRE.InferenceEngine.Rules.AtomGroup GetInferenceEngineAtomGroup()
        {
            //return new NxBRE.InferenceEngine.Rules.Atom(this.Property, new NxBRE.InferenceEngine.Rules.Variable(this.Class), new NxBRE.InferenceEngine.Rules.Individual(this.Value));
            return new NxBRE.InferenceEngine.Rules.AtomGroup(this.LogicalOperator, this.Predicates.ToNxBre());
        }

        public NxBRE.InferenceEngine.Rules.AtomGroup ToNxBre()
        {
            return new NxBRE.InferenceEngine.Rules.AtomGroup(this.LogicalOperator, this.Predicates.ToNxBre().ToArray());
        }

        //public static readonly PropertyInfo<AtomList> AtomsProperty = RegisterProperty<AtomList>(c => c.Atoms);
        //public AtomList Atoms
        //{
        //    get 
        //    {
        //        if (!(FieldManager.FieldExists(AtomsProperty)))
        //            LoadProperty(AtomsProperty, DataPortal.CreateChild<AtomList>());
        //        return GetProperty(AtomsProperty); 
        //    }
        //    private set { SetProperty(AtomsProperty, value); }
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
            //FieldManager.UpdateChildren();
            base.DataPortal_Update();
        }

        protected void Child_Update(Predicate parent)
        {
            //FieldManager.UpdateChildren();
            base.DataPortal_Update();
        }

        //[RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            //using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            //{
            //    var atomGroupData = new DataAccess.AtomGroup();
            //    atomGroupData.Name = "";
            //    atomGroupData.LogicalOperator = "OR";
            //    ctx.DbContext.AtomGroups.Add(atomGroupData);
            //    ctx.DbContext.SaveChanges();
            //    this.AtomGroupId = atomGroupData.AtomGroupId;
            //}
            this.AtomGroupId = Guid.NewGuid();
            this.Predicates = DataPortal.Create<PredicateList>(this);
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
                    var predicateData = from p in ctx.DbContext.AtomGroupItem
                                        where p.AtomGroupId == atomGroupId
                                        orderby p.Priority
                                        select p;
                    if (predicateData != null)
                        Predicates = DataPortal.FetchChild<PredicateList>(predicateData);
                    else
                        Predicates = DataPortal.CreateChild<PredicateList>(atomGroupId);
                }
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = CreateEntity();
                ctx.DbContext.AtomGroup.Add(atomGroupData);
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
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
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
                //this.Predicates.Save();
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
