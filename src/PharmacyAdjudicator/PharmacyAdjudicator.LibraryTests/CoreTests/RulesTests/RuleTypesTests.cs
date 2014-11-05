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
    }
}
