using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Data;
using Portal.Business;
using Newtonsoft.Json;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The <see cref="IDataStore"/> accessor.
        /// </summary>
        IDataStore DataStore = new DataStoreFactory().DataStore;

        /// <summary>
        /// The Index Page
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> containing the view data.</returns>
        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            model.PageOptions = new PageOptions();
            model.Response = DataStore.GetData(model.PageOptions);
            return View(model);
        }

        /// <summary>
        /// Adds a subject to the DataStore object.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> containing the updated view data.</returns>
        public ActionResult AddToSubjectTable()
        {
            IndexModel model = new IndexModel();
            TestSubject subject = new TestSubject()
            {
               Email = Request.QueryString["Email"],
               FirstName = Request.QueryString["FirstName"],
               LastName = Request.QueryString["LastName"],
               Password = Request.QueryString["Password"]
            };
            DataStore.AddSubject(subject);
            return UpdateSubjectTable();
        }

        /// <summary>
        /// Gets the updated table view.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> containing the updated view data.</returns>
        public ActionResult UpdateSubjectTable()
        {
            PageOptions options = new PageOptions(Request.QueryString["SortBy"], Request.QueryString["PageNumber"]);
            IndexModel model = new IndexModel();
            model.Response = DataStore.GetData(options);
            model.PageOptions = options;
            return View("SubjectTable", model);
        }

        /// <summary>
        /// Removes a subject from the datastore.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> containing the updated view data.</returns>
        public ActionResult RemoveSubject()
        {
            TestSubject subject = new TestSubject()
            {
                Email = Request.QueryString["Email"],
            };
            DataStore.DeleteSubject(subject);
            return UpdateSubjectTable();
        }

        /// <summary>
        /// Checks if an email address is unique (not in the datastore).
        /// </summary>
        /// <returns>True if unique, False if it already exists.</returns>
        public bool UniqueCheck()
        {
            string email = Request.QueryString["Email"];
            return DataStore.HasDuplicate(new TestSubject() { Email = email });
        }
    }
}
