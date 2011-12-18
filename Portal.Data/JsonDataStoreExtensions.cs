using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portal.Business;

namespace Portal.Data
{
    /// <summary>
    /// A helper class for <see cref="TestSubject"/>
    /// </summary>
    public static class JsonDataStoreExtensions
    {
        /// <summary>
        /// Validates the a TestSubject can be placed into the datastore.
        /// </summary>
        /// <param name="subject">The subject to validate.</param>
        public static void Validate(this TestSubject subject)
        {
            if (string.IsNullOrEmpty(subject.FirstName))
            {
                throw new ArgumentException("FirstName cannot be empty.", "FirstName");
            }
            if (string.IsNullOrEmpty(subject.LastName))
            {
                throw new ArgumentException("LastName cannot be empty.", "LastName");
            }
            if (string.IsNullOrEmpty(subject.Email))
            {
                throw new ArgumentException("Email cannot be empty.", "Email");
            }
            if (string.IsNullOrEmpty(subject.Password))
            {
                throw new ArgumentException("Password cannot be empty.", "Password");
            }
        }
    }
}
