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
            //Drugs.Add(new DrugData() { Ndc = "999999981500", BrandName = "BRIEF,FITTED LARGE EXTRA ABSORBENT", Upn = "", VaClass = "XA399" });
            var drug = Library.Core.Drug.GetByNdc("999999981500");

            Assert.AreEqual(drug.BrandName, "BRIEF,FITTED LARGE EXTRA ABSORBENT");
            Assert.AreEqual(drug.Upn, "");
            Assert.AreEqual(drug.VaClass, "XA399");
        }
    }
}
