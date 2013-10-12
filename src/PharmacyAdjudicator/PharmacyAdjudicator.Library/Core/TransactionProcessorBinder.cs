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

        //Require constructor with arguments to be used
        private TransactionProcessorBinder()
            : base(BindingTypes.BeforeAfter)
        { }

        public TransactionProcessorBinder(Core.Transaction transaction)
            : base(BindingTypes.BeforeAfter)
        {
            BusinessObjects["TRANSACTION"] = transaction;

        }

        public override void BeforeProcess()
        {
            GrindObjectToFacts(BusinessObjects["TRANSACTION"]);
        }

        public override NxBRE.InferenceEngine.NewFactEvent OnNewFact
        {
            get
            {
                return new NxBRE.InferenceEngine.NewFactEvent(NewFactHandler);
            }
        }

        public void NewFactHandler(NewFactEventArgs nfea)
        {
            //if (nfea.Fact.Type == "Formulary")
            //    ((Core.Transaction)nfea.Fact.GetPredicateValue(0)).Formulary = bool.Parse(nfea.Fact.GetPredicateValue(1).ToString());

            if (nfea.Fact.GetPredicateValue(0) is Core.Transaction)
            {
                PropertyInfo property = typeof(Core.Transaction).GetProperty(nfea.Fact.Type);
                if (Attribute.IsDefined(property, typeof(InferrableAttribute)))
                {
                    if (property.PropertyType == typeof(bool))
                        property.SetValue(((Core.Transaction)nfea.Fact.GetPredicateValue(0)), bool.Parse(nfea.Fact.GetPredicateValue(1).ToString()));
                    else if (property.PropertyType == typeof(string))
                        property.SetValue(((Core.Transaction)nfea.Fact.GetPredicateValue(0)), nfea.Fact.GetPredicateValue(1).ToString());
                    else if (property.PropertyType == typeof(decimal))
                        property.SetValue(((Core.Transaction)nfea.Fact.GetPredicateValue(0)), decimal.Parse(nfea.Fact.GetPredicateValue(1).ToString()));
                }
                else
                {
                    throw new Exception("Tried to set property type of " + nfea.Fact.Type + " but the property is not marked as Inferrable");
                }
            }
        }

        private void GrindObjectToFacts(object objectToGrind)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>(objectToGrind.GetType().GetProperties());
            foreach (var property in properties)
            {
                var isFact = Attribute.IsDefined(property, typeof(FactAttribute));
                if (isFact)
                {
                    if (property.PropertyType == typeof(Core.Drug))
                    {
                        GrindObjectToFacts(objectToGrind, (Core.Drug)property.GetValue(objectToGrind));
                    }
                    else
                    {
                        IEF.AssertNewFactOrFail(property.Name, objectToGrind, property.GetValue(objectToGrind));
                    }
                }
            }
        }

        private void GrindObjectToFacts(object objectToGrind, Core.Drug drug)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>(drug.GetType().GetProperties());
            foreach (var property in properties)
            {
                var isFact = Attribute.IsDefined(property, typeof(FactAttribute));
                if (isFact)
                {
                    IEF.AssertNewFactOrFail(property.Name, objectToGrind, drug, property.GetValue(drug));
                }
            }
        }
    }
}