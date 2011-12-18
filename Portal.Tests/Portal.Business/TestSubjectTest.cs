using Portal.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Portal.Tests
{
    
    
    /// <summary>
    ///This is a test class for TestSubjectTest and is intended
    ///to contain all TestSubjectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TestSubjectTest
    {
        /// <summary>
        ///A test for TestSubject Constructor
        ///</summary>
        [TestMethod()]
        public void TestSubjectConstructorTest()
        {
            TestSubject target = new TestSubject();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Email
        ///</summary>
        [TestMethod()]
        public void EmailTest()
        {
            TestSubject target = new TestSubject(); 
            string expected = null; 
            Assert.AreEqual(expected, target.Email);
        }

        /// <summary>
        ///A test for FirstName
        ///</summary>
        [TestMethod()]
        public void FirstNameTest()
        {
            TestSubject target = new TestSubject();
            string expected = null;
            Assert.AreEqual(expected, target.FirstName);
        }

        /// <summary>
        ///A test for LastName
        ///</summary>
        [TestMethod()]
        public void LastNameTest()
        {
            TestSubject target = new TestSubject();
            string expected = null;
            Assert.AreEqual(expected, target.LastName);
        }

        /// <summary>
        ///A test for Password
        ///</summary>
        [TestMethod()]
        public void PasswordTest()
        {
            TestSubject target = new TestSubject();
            string expected = null;
            Assert.AreEqual(expected, target.Password);
        }
    }
}
