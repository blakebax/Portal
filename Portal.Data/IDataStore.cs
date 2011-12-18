using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portal.Business;

namespace Portal.Data
{
    /// <summary>
    /// Represents an <see cref="IDataStore"/> object.
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// Gets TestSubject from the datastore.
        /// </summary>
        /// <param name="paging">The <see cref="PageOptions"/>.</param>
        /// <returns>A <see cref="QueryResponse"/> containing the list of TestSubjects and other response information.</returns>
        QueryResponse GetData(PageOptions paging);

        /// <summary>
        /// Checks if there is a duplicate <see cref="TestSubject"/> in the datastore.
        /// </summary>
        /// <param name="subject">The subject to check against.</param>
        /// <returns>True is no duplicates, false if there are duplicates.</returns>
        bool HasDuplicate(TestSubject subject);

        /// <summary>
        /// Adds a subject to the datastore.
        /// </summary>
        /// <param name="subject">The <see cref="TestSubject"/> to add.</param>
        void AddSubject(TestSubject subject);

        /// <summary>
        /// Deletes a subject from the datastore.
        /// </summary>
        /// <param name="subject">The <see cref="TestSubject"/> to remove.</param>
        void DeleteSubject(TestSubject subject);
    }
}
