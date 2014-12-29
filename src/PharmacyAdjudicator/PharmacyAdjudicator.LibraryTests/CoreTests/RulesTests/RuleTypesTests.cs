using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.RulesTests
{
    /// <summary>
    /// Testing utility methods for extracting inferrable properties from Transaction class
    /// </summary>
    [TestClass]
    public class RuleTypesTests
    {
        public RuleTypesTests()
        {
        }

        [TestMethod]
        public void Can_get_inferrable_property_list()
        {
            var propertyList = Library.Core.Rules.RuleTypes.GetInferrableProperties();
            Assert.IsTrue(propertyList.Count > 0);
        }

        [TestMethod]
        public void Can_get_fact_property_list()
        {
            var propertyList = Library.Core.Rules.RuleTypes.GetFactProperties();
            Assert.IsTrue(propertyList.Count > 0);
        }

        [TestMethod]
        public void Can_get_list_of_types()
        {
            var typeList = Library.Core.Rules.RuleTypes.GetTypes();
            Assert.IsTrue(typeList.Count > 0);
        }

        [TestMethod]
        public void Can_get_list_of_facts_for_types()
        {
            var transactionFacts = Library.Core.Rules.RuleTypes.GetFactProperties(typeof(Library.Core.Transaction));
            Assert.IsTrue(transactionFacts.Count > 0);

            var drugFacts = Library.Core.Rules.RuleTypes.GetFactProperties(typeof(Library.Core.Drug));
            Assert.IsTrue(drugFacts.Count > 0);
        }
    }
}
