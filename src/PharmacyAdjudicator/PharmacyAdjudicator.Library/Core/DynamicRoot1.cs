using System;
using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class DynamicRoot1 : BusinessBase<DynamicRoot1>
    {
        #region Business Methods

        // TODO: add your own fields, properties and methods

        // example with private backing field
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, RelationshipTypes.PrivateField);
        private int _Id = IdProperty.DefaultValue;
        public int Id
        {
            get { return GetProperty(IdProperty, _Id); }
            set { SetProperty(IdProperty, ref _Id, value); }
        }

        // example with managed backing field
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        public static DynamicRoot1 NewDynamicRoot()
        {
            return DataPortal.Create<DynamicRoot1>();
        }

        internal static DynamicRoot1 GetDynamicRoot(object rootData)
        {
            return new DynamicRoot1(rootData);
        }

        private DynamicRoot1()
        { /* Require use of factory methods */ }

        private DynamicRoot1(object rootData)
        {
            Fetch(rootData);
        }

        #endregion

        #region Data Access

        private void Fetch(object rootData)
        {
            MarkOld();
            // TODO: load values from rootData
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
            // TODO: delete values
        }

        #endregion
    }
}