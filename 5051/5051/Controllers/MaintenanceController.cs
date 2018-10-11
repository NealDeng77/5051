using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;

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
                Source = SystemGlobalsModel.Instance.DataSourceValue,
                ConfirmSource = DataSourceEnum.Unknown,

                Destination = DataSourceEnum.Unknown,
                ConfirmDestination = DataSourceEnum.Unknown,

                Password = ""
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult BackupData([Bind(Include =
                                            "Source," +
                                            "ConfirmSource," +
                                            "Destination," +
                                            "ConfirmDestination," +
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

            if (BackupData.Destination != BackupData.ConfirmDestination)
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            if (BackupData.Source != BackupData.ConfirmSource)
            {
                return RedirectToAction("BackupData", "Maintenance");
            }


            if (string.IsNullOrEmpty(BackupData.Password))
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            //if (SystemGlobalsModel.Instance.DataSourceValue == DataSourceEnum.Mock)
            //{
            //    return RedirectToAction("BackupData", "Maintenance");
            //}

            //DataSourceBackendTable.Instance.CopyDataDirect<AvatarItemModel>(DestinationDataSource, "avataritemmodel");

            //DataSourceBackendTable.Instance.CopyDataDirect<FactoryInventoryModel>(DestinationDataSource, "factoryinventorymodel");

            //DataSourceBackendTable.Instance.CopyDataDirect<ApplicationUser>(DestinationDataSource, "identitymodel");

            DataSourceBackend.Instance.SchoolCalendarBackend.BackupData(BackupData.Source, BackupData.Destination);

            DataSourceBackend.Instance.SchoolDismissalSettingsBackend.BackupData(BackupData.Source, BackupData.Destination);

            DataSourceBackend.Instance.StudentBackend.BackupData(BackupData.Source, BackupData.Destination);

            DataSourceBackend.Instance.KioskSettingsBackend.BackupData(BackupData.Source, BackupData.Destination);

            return RedirectToAction("Index", "Maintenance");
        }

    }
}