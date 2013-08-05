using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PharmacyAdjudicator.LibraryTests.CoreTests
{
    /// <summary>
    /// Summary description for DrugListTest
    /// </summary>
    [TestClass]
    public class DrugListTest
    {

        [TestInitialize()]
        public void DrugTestInitialize()
        {
            var principal = new System.Security.Principal.GenericPrincipal(
            new System.Security.Principal.GenericIdentity("Test"),
            new string[] { "RuleManager" });
            Csla.ApplicationContext.User = principal;
        }

        [TestMethod]
        public void GetListByBrandName()
        {
            var drugs = Library.Core.DrugList.GetByBrandName("WAFER");
            Assert.IsTrue(drugs.Count > 0, "Drug list is zero when expecting results");
        }
    }
}
