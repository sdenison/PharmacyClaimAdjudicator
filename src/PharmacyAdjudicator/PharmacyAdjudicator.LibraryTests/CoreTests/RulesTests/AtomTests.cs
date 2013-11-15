using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PharmacyAdjudicator.Library.Core;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.RulesTests
{
    [TestClass]
    public class AtomTests
    {
        //[TestInitialize()]
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
        public void Can_create_formulary_true_atom()
        {
            var myAtom = Library.Core.Rules.Atom.NewAtom();
            myAtom.Class = "Transaction";
            myAtom.Property = "Formulary";
            myAtom.Value = "True";

            myAtom.Save();

            NxBRE.InferenceEngine.Rules.Atom nxBreAtom = myAtom.GetInferenceEngineAtom();
            Assert.AreEqual(nxBreAtom.Type, myAtom.Property);
        }

        [TestMethod]
        public void Can_retrieve_an_atom_and_update_values()
        {
            var myAtom = Library.Core.Rules.Atom.NewAtom();
            myAtom.Class = "Transaction";
            myAtom.Property = "Formulary";
            myAtom.Value = "False";
            myAtom = myAtom.Save();
            Assert.AreEqual(myAtom.Value, "False");

            var atomFromDatabase = Library.Core.Rules.Atom.GetByAtomId(myAtom.AtomId);
            atomFromDatabase.Value = "True";
            atomFromDatabase = atomFromDatabase.Save();
            Assert.AreEqual(atomFromDatabase.Value, "True");
        }

        //Need AtomGroup to complete this test
        //[TestMethod]
        //public void Can_create_implication_with_stored_atoms()
        //{
        //    var deduction = Library.Core.Rules.Atom.NewAtom();
        //    deduction.Class = "Transaction";
        //    deduction.Property = "Formulary";
        //    deduction.Value = "True";

        //    deduction.Save();

        //    NxBRE.InferenceEngine.Rules.Atom nxBreAtom = deduction.GetInferenceEngineAtom();
        //    Assert.AreEqual(nxBreAtom.Type, deduction.Property);

        //    var patternToMatch = Library.Core.Rules.Atom.NewAtom();
        //    patternToMatch.Class = "Drug";
        //    patternToMatch.Property = "VaClass";
        //    patternToMatch.Value = "NONSALICYLATE NSAIS,ANTIRHEUMATIC";
        //    patternToMatch.Save();

        //    var implication = new NxBRE.InferenceEngine.Rules.Implication("Test formulary implication", NxBRE.InferenceEngine.Rules.ImplicationPriority.Medium,
        //        "", "", deduction.GetInferenceEngineAtom(), );
        //}
    }
}
