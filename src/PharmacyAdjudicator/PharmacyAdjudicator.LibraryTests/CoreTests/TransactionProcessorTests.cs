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
            
            //IRuleBaseAdapter onTheFlyRules = new NxBRE.InferenceEngine.Rules.

            var onTheFlyRules = new Library.Core.RuleBase();


            var atom1 = new Atom("DosageForm", new Variable("Drug"), new Individual("PWDR,RENST-ORAL"));
            var atom2 = new Atom("DosageForm", new Variable("Drug"), new Individual("TAB"));
            var dosageAtomGroup = new AtomGroup(AtomGroup.LogicalOperator.Or, atom1, atom2);

            //could also be

            var atom3 = new Atom("VaClass", new Variable("Drug"), new Individual("PENICILLINS,AMINO DERIVATIVES"));

            //var mainAtomGroup = new AtomGroup(AtomGroup.LogicalOperator.And, atom3, dosageAtomGroup, atom4);
            //moving Contains{?Transaction,?Drug} to RuleAdapter.Facts
            var atom4 = new Atom("Contains", new Variable("Transaction"), new Variable("Drug")); 

            //var mainAtomGroup = new AtomGroup(AtomGroup.LogicalOperator.And, atom3, dosageAtomGroup);
            var mainAtomGroup = new AtomGroup(AtomGroup.LogicalOperator.And, atom3, dosageAtomGroup, atom4);

            var deduction1 = new Atom("Formulary", new Variable("Transaction"), new Individual("True"));
            var implication1 = new Implication("PENICILLINS,AMINO DERIVATIVES with PWDR,RENST-ORAL OR TAB are formulary", ImplicationPriority.Medium, "",
                "", deduction1, mainAtomGroup);

            //implication1.AtomGroup.AllAtoms.Add(atom4);

            //var atomGroup = new AtomGroup(AtomGroup.LogicalOperator.And, new Atom("VaClass", new Variable("Drug"), new Individual("ANTI-INFECTIVES,OTHER")),
            //    new Atom("DosageForm", new Variable("Drug"), new Individual("INJ")));
            //var implication = new NxBRE.InferenceEngine.Rules.Implication("ANTI-INFECTIVES,OTHER with INJ are formulary", ImplicationPriority.Minimum,
            //    "", "", new Atom("Formulary", new Variable("Transaction"), new Individual("True")), atomGroup);

            var atom5 = new Atom("Formulary", new Variable("Transaction"), new Individual("True"));
            var atom6 = new Atom("AmountOfCopay", new Variable("Transaction"), new Individual("5"));


            var implication2 = new Implication("Formulary Drugs Are $5 Copay", ImplicationPriority.Medium, "", "", atom6, new AtomGroup(AtomGroup.LogicalOperator.And, atom5));


            onTheFlyRules.Implications.Add(implication1);
            onTheFlyRules.Implications.Add(implication2);

            //Should fail while this is comented out because Transaction is not associated with Drug
            //var atom4 = new Atom("Contains", new Variable("Transaction"), new Variable("Drug"));
            //var fact1 = new Fact("Contains", new Variable("Transaction"), new Variable("Drug"));
            //onTheFlyRules.Facts.Add(fact1);


            //onTheFlyRules.f
            //NxBRE.InferenceEngine.IO.IEFacade fb = new NxBRE.InferenceEngine.IO.IEFacade();

            var binder = new Library.Core.TransactionProcessorBinder();
            IEImpl ie = new IEImpl(binder);

            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\CliniorilShouldHave8copayAndMaxFeeOf8.ruleml", System.IO.FileAccess.Read);
            IEImpl ie2 = new IEImpl(binder);
            ie2.LoadRuleBase(rules);

            //Atom atom1 = new Atom



            var transaction = new Library.Core.Transaction(drug);

            //onTheFlyRules.Facts.Add(new Fact(new Atom("Contains", new Individual(transaction), new Individual(transaction.Drug))));

            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, onTheFlyRules);
            Assert.IsTrue(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.AmountOfCopay, (decimal)5.0);
        }

        //public static Transaction Process(Transaction transaction, IRuleBaseAdapter rules)
        //{
        //    var binder = new TransactionProcessorBinder();
        //    var ie = new IEImpl(binder);
        //    Hashtable transactions = new Hashtable();
        //    transactions.Add("TRANSACTION", transaction);
        //    ie.LoadRuleBase(rules);
        //    ie.Process(transactions);
        //    return transaction;
        //}

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
