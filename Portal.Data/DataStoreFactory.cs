using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
namespace Portal.Data
{
    /// <summary>
    /// Creates an <see cref="IDataStore"/> object for data access.
    /// </summary>
    public class DataStoreFactory
    {
        /// <summary>
        /// Gets the <see cref="IDataStore"/> object based on the DataStoreType app setting.
        /// </summary>
        public IDataStore DataStore
        {
            get
            {
               return (IDataStore)Activator.CreateInstance(Type.GetType(ConfigurationManager.AppSettings["DataStoreType"]));          
            }
        }
    }
}
