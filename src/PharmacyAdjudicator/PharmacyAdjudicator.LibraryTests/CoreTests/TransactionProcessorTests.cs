using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NxBRE.InferenceEngine;
using NxBRE.InferenceEngine.Core;
using NxBRE.InferenceEngine.Rules;

using NxBRE.Util;

namespace PharmacyAdjudicator.TestLibrary.CoreTests
{
    [TestClass]
    public class TransactionProcessorTests
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

            //Using SQL Server script to recreate the database
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "Scripts\\recreate_database.bat";
            proc.StartInfo.RedirectStandardError = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
        } 
        
        [TestMethod]
        public void NonFormularyWorks()
        {
            var drug = Library.Core.Drug.GetByNdc("00006094268");
            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml", System.IO.FileAccess.Read);
            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, rules);
            Assert.IsFalse(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)20.0);
        }

        [TestMethod]
        public void Formulary_is_true_using_VaClass_and_DosageForm()
        {
            //DosageForm = PWDR,RENST-ORAL
            var drug = Library.Core.Drug.GetByNdc("52959061700");
            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml", System.IO.FileAccess.Read);
            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, rules);
            Assert.IsTrue(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)5.0);
        }

        [TestMethod]
        public void Formulary_is_true_using_VaClass_and_DosageForm_with_on_the_fly_rules()
        {
            //DosageForm = PWDR,RENST-ORAL
            var drug = Library.Core.Drug.GetByNdc("52959061700");
            var onTheFlyRules = new Library.Core.RuleBase();

            var atom1 = new Atom("DosageForm", new Variable("Drug"), new Individual("PWDR,RENST-ORAL"));
            var atom2 = new Atom("DosageForm", new Variable("Drug"), new Individual("TAB"));
            var dosageAtomGroup = new AtomGroup(AtomGroup.LogicalOperator.Or, atom1, atom2);
            var atom3 = new Atom("VaClass", new Variable("Drug"), new Individual("PENICILLINS,AMINO DERIVATIVES"));
            //moving Contains{?Transaction,?Drug} to RuleAdapter.Facts
            var atom4 = new Atom("Contains", new Variable("Transaction"), new Variable("Drug")); 
            var mainAtomGroup = new AtomGroup(AtomGroup.LogicalOperator.And, atom3, dosageAtomGroup, atom4);
            var deduction1 = new Atom("Formulary", new Variable("Transaction"), new Individual("True"));
            var implication1 = new Implication("PENICILLINS,AMINO DERIVATIVES with PWDR,RENST-ORAL OR TAB are formulary", ImplicationPriority.Medium, "",
                "", deduction1, mainAtomGroup);
            var atom5 = new Atom("Formulary", new Variable("Transaction"), new Individual("True"));
            var atom6 = new Atom("AmountOfCopay", new Variable("Transaction"), new Individual("5"));
            var implication2 = new Implication("Formulary Drugs Are $5 Copay", ImplicationPriority.Medium, "", "", atom6, new AtomGroup(AtomGroup.LogicalOperator.And, atom5));
            onTheFlyRules.Implications.Add(implication1);
            onTheFlyRules.Implications.Add(implication2);
            var binder = new Library.Core.TransactionProcessorBinder();
            IEImpl ie = new IEImpl(binder);

            var transaction = new Library.Core.Transaction(drug);

            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, onTheFlyRules);
            Assert.IsTrue(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)5.0);
        }


        [TestMethod]
        public void Formulary_is_true_using_VaClass_and_DosageForm_with_persistable_rules()
        {
            
            var onTheFlyRules = new Library.Core.RuleBase();

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
            dosageAtomGroup.LogicalOperator = AtomGroup.LogicalOperator.Or;
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
            pennicillinsOrNdcStartsWith.LogicalOperator = AtomGroup.LogicalOperator.Or;
            pennicillinsOrNdcStartsWith.AddPredicate(drugClassAtom);
            pennicillinsOrNdcStartsWith.AddPredicate(atom3);
            pennicillinsOrNdcStartsWith = pennicillinsOrNdcStartsWith.Save();

            var dosageAndClassAtomGroup = Library.Core.Rules.AtomGroup.NewAtomGroup();
            dosageAndClassAtomGroup.LogicalOperator = AtomGroup.LogicalOperator.And;
            //dosageAndClassAtomGroup.AddPredicate(dosageAtomGroup);
            dosageAndClassAtomGroup.AddPredicate(pennicillinsOrNdcStartsWith);
            dosageAndClassAtomGroup.AddPredicate(dosageAtomGroup);
            dosageAndClassAtomGroup = dosageAndClassAtomGroup.Save();


            var dosageClassFormularyImplication = Library.Core.Rules.Implication.NewImplication();
            var head1 = Library.Core.Rules.Atom.NewAtom();
            head1.Class = "Transaction";
            head1.Property = "Formulary";
            head1.Value = "True";
            dosageClassFormularyImplication.Head = head1;
            dosageClassFormularyImplication.Body = dosageAndClassAtomGroup;
            dosageClassFormularyImplication.Label = "PENICILLINS,AMINO DERIVATIVES with PWDR,RENST-ORAL OR TAB are formulary";
            dosageClassFormularyImplication = dosageClassFormularyImplication.Save();

            var formularyDrugsAre5Dollars = Library.Core.Rules.Implication.NewImplication();
            formularyDrugsAre5Dollars.Label = "Formulary drugs have 5 dollar copay";
            var amountOfCopay5Dollars = Library.Core.Rules.Atom.NewAtom();
            amountOfCopay5Dollars.Class = "Transaction";
            amountOfCopay5Dollars.Property = "AmountOfCopay";
            amountOfCopay5Dollars.Value = "5";
            amountOfCopay5Dollars = amountOfCopay5Dollars.Save();
            formularyDrugsAre5Dollars.Head = amountOfCopay5Dollars;

            var transFormularyTrue = Library.Core.Rules.AtomGroup.NewAtomGroup();
            var head2 = Library.Core.Rules.Atom.GetByAtomId(head1.AtomId);
            transFormularyTrue.AddPredicate(head2);
            transFormularyTrue = transFormularyTrue.Save();
            formularyDrugsAre5Dollars.Body = transFormularyTrue;
            formularyDrugsAre5Dollars = formularyDrugsAre5Dollars.Save();

            var nonFormularyDrugsAre100Dollars = Library.Core.Rules.Implication.NewImplication();
            nonFormularyDrugsAre100Dollars.Label = "Non-formulary drugs have 100 dollar copay";
            var amountOfCopay100Dollars = Library.Core.Rules.Atom.NewAtom();
            amountOfCopay100Dollars.Class = "Transaction";
            amountOfCopay100Dollars.Property = "AmountOfCopay";
            amountOfCopay100Dollars.Value = "100";
            amountOfCopay100Dollars = amountOfCopay100Dollars.Save();
            nonFormularyDrugsAre100Dollars.Head = amountOfCopay100Dollars;

            onTheFlyRules.Implications.Add(dosageClassFormularyImplication.ToNxBre());
            onTheFlyRules.Implications.Add(formularyDrugsAre5Dollars.ToNxBre());
            var binder = new Library.Core.TransactionProcessorBinder();
            IEImpl ie = new IEImpl(binder);

            //This drug should match based on Dosage Form and VaClass
            var drug = Library.Core.Drug.GetByNdc("52959061700");
            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, onTheFlyRules);
            Assert.IsTrue(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)5.0);

            //This drug should not match
            var drug2 = Library.Core.Drug.GetByNdc("00000000000");
            var transaction2 = new Library.Core.Transaction(drug2);
            var transAfterProcessing2 = Library.Core.TransactionProcessor.Process(transaction2, onTheFlyRules);
            Assert.IsFalse(transAfterProcessing2.Formulary);
            //Assert.IsTrue(transAfterProcessing2.AmountOfCopay > 0);

            //This drug should match based on NDC number and not on VaClass
            var drug3 = Library.Core.Drug.GetByNdc("99999990001");
            var transaction3 = new Library.Core.Transaction(drug3);
            var transAfterProcessing3 = Library.Core.TransactionProcessor.Process(transaction3, onTheFlyRules);
            Assert.IsTrue(transAfterProcessing3.Formulary);

        }

        [TestMethod]
        public void AtomGroup_should_save_children_no_matter_how_they_were_created()
        {
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
            pennicillinsOrNdcStartsWith.LogicalOperator = AtomGroup.LogicalOperator.Or;
            pennicillinsOrNdcStartsWith.AddPredicate(drugClassAtom);
            pennicillinsOrNdcStartsWith.AddPredicate(atom3);

            //Should throw an exception when atom3 has not yet been saved.
            try
            {
                var atom3Clone = Library.Core.Rules.Atom.GetByAtomId(atom3.AtomId);
            }
            catch(Exception ex)
            {
                if (ex.GetBaseException() is Library.DataNotFoundException)
                {
                    Assert.IsTrue(true);
                }
                else
                    throw ex;
            }

            pennicillinsOrNdcStartsWith = pennicillinsOrNdcStartsWith.Save();

            //atom3 is not in the database.
            var atom3Clone2 = Library.Core.Rules.Atom.GetByAtomId(atom3.AtomId);
            Assert.IsTrue(atom3Clone2 != null);
        }

        [TestMethod]
        public void Formulary_is_true_using_VaClass_and_DosageForm_2()
        {
            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml", System.IO.FileAccess.Read);
            var otherRules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM900OnFormulary.ruleml", System.IO.FileAccess.Read);
            var compositeRules = new NxBRE.InferenceEngine.IO.CompositeRuleBaseAdapter("this is my composite rule base", "forward", rules, otherRules);

            //Should be on formulary
            //DosageForm = TAB
            //VaClass = PENICILLINS,AMINO DERIVATIVES
            var drug = Library.Core.Drug.GetByNdc("00247002603");
            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, compositeRules);
            Assert.IsTrue(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)5.0);


            //Should be non-formulary with copay of 20
            //VaClass = PADS,GAUZE WITH ADHESIVE
            drug = Library.Core.Drug.GetByNdc("00000099999");
            transaction = new Library.Core.Transaction(drug);
            transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, compositeRules);
            Assert.IsFalse(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)20.0);
        }

        [TestMethod]
        public void Formulary_is_false_using_good_VaClass_and_bad_DosageForm()
        {
            //DosageForm = TAB,CHEWABLE
            var drug = Library.Core.Drug.GetByNdc("21695031542");
            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml", System.IO.FileAccess.Read);

            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, rules);
            Assert.IsFalse(transAfterProcessing.Formulary);

            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)20.0);
        }

    }
}
