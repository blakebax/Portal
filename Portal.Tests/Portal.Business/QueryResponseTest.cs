using Portal.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Portal.Tests
{
    /// <summary>
    ///This is a test class for QueryResponseTest and is intended
    ///to contain all QueryResponseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QueryResponseTest
    {

        /// <summary>
        ///A test for QueryResponse Constructor
        ///</summary>
        [TestMethod()]
        public void QueryResponseConstructorTest()
        {
            QueryResponse target = new QueryResponse();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Subjects
        ///</summary>
        [TestMethod()]
        public void SubjectsTest()
        {
            QueryResponse target = new QueryResponse();
            List<TestSubject> expected = null; 
            Assert.AreEqual(expected, target.Subjects);
        }

        /// <summary>
        ///A test for Total
        ///</summary>
        [TestMethod()]
        public void TotalTest()
        {
            QueryResponse target = new QueryResponse();
            int expected = 0; 
            Assert.AreEqual(expected, target.Total);
        }
    }
}
