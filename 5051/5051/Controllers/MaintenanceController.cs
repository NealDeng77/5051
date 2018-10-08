using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace _5051.Controllers
{
    public class MaintenanceController : BaseController
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
                                            "Destination," +
                                            "Confirm," +
                                            "Password," +
                                             "")] BackupDataInputModel BackupData)
        {
            // Todo: 
            // Check for Valid User
            //return RedirectToAction("UserList", "Support");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            if (BackupData.Destination != BackupData.Confirm)
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            if (string.IsNullOrEmpty(BackupData.Password))
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            if (SystemGlobalsModel.Instance.DataSourceValue == DataSourceEnum.Mock)
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            var DestinationDataSource = BackupData.Destination;

            DataSourceBackendTable.Instance.CopyDataDirect<AvatarItemModel>(DestinationDataSource, "avataritemmodel");

            DataSourceBackendTable.Instance.CopyDataDirect<FactoryInventoryModel>(DestinationDataSource, "factoryinventorymodel");

            DataSourceBackendTable.Instance.CopyDataDirect<ApplicationUser>(DestinationDataSource, "identitymodel");

            DataSourceBackendTable.Instance.CopyDataDirect<SchoolCalendarModel>(DestinationDataSource, "schoolcalendarmodel");

            DataSourceBackendTable.Instance.CopyDataDirect<SchoolDismissalSettingsModel>(DestinationDataSource, "schooldismissalsettingsmodel");

            DataSourceBackendTable.Instance.CopyDataDirect<StudentModel>(DestinationDataSource, "studentmodel");



            // TODO  Why does this fail?
            //DataSourceBackendTable.Instance.CopyDataDirect<KioskSettingsModel>(DestinationDataSource, "kiosksettingsmodel");

            return RedirectToAction("Index", "Maintenance");
        }

    }
}