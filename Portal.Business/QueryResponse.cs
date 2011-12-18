using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Business
{
    /// <summary>
    /// Represent a <see cref="QueryResponse"/> object, which is response to a datastore query.
    /// </summary>
    public class QueryResponse
    {
        /// <summary>
        /// The list of <see cref="TestSubject"/> objects found.
        /// </summary>
        public List<TestSubject> Subjects
        {
            get;
            set;
        }

        /// <summary>
        /// The total number of <see cref="TestSubject"/> objects in the datastore.
        /// </summary>
        public int Total
        {
            get;
            set;
        }
    }
}
