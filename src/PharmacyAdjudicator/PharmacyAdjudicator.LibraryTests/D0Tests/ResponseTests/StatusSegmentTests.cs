using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library.D0.Response;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for StatusSegmentTests
    /// </summary>
    [TestClass]
    public class StatusSegmentTests
    {
        public StatusSegmentTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestApproved_34_5_2()
        {
            Library.D0.Response.StatusSegment status = new Library.D0.Response.StatusSegment();
            status.TransactionResponseStatus = "P";
            status.AuthorizationNumber = "123456789123456789";
            status.AdditionalMessageInformationCount = 1;

            status.AdditionalMessages = new List<Library.D0.Response.StatusSegment.AdditionalMessageContainer>();
            Library.D0.Response.StatusSegment.AdditionalMessageContainer additionalMessage = new Library.D0.Response.StatusSegment.AdditionalMessageContainer();
            additionalMessage.AdditionalMessageInformationQualifier = "01";
            additionalMessage.AdditionalMessageInformation = "TRANSACTION MESSAGE TEXT";

            status.AdditionalMessages.Add(additionalMessage);
            status.HelpDeskPhoneNumberQualifier = "03";
            status.HelpDeskPhoneNumber = "6023570862";

            string expectedNcpdpString = "<1E><1C>AM21<1C>ANP<1C>F3123456789123456789<1C>UF1<1C>UHØ1<1C>FQTRANSACTION MESSAGE TEXT<1C>7FØ3<1C>8F6Ø2357Ø862";
            string ncpdpString = status.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestCaptured_34_5_3()
        {
            Library.D0.Response.StatusSegment status = new Library.D0.Response.StatusSegment();
            status.TransactionResponseStatus = "C"; //captured
            status.AuthorizationNumber = "123456789123456789";
            status.HelpDeskPhoneNumberQualifier = "03";
            status.HelpDeskPhoneNumber = "6023570862";

            string expectedNcpdpString = "<1E><1C>AM21<1C>ANC<1C>F3123456789123456789<1C>7FØ3<1C>8F6Ø2357Ø862";
            string ncpdpString = status.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestApprovedMessage_34_5_4()
        {
            StatusSegment status = new StatusSegment();
            status.TransactionResponseStatus = "P";
            status.AuthorizationNumber = "123456789123456789";
            status.AdditionalMessageInformationCount = 1;

            status.AdditionalMessages = new List<StatusSegment.AdditionalMessageContainer>();
            StatusSegment.AdditionalMessageContainer additionalMessage = new StatusSegment.AdditionalMessageContainer();
            additionalMessage.AdditionalMessageInformationQualifier = "01";
            additionalMessage.AdditionalMessageInformation = "USE NAPROXEN";
            status.AdditionalMessages.Add(additionalMessage);

            status.ApprovedMessageCodeCount = 2;
            status.ApprovedMessages = new List<StatusSegment.ApprovedMessage>();
            StatusSegment.ApprovedMessage approvedMessage = new StatusSegment.ApprovedMessage();
            approvedMessage.ApprovedMessageCode = "002";
            status.ApprovedMessages.Add(approvedMessage);
            approvedMessage = new StatusSegment.ApprovedMessage();
            approvedMessage.ApprovedMessageCode = "003";
            status.ApprovedMessages.Add(approvedMessage);
            Assert.AreEqual(2, status.ApprovedMessages.Count);

            status.HelpDeskPhoneNumberQualifier = "03";
            status.HelpDeskPhoneNumber = "6023570862";

            string expectedNcpdpString = "<1E><1C>AM21<1C>ANP<1C>F3123456789123456789<1C>UF1<1C>UHØ1<1C>FQUSE NAPROXEN<1C>5F2<1C>6FØØ2<1C>6FØØ3<1C>7FØ3<1C>8F6Ø2357Ø862";
            string ncpdpString = status.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestRejectedResponse_34_5_6()
        {
            StatusSegment status = new StatusSegment();
            status.TransactionResponseStatus = "R"; //Rejected
            status.RejectCount = 1;

            status.Rejects = new List<StatusSegment.RejectContainer>();
            StatusSegment.RejectContainer reject = new StatusSegment.RejectContainer();
            reject.RejectCode = "70"; //Product/Service not covered
            status.Rejects.Add(reject);

            status.HelpDeskPhoneNumberQualifier = "03";
            status.HelpDeskPhoneNumber = "6023570862";

            string expectedNcpdpString = "<1E><1C>AM21<1C>ANR<1C>FA1<1C>FB7Ø<1C>7FØ3<1C>8F6Ø2357Ø862";
            string ncpdpString = status.ToNcpdpString();
            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }
    }
}
