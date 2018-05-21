using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using _5051.Models;

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
            var myStudent = Backend.StudentBackend.Instance.GetDefault();
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);
            if (myReturn == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(myReturn);
        }


        /// <summary>
        /// Index Page
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult Index(string id = null)
        {
            var myStudent = Backend.StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);
            if (myReturn == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(myReturn);
        }

        /// <summary>
        /// Student's Avatar page
        /// </summary>
        /// <returns></returns>
        // Post: Portal
        [HttpPost]
        public ActionResult Avatar([Bind(Include=
                                        "AvatarId,"+
                                        "StudentId,"+
                                        "")] StudentAvatarModel data)
        {
            // If data passed up is not valid, go back to the Index page so the user can try again
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

            // If the Avatar Id is blank, error out
            if (string.IsNullOrEmpty(data.AvatarId))
            {
                return RedirectToAction("Error", "Home");
            }

            // If the Student Id is black, error out
            if (string.IsNullOrEmpty(data.StudentId))
            {
                return RedirectToAction("Error", "Home");
            }

            // Lookup the student id, will just replace the Avatar Id on it if it is valid
            var myStudent = Backend.StudentBackend.Instance.Read(data.StudentId);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Set the Avatar ID on the Student and update in data store
            myStudent.AvatarId = data.AvatarId;
            Backend.StudentBackend.Instance.Update(myStudent);

            // Editing is done, so go back to the Student Portal
            return RedirectToAction("Index", "Portal", new { Id = myStudent.Id });
        }

        /// <summary>
        /// Student's Avatar page
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Selected Avatar View Model</returns>
        // GET: Portal
        public ActionResult Avatar(string id = null)
        {
            // var currentUser = User.Identity.GetUserName();
            //var currentUserId = User.Identity.GetUserId();

            var myStudent = Backend.StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myAvatar = Backend.AvatarBackend.Instance.Read(myStudent.AvatarId);
            if (myAvatar == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var SelectedAvatarViewModel = new SelectedAvatarForStudentViewModel();

            // Populate the Values to use
            SelectedAvatarViewModel.AvatarList = Backend.AvatarBackend.Instance.Index();

            // Build up the List of AvatarLevels, each list holds the avatar of that level.
            SelectedAvatarViewModel.MaxLevel = SelectedAvatarViewModel.AvatarList.Aggregate((i1, i2) => i1.Level > i2.Level? i1 : i2).Level;

            SelectedAvatarViewModel.AvatarLevelList = new List<AvatarViewModel>();
            // populate each list at the level
            for (var i=1; i <= SelectedAvatarViewModel.MaxLevel; i++)
            {
                var tempList = SelectedAvatarViewModel.AvatarList.Where(m => m.Level == i).ToList();
                var tempAvatarList = new AvatarViewModel();
                tempAvatarList.AvatarList = new List<AvatarModel>();
                tempAvatarList.AvatarList.AddRange(tempList);
                tempAvatarList.ListLevel = i;
                SelectedAvatarViewModel.AvatarLevelList.Add(tempAvatarList);
            }

            SelectedAvatarViewModel.SelectedAvatar = myAvatar;
            SelectedAvatarViewModel.Student = myStudent;

            return View(SelectedAvatarViewModel);
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
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult House(string id = null)
        {
            var myStudent = Backend.StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);
            if (myReturn == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(myReturn);
        }

        /// <summary>
        ///  My Settings
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult Settings(string id = null)
        {
            var myStudent = Backend.StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);
            if (myReturn == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(myReturn);
        }


        /// <summary>
        /// Student's Avatar page
        /// </summary>
        /// <returns></returns>
        // Post: Portal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "AvatarId,"+
                                        "AvatarLevel,"+
                                        "Tokens,"+
                                        "Status,"+
                                        "AvatarUri,"+
                                        "")] StudentDisplayViewModel data)
        {
            // If data passed up is not valid, go back to the Index page so the user can try again
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

            // If the Avatar Id is blank, error out
            if (string.IsNullOrEmpty(data.AvatarId))
            {
                return RedirectToAction("Error", "Home");
            }

            // If the Student Id is black, error out
            if (string.IsNullOrEmpty(data.Id))
            {
                return RedirectToAction("Error", "Home");
            }

            // Lookup the student id, will just replace the Avatar Id on it if it is valid
            var myStudent = Backend.StudentBackend.Instance.Read(data.Id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Set the Avatar ID on the Student and update in data store
            myStudent.Name= data.Name;
            Backend.StudentBackend.Instance.Update(myStudent);

            // Editing is done, so go back to the Student Portal and pass the Student Id
            return RedirectToAction("Index", "Portal", new { Id = myStudent.Id });
        }

        /// <summary>
        /// My Attendance Reports
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult Report(string id = null)
        {
            var myStudent = Backend.StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);
            if (myReturn == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(myReturn);
        }
    }
}