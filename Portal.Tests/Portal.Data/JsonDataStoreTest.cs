using Portal.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Portal.Business;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Portal.Tests
{
    
    
    /// <summary>
    ///This is a test class for JsonDataStoreTest and is intended
    ///to contain all JsonDataStoreTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JsonDataStoreTest
    {
        /// <summary>
        ///A test for the default.ctor, IDataStore methods tested in DataStoreFactoryTest.cs
        ///</summary>
        [TestMethod()]
        public void JsonDataStoreConstructorTest()
        {
            File.WriteAllText("datastore.txt", JsonConvert.SerializeObject(new List<TestSubject>() { new TestSubject() }));
            JsonDataStore target = new JsonDataStore();
            Assert.IsNotNull(target);
        }
    }
}
