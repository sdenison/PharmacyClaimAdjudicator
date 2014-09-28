using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using PharmacyAdjudicator.Library.Security;

namespace PharmacyAdjudicator.TestLibrary.SecurityTests
{
    [TestClass]
    public class PAPrincipalTests
    {
        //Cant use PAPrincipal.LoginAsync because .NET identity/principal are set at the thread level.
        [TestMethod]
        public void Login_can_fail()
        {
            PharmacyAdjudicator.Library.Security.PAPrincipal.Login("badusername", "badpassword");
            var loggedInUser = Csla.ApplicationContext.User;
            Assert.IsFalse(loggedInUser.Identity.IsAuthenticated);
        }

        [TestMethod]
        public void Login_can_succeed()
        {
            PharmacyAdjudicator.Library.Security.PAPrincipal.Login("sam", "p");
            var loggedInUser = Csla.ApplicationContext.User;
            Assert.AreEqual(loggedInUser.Identity.Name, "sam");
            Assert.IsTrue(loggedInUser.Identity.IsAuthenticated);
        }
    }
}
