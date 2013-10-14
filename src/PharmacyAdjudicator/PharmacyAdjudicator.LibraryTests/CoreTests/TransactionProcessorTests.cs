using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PharmacyAdjudicator.TestLibrary.CoreTests
{
    [TestClass]
    public class TransactionProcessorTests
    {
        [TestMethod]
        public void NonFormularyWorks()
        {

            var drug = Library.Core.Drug.GetByNdc("00006094268");
            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml.xml", System.IO.FileAccess.Read);
            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, rules);
            Assert.IsFalse(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.Copay, (decimal)20.0);
        }

        [TestMethod]
        public void Formulary_is_true_using_VaClass_and_DosageForm()
        {
            //DosageForm = PWDR,RENST-ORAL
            var drug = Library.Core.Drug.GetByNdc("52959061700");
            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml.xml", System.IO.FileAccess.Read);
            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, rules);
            Assert.IsTrue(transAfterProcessing.Formulary);
            Assert.AreEqual(transAfterProcessing.Copay, (decimal)5.0);
        }

        [TestMethod]
        public void Formulary_is_true_using_VaClass_and_DosageForm_2()
        {
            //DosageForm = TAB
            var drug = Library.Core.Drug.GetByNdc("00247002603");
            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml.xml", System.IO.FileAccess.Read);
            var otherRules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM900OnFormulary.ruleml.xml", System.IO.FileAccess.Read);

            var compositeRules = new NxBRE.InferenceEngine.IO.CompositeRuleBaseAdapter("this is my rule base", "forward", rules, otherRules);

            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, compositeRules);
            Assert.IsTrue(transAfterProcessing.Formulary);

            Assert.AreEqual(transAfterProcessing.Copay, (decimal)5.0);
        }

        [TestMethod]
        public void Formulary_is_false_using_good_VaClass_and_bad_DosageForm()
        {
            //DosageForm = TAB,CHEWABLE
            var drug = Library.Core.Drug.GetByNdc("21695031542");
            var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml.xml", System.IO.FileAccess.Read);
            var transaction = new Library.Core.Transaction(drug);
            var transAfterProcessing = Library.Core.TransactionProcessor.Process(transaction, rules);
            Assert.IsFalse(transAfterProcessing.Formulary);

            Assert.AreEqual(transAfterProcessing.Copay, (decimal)20.0);
        }
    }
}
