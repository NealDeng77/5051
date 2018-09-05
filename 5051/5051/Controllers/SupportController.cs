using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using Microsoft.AspNet.Identity.Owin;
using System;

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

            // Get the List of Users, and convert them to Student Display Views so they get an avatar.  
            // Because the ID may not be a student ID, use the Identity ID
            // Because the Name may be null, use the Identity Name

            var myReturn = new UserListViewModel();

            var data = IdentityDataSourceTable.Instance.ListAllStudentUsers();
            foreach (var item in data)
            {
                var user = DataSourceBackend.Instance.StudentBackend.Read(item.Id);
                var temp = new StudentDisplayViewModel(user);
                temp.Name = item.UserName;
                temp.Id = item.Id;
                myReturn.StudentList.Add(temp);
            }

            data = IdentityDataSourceTable.Instance.ListAllTeacherUsers();
            foreach (var item in data)
            {
                var user = DataSourceBackend.Instance.StudentBackend.Read(item.Id);
                var temp = new StudentDisplayViewModel(user);
                temp.Name = item.UserName;
                temp.Id = item.Id;
                myReturn.TeacherList.Add(temp);
            }

            data = IdentityDataSourceTable.Instance.ListAllSupportUsers();
            foreach (var item in data)
            {
                var user = DataSourceBackend.Instance.StudentBackend.Read(item.Id);
                var temp = new StudentDisplayViewModel(user);
                temp.Name = item.UserName;
                temp.Id = item.Id;
                myReturn.SupportList.Add(temp);
            }

            data = IdentityDataSourceTable.Instance.ListAllUsers();
            foreach (var item in data)
            {
                var user = DataSourceBackend.Instance.StudentBackend.Read(item.Id);
                var temp = new StudentDisplayViewModel(user);
                temp.Name = item.UserName;
                temp.Id = item.Id;
                myReturn.UserList.Add(temp);
            }

            return View(myReturn);
        }


        //GET Support/UserInfo/userID
        public ActionResult UserInfo(string id = null)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var myUserInfo = IdentityDataSourceTable.Instance.FindUserByID(id);

            var data = new ApplicationUserViewModel(myUserInfo);

            return View(data);
        }

        //GET support/togglestudent/userID
        public ActionResult ToggleUser(string id = null, string item = null)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(item))
            {
                return RedirectToAction("Index", "Home");
            }

            var myUserInfo = IdentityDataSourceTable.Instance.FindUserByID(id);
            if (myUserInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var RoleEnum = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), item, true);

            var myReturn = new ApplicationUserInputModel(myUserInfo);
            myReturn.Id = myUserInfo.Id;
            myReturn.Role = RoleEnum;
            myReturn.State = IdentityDataSourceTable.Instance.UserHasClaimOfValue(myUserInfo.Id, RoleEnum.ToString(), "True");

            return View(myReturn);
        }

        //toggles whether or not given user is a student
        [HttpPost]
        public ActionResult ToggleUser([Bind(Include =
                                                "Id,"+
                                                "State,"+
                                                "Role,"+
                                                "")] ApplicationUserInputModel data)
        {
            //if (DataSourceBackend.IsUserNotInRole(User.Identity.GetUserId(), "SupportUser"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                return RedirectToAction("Error", "Home");
            }

            if (data.Role == UserRoleEnum.Unknown)
            {
                return RedirectToAction("Error", "Home");
            }


            if (IdentityDataSourceTable.Instance.UserHasClaimOfValue(data.Id, data.Role.ToString(), "True"))
            {
                IdentityDataSourceTable.Instance.RemoveClaimFromUser(data.Id, data.Role.ToString());
            }
            else
            {
                IdentityDataSourceTable.Instance.AddClaimToUser(data.Id, data.Role.ToString(), "True");
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
            if (!ModelState.IsValid)
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
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var newStudent = new StudentModel();

            newStudent.Name = user.Email;
            newStudent.Password = user.Password;

            //var createUserResult = IdentityDataSourceTable.Instance.CreateNewStudent(newStudent);
            var createUserResult = StudentBackend.Instance.Create(newStudent);

            if (createUserResult == null)
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

            if (createResult == null)
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
            if (findResult == null)
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

            var deleteResult = IdentityDataSourceTable.Instance.DeleteUser(findResult.Id);
            if (!deleteResult)
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

        //GET
        public ActionResult ChangeUserPassword(string id = null)
        {
            var findResult = IdentityDataSourceTable.Instance.FindUserByID(id);
            if (findResult == null)
            {
                return RedirectToAction("UserList", "Support");
            }

            var passModel = new ChangePasswordViewModel();
            passModel.UserID = findResult.Id;


            return View(passModel);
        }

        [HttpPost]
        public ActionResult ChangeUserPassword([Bind(Include =
                                             "UserID," +
                                             "NewPassword," +
                                             "")] ChangePasswordViewModel model)
        {
            var backend = IdentityDataSourceTable.Instance;
            var findResult = backend.FindUserByID(model.UserID);
            if (findResult == null)
            {
                return View(model);
            }

            if (backend.UserHasClaimOfValue(findResult.Id, "SupportUser", "True"))
            {
                var changePassResult = IdentityDataSourceTable.Instance.ChangeUserPassword(findResult.UserName, model.NewPassword, IdentityDataSourceTable.IdentityRole.Support);
                if (!changePassResult)
                {
                    ModelState.AddModelError("", "Invalid Change Password Attempt.");
                    return View(model);
                }
            }
            else if (backend.UserHasClaimOfValue(findResult.Id, "TeacherUser", "True"))
            {
                var changePassResult = IdentityDataSourceTable.Instance.ChangeUserPassword(findResult.UserName, model.NewPassword, IdentityDataSourceTable.IdentityRole.Teacher);
                if (!changePassResult)
                {
                    ModelState.AddModelError("", "Invalid Change Password Attempt.");
                    return View(model);
                }
            }
            else
            {
                var changePassResult = IdentityDataSourceTable.Instance.ChangeUserPassword(findResult.UserName, model.NewPassword, IdentityDataSourceTable.IdentityRole.Student);
                if (!changePassResult)
                {
                    ModelState.AddModelError("", "Invalid Change Password Attempt.");
                    return View(model);
                }
            }

            return RedirectToAction("UserList", "Support");
        }
    }
}