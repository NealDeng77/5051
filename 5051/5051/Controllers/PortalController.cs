using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    public class PortalController : Controller
    {
        /// <summary>
        /// Index Page
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Student's Avatar page
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Avatar()
        {
            return View();
        }

        /// <summary>
        /// The Group's House information
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Group()
        {
            return View();
        }

        /// <summary>
        ///  My House
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult House()
        {
            return View();
        }

        /// <summary>
        ///  My Settings
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Setting()
        {
            return View();
        }

        /// <summary>
        /// My Attendance Reports
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Report()
        {
            return View();
        }
    }
}