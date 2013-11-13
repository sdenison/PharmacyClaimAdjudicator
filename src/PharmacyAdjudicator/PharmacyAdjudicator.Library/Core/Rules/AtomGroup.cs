using System;
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

        public static readonly PropertyInfo<AtomList> AtomsProperty = RegisterProperty<AtomList>(c => c.Atoms);
        public AtomList Atoms
        {
            get { return GetProperty(AtomsProperty); }
            set { SetProperty(AtomsProperty, value); }
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

        private void DataPortal_Fetch(int criteria)
        {
            // TODO: load values
        }

        protected override void DataPortal_Insert()
        {
            // TODO: insert values
        }

        protected override void DataPortal_Update()
        {
            // TODO: update values
        }

        protected override void DataPortal_DeleteSelf()
        {
            //DataPortal_Delete(this.Id);
        }

        private void DataPortal_Delete(int criteria)
        {
            // TODO: delete values
        }

        #endregion
    }
}
