using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PharmacyAdjudicator.Library.Core.Client;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.ClientTests
{
    [TestClass]
    public class ClientEditTests
    {
        [TestInitialize()]
        public void Setup()
        {
            var principal = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("Test"),
                new string[] { "RuleManager" });
            //var principal = new System.Security.Principal.GenericPrincipal(
            //    new System.Security.Principal.GenericIdentity("Test"),
            //    new string[] { "PatientViewer" });
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
        public void Should_be_able_to_create_client()
        {
            var newClient = ClientEdit.NewClient("ASDF1");
            newClient.Name = "ASDF Corporation";
            newClient = newClient.Save();
        }

        [TestMethod]
        public void Should_be_able_to_retrieve_client()
        {
            //Create client
            var newClient = ClientEdit.NewClient("ASDF1");
            newClient.Name = "ASDF Corporation";
            newClient = newClient.Save();

            //Retrieve client
            var retrievedClient = ClientEdit.GetByClientId("ASDF1");
            Assert.IsTrue(retrievedClient.Name.Equals("ASDF Corporation"));
        }

        [TestMethod]
        public void Can_update_client_name_from_database()
        {
            //Create client
            var newClient = ClientEdit.NewClient("ASDF1");
            newClient.Name = "ASDF Corporation";
            newClient = newClient.Save();

            //Retrieve client
            var retrievedClient1 = ClientEdit.GetByClientId("ASDF1");
            Assert.IsTrue(retrievedClient1.Name.Equals("ASDF Corporation"));

            //Update name
            retrievedClient1.Name = "New Corporation Name";
            retrievedClient1 = retrievedClient1.Save();

            //Retrieve the client again
            var retrievedClient2 = ClientEdit.GetByClientId("ASDF1");
            Assert.IsTrue(retrievedClient2.Name.Equals("New Corporation Name"));
        }
    }
}
