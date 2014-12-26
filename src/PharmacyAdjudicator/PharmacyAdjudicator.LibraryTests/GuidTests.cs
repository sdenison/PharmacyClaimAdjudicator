using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyAdjudicator.Library;

namespace PharmacyAdjudicator.TestLibrary
{
    [TestClass]
    public class GuidTests
    {
        [TestMethod]
        public void Can_generate_sequential_guids()
        {
            //Thread.Sleep has to be called because timestamp is used in GenerateComb().
            SqlGuid sequentialGuid1 = Library.Utils.GuidHelper.GenerateComb();
            Thread.Sleep(10);
            SqlGuid sequentialGuid2 = Library.Utils.GuidHelper.GenerateComb();
            Thread.Sleep(10);
            SqlGuid sequentialGuid3 = Library.Utils.GuidHelper.GenerateComb();
            Thread.Sleep(10);
            SqlGuid sequentialGuid4 = Library.Utils.GuidHelper.GenerateComb();
            Thread.Sleep(10);
            SqlGuid sequentialGuid5 = Library.Utils.GuidHelper.GenerateComb();

            List<SqlGuid> guidList = new List<SqlGuid>();
            //Add guids to list in random order.
            guidList.Add(sequentialGuid3);
            guidList.Add(sequentialGuid2);
            guidList.Add(sequentialGuid5);
            guidList.Add(sequentialGuid4);
            guidList.Add(sequentialGuid1);

            //Make sure list is in order that we added the Guids.
            Assert.IsTrue(guidList[0].Equals(sequentialGuid3));
            Assert.IsTrue(guidList[1].Equals(sequentialGuid2));
            Assert.IsTrue(guidList[2].Equals(sequentialGuid5));
            Assert.IsTrue(guidList[3].Equals(sequentialGuid4));
            Assert.IsTrue(guidList[4].Equals(sequentialGuid1));

            //Sanity check that the third Guid created is more than second Guid Created
            Assert.IsTrue(sequentialGuid3.CompareTo(sequentialGuid2) > 0);

            guidList.Sort();

            //Make sure the list is in order that the Guids were created.
            Assert.IsTrue(guidList[0].Equals(sequentialGuid1));
            Assert.IsTrue(guidList[1].Equals(sequentialGuid2));
            Assert.IsTrue(guidList[2].Equals(sequentialGuid3));
            Assert.IsTrue(guidList[3].Equals(sequentialGuid4));
            Assert.IsTrue(guidList[4].Equals(sequentialGuid5));
        }
    }
}
