using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library;
using Csla.Core;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.RulesTests
{
    /// <summary>
    /// Summary description for RuleTests
    /// </summary>
    [TestClass]
    public class RuleTests
    {
        [ClassInitialize()]
        public static void Setup(TestContext testContext)
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            //var principal = new System.Security.Principal.GenericPrincipal(
            //    new System.Security.Principal.GenericIdentity("Test"),
            //    new string[] { "PatientViewer" });
            Csla.ApplicationContext.User = principal;

            //Using SQL Server script to recreate the datC:\Users\sdenison\work\Projects\PharmacyClaimAdjudicator\src\PharmacyAdjudicator\PharmacyAdjudicator.LibraryTests\CoreTests\RulesTests\AtomTests.csabase
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "Scripts\\recreate_database.bat";
            proc.StartInfo.RedirectStandardError = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
        }

        public RuleTests()
        {
        }

        [TestMethod]
        public void Can_create_boolean_rule_type_with_default()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "Formulary";
            rule.DefaultValue = true;
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
            rule.DefaultValue = Library.Core.Enums.ResponseStatus.Rejected;
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
            SaveChild(rule);
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
            implication.Head.Value = (20).ToString();

            implication.Body = Library.Core.Rules.AtomGroup.NewAtomGroup();
            implication.Body.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            var atom1 = implication.Body.AddAtom();//Library.Core.Rules.Atom.NewAtom();
            atom1.Class = "Transaction";
            atom1.Property = "Formulary";
            atom1.Value = "True";
            //implication.Body.AddPredicate(atom1);
            rule.Implications.Add(implication);
            //var transFormularyTrue = Library.Core.Rules.AtomGroup.NewAtomGroup();
            SaveChild(rule);
            var ruleFromDb = Library.Core.Rules.Rule.GetByRuleId(rule.RuleId);
            Assert.IsNotNull(ruleFromDb);
            Assert.IsTrue(ruleFromDb.Implications.Count == 1);
        }

        [TestMethod]
        public void Can_create_implication_from_rule()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "AmountOfCopay";
            rule.DefaultValue = (5.5).ToString();
            var implication = rule.AddImplication();
            Assert.IsTrue(implication.Head.Property.Equals("AmountOfCopay"));
            implication.Head.Value = (20).ToString();
            implication.Body.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            var atom1 = implication.Body.AddAtom();
            atom1.Class = "Transaction";
            atom1.Property = "Formulary";
            atom1.Value = "True";
            SaveChild(rule);
            var ruleFromDb = Library.Core.Rules.Rule.GetByRuleId(rule.RuleId);
            Assert.IsNotNull(ruleFromDb);
            Assert.IsTrue(ruleFromDb.Implications.Count == 1);
        }

        [TestMethod]
        public void Can_not_add_rule_with_implication_that_has_different_property_type_than_rule()
        {
            var rule = Library.Core.Rules.Rule.NewRule();
            rule.RuleType = "AmountOfCopay";
            rule.DefaultValue = (5.5).ToString();

            var implication = Library.Core.Rules.Implication.NewImplication();
            implication.Head = Library.Core.Rules.Atom.NewAtom();
            implication.Head.Class = "Transaction";
            implication.Head.Property = "DispensingFeePaid";
            implication.Head.Value = (20).ToString();

            implication.Body = Library.Core.Rules.AtomGroup.NewAtomGroup();
            implication.Body.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            var atom1 = implication.Body.AddAtom(); // Library.Core.Rules.Atom.NewAtom();
            atom1.Class = "Transaction";
            atom1.Property = "Formulary";
            atom1.Value = "True";
            rule.AddImplication(implication);
            var brokenRules = rule.GetBrokenRules();
            Assert.IsTrue(rule.BrokenRulesCollection.Count == 1); //The implication.Head.Property should be the same as rule.RuleType
            Assert.IsTrue(rule.BrokenRulesCollection[0].Description.Contains("AmountOfCopay cannot"));
        }

        private void SaveChild(Library.Core.Rules.Rule child)
        {
            //add using statement to mimic what happens in root
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                Csla.DataPortal.UpdateChild(child);
                ctx.DbContext.SaveChanges();
            }
        }
    }
}
