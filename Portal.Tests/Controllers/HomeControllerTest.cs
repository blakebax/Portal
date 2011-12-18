using Portal.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using Portal.Business;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Moq;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Specialized;

namespace Portal.Tests
{


    /// <summary>
    /// This is a test class for HomeControllerTest and is intended
    /// to contain all HomeControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HomeControllerTest
    {
        /// <summary>
        ///A test for Index
        ///</summary>
        [TestMethod()]
        public void IndexTest()
        {
            CreateDataFile();
            HomeController target = new HomeController();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AddToSubjectTable
        ///</summary>
        [TestMethod()]
        public void AddToSubjectTableTest()
        {
            CreateDataFile();
            NameValueCollection querystring = new NameValueCollection { 
                { "Email", "blake@test.com" },
                { "FirstName", "blake" },
                { "LastName", "b" },
                { "Password", "12345" },
                { "PageNumber", "0" },
                { "SortBy", "0" } 
            };
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.Request.QueryString).Returns(querystring);

            HomeController controller = new HomeController();
            controller.ControllerContext = mock.Object;

            ActionResult target = controller.AddToSubjectTable();
            
            Assert.IsNotNull(target);
            // Test that the datastore was actually updated
            Assert.IsTrue(((target as ViewResult).Model as IndexModel).Response.Total > 1);
        }

        /// <summary>
        ///A test for AddToSubjectTable with missing data
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void AddToSubjectTableFailTest()
        {
            CreateDataFile();

            // Mock a request with missing data
            var querystring = new System.Collections.Specialized.NameValueCollection { { "Email", "12345" } };
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.Request.QueryString).Returns(querystring);

            HomeController controller = new HomeController();
            controller.ControllerContext = mock.Object;
            controller.AddToSubjectTable();
        }

        /// <summary>
        ///A test for UpdateSubjectTable
        ///</summary>
        [TestMethod()]
        public void UpdateSubjectTableTest()
        {
            CreateDataFile();
            NameValueCollection querystring = new NameValueCollection { 
                { "PageNumber", "14" },
                { "SortBy", "0" } 
            };
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.Request.QueryString).Returns(querystring);
            HomeController controller = new HomeController();
            controller.ControllerContext = mock.Object;
            var target = controller.UpdateSubjectTable();
            Assert.IsNotNull(target);
            Assert.IsTrue(((target as ViewResult).Model as IndexModel).PageOptions.PageNumber == 14);
        }

        /// <summary>
        ///A test for RemoveSubject
        ///</summary>
        [TestMethod()]
        public void RemoveSubjectTest()
        {
            CreateDataFile();
            NameValueCollection querystring = new NameValueCollection { 
                { "Email", "1@1" },
                { "PageNumber", "0" },
                { "SortBy", "0" } 
            };
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.Request.QueryString).Returns(querystring);
            HomeController controller = new HomeController();
            controller.ControllerContext = mock.Object;
            var target = controller.RemoveSubject();
            Assert.IsNotNull(target);
            QueryResponse response = ((target as ViewResult).Model as IndexModel).Response;
            Assert.IsTrue(response.Subjects.Where(sub => sub.Email == "1@1").Count() == 0);
        }

        /// <summary>
        ///A test for Uniqueness of entry based on email in the datastore (False)
        ///</summary>
        [TestMethod()]
        public void UniqueCheckTestFalse()
       { 
            CreateDataFile();
            NameValueCollection querystring = new NameValueCollection { 
                { "Email", "1@1" },
            };
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.Request.QueryString).Returns(querystring);
            HomeController controller = new HomeController();
            controller.ControllerContext = mock.Object;
            var target = controller.UniqueCheck();
            Assert.IsFalse(target);
        }

        /// <summary>
        ///A test for Uniqueness of entry based on email in the datastore (True)
        ///</summary>
        [TestMethod()]
        public void UniqueCheckTestTrue()
        {
            CreateDataFile();
            NameValueCollection querystring = new NameValueCollection { 
                { "Email", "#&*$^#*&^$" },
            };
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.Request.QueryString).Returns(querystring);
            HomeController controller = new HomeController();
            controller.ControllerContext = mock.Object;
            var target = controller.UniqueCheck();
            Assert.IsTrue(target);
        }

        private void CreateDataFile()
        {
            if (!File.Exists("datastore.txt"))
            {
                List<TestSubject> list = new List<TestSubject>()
                        {
                            new TestSubject() { 
                                Password = "123", FirstName = "james", LastName = "bond", Email = "1@1"
                            }
                        };
                File.WriteAllText("datastore.txt", JsonConvert.SerializeObject(list));
            }
        }
    }
}