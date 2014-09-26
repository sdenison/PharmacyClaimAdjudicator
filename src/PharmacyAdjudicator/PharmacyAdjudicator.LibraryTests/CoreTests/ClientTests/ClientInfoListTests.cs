using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PharmacyAdjudicator.Library.Core.Client;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.ClientTests
{
    [TestClass]
    public class ClientInfoListTests
    {
        [TestInitialize()]
        public void Setup()
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
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
        public void Client_list_should_exist()
        {
            var clients = ClientInfoList.GetAllClients();
            Assert.IsTrue(clients.Count > 0);
        }
    }
}
