using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using _5051.Models;
using _5051.Controllers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;


namespace _5051.Backend
{
    public class IdentityBackend
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public IdentityBackend() { }

        /// <summary>
        /// Creates a new Support User
        /// returns the newly created user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewSupportUser(string userName, string password)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = userName, Email = userName + "@seattleu.edu" };

            var result = UserManager.Create(user, password);

            if (!result.Succeeded)
            {
                //if user does exist, delete. This is just temporary
                var findResult = FindUserByUserName(userName);

                var deleteResult = DeleteUser(findResult);

                user = new ApplicationUser { UserName = userName, Email = userName + "@seattleu.edu" };

                result = UserManager.Create(user, userName);
            }

            UserManager.AddClaim(user.Id, new Claim("TeacherUser", "True"));

            UserManager.AddClaim(user.Id, new Claim("SupportUser", "True"));

            return user;
        }


        /// <summary>
        /// Creates a teacher user
        /// returns the newly created user or null if failure
        /// </summary>
        /// <param name="teacherName"></param>
        /// <param name="teacherPassword"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewTeacher(string teacherName, string teacherPassword, string teacherId)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = teacherName, Email = teacherName + "@seattleu.edu", Id = teacherId };

            var result = UserManager.Create(user, teacherPassword);

            if (!result.Succeeded)
            {
                var findResult = FindUserByUserName(teacherName);

                return null;
            }

            UserManager.AddClaim(user.Id, new Claim("TeacherUser", "True"));

            UserManager.AddClaim(user.Id, new Claim("TeacherID", teacherId));

            return user;
        }

        /// <summary>
        /// Creates a student user
        /// returns the newly created user or null if failed
        /// right now the password is changed to be the same as the student name
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="studentPassword"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewStudent(StudentModel student)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = student.Name, Email = student.Name + "@seattleu.edu", Id = student.Id };

            var result = UserManager.Create(user, student.Name);

            if (!result.Succeeded)
            {
                var findResult = FindUserByUserName(student.Name);

                return null;
            }

            UserManager.AddClaim(user.Id, new Claim("StudentUser", "True"));

            UserManager.AddClaim(user.Id, new Claim("StudentID", student.Id));

            return user;
        }


        /// <summary>
        /// updates all fields in the database except the id
        /// returns false if update fails
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool UpdateStudent(StudentModel student)
        {
            if(student == null)
            {
                return false;
            }

            var findResult = FindUserByID(student.Id);

            if(findResult == null)
            {
                return false;
            }

            //update all fields in db to match given student record
            findResult.UserName = student.Name;

            var updateResult = UserManager.Update(findResult);

            return true;
        }


        /// <summary>
        /// Finds and returns the user using the given user name
        /// if user does not exist, returns null
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ApplicationUser FindUserByUserName(string userName)
        {
            var findResult = UserManager.FindByName(userName);

            return findResult;
        }

        /// <summary>
        /// Finds and returns the user using the given id
        /// if user does not exist, returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationUser FindUserByID(string id)
        {
            var findResult = UserManager.FindById(id);

            return findResult;
        }

        /// <summary>
        /// Lists all the users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllUsers()
        {
            var userList = UserManager.Users.ToList();

            return userList;
        }

        /// <summary>
        /// Lists all the student users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllStudentUsers()
        {
            var userList = ListAllUsers();
            var studentList = new List<ApplicationUser>();

            foreach (var user in userList)
            {
                if (UserHasClaimOfValue(user, "StudentUser", "True"))
                {
                    studentList.Add(user);
                }
            }

            return studentList;
        }

        /// <summary>
        /// Lists all the teacher user
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllTeacherUsers()
        {
            var userList = ListAllUsers();
            var teacherList = new List<ApplicationUser>();

            foreach (var user in userList)
            {
                if (UserHasClaimOfValue(user, "TeacherUser", "True"))
                {
                    teacherList.Add(user);
                }
            }

            return teacherList;
        }


        /// <summary>
        /// lists all the support users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllSupportUsers()
        {
            var userList = ListAllUsers();
            var supportList = new List<ApplicationUser>();

            foreach (var user in userList)
            {
                if(UserHasClaimOfValue(user, "SupportUser", "True"))
                {
                    supportList.Add(user);
                }
            }

            return supportList;
        }

        /// <summary>
        /// checks if user has the claim type
        /// returns false if user does not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public bool UserHasClaim(ApplicationUser user, string claimType)
        {
            var claims = user.Claims.ToList();

            foreach (var item in claims)
            {
                if(item.ClaimType == claimType)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// checks if user has the given claim type and value
        /// returns false if not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public bool UserHasClaimOfValue(ApplicationUser user, string claimType, string claimValue)
        {
            var claims = user.Claims.ToList();

            foreach (var item in claims)
            {
                if (item.ClaimType == claimType && item.ClaimValue == claimValue)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Deletes the given user
        /// returns false if delete fails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(ApplicationUser user)
        {
            var deleteResult = UserManager.Delete(user);

            if (deleteResult.Succeeded)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes the user with the given id
        /// returns false if delete fails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(string id)
        {
            var findResult = FindUserByID(id);

            var deleteResult = UserManager.Delete(findResult);

            if (deleteResult.Succeeded)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Logs the user in with the given password
        /// returns false if login fails
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogUserIn(ApplicationUser user, string password)
        {
            var result = SignInManager.PasswordSignIn(user.UserName, password, isPersistent: false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return true;
                case SignInStatus.Failure:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Logs the user in using the given password
        /// returns false if login fails
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogUserIn(string userName, string password)
        {
            var result = SignInManager.PasswordSignIn(userName, password, isPersistent: false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return true;
                case SignInStatus.Failure:
                default:
                    return false;
            }
        }

        /// <summary>
        /// logs the currently logged in user out
        /// </summary>
        /// <returns></returns>
        public bool LogUserOut()
        {
            SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return true;
        }

        //need all the above functions again but async

    }


    public static class IdentityExtensions
    {

        public static bool GetIsTeacherUser(this IIdentity identity)
        {
            if (identity == null)
            {
                return false;
            }

            // return new ManageController().IsUserTeacherUser(userId);

            var claim = ((ClaimsIdentity)identity).FindFirst("TeacherUser");
            // Test for null to avoid issues during local testing

            if (claim == null)
            {
                return false;
            }

            if (claim.Value.Equals("True"))
            {
                return true;
            }

            return false;
        }

        public static bool GetIsSupportUser(this IIdentity identity)
        {
            if (identity == null)
            {
                return false;
            }

            // return new ManageController().IsUserTeacherUser(userId);

            var claim = ((ClaimsIdentity)identity).FindFirst("SupportUser");
            // Test for null to avoid issues during local testing

            if (claim == null)
            {
                return false;
            }

            if (claim.Value.Equals("True"))
            {
                return true;
            }

            return false;
        }


    }
}