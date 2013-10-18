using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NxBRE.InferenceEngine;
using NxBRE.InferenceEngine.IO;

namespace PharmacyAdjudicator.Library.Core
{
    public class TransactionProcessorBinder :  AbstractBinder
    {

        public TransactionProcessorBinder()
            : base(BindingTypes.BeforeAfter)
        { }

        public override void BeforeProcess()
        {
            if (IEF.FactExists("DefaultDispensingFee"))
            {
                var fact2 = IEF.GetFact("DefaultDispensingFee");
                var defaultDipsFee = decimal.Parse(fact2.GetPredicateValue(0).ToString());
                ((Core.Transaction)BusinessObjects["TRANSACTION"]).DispensingFeePaid = defaultDipsFee;
            }

            GrindObjectToFacts(BusinessObjects["TRANSACTION"]);
        }

        public override NewFactEvent OnNewFact
        {
            get
            {
                return new NxBRE.InferenceEngine.NewFactEvent(NewFactHandler);
            }
        }

        public void NewFactHandler(NewFactEventArgs nfea)
        {
            HandleFact(nfea.Fact);
        }

        public override NewFactEvent OnModifyFact
        {
            get
            {
                return new NxBRE.InferenceEngine.NewFactEvent(ModifiedFactHandler);
            }
        }

        public void ModifiedFactHandler(NewFactEventArgs nfea)
        {
            HandleFact(nfea.OtherFact);
        }

        /// <summary>
        /// Fires when facts are asserted or modified.  Binds implications back to business objects.
        /// </summary>
        /// <param name="fact"></param>
        private void HandleFact(NxBRE.InferenceEngine.Rules.Fact fact)
        {
            if (fact.GetPredicateValue(0) is Core.Transaction)
            {
                PropertyInfo property = typeof(Core.Transaction).GetProperty(fact.Type);
                if (Attribute.IsDefined(property, typeof(InferrableAttribute)))
                {
                    if (property.PropertyType == typeof(bool))
                        property.SetValue(((Core.Transaction)fact.GetPredicateValue(0)), bool.Parse(fact.GetPredicateValue(1).ToString()));
                    else if (property.PropertyType == typeof(string))
                        property.SetValue(((Core.Transaction)fact.GetPredicateValue(0)), fact.GetPredicateValue(1).ToString());
                    else if (property.PropertyType == typeof(decimal))
                        property.SetValue(((Core.Transaction)fact.GetPredicateValue(0)), decimal.Parse(fact.GetPredicateValue(1).ToString()));
                    else if (property.PropertyType == typeof(Enums.BasisOfReimbursement))
                    {
                        property.SetValue(((Core.Transaction)fact.GetPredicateValue(0)), (Enums.BasisOfReimbursement)int.Parse(fact.GetPredicateValue(1).ToString()));
                    }
                }
                else
                {
                    throw new Exception("Tried to set property type of " + fact.Type + " but the property is not marked as Inferrable");
                }
            }
        }

        /// <summary>
        /// Grinds business objects to facts so that the inference engine can reason about them.
        /// </summary>
        /// <param name="objectToGrind"></param>
        private void GrindObjectToFacts(object objectToGrind)
        {
            //var originalFactsCopy = new List<NxBRE.InferenceEngine.Rules.Fact>();
            //var originalFacts = IEF.Facts;
            //while (originalFacts.MoveNext())
            //{
            //    var originalFact = (NxBRE.InferenceEngine.Rules.Fact)originalFacts.Current;
            //    originalFactsCopy.Add(originalFact);
            //}

            List<PropertyInfo> properties = new List<PropertyInfo>(objectToGrind.GetType().GetProperties());
            foreach (var property in properties)
            {
                //Properties to be ground into facts need to have the Fact or ComplexFact attribute set.
                if (Attribute.IsDefined(property, typeof(FactAttribute)))
                {
                    IEF.AssertNewFactOrFail(property.Name, objectToGrind, property.GetValue(objectToGrind));
                }
                else if (Attribute.IsDefined(property, typeof(ComplexFactAttribute)))
                {
                    IEF.AssertNewFactOrFail("Contains", objectToGrind, property.GetValue(objectToGrind));
                    GrindObjectToFacts(property.GetValue(objectToGrind));
                }
            }

            
            //if (IEF.FactExists("Dispensing Fee Paid"))
            //{
            //    IEF.Modify("Dispensing Fee Paid", IEF.GetFact("Dispensing Fee Paid"));
            //}
            
            //foreach (var fact in originalFactsCopy)
            //{
            //    IEF.Modify(fact, fact);
            //}

        }

        private decimal CalculateIngredientCostPaid(Core.Transaction transaction)
        {
            switch (transaction.BasisOfReimbursement)
            {
                case(Enums.BasisOfReimbursement.IngredientCostPaid):
                    return transaction.IngredientCostSubmitted;
                default:
                    throw new NotImplementedException("BasisOfReimbursement '" + transaction.BasisOfReimbursement + "' is not defined.");
            }
        }

        public override object Compute(string operationName, System.Collections.IDictionary arguments)
        {
            if (operationName == "CalculateIngredientCostPaid")
            {
                return CalculateIngredientCostPaid((Core.Transaction)arguments["Transaction"]);
            }
            else
            {
                throw new NotImplementedException("Operation '" + operationName + "' is not supported by this binder.");
            }
        } 
    }
}