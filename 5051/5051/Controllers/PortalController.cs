using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace _5051.Controllers
{
    public class PortalController : Controller
    {
        /// <summary>
        /// The Login in page for the Portal, shows all the Students
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Login()
        {
            return View();
        }


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
           // var currentUser = User.Identity.GetUserName();
            //var currentUserId = User.Identity.GetUserId();

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
        public ActionResult Settings()
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