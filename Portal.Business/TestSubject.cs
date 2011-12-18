using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Business
{
    /// <summary>
    /// Represents a <see cref="TestSubject"/> object.
    /// </summary>
    public class TestSubject
    {
        /// <summary>
        /// The subject's first name.
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The subject's last name.
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The subject's email address.
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// The subject's password.
        /// </summary>
        public string Password
        {
            get;
            set;
        }
    }
}
