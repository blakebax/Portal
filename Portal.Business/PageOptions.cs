using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Business
{
    /// <summary>
    /// Represents a <see cref="PageOptions"/> object
    /// </summary>
    public class PageOptions
    {
        /// <summary>
        /// The default .ctor
        /// </summary>
        public PageOptions()
        {
            this.sort = Sort.LastName;
            this.pageNumber = 0;
        }

        /// <summary>
        /// The .ctor with sort and pagenumber.
        /// </summary>
        /// <param name="Sort">A value of <see cref="PageOptions.Sort"/>.</param>
        /// <param name="PageNumber">A value of a PageNumber.</param>
        public PageOptions(PageOptions.Sort Sort, int PageNumber)
        {
            this.sort = Sort.LastName;
            this.pageNumber = 0;
        }

        /// <summary>
        /// The .ctor with sort and pagenumber parsing.
        /// </summary>
        /// <param name="Sort">A string value of <see cref="PageOptions.Sort"/>.</param>
        /// <param name="PageNumber">A string value of a PageNumber.</param>
        public PageOptions(string Sort, string PageNumber)
        {
            this.pageNumber = Convert.ToInt32(PageNumber);
            this.sort = (PageOptions.Sort)Enum.Parse(typeof(PageOptions.Sort), Sort);
        }

        /// <summary>
        /// The sort option for a query.
        /// </summary>
        private Sort sort;
        public Sort SortBy
        {
            get
            {
                return this.sort;
            }
        }

        /// <summary>
        /// The requested page number (0-based) for a query.
        /// </summary>
        private int pageNumber;
        public int PageNumber
        {
            get
            {
                return this.pageNumber;
            }
        }

        /// <summary>
        /// An enumeration for sorting types.
        /// </summary>
        public enum Sort
        {
            FirstName,
            LastName,
            Email,
            Password
        }
    }
}
