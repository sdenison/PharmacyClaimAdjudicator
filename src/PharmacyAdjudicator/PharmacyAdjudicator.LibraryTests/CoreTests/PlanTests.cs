using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PharmacyAdjudicator.Library.Core;
using NxBRE.InferenceEngine;
using NxBRE.InferenceEngine.Rules;
using System.IO;
using PharmacyAdjudicator.Library.Core.Rules;
using PharmacyAdjudicator.Library.Core.Plan;

namespace PharmacyAdjudicator.TestLibrary.CoreTests
{
    [TestClass]
    public class PlanTests
    {
        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            //var principal = new System.Security.Principal.GenericPrincipal(
            //    new System.Security.Principal.GenericIdentity("Test"),
            //    new string[] { "PatientViewer" });
            Csla.ApplicationContext.User = principal;

            //Using SQL Server script to recreate the database
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "Scripts\\recreate_database.bat";
            proc.StartInfo.RedirectStandardError = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            Csla.ApplicationContext.User = principal;
        }

        [TestMethod]
        public void Should_be_able_to_create_plan_and_all_rules_should_be_present()
        {
            PlanEdit testPlan = PlanEdit.NewPlan("NEW-PLAN-ID-1");
            testPlan.Name = "This is a test plan";
            Assert.IsTrue(testPlan.AssignedRules.Count > 0);
            testPlan = testPlan.Save();
        }

        [TestMethod]
        public void Formulary_is_true_using_VaClass_and_DosageForm_with_persistable_rules()
        {
            var onTheFlyRules = new Library.Core.RuleBase();

            PlanEdit testPlan = PlanEdit.NewPlan("NEW-PLAN-ID-2");
            testPlan.Name = "This is the first plan to process a claim";

            //Create a Formulary = true implication with the following atoms
            var atom1 = Library.Core.Rules.Atom.NewAtom();
            atom1.Class = "Drug";
            atom1.Property = "DosageForm";
            atom1.Value = "PWDR,RENST-ORAL";
            atom1 = atom1.Save();

            var atom2 = Library.Core.Rules.Atom.NewAtom();
            atom2.Class = "Drug";
            atom2.Property = "DosageForm";
            atom2.Value = "TAB";
            atom2 = atom2.Save();

            var dosageAtomGroup = Library.Core.Rules.AtomGroup.NewAtomGroup();
            dosageAtomGroup.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
            dosageAtomGroup.AddPredicate(atom1);
            dosageAtomGroup.AddPredicate(atom2);
            dosageAtomGroup.Name = "DosageForm is TAB or PWDR,RENST-ORAL";
            dosageAtomGroup = dosageAtomGroup.Save();


            var drugClassAtom = Library.Core.Rules.Atom.NewAtom();
            drugClassAtom.Class = "Drug";
            drugClassAtom.Property = "VaClass";
            drugClassAtom.Value = "PENICILLINS,AMINO DERIVATIVES";
            drugClassAtom = drugClassAtom.Save();

            //Testing operations
            var atom3 = Library.Core.Rules.Atom.NewAtom();
            atom3.Class = "Drug";
            atom3.Property = "Ndc";
            atom3.Value = "9999*";
            atom3.Operation = "Matches";

            var pennicillinsOrNdcStartsWith = Library.Core.Rules.AtomGroup.NewAtomGroup();
            pennicillinsOrNdcStartsWith.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
            pennicillinsOrNdcStartsWith.AddPredicate(drugClassAtom);
            pennicillinsOrNdcStartsWith.AddPredicate(atom3);
            pennicillinsOrNdcStartsWith = pennicillinsOrNdcStartsWith.Save();

            var dosageAndClassAtomGroup = Library.Core.Rules.AtomGroup.NewAtomGroup();
            dosageAndClassAtomGroup.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            dosageAndClassAtomGroup.AddPredicate(pennicillinsOrNdcStartsWith);
            dosageAndClassAtomGroup.AddPredicate(dosageAtomGroup);
            dosageAndClassAtomGroup = dosageAndClassAtomGroup.Save();

            var dosageClassFormularyImplication = Library.Core.Rules.Implication.NewImplication();
            var head1 = Library.Core.Rules.Atom.NewAtom();
            head1.Class = "Transaction";
            head1.Property = "Formulary";
            head1.Value = "true";
            dosageClassFormularyImplication.Head = head1;
            dosageClassFormularyImplication.Body = dosageAndClassAtomGroup;
            dosageClassFormularyImplication.Label = "PENICILLINS,AMINO DERIVATIVES with PWDR,RENST-ORAL OR TAB are formulary";

            var rule = testPlan.AssignedRules.FirstOrDefault(r => r.RuleType == "Formulary");
            rule.AddImplication(dosageClassFormularyImplication);
            testPlan = testPlan.Save();

            var formularyDrugsAre5Dollars = Library.Core.Rules.Implication.NewImplication();
            formularyDrugsAre5Dollars.Label = "Formulary drugs have 5 dollar copay";

            formularyDrugsAre5Dollars.Body = Library.Core.Rules.AtomGroup.NewAtomGroup();
            var formularyTrueAtom = Library.Core.Rules.Atom.NewAtom();
            formularyTrueAtom.Class = "Transaction";
            formularyTrueAtom.Property = "Formulary";
            formularyTrueAtom.Value = "true";
            formularyDrugsAre5Dollars.Body.AddPredicate(formularyTrueAtom);

            var amountOfCopay5Dollars = Library.Core.Rules.Atom.NewAtom();
            amountOfCopay5Dollars.Class = "Transaction";
            amountOfCopay5Dollars.Property = "AmountOfCopay";
            amountOfCopay5Dollars.Value = "5";
            formularyDrugsAre5Dollars.Head = amountOfCopay5Dollars;
            rule = testPlan.AssignedRules.FirstOrDefault(r => r.RuleType == "AmountOfCopay");
            rule.DefaultValue = "100";
            rule.AddImplication(formularyDrugsAre5Dollars);
            testPlan = testPlan.Save();

            //This drug should match based on Dosage Form and VaClass
            var drug = Library.Core.Drug.GetByNdc("52959061700");
            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, testPlan);
            Assert.IsTrue(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)5.0); //number 5 is alive

            //This drug should not match
            var drug2 = Library.Core.Drug.GetByNdc("00000000000");
            var transaction2 = new Library.Core.Transaction(drug2);
            var transAfterProcessing2 = Library.Core.TransactionProcessor.Process(transaction2, testPlan);
            Assert.IsFalse(transAfterProcessing2.Formulary);
            //Plan default for AmountOfCopay was set to 100.  Since formulary didn't change neither did the AmountOfCopay default.
            Assert.IsTrue(transAfterProcessing2.AmountOfCopay == 100);

            //This drug should match based on NDC number and not on VaClass
            var drug3 = Library.Core.Drug.GetByNdc("99999990001");
            var transaction3 = new Library.Core.Transaction(drug3);
            var transAfterProcessing3 = Library.Core.TransactionProcessor.Process(transaction3, testPlan);
            Assert.IsTrue(transAfterProcessing3.Formulary);
        }
    }
}
