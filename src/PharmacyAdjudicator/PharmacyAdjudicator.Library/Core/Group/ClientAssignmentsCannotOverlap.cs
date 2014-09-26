using Csla.Core;
using Csla.Rules;

namespace PharmacyAdjudicator.Library.Core.Group
{
    public class ClientAssignmentsCannotOverlap : Csla.Rules.BusinessRule
    {

        // TODO: Add additional parameters to your rule to the constructor
        public ClientAssignmentsCannotOverlap(IPropertyInfo primaryProperty) : base(primaryProperty)
        {
            // TODO: If you are  going to add InputProperties make sure to uncomment line below as InputProperties is NULL by default
            //if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

            // TODO: Add additional constructor code here 


            // TODO: Marke rule for IsAsync if Execute method implemets asyncronous calls
            // IsAsync = true; 
        }

        protected override void Execute(RuleContext context)
        {
            // TODO: Asyncronous rules 
            // If rule is async make sure that ALL excution paths call context.Complete
            var group = (GroupEdit)context.Target;
            bool hasOverlap = false;
            foreach(var clientAssignmentToCheck in group.ClientAssignments)
            {
                foreach (var clientAssignment in group.ClientAssignments)
                {
                    //If we're not looking at the same record
                    if (clientAssignmentToCheck.RecordId != clientAssignment.RecordId)
                    {
                        //hasOverlap = true when the effective date is smaller but the expiration date is more than the next effective date
                        if (clientAssignmentToCheck.EffectiveDate <= clientAssignment.EffectiveDate)
                        {
                            if (clientAssignmentToCheck.ExpirationDate > clientAssignment.EffectiveDate)
                            {
                                hasOverlap = true;
                                break;
                            }
                        }
                        //hasOverlap = true when the effective date is greater but the expiration date is less than the next expiration date
                        if (clientAssignmentToCheck.EffectiveDate > clientAssignment.EffectiveDate)
                        {
                            if (clientAssignmentToCheck.ExpirationDate < clientAssignment.ExpirationDate)
                            {
                                hasOverlap = true;
                                break;
                            }
                        }
                    }
                }
                if (hasOverlap) 
                    break;
            }
            if (hasOverlap)
            {
                context.AddErrorResult("Effective and expiration dates cannot overlap other records.");
            }
        }
    }
}
