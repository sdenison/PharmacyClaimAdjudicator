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

        [TestMethod]
        public void Should_not_be_able_to_add_client_with_existing_id()
        {
            //Create client
            var newClient = ClientEdit.NewClient("ASDF1");
            newClient.Name = "ASDF Corporation";
            newClient = newClient.Save();

            //Retrieve client
            var retrievedClient1 = ClientEdit.GetByClientId("ASDF1");
            Assert.IsTrue(retrievedClient1.Name.Equals("ASDF Corporation"));

            //Try to create the client again
            try
            {
                var newClient2 = ClientEdit.NewClient("ASDF1");
            }
            catch (Exception ex)
            {
                if (!(ex.GetBaseException() is Library.DataAlreadyExistsException))
                {
                    Assert.Fail("Exception base type should be DataAlreadyExistsException.");
                }
            }
        }

        [TestMethod]
        public void Can_test_if_client_id_exists()
        {
            //Create client
            var newClient = ClientEdit.NewClient("ASDF1");
            newClient.Name = "ASDF Corporation";
            newClient = newClient.Save();

            Assert.IsTrue(ClientEdit.Exists("ASDF1"));
            Assert.IsFalse(ClientEdit.Exists("13234"));
        }

        [TestMethod]
        public void Can_pull_acme_client()
        {
            var client = ClientEdit.GetByClientId("ACME");
            Assert.IsTrue(client.Name.Equals("New ACME Corporation Name"));
        }
    }
}
