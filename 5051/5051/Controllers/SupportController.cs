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
        private IdentityBackend identityBackend = new IdentityBackend();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

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
            if (User.Identity.GetIsSupportUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //GET lists all the users
        public ActionResult UserList()
        {
            if (User.Identity.GetIsSupportUser())
            {
                var userList = identityBackend.ListAllUsers();

                return View(userList);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        //GET Support/UserInfo/userID
        public ActionResult UserInfo(string id = null)
        {
            if (User.Identity.GetIsSupportUser())
            {
                var myUserInfo = identityBackend.FindUserByID(id);

                return View(myUserInfo);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //GET support/togglestudent/userID
        public ActionResult ToggleStudent(string id = null)
        {
            if (User.Identity.GetIsSupportUser())
            {
                var myUserInfo = identityBackend.FindUserByID(id);

                return View(myUserInfo);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //toggles whether or not given user is a student
        [HttpPost]
        public ActionResult ToggleStudent([Bind(Include =
                                                "Id,"+
                                                "")] ApplicationUser studentUser)
        {
            if (User.Identity.GetIsSupportUser())
            {
                if (identityBackend.UserHasClaimOfValue(studentUser.Id, "StudentUser", "True"))
                {
                    identityBackend.RemoveClaimFromUser(studentUser.Id, "StudentUser");
                }
                else
                {
                    identityBackend.AddClaimToUser(studentUser.Id, "StudentUser", "True");
                }

                return RedirectToAction("UserList", "Support");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //GET support/toggleTeacher/userID
        public ActionResult ToggleTeacher(string id = null)
        {
            if (User.Identity.GetIsSupportUser())
            {
                var myUserInfo = identityBackend.FindUserByID(id);

                return View(myUserInfo);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //toggles whether or not user is a teacher
        [HttpPost]
        public ActionResult ToggleTeacher([Bind(Include =
                                                "Id,"+
                                                "")] ApplicationUser teacherUser)
        {
            if (User.Identity.GetIsSupportUser())
            {
                if (identityBackend.UserHasClaimOfValue(teacherUser.Id, "TeacherUser", "True"))
                {
                    identityBackend.RemoveClaimFromUser(teacherUser.Id, "TeacherUser");
                }
                else
                {
                    identityBackend.AddClaimToUser(teacherUser.Id, "TeacherUser", "True");
                }

                return RedirectToAction("UserList", "Support");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //GET support/ToggleSupport/userID
        public ActionResult ToggleSupport(string id = null)
        {
            if (User.Identity.GetIsSupportUser())
            {
                var myUserInfo = identityBackend.FindUserByID(id);

                return View(myUserInfo);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //toggles whether or not user is a support user
        [HttpPost]
        public ActionResult ToggleSupport([Bind(Include =
                                                "Id,"+
                                                "")] ApplicationUser supportUser)
        {
            if (User.Identity.GetIsSupportUser())
            {
                if (identityBackend.UserHasClaimOfValue(supportUser.Id, "SupportUser", "True"))
                {
                    identityBackend.RemoveClaimFromUser(supportUser.Id, "SupportUser");
                }
                else
                {
                    identityBackend.AddClaimToUser(supportUser.Id, "SupportUser", "True");
                }

                return RedirectToAction("UserList", "Support");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}