using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PharmacyAdjudicator.LibraryTests.CoreTests
{
    /// <summary>
    /// Summary description for DrugTest
    /// </summary>
    [TestClass]
    public class DrugTest
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
        public void TestGetByNdc()
        {
            var drug = Library.Core.Drug.GetByNdc("55289035994");

            Assert.AreEqual(drug.BrandName, "POTASSIUM CHLORIDE 10MEQ TAB,SA (DISPERSIBLE)");
            Assert.AreEqual(drug.VaClass, "POTASSIUM");
        }

        [TestMethod]
        public void GetClinoril()
        {
            var drug = Library.Core.Drug.GetByNdc("00006094268");
            Assert.AreEqual(drug.BrandName, "SULINDAC 200MG TAB");
            Assert.AreEqual(drug.DosageForm, "TAB");
            Assert.AreEqual(drug.VaClass, "NONSALICYLATE NSAIS,ANTIRHEUMATIC");
        }
    }
}
