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

        ////POST
        //[HttpPost]
        //public ActionResult UserInfo([Bind(Include =
        //                                    "Id,"+
        //                                    "")] StudentModel data)
        //{

        //    //user idbackend
        //    //if user has claim, remove claim, else add the claim
        //    //what could this be based on tho?
        //    identityBackend.FindUserByID(data.Id);


        //    return RedirectToAction("UserList", "Support");
        //}

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


        [HttpPost]
        public ActionResult ToggleTeacher([Bind(Include =
                                                "Id,"+
                                                "")] ApplicationUser studentUser)
        {
            if (User.Identity.GetIsSupportUser())
            {
                if (identityBackend.UserHasClaimOfValue(studentUser.Id, "TeacherUser", "True"))
                {
                    identityBackend.RemoveClaimFromUser(studentUser.Id, "TeacherUser");
                }
                else
                {
                    identityBackend.AddClaimToUser(studentUser.Id, "TeacherUser", "True");
                }

                return RedirectToAction("UserList", "Support");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

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


        [HttpPost]
        public ActionResult ToggleSupport([Bind(Include =
                                                "Id,"+
                                                "")] ApplicationUser studentUser)
        {
            if (User.Identity.GetIsSupportUser())
            {
                if (identityBackend.UserHasClaimOfValue(studentUser.Id, "SupportUser", "True"))
                {
                    identityBackend.RemoveClaimFromUser(studentUser.Id, "SupportUser");
                }
                else
                {
                    identityBackend.AddClaimToUser(studentUser.Id, "SupportUser", "True");
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