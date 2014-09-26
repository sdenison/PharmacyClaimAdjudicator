using Csla.Core;
using Csla.Rules;
using System.Collections.Generic;

namespace PharmacyAdjudicator.Library.Core.Group
{
    public class ExpirationCannotBeLessThanEffectiveDateRule : Csla.Rules.BusinessRule
    {

        // TODO: Add additional parameters to your rule to the constructor
        public ExpirationCannotBeLessThanEffectiveDateRule(IPropertyInfo primaryProperty)
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
            var assignment = (ClientAssignment)context.Target;
            if (assignment.ExpirationDate.CompareTo(assignment.EffectiveDate) < 0 )
                context.AddErrorResult("Expiration Date cannot be greater than Expiration Date.");


            // TODO: Add actual rule code here. 
            //if (broken condition)
            //{
            //  context.AddErrorResult("Broken rule message");
            //}
        }
    }
}
