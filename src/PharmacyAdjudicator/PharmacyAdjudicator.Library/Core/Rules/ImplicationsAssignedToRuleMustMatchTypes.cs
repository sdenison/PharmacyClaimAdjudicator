using Csla.Core;
using Csla.Rules;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    public class ImplicationsAssignedToRuleMustMatchTypes : Csla.Rules.BusinessRule
    {

        public ImplicationsAssignedToRuleMustMatchTypes(IPropertyInfo primaryProperty) : base(primaryProperty)
        {
        }

        /// <summary>
        /// All implications for a given rule must have a head atom with the same property as the rule
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(RuleContext context)
        {
            var rule = (Rule)context.Target;
            foreach(var implication in rule.Implications)
            {
                if (!implication.Head.Property.Equals(rule.RuleType))
                    context.AddErrorResult("Rule type " + rule.RuleType + " cannot have implication of type " + implication.Head.Property);
            }
        }
    }
}
