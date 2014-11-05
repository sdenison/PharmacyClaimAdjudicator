using Csla.Core;
using Csla.Rules;

namespace PharmacyAdjudicator.Library.Core.Group
{
    public class ClientAssignmentsCannotOverlap : Csla.Rules.BusinessRule
    {

        public ClientAssignmentsCannotOverlap(IPropertyInfo primaryProperty) : base(primaryProperty)
        {
        }

        protected override void Execute(RuleContext context)
        {
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
                            if (clientAssignmentToCheck.ExpirationDate >= clientAssignment.EffectiveDate)
                            {
                                hasOverlap = true;
                                break;
                            }
                        }
                        //hasOverlap = true when the effective date is greater but the expiration date is less than the next expiration date
                        if (clientAssignmentToCheck.EffectiveDate > clientAssignment.EffectiveDate)
                        {
                            if (clientAssignmentToCheck.ExpirationDate <= clientAssignment.ExpirationDate)
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
