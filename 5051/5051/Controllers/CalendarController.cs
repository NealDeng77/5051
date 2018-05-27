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
            return View();
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