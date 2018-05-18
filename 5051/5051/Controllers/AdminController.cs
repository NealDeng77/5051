using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Reports
        /// </summary>
        /// <returns></returns>
        // GET: Report
        public ActionResult Report()
        {
            return View();
        }

        /// <summary>
        /// Calendar
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Calendar()
        {
            return View();
        }
    }
}