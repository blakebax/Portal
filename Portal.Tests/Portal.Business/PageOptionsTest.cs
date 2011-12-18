using Portal.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Portal.Tests
{
    
    
    /// <summary>
    ///This is a test class for PageOptionsTest and is intended
    ///to contain all PageOptionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PageOptionsTest
    {
        /// <summary>
        ///A test for PageOptions Constructor
        ///</summary>
        [TestMethod()]
        public void PageOptionsConstructorTest()
        {
            PageOptions target = new PageOptions();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for PageNumber
        ///</summary>
        [TestMethod()]
        public void PageNumberTest()
        {
            PageOptions target = new PageOptions(); // TODO: Initialize to an appropriate value
            int expected = 0;
            Assert.AreEqual(expected, target.PageNumber, "Default page number should be 0.");
        }

        /// <summary>
        ///A test for SortBy
        ///</summary>
        [TestMethod()]
        public void SortByTest()
        {
            PageOptions target = new PageOptions(); // TODO: Initialize to an appropriate value
            PageOptions.Sort expected = PageOptions.Sort.LastName;
            Assert.AreEqual(expected, target.SortBy, "Default sort should be LastName.");
        }
    }
}
