using Portal.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Newtonsoft.Json;
using Portal.Business;
using System.Collections.Generic;

namespace Portal.Tests
{
    
    
    /// <summary>
    ///This is a test class for DataStoreFactoryTest and is intended
    ///to contain all DataStoreFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataStoreFactoryTest
    {
        /// <summary>
        ///A test for DataStoreFactory Constructor
        ///</summary>
        [TestMethod()]
        public void DataStoreFactoryConstructorTest()
        {
            this.CreateDataFile();
            DataStoreFactory target = new DataStoreFactory();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for DataStore
        ///</summary>
        [TestMethod()]
        public void DataStoreTest()
        {
            this.CreateDataFile();
            DataStoreFactory target = new DataStoreFactory(); // TODO: Initialize to an appropriate value
            IDataStore actual;
            actual = target.DataStore;
            Assert.IsNotNull(target.DataStore);
        }

        /// <summary>
        ///A test for DataStore Count
        ///</summary>
        [TestMethod()]
        public void DataStoreCountTest()
        {
            this.CreateDataFile();
            DataStoreFactory target = new DataStoreFactory(); // TODO: Initialize to an appropriate value
            int actual = target.DataStore.GetData(new PageOptions()).Total;
            Assert.IsTrue(actual > 0);
        }

        /// <summary>
        ///A test for DataStore Duplicate (false)
        ///</summary>
        [TestMethod()]
        public void DataStoreFalseDuplicateTest()
        {
            this.CreateDataFile();
            DataStoreFactory target = new DataStoreFactory(); // TODO: Initialize to an appropriate value
            bool actual = target.DataStore.HasDuplicate(new TestSubject() { Email = "@@@" });
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for DataStore Duplicate (true)
        ///</summary>
        [TestMethod()]
        public void DataStoreFalseDuplicateTrueTest()
        {
            this.CreateDataFile();
            DataStoreFactory target = new DataStoreFactory(); // TODO: Initialize to an appropriate value
            bool actual = target.DataStore.HasDuplicate(new TestSubject() { Email = "1@1" });
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///A test for adding a testsubject incorrectly to the datastore.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void DataStoreAddSubjectTestFail()
        {
            this.CreateDataFile();
            DataStoreFactory target = new DataStoreFactory(); // TODO: Initialize to an appropriate value
            target.DataStore.AddSubject(new TestSubject());
        }

        /// <summary>
        ///A test for adding a testsubject correctly to the datastore.
        ///</summary>
        [TestMethod()]
        public void DataStoreAddSubjectTestSuccess()
        {
            this.CreateDataFile();
            DataStoreFactory target = new DataStoreFactory(); // TODO: Initialize to an appropriate value
            target.DataStore.AddSubject(new TestSubject() { FirstName = "m.", LastName = "mathers", Email = "test@test", Password = "pw" });

            int newCount = target.DataStore.GetData(new PageOptions()).Total;
            Assert.IsTrue(newCount > 1);
        }

        // I could add more tests time pending to minitor paging and such...

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
