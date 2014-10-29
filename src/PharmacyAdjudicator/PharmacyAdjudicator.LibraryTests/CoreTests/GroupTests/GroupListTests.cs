using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PharmacyAdjudicator.Library.Core.Group;

namespace PharmacyAdjudicator.TestLibrary.CoreTests.GroupTests
{
    [TestClass]
    public class GroupListTests
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
        public void Group_list_can_pull_by_group_id()
        {
            var criteria = new GroupSearchCriteria();
            criteria.GroupId = "GROUP1";
            criteria.ClientId = "ACME";
            var groups = GroupList.GetByCriteria(criteria);
            Assert.IsTrue(groups.Count > 0);
        }
    }
}
