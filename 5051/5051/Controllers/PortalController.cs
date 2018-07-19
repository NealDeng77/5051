using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class PortalController : BaseController
    {
        /// <summary>
        /// The list of all the active students in the class, so they can Roster
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Roster()
        {
            var myDataList = _5051.Backend.DataSourceBackend.Instance.StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        /// <summary>
        /// The Roster in page for the Portal, shows all the Students
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Login(string id=null)
        {
            var myStudent = Backend.StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Roster", "Portal");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);
            // Null not possible
            //if (myReturn == null)
            //{
            //    return RedirectToAction("Roster", "Portal");
            //}

            return View(myReturn);
        }

        /// <summary>
        /// Login for the student, take the ID, the rest of the fields are required but not used
        /// </summary>
        /// <param name="data"></param>
        /// <returns>if all is OK, then redirect to the student protal page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "AvatarId,"+
                                        "AvatarLevel,"+
                                        "Tokens,"+
                                        "Status,"+
                                        "Password,"+
                                        "ExperiencePoints,"+
                                        "Password,"+
                                        "")] StudentDisplayViewModel data)
        {
            // Any password is accepted for now. does not really login...

            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.Id);
            if (myStudent == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            // all is OK, so redirect to the student index page and pass in the student ID for now.
            return RedirectToAction("Index","Portal", new { id = data.Id });
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
                return RedirectToAction("Roster", "Portal");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);
            // Null not possible
            //if (myReturn == null)
            //{
            //    return RedirectToAction("Roster", "Portal");
            //}

            return View(myReturn);
        }

        /// <summary>
        /// Attendance Page
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult Attendance(string id = null)
        {
            var myStudent = Backend.StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Roster", "Portal");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);
            // not possible to be null
            //if (myReturn == null)
            //{
            //    return RedirectToAction("Roster", "Portal");
            //}

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
        public ActionResult Visit(string id = null)
        {
            // Get the list of other students' shop
            // it will show the name of the student's shop and its owner 

            // then, once students click on the specific student's shop
            // he/she can go visiting it

            //var myStudent = Backend.StudentBackend.Instance.Read(id);
            //if (myStudent == null)
            //{
            //    return RedirectToAction("Error", "Home");
            //}

            //var myReturn = new StudentDisplayViewModel(myStudent);
            //// Not possible for Null return, 
            ////if (myReturn == null)
            ////{
            ////    return RedirectToAction("Error", "Home");
            ////}

            return View();
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
            // null not possible
            //if (myReturn == null)
            //{
            //    return RedirectToAction("Error", "Home");
            //}

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
                                        "ExperiencePoints,"+
                                        "Password,"+

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
            var myStudent = StudentBackend.Instance.Read(id);

            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }


            var myReport = new StudentReportViewModel
            {
                StudentId = id,
                Year = DateTime.UtcNow.Year,
                Month = DateTime.UtcNow.Month
            };

            var myReturn = ReportBackend.Instance.GenerateMonthlyReport(myReport);

            return View(myReturn);
        }
    }
}