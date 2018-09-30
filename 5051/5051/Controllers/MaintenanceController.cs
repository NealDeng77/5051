using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace _5051.Controllers
{
    public class MaintenanceController: BaseController
    {
        // GET: Support
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BackupData()
        {
            // TODO:
            // Lookup Current user, make sure they are in the support list.
            //var findResult = IdentityBackend.Instance.FindUserByID(id);
            //if (findResult == null)
            //{
            //return RedirectToAction("UserList", "Support");
            //}

            var data = new BackupDataInputModel
            {
                Destination = DataSourceEnum.Unknown,
                Confirm = DataSourceEnum.Unknown,
                Password = ""
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult BackupData([Bind(Include =
                                             "DataSourceEnum," +
                                             "")] DataSourceEnum DestinationDataSourceEnum)
        {
            // Todo: 
            // Check for Valid User
            //return RedirectToAction("UserList", "Support");

            if (!ModelState.IsValid)
            {
                return View();
            }

            return RedirectToAction("Maintenance", "Support");
        }

    }
}