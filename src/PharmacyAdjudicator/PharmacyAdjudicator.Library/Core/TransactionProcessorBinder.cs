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
                var transaction = (Core.Transaction)fact.GetPredicateValue(0);
                var value = fact.GetPredicateValue(1).ToString();
                PropertyInfo property = typeof(Core.Transaction).GetProperty(fact.Type);

                SetProperty(property, transaction, value);
            }
            else
            {
                throw new Exception("Tried to set a property for a type that is not Core.Transaction.  This is not supported yet.");
            }
        }

        /// <summary>
        /// Grinds business objects to facts so that the inference engine can reason about them.
        /// </summary>
        /// <param name="objectToGrind"></param>
        private void GrindObjectToFacts(object objectToGrind)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>(objectToGrind.GetType().GetProperties());
            foreach (var property in properties)
            {
                //Bind defaults from rules
                if (IEF.FactExists("Default " + property.Name))
                {
                    var fact = IEF.GetFact("Default " + property.Name);
                    if (objectToGrind.GetType() == typeof(Core.Transaction))
                        SetProperty(property, (Core.Transaction) objectToGrind, fact.GetPredicateValue(0).ToString());
                }
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
        }

        /// <summary>
        /// Sets the property on the transaction record from the object value
        /// </summary>
        /// <param name="property">Description of property to set value on</param>
        /// <param name="transaction">Object to set the property on</param>
        /// <param name="value">Value that the property should be set to.  Throws Exception if types don't cast correctly</param>
        private void SetProperty(PropertyInfo property, Core.Transaction transaction, string value)
        {
            if (Attribute.IsDefined(property, typeof(InferrableAttribute)))
            {
                if (property.PropertyType == typeof(bool))
                    property.SetValue(transaction, bool.Parse(value));
                else if (property.PropertyType == typeof(string))
                    property.SetValue(transaction, (string)value);
                else if (property.PropertyType == typeof(decimal))
                    property.SetValue(transaction, decimal.Parse(value.ToString()));
                else if (property.PropertyType == typeof(Enums.BasisOfReimbursement))
                    property.SetValue(transaction, (Enums.BasisOfReimbursement)int.Parse(value));
                else
                    throw new Exception("No conversion defined for " + property.Name + ".");
            }
            else
            {
                throw new Exception("Tried to set property type of " + property.Name + " but the property is not marked as Inferrable");
            }
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