using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    /// <summary>
    /// The Calendar crudi
    /// </summary>
    public class CalendarController : Controller
    {
        /// <summary>
        /// Calendar Index
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Index()
        {
            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Index();
            return View(myData);
        }

        /// <summary>
        /// Calendar Edit
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Edit()
        {
            return View();
        }

    }
}