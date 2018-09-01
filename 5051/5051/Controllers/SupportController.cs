using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using _5051.Controllers;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;

namespace _5051.Controllers
{
    public class SupportController : BaseController
    {
        private IdentityDataSourceMock identityBackend = new IdentityDataSourceMock();
        private DataSourceBackend DataSourceBackend = DataSourceBackend.Instance;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public SupportController() { }

        public SupportController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Support
        public ActionResult Index()
        {
            return View();
        }

        //GET lists all the users
        public ActionResult UserList()
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var userList = IdentityDataSourceTable.Instance.ListAllUsers();

            return View(userList);
        }


        //GET Support/UserInfo/userID
        public ActionResult UserInfo(string id = null)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var myUserInfo = IdentityDataSourceTable.Instance.FindUserByID(id);

            return View(myUserInfo);
            
        }

        //GET support/togglestudent/userID
        public ActionResult ToggleStudent(string id = null)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var myUserInfo = IdentityDataSourceTable.Instance.FindUserByID(id);

            return View(myUserInfo);
        }

        //toggles whether or not given user is a student
        [HttpPost]
        public ActionResult ToggleStudent([Bind(Include =
                                                "Id,"+
                                                "")] ApplicationUser studentUser)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            if (IdentityDataSourceTable.Instance.UserHasClaimOfValue(studentUser.Id, "StudentUser", "True"))
            {
                IdentityDataSourceTable.Instance.RemoveClaimFromUser(studentUser.Id, "StudentUser");
            }
            else
            {
                IdentityDataSourceTable.Instance.AddClaimToUser(studentUser.Id, "StudentUser", "True");
            }

            return RedirectToAction("UserList", "Support");

        }

        //GET support/toggleTeacher/userID
        public ActionResult ToggleTeacher(string id = null)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var myUserInfo = IdentityDataSourceTable.Instance.FindUserByID(id);

            return View(myUserInfo);

        }

        //toggles whether or not user is a teacher
        [HttpPost]
        public ActionResult ToggleTeacher([Bind(Include =
                                                "Id,"+
                                                "")] ApplicationUser teacherUser)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            if (IdentityDataSourceTable.Instance.UserHasClaimOfValue(teacherUser.Id, "TeacherUser", "True"))
            {
                IdentityDataSourceTable.Instance.RemoveClaimFromUser(teacherUser.Id, "TeacherUser");
            }
            else
            {
                IdentityDataSourceTable.Instance.AddClaimToUser(teacherUser.Id, "TeacherUser", "True");
            }

            return RedirectToAction("UserList", "Support");
        }

        //GET support/ToggleSupport/userID
        public ActionResult ToggleSupport(string id = null)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var myUserInfo = IdentityDataSourceTable.Instance.FindUserByID(id);

            return View(myUserInfo);
        }

        //toggles whether or not user is a support user
        [HttpPost]
        public ActionResult ToggleSupport([Bind(Include =
                                                "Id,"+
                                                "")] ApplicationUser supportUser)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            if (IdentityDataSourceTable.Instance.UserHasClaimOfValue(supportUser.Id, "SupportUser", "True"))
            {
                IdentityDataSourceTable.Instance.RemoveClaimFromUser(supportUser.Id, "SupportUser");
            }
            else
            {
                IdentityDataSourceTable.Instance.AddClaimToUser(supportUser.Id, "SupportUser", "True");
            }

            return RedirectToAction("UserList", "Support");
        }

        //GET
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel user)
        {
            if(!ModelState.IsValid)
            {
                return View(user);
            }

            var loginResult = IdentityDataSourceTable.Instance.LogUserIn(user.Email, user.Password, IdentityDataSourceTable.IdentityRole.Support);
            if (!loginResult)
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View(user);
            }

            return RedirectToAction("Index", "Support");
        }

        //GET
        public ActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudent(LoginViewModel user)
        {
            if(!ModelState.IsValid)
            {
                return View(user);
            }

            var newStudent = new StudentModel();

            newStudent.Name = user.Email;
            newStudent.Password = user.Password;

            var createUserResult = IdentityDataSourceTable.Instance.CreateNewStudent(newStudent);

            if(createUserResult == null)
            {
                ModelState.AddModelError("", "Invalid create user attempt");
            }

            return RedirectToAction("UserList", "Support");
        }

        //GET
        public ActionResult CreateTeacher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTeacher(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var createResult = IdentityDataSourceTable.Instance.CreateNewTeacher(user.Email, user.Password, user.Email);

            if(createResult == null)
            {
                ModelState.AddModelError("", "Invalid Create Attempt");
            }

            return RedirectToAction("UserList", "Support");
        }

        //GET
        public ActionResult CreateSupport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSupport(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var createResult = IdentityDataSourceTable.Instance.CreateNewSupportUser(user.Email, user.Password, user.Email);

            if (createResult == null)
            {
                ModelState.AddModelError("", "Invalid Create Attempt");
            }

            return RedirectToAction("UserList", "Support");
        }

        //GET
        public ActionResult DeleteUser(string id = null)
        {
            var findResult = IdentityDataSourceTable.Instance.FindUserByID(id);
            if(findResult == null)
            {
                return RedirectToAction("UserList", "Support");
            }

            return View(findResult);
        }

        [HttpPost]
        public ActionResult DeleteUser([Bind(Include =
                                             "Id," +
                                             "")] ApplicationUser user)
        {
            var findResult = IdentityDataSourceTable.Instance.FindUserByID(user.Id);
            if (findResult == null)
            {
                return View(user);
            }

            var deleteResult = IdentityDataSourceTable.Instance.DeleteUser(findResult);
            if(!deleteResult)
            {
                ModelState.AddModelError("", "Invalid Delete Attempt.");
                return View(user);
            }

            return RedirectToAction("UserList", "Support");
        }

        public ActionResult Settings()
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            return View();
        }

        /// <summary>
        /// Calls the data sources and has them reset to default data
        /// </summary>
        /// <returns></returns>
        // GET: Reset
        public ActionResult Reset()
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            DataSourceBackend.Instance.Reset();
            return RedirectToAction("Index", "Support");
        }

        /// <summary>
        /// Change the data set from default to demo, to ut etc.
        /// </summary>
        /// <returns></returns>
        // GET: Settings
        public ActionResult DataSourceSet(string id = null)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Support");
            }

            DataSourceDataSetEnum SetEnum = DataSourceDataSetEnum.Default;
            switch (id)
            {
                case "Default":
                    SetEnum = DataSourceDataSetEnum.Default;
                    break;

                case "Demo":
                    SetEnum = DataSourceDataSetEnum.Demo;
                    break;

                case "UnitTest":
                    SetEnum = DataSourceDataSetEnum.UnitTest;
                    break;
            }

            DataSourceBackend.Instance.SetDataSourceDataSet(SetEnum);

            return RedirectToAction("Index", "Support");
        }

        /// <summary>
        /// Change the data source
        /// </summary>
        /// <returns></returns>
        // GET: Settings
        public ActionResult DataSource(string id = null)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Support");
            }

            DataSourceEnum SetEnum = DataSourceEnum.Mock;
            switch (id)
            {
                case "Mock":
                    SetEnum = DataSourceEnum.Mock;
                    break;

                case "SQL":
                    SetEnum = DataSourceEnum.SQL;
                    break;

                case "Local":
                    SetEnum = DataSourceEnum.Local;
                    break;

                case "ServerLive":
                    SetEnum = DataSourceEnum.ServerLive;
                    break;

                case "ServerTest":
                    SetEnum = DataSourceEnum.ServerTest;
                    break;

                case "Unknown":
                default:
                    SetEnum = DataSourceEnum.Unknown;
                    break;
            }

            DataSourceBackend.Instance.SetDataSource(SetEnum);

            return RedirectToAction("Index", "Support");
        }
    }
}