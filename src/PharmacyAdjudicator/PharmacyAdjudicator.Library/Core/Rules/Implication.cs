using System;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class Implication : BusinessBase<Implication>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> LabelProperty = RegisterProperty<string>(c => c.Label);
        public string Label
        {
            get { return GetProperty(LabelProperty); }
            set { SetProperty(LabelProperty, value); }
        }

        public static readonly PropertyInfo<Atom> HeadProperty = RegisterProperty<Atom>(c => c.Head);
        public Atom Head
        {
            get { return GetProperty(HeadProperty); }
            set { SetProperty(HeadProperty, value); }
        }

        public static readonly PropertyInfo<Predicate> BodyProperty = RegisterProperty<Predicate>(c => c.Body);
        public Predicate Body
        {
            get { return GetProperty(BodyProperty); }
            set { SetProperty(BodyProperty, value); }
        }

        public static readonly PropertyInfo<long> ImplicationIdProperty = RegisterProperty<long>(c => c.ImplicationId);
        public long ImplicationId
        {
            get { return GetProperty(ImplicationIdProperty); }
            private set { LoadProperty(ImplicationIdProperty, value); }
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

        public static Implication NewImplication()
        {
            return DataPortal.Create<Implication>();
        }

        public static Implication GetById(long id)
        {
            return DataPortal.Fetch<Implication>(id);
        }

        public static Implication GetByLabel(string label)
        {
            return DataPortal.Fetch<Implication>(label);
        }

        public static void DeleteById(long id)
        {
            DataPortal.Delete<Implication>(id);
        }

        private Implication()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var identityAtom = new DataAccess.Implication();
                ctx.DbContext.SaveChanges();
                this.ImplicationId = identityAtom.ImplicationId;
            }
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(long criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var implicationData = (from i in ctx.DbContext.Implications
                                           where i.ImplicationId == criteria
                                           select i).FirstOrDefault();
                if (implicationData == null)
                    throw new DataNotFoundException("ImplicationId = " + criteria);
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(implicationData);
                }
            }
        }


        private void DataPortal_Fetch(string criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var implicationData = (from i in ctx.DbContext.Implications
                                       where i.Label == criteria
                                       select i).FirstOrDefault();
                if (implicationData == null)
                    throw new DataNotFoundException("ImplicationId = " + criteria);
                using (BypassPropertyChecks)
                {
                    PopulateByEntity(implicationData);
                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var implicationData = new DataAccess.Implication();
                implicationData.AtomGroupId = this.Body.AtomGroupId;
                implicationData.DeductionAtomId = this.Head.AtomId;
                ctx.DbContext.Implications.Add(implicationData);
                ctx.DbContext.SaveChanges();
                using (BypassPropertyChecks)
                    this.ImplicationId = implicationData.ImplicationId;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //var implicationData = (from i in ctx.DbContext.Implications
                //                       where i.Label == criteria
                //                       select i).FirstOrDefault();
                this.Head.Save();
                this.Body.Save();
                //;this.FieldManager.UpdateChildren();
                //if (implicationData == null)
                //    throw new DataNotFoundException("ImplicationId = " + criteria);
                //using (BypassPropertyChecks)
                //{
                //    PopulateByEntity(implicationData);
                //}
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.ImplicationId);
        }

        private void DataPortal_Delete(long criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var implicationData = (from i in ctx.DbContext.Implications
                                       where i.ImplicationId == criteria
                                       select i).FirstOrDefault();
                ctx.DbContext.Implications.Remove(implicationData);
                ctx.DbContext.SaveChanges();
            }
        }

        private void PopulateByEntity(DataAccess.Implication implicationData)
        {
            this.ImplicationId = implicationData.ImplicationId;
            this.Head = Atom.GetByAtomId(implicationData.DeductionAtomId.Value);
            this.Body = Predicate.GetByRecordId(implicationData.AtomGroupId);
        }

        #endregion
    }
}
