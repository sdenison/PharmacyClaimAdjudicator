using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using PharmacyAdjudicator.Library.Security;

namespace PharmacyAdjudicator.TestLibrary.SecurityTests
{
    [TestClass]
    public class PAIdentityTests
    {
        [TestMethod]
        public void Login_can_fail_with_bad_username_and_password()
        {
            PAIdentity identity = PAIdentity.GetPAIdentityAsync("badusername", "badpassword").Result;
            Assert.AreEqual(identity.Name, string.Empty);
        }

        [TestMethod]
        public void Login_will_return_Identity_with_valid_username_and_password()
        {
            PAIdentity identity = PAIdentity.GetPAIdentityAsync("sam", "password").Result;
            Assert.AreEqual(identity.Name, "sam");
            Assert.IsTrue(identity.IsAuthenticated);
        }
    }
}
