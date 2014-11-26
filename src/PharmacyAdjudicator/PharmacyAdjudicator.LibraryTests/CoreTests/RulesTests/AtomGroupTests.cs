using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Core.Rules;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.RulesTests
{
    [TestClass]
    public class AtomGroupTests
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
        public void Can_create_named_atom_group_with_one_atom_in_item_list()
        {
            var atomGroup = Library.Core.Rules.AtomGroup.NewAtomGroup();
            atomGroup.Name = "First Test Atom Group";
            atomGroup.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            atomGroup = atomGroup.Save();

            var atom = Library.Core.Rules.Atom.NewAtom();
            atom.Class = "Transaction";
            atom.Property = "Formulary";
            atom.Value = "False";
            atom.Save();
            atomGroup.AddPredicate(atom);

            //Assert.AreEqual(1, atomGroup.Predicates.Count);
            Assert.AreEqual(1, atomGroup.Children.Count);// .Predicates.Count);
        }

        [TestMethod]
        public void Can_use_atom_group_as_predicate_in_another_atom_group()
        {
            //Create first AtomGroup
            var atomGroup = Library.Core.Rules.AtomGroup.NewAtomGroup();
            atomGroup.Name = "First Test Atom Group";
            atomGroup.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            atomGroup = atomGroup.Save();

            //Add Atom predicate to first AtomGroup
            var atom = Library.Core.Rules.Atom.NewAtom();
            atom.Class = "Transaction";
            atom.Property = "Formulary";
            atom.Value = "False";
            //atom.Save();
            atomGroup.AddPredicate(atom);
            atomGroup = atomGroup.Save();

            //Assert.AreEqual(1, atomGroup.Predicates.Count);
            Assert.AreEqual(1, atomGroup.Children.Count); //.Predicates.Count);

            //Create a second AtomGroup
            var atomGroup2 = Library.Core.Rules.AtomGroup.NewAtomGroup();
            atomGroup2.Name = "Second Test Atom Group";
            atomGroup2.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
            atomGroup2 = atomGroup2.Save();

            atomGroup2.AddPredicate(atomGroup);
            atomGroup2 = atomGroup2.Save();

            //Assert.AreEqual(atomGroup2.Predicates[0].PredicateType, Library.Core.Rules.Predicate.PredicateTypeEnum.AtomGroup);
            //Assert.AreEqual(atomGroup2.Predicates[0].AtomGroup.Name, "First Test Atom Group");

            var predicate = (AtomGroup)atomGroup2.Children[0];
            Assert.AreEqual(predicate.Name, "First Test Atom Group");
            //Assert.AreEqual(atomGroup2.Children[0].PredicateType, Library.Core.Rules.Predicate.PredicateTypeEnum.AtomGroup);
            //Assert.AreEqual(atomGroup2.Children[0].AtomGroup.Name, "First Test Atom Group");
        }

        [TestMethod]
        public void Can_resolve_back_to_rule_engine_types()
        {
            //Create first AtomGroup
            var atomGroup = Library.Core.Rules.AtomGroup.NewAtomGroup();
            atomGroup.Name = "First Test Atom Group";
            atomGroup.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            atomGroup = atomGroup.Save();

            //Add Atom predicate to first AtomGroup
            var atom = Library.Core.Rules.Atom.NewAtom();
            atom.Class = "Transaction";
            atom.Property = "Formulary";
            atom.Value = "False";
            atom = atom.Save();
            atomGroup.AddPredicate(atom);

            Assert.AreEqual(1, atomGroup.Children.Count);

            //Create a second AtomGroup
            var atomGroup2 = Library.Core.Rules.AtomGroup.NewAtomGroup();
            atomGroup2.Name = "Second Test Atom Group";
            atomGroup2.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
            atomGroup2 = atomGroup2.Save();

            atomGroup2.AddPredicate(atomGroup);
            atomGroup2 = atomGroup2.Save();

            //Assert.AreEqual(atomGroup2.Predicates[0].PredicateType, Library.Core.Rules.Predicate.PredicateTypeEnum.AtomGroup);
            //Assert.AreEqual(atomGroup2.Predicates[0].AtomGroup.Name, "First Test Atom Group");

            var predicate = (AtomGroup)atomGroup2.Children[0];
            Assert.AreEqual(predicate.Name, "First Test Atom Group");

            NxBRE.InferenceEngine.Rules.AtomGroup rulesEngineAtomGroup = atomGroup2.ToNxBre();
            Assert.AreEqual(rulesEngineAtomGroup.AllAtoms.Count, 1);
        }

        [TestMethod]
        public void Recreating_AM111OnFormulary_rules_in_database()
        {
            //Create first AtomGroup
            var atomGroup = Library.Core.Rules.AtomGroup.NewAtomGroup();
            atomGroup.Name = "First Test Atom Group";
            atomGroup.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
            atomGroup = atomGroup.Save();

            //Add Atom predicate to first AtomGroup
            var atom = Library.Core.Rules.Atom.NewAtom();
            atom.Class = "Drug";
            atom.Property = "VaClass";
            atom.Value = "PENICILLINS,AMINO DERIVATIVES";
            atom = atom.Save();
            atomGroup.AddPredicate(atom);

            //Assert.AreEqual(1, atomGroup.Predicates.Count);
            Assert.AreEqual(1, atomGroup.Children.Count);

            //Create a second AtomGroup
            var atomGroup2 = Library.Core.Rules.AtomGroup.NewAtomGroup();
            atomGroup2.Name = "Second Test Atom Group";
            atomGroup2.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
            atomGroup2 = atomGroup2.Save();

            var dosageForm1 = Library.Core.Rules.Atom.NewAtom();
            dosageForm1.Class = "Drug";
            dosageForm1.Property = "DosageForm";
            dosageForm1.Value = "PWDR,RENST-ORAL";
            dosageForm1 = dosageForm1.Save();

            atomGroup2.AddPredicate(dosageForm1);

            var dosageForm2 = Library.Core.Rules.Atom.NewAtom();
            dosageForm2.Class = "Drug";
            dosageForm2.Property = "DosageForm";
            dosageForm2.Value = "TAB";
            dosageForm2 = dosageForm2.Save();

            atomGroup2.AddPredicate(dosageForm2);
            atomGroup2 = atomGroup2.Save();

            //Add Or AtomGroup to first top level AtomGroup
            atomGroup.AddPredicate(atomGroup2);

            ////Hacked: The Contains fact need to be rethought.
            //var containsAtom = Library.Core.Rules.Atom.NewAtom(); 
            //containsAtom.Property = "Contains"; 
            //containsAtom.Class = "Transaction";
            //containsAtom.Value = "Drug";
            //containsAtom = containsAtom.Save();

            //atomGroup.AddPredicate(containsAtom);

            atomGroup = atomGroup.Save();

            NxBRE.InferenceEngine.Rules.AtomGroup rulesEngineAtomGroup = atomGroup.ToNxBre();
            Assert.AreEqual(rulesEngineAtomGroup.AllAtoms.Count, 3);
        }

        //This test is no longer need.  
        //[TestMethod]
        //public void Can_create_and_retrieve_AM111OnFormulary_rules_in_database()
        //{
        //    //Create first AtomGroup
        //    var atomGroup = Library.Core.Rules.AtomGroup.NewAtomGroup();
        //    atomGroup.Name = "First Test Atom Group";
        //    atomGroup.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;
        //    atomGroup = atomGroup.Save();

        //    //Add Atom predicate to first AtomGroup
        //    var atom = Library.Core.Rules.Atom.NewAtom();
        //    atom.Class = "Drug";
        //    atom.Property = "VaClass";
        //    atom.Value = "PENICILLINS,AMINO DERIVATIVES";
        //    atom = atom.Save();
        //    atomGroup.AddPredicate(atom);

        //    Assert.AreEqual(1, atomGroup.Predicates.Count);

        //    //Create a second AtomGroup
        //    var atomGroup2 = Library.Core.Rules.AtomGroup.NewAtomGroup();
        //    atomGroup2.Name = "Second Test Atom Group";
        //    atomGroup2.LogicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
        //    atomGroup2 = atomGroup2.Save();

        //    var dosageForm1 = Library.Core.Rules.Atom.NewAtom();
        //    dosageForm1.Class = "Drug";
        //    dosageForm1.Property = "DosageForm";
        //    dosageForm1.Value = "PWDR,RENST-ORAL";
        //    //dosageForm1 = dosageForm1.Save();

        //    atomGroup2.AddPredicate(dosageForm1);

        //    var dosageForm2 = Library.Core.Rules.Atom.NewAtom();
        //    dosageForm2.Class = "Drug";
        //    dosageForm2.Property = "DosageForm";
        //    dosageForm2.Value = "TAB";
        //    dosageForm2 = dosageForm2.Save();

        //    atomGroup2.AddPredicate(dosageForm2);
        //    atomGroup2 = atomGroup2.Save();

        //    //Add Or AtomGroup to first top level AtomGroup
        //    atomGroup.AddPredicate(atomGroup2);

        //    //Hacked: The Contains fact need to be rethought.
        //    //var containsAtom = Library.Core.Rules.Atom.NewAtom();
        //    //containsAtom.Property = "Contains";
        //    //containsAtom.Class = "Transaction";
        //    //containsAtom.Value = "Drug";
        //    //containsAtom = containsAtom.Save();

        //    //atomGroup.AddPredicate(containsAtom);

        //    atomGroup = atomGroup.Save();

        //    NxBRE.InferenceEngine.Rules.AtomGroup rulesEngineAtomGroup = atomGroup.ToNxBre();
        //    Assert.AreEqual(rulesEngineAtomGroup.AllAtoms.Count, 4);

        //    var storedAtomGroup = Library.Core.Rules.AtomGroup.GetById(atomGroup.AtomGroupId);
        //    NxBRE.InferenceEngine.Rules.AtomGroup rulesEngineStoredAtomGroup = storedAtomGroup.ToNxBre();
        //    Assert.AreEqual(rulesEngineStoredAtomGroup.AllAtoms.Count, 4);
        //}
    }
}
