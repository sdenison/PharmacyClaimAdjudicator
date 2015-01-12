using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Core.Rules;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.RulesTests
{
    [TestClass]
    public class OperatorDictionaryTests
    {
        [TestMethod]
        public void Can_get_operators_for_type_decimal()
        {
            var validOperators = OperatorDictionary.Operators[typeof(decimal)];
            Assert.IsTrue(validOperators.Count > 1);
            var validOperators2 = OperatorDictionary.Operators[typeof(decimal)];
            Assert.IsTrue(validOperators2.Count > 1);
        }

        [TestMethod]
        public void Atom_should_have_allowed_operation_when_decimal()
        {
            var decimalAtom = Atom.NewAtom();
            decimalAtom.Class = "Transaction";
            decimalAtom.Property = "IngredientCostSubmitted";
            Assert.IsTrue(decimalAtom.AllowedOperators.Count == 5);
        }

        [TestMethod]
        public void Brand_new_atom_should_behave_like_string()
        {
            var atom = Atom.NewAtom();
            Assert.IsTrue(atom.AllowedOperators.Count == 2);
        }

        [TestMethod]
        public void Enum_atom_should_only_have_equals_available()
        {
            var atom = Atom.NewAtom();
            atom.Class = "Transaction";
            atom.Property = "BasisOfReimbursement";
            Assert.IsTrue(atom.AllowedOperators.Count == 1);

        }
    }
}
