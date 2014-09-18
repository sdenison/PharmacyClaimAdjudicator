using Csla.Core;
using Csla.Rules;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyAdjudicator.Library.Core
{
    public class OnlyUniqueAddressTypesAllowedInAddressList : Csla.Rules.BusinessRule
    {

        // TODO: Add additional parameters to your rule to the constructor
        public OnlyUniqueAddressTypesAllowedInAddressList(IPropertyInfo primaryProperty)
            : base(primaryProperty)
        {
            // TODO: If you are  going to add InputProperties make sure to uncomment line below as InputProperties is NULL by default
            if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

            // TODO: Add additional constructor code here 


            // TODO: Marke rule for IsAsync if Execute method implemets asyncronous calls
            // IsAsync = true; 
        }

        protected override void Execute(RuleContext context)
        {
            // TODO: Asyncronous rules 
            // If rule is async make sure that ALL excution paths call context.Complete


            // TODO: Add actual rule code here. 
            var patient = (Patient)context.Target;
            var query = patient.PatientAddresses.GroupBy(p => p.AddressType)
                        .Where(g => g.Count() > 1)
                        .Select(y => y.Key)
                        .ToList();
            if (query.Count > 0)
                context.AddErrorResult("Address list cannot contain duplicate address types.");
            
            //if (broken condition)
            //{
            //  context.AddErrorResult("Broken rule message");
            //}
        }
    }
}
