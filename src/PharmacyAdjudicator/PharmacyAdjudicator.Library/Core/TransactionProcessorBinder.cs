using System;
using System.Collections.Generic;
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
                PropertyInfo property = typeof(Core.Transaction).GetProperty(fact.Type);
                if (Attribute.IsDefined(property, typeof(InferrableAttribute)))
                {
                    if (property.PropertyType == typeof(bool))
                        property.SetValue(((Core.Transaction)fact.GetPredicateValue(0)), bool.Parse(fact.GetPredicateValue(1).ToString()));
                    else if (property.PropertyType == typeof(string))
                        property.SetValue(((Core.Transaction)fact.GetPredicateValue(0)), fact.GetPredicateValue(1).ToString());
                    else if (property.PropertyType == typeof(decimal))
                        property.SetValue(((Core.Transaction)fact.GetPredicateValue(0)), decimal.Parse(fact.GetPredicateValue(1).ToString()));
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
        }
    }
}