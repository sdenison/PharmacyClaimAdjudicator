using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using NxBRE.InferenceEngine.Rules;

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

        public static readonly PropertyInfo<AtomGroup> BodyProperty = RegisterProperty<AtomGroup>(c => c.Body);
        public AtomGroup Body
        {
            get { return GetProperty(BodyProperty); }
            set { SetProperty(BodyProperty, value); }
        }

        public static readonly PropertyInfo<Guid> ImplicationIdProperty = RegisterProperty<Guid>(c => c.ImplicationId);
        public Guid ImplicationId
        {
            get { return GetProperty(ImplicationIdProperty); }
            private set { LoadProperty(ImplicationIdProperty, value); }
        }

        /// <summary>
        /// Converts Implication into NxBRE implication.
        /// </summary>
        /// <returns></returns>
        public NxBRE.InferenceEngine.Rules.Implication ToNxBre()
        {
            var head = this.Head.ToNxBre();
            List<NxBRE.InferenceEngine.Rules.Atom> containAtoms = new List<NxBRE.InferenceEngine.Rules.Atom>();
            
            //We have to assert a Contains fact so unification can happen with objects contained within the transaction.  eg. Drug, Patient, Doctor, Pharmacy
            foreach(var contains in Body.ComplexFactsUsed())
            {
                if (!contains.Equals("Transaction"))
                {
                    containAtoms.Add(new NxBRE.InferenceEngine.Rules.Atom("Contains", new NxBRE.InferenceEngine.Rules.Variable("Transaction"), new NxBRE.InferenceEngine.Rules.Variable(contains)));
                }
            }
            if (containAtoms.Count == 0)
            {
                return new NxBRE.InferenceEngine.Rules.Implication(this.Label, ImplicationPriority.Medium, "", "", head, Body.ToNxBre());
            }
            else
            {
                List<object> members = Body.ToNxBre().Members.ToList();
                foreach (var atom in containAtoms)
                {
                    members.Add(atom);
                }
                var returnAtomGroup = new NxBRE.InferenceEngine.Rules.AtomGroup(NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And, members.ToArray());
                return new NxBRE.InferenceEngine.Rules.Implication(this.Label, ImplicationPriority.Medium, "", "", head, returnAtomGroup);
            }
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

        public static Implication GetById(Guid id)
        {
            return DataPortal.Fetch<Implication>(id);
        }

        public static Implication GetByLabel(string label)
        {
            return DataPortal.Fetch<Implication>(label);
        }

        public static void DeleteById(Guid id)
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
            //using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            //{
            //    var identityAtom = new DataAccess.Implication();
            //    ctx.DbContext.SaveChanges();
            //}
            this.ImplicationId = Guid.NewGuid();
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(Guid criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var implicationData = (from i in ctx.DbContext.Implication
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
                var implicationData = (from i in ctx.DbContext.Implication
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
                implicationData.ImplicationId = this.ImplicationId;
                implicationData.AtomGroupId = this.Body.AtomGroupId;
                implicationData.DeductionAtomId = this.Head.AtomId;
                implicationData.Label = this.Label;
                FieldManager.UpdateChildren(this);
                ctx.DbContext.Implication.Add(implicationData);
                ctx.DbContext.SaveChanges();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //var implicationData = (from i in ctx.DbContext.Implications
                //                       where i.Label == criteria
                //                       select i).FirstOrDefault();

                //
                //this.Head.Save();
                //this.Body.Save();


                this.FieldManager.UpdateChildren();
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

        private void DataPortal_Delete(Guid criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var implicationData = (from i in ctx.DbContext.Implication
                                       where i.ImplicationId == criteria
                                       select i).FirstOrDefault();
                ctx.DbContext.Implication.Remove(implicationData);
                ctx.DbContext.SaveChanges();
            }
        }

        private void PopulateByEntity(DataAccess.Implication implicationData)
        {
            this.ImplicationId = implicationData.ImplicationId;
            this.Head = Atom.GetByAtomId(implicationData.DeductionAtomId);
            this.Body = AtomGroup.GetById(implicationData.AtomGroupId);
        }

        #endregion
    }
}
