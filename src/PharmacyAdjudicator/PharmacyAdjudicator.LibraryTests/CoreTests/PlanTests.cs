using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.Core;
using NxBRE.InferenceEngine;
using System.IO;

namespace PharmacyAdjudicator.TestLibrary.CoreTests
{
    [TestClass]
    public class PlanTests
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

        //[TestMethod]
        //public void Should_be_able_to_create_plan_with_simple_rules()
        //{
        //    Plan testPlan = Plan.NewPlan("NEW-PLAN-ID-1");
        //    var rules = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\AM111OnFormulary.ruleml", System.IO.FileAccess.Read);
        //    testPlan.Name = "Test Plan 1";
        //    //testPlan.RuleML = rules;

        //    //var binder = new TransactionProcessorBinder();
        //    IInferenceEngine ie = new IEImpl();
        //    //Hashtable transactions = new Hashtable();
        //    //transactions.Add("TRANSACTION", transaction);
        //    ie.LoadRuleBase(rules);

        //    System.IO.Stream ms = new System.IO.MemoryStream();
        //    //var ruleAdapter = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter(ms, FileAccess.Write);
        //    //var ruleAdapter = new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\NewRuleFile.ruleml", FileAccess.Write, NxBRE.InferenceEngine.IO.SaveFormatAttributes.Compact| NxBRE.InferenceEngine.IO.SaveFormatAttributes.ForceDataTyping);

        //    //ie.SaveRuleBase(ruleAdapter);
        //    ////ie.SaveRuleBase(new NxBRE.InferenceEngine.IO.RuleML09NafDatalogAdapter("RuleFiles\\NewRuleFile.ruleml", FileAccess.Write, NxBRE.InferenceEngine.IO.SaveFormatAttributes.Compact | NxBRE.InferenceEngine.IO.SaveFormatAttributes.ForceDataTyping));


        //    ////ms.
        //    //ms.Position = 0;
        //    //using (StreamReader sr = new StreamReader(ms))
        //    //{
        //    // //   testPlan.RuleML = sr.ReadToEnd();
        //    //}
        //    //testPlan.RuleMLVersion = "RuleML09NafDatalogAdapter";
        //    //ie.Process(transactions);
        //}
    }
}
