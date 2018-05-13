using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;


namespace _5051.Controllers
{
    public class AvatarController : Controller
    {
        private AvatarViewModel avatarViewModel = new AvatarViewModel();
        private AvatarBackend avatarBackend = new AvatarBackend();

        // GET: Avatar
        public ActionResult Index()
        {
            // Load the list of data into the AvatarList
            avatarViewModel.AvatarList = avatarBackend.DataSource.Index();
            return View(avatarViewModel);
        }

        // GET: Avatar/Details/5
        public ActionResult Details(string id = null)
        {
            var myData = avatarBackend.DataSource.Read(id);
            return View(myData);
        }

        // GET: Avatar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avatar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Avatar/Edit/5
        public ActionResult Update(string id = null)
        {
            var myData = avatarBackend.DataSource.Read(id);
            return View(myData);
        }

        // POST: Avatar/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "")] AvatarModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (data == null)
                    {
                        return RedirectToAction("Error", new { route = "Home", action = "Error" });
                    }

                    if (string.IsNullOrEmpty(data.Id))
                    {
                        return View(data);
                    }

                    avatarBackend.DataSource.Update(data);

                    return RedirectToAction("Index");
                }

                // Send back for edit
                return View(data);
            }
            catch
            {
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }
        }

        // GET: Avatar/Delete/5
        public ActionResult Delete(string id = null)
        {
            return View();
        }

        // POST: Avatar/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "")] AvatarModel data)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
