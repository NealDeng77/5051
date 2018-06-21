﻿using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    /// <summary>
    /// School Dismissal Settings defaults to a single record.  So no Create or Delete, just Read, and Update
    /// </summary>

    public class KioskSettingsController : Controller
    {
        // The Backend Data source
        private KioskSettingsBackend KioskSettingsBackend = KioskSettingsBackend.Instance;

        /// <summary>
        /// Read information on a single KioskSettings
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: KioskSettings/Details/5
        public ActionResult Read(string id = null)
        {
            var myData = KioskSettingsBackend.Read(id);
            if (myData == null)
            {
                // If no ID is passed in, get the first one.
                myData = KioskSettingsBackend.GetDefault();
                if (myData == null)
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            return View(myData);
        }

        /// <summary>
        /// This will show the details of the KioskSettings to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: KioskSettings/Edit/5
        public ActionResult Update(string id = null)
        {
            var myData = KioskSettingsBackend.Read(id);
            if (myData == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(myData);
        }

        /// <summary>
        /// This updates the KioskSettings based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: KioskSettings/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id," +
                                        "Password," +

                                        "")] KioskSettingsModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for edit
                return View(data);
            }

            KioskSettingsBackend.Update(data);

            return RedirectToAction("Settings", "Admin");
        }
    }
}