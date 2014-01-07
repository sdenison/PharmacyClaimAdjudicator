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

        public static readonly PropertyInfo<int> AtomGroupIdProperty = RegisterProperty<int>(c => c.AtomGroupId);
        public int AtomGroupId
        {
            get { return GetProperty(AtomGroupIdProperty); }
            set { SetProperty(AtomGroupIdProperty, value); }
        }

        public static readonly PropertyInfo<PredicateList> PredicatesProperty = RegisterProperty<PredicateList>(c => c.Predicates);
        public PredicateList Predicates
        {
            get 
            {
                if (!(FieldManager.FieldExists(PredicatesProperty)))
                    LoadProperty(PredicatesProperty, DataPortal.CreateChild<PredicateList>(this.AtomGroupId));
                return GetProperty(PredicatesProperty); 
            }
            set { SetProperty(PredicatesProperty, value); }
        }

        public void AddPredicate(Atom predicate)
        {
            this.Predicates.Add(this, predicate);
        }

        public void AddPredicate(AtomGroup predicate)
        {
            this.Predicates.Add(this, predicate);
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

        public static AtomGroup GetById(int id)
        {
            return DataPortal.Fetch<AtomGroup>(id);
        }

        public static void DeleteById(int id)
        {
            DataPortal.Delete<AtomGroup>(id);
        }

        private AtomGroup()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(int atomGroupId)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = (from a in ctx.DbContext.AtomGroups
                                     where a.AtomGroupId == atomGroupId
                                     select a).FirstOrDefault();

                if (atomGroupData == null)
                    throw new DataNotFoundException("AtomGroupId = " + atomGroupId.ToString());
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(atomGroupData);
                    var predicateData = from p in ctx.DbContext.AtomGroupItems
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
                ctx.DbContext.AtomGroups.Add(atomGroupData);
                ctx.DbContext.SaveChanges();
                this.AtomGroupId = atomGroupData.AtomGroupId;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupData = (from a in ctx.DbContext.AtomGroups
                                     where a.AtomGroupId == this.AtomGroupId
                                     select a).FirstOrDefault();
                atomGroupData.LogicalOperator = this.LogicalOperator.ToString();
                atomGroupData.Name = this.Name;
                ctx.DbContext.SaveChanges();
                FieldManager.UpdateChildren(this);
                //this.Predicates.Save();
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            //DataPortal_Delete(this.Id);
        }

        private void DataPortal_Delete(int criteria)
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
            atomGroupData.LogicalOperator = this.LogicalOperator.ToString();
            atomGroupData.Name = this.Name;

            return atomGroupData;
        }

        #endregion
    }
}