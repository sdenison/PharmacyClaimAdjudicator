using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.RulesTests
{
    /// <summary>
    /// Summary description for RuleTests
    /// </summary>
    [TestClass]
    public class RuleTests
    {
        public RuleTests()
        {
        }

        [TestMethod]
        public void Can_create_boolean_rule_type_with_default()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "Formulary";
            rule.DefaultValue = "true";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_argument_exception_when_default_set_to_non_boolean_value()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "Formulary";
            rule.DefaultValue = "asdf"; //Will throw exception because Formulary is boolean and default value cannot be converted to boolean.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_argument_exception_when_RuleType_does_not_exist_as_inferrable_type_of_Transaction()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "asdf"; //Not a valid inferrable property of Formulary.
        }

        [TestMethod]
        public void Can_set_response_type_default_value()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "ResponseStatus";
            rule.DefaultValue = Library.Core.Enums.ResponseStatus.Rejected.ToString();
        }

        [TestMethod]
        public void Can_set_default_for_AmountOfCopay()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "AmountOfCopay";
            rule.DefaultValue = (5.5).ToString();  //sets default copay to $5.50
        }

        [TestMethod]
        public void Can_save_rule()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "AmountOfCopay";
            rule.DefaultValue = (5.5).ToString();
            rule = rule.Save();
            var ruleFromDb = Library.Core.Rules.Rule.GetByRuleId(rule.RuleId);
            Assert.IsNotNull(ruleFromDb);
        }

        [TestMethod]
        public void Can_add_rule_with_implication()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "AmountOfCopay";
            rule.DefaultValue = (5.5).ToString();

            var implication = Library.Core.Rules.Implication.NewImplication();
            implication.Head = Library.Core.Rules.Atom.NewAtom();
            implication.Head.Class = "Transaction";
            implication.Head.Property = "AmountOfCopay";
            implication.Head.Value = "20";

            implication.Body = Library.Core.Rules.AtomGroup.NewAtomGroup();
            implication.Body.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            var atom1 = Library.Core.Rules.Atom.NewAtom();
            atom1.Class = "Transaction";
            atom1.Property = "Formulary";
            atom1.Value = "True";
            implication.Body.AddPredicate(atom1);

            rule.Implications.Add(implication);

            var transFormularyTrue = Library.Core.Rules.AtomGroup.NewAtomGroup();
            

            rule = rule.Save();
            var ruleFromDb = Library.Core.Rules.Rule.GetByRuleId(rule.RuleId);
            Assert.IsNotNull(ruleFromDb);
        }

    }
}
