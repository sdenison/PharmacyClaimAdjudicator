using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Utils;

namespace PharmacyAdjudicator.TestLibrary.UtilsTests
{
    [TestClass]
    public class ToProperCaseExtensionTests
    {
        public ToProperCaseExtensionTests()
        {
        }

        [TestMethod]
        public void Can_get_proper_case_from_camel_case_string()
        {
            string testString = "ThisIsATestString";
            var expectedString = "This Is A Test String";
            Assert.AreEqual(testString.ToProperCase(), expectedString);
        }
    }
}
