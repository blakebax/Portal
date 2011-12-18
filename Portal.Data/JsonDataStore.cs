using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portal.Business;
using System.IO;
using Newtonsoft.Json;

namespace Portal.Data
{
    /// <summary>
    /// See <see cref="IDataStore"/> for method descriptions.
    /// </summary>
    internal class JsonDataStore : IDataStore
    {
        // The private static datastore - this works for a small dataset
        // such as this, but a larger dataset would require a differnt
        // solution, like a database instead of in-memory
        private static List<TestSubject> datastore = null;

        /// <summary>
        /// The default .ctor
        /// </summary>
        public JsonDataStore()
        {
            if (datastore == null)
            {
                string textData = File.ReadAllText("datastore.txt");
                if (!string.IsNullOrEmpty(textData))
                {
                    datastore = JsonConvert.DeserializeObject<List<TestSubject>>(textData);
                }
            }
        }

        /// <summary>
        /// See <see cref="IDataStore"/> for method descriptions.
        /// </summary>
        public QueryResponse GetData(PageOptions paging)
        {
            QueryResponse response = new QueryResponse();
            IEnumerable<TestSubject> results = new List<TestSubject>();
            if (paging == null)
            {
                paging = new PageOptions();
            }
            if (paging.SortBy == PageOptions.Sort.FirstName)
                results = datastore.OrderBy(e => e.FirstName);
            else if (paging.SortBy == PageOptions.Sort.LastName)
                results = datastore.OrderBy(e => e.LastName);
            else if (paging.SortBy == PageOptions.Sort.Email)
                results = datastore.OrderBy(e => e.Email);
            else if (paging.SortBy == PageOptions.Sort.Password)
                results = datastore.OrderBy(e => e.Password);

            response.Subjects = results.Skip(paging.PageNumber * 5).Take(5).ToList();
            response.Total = datastore.Count;

            return response;
        }

        /// <summary>
        /// See <see cref="IDataStore"/> for method descriptions.
        /// </summary>
        public void AddSubject(TestSubject subject)
        {
            // A well designed UI should prevent this, but let's be safe.
            subject.Validate();
            datastore.Add(subject);
            // flush the updated dataset
            this.Flush();
        }

        /// <summary>
        /// See <see cref="IDataStore"/> for method descriptions.
        /// </summary>
        public bool HasDuplicate(TestSubject subject) 
        {
            TestSubject dupe = datastore.Select(entry => entry).Where(entry => entry.Email == subject.Email).FirstOrDefault();
            if (dupe != null)
            {
                return false;
            }
            else return true;
        }

        /// <summary>
        /// See <see cref="IDataStore"/> for method descriptions.
        /// </summary>
        public void DeleteSubject(TestSubject subject)
        {
            var match = datastore.Where(sub => sub.Email == subject.Email).SingleOrDefault();
            if (match != null)
            {
                datastore.Remove(match);
            }
            // flush the updated dataset
            this.Flush(); 
        }

        /// <summary>
        /// Flushes the data back to the text file.
        /// </summary>
        private void Flush()
        {
            File.WriteAllText("datastore.txt", JsonConvert.SerializeObject(datastore));
        }
    }
}
