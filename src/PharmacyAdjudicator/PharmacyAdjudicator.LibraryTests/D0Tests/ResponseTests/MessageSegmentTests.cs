using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PharmacyAdjudicator.LibraryTests.D0Tests.ResponseTests
{
    /// <summary>
    /// Summary description for MessageSegmentTests
    /// </summary>
    [TestClass]
    public class MessageSegmentTests
    {
        public MessageSegmentTests()
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
        public void TestExpressions()
        {
            Library.D0.Response.MessageSegment msg = new Library.D0.Response.MessageSegment();
            msg.Message = "This is a test message";

            string expectedNcpdpString = "<1E><1C>AM2Ø<1C>F4This is a test message";
            string ncpdpString = msg.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }

        [TestMethod]
        public void TestEmptyMessage()
        {
            Library.D0.Response.MessageSegment msg = new Library.D0.Response.MessageSegment();

            //We expect nothing since the segment only has the identifier.
            string expectedNcpdpString = "";
            string ncpdpString = msg.ToNcpdpString();

            Assert.AreEqual(NcpdpHelper.FromNcpdpToHumanReadable(ncpdpString), expectedNcpdpString);
        }
    }
}
