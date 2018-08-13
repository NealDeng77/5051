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


        /// <summary>
        /// Creates a new Support User with a password the same as the user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewSupportUser(string userName)
        {
            //check if username exists, if so delete
            var user = new ApplicationUser { UserName = userName, Email = userName + "@seattleu.edu" };
            //var result = await UserManager.CreateAsync(user, user.UserName);
            var result = UserManager.Create(user, userName);

            if (!result.Succeeded)
            {
                var findResult = FindUserByUserName(userName);

                var deleteResult = DeleteUser(findResult);

                user = new ApplicationUser { UserName = userName, Email = userName + "@seattleu.edu" };

                result = UserManager.Create(user);
            }

            UserManager.AddClaim(user.Id, new Claim("TeacherUser", "True"));

            UserManager.AddClaim(user.Id, new Claim("SupportUser", "True"));

            LogUserIn(user, userName);

            return user;
        }


        /// <summary>
        /// Creates a teacher user
        /// </summary>
        /// <param name="teacherName"></param>
        /// <param name="teacherPassword"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewTeacher(string teacherName, string teacherPassword, string teacherId)
        {
            var user = new ApplicationUser { UserName = teacherName, Email = teacherName + "@seattleu.edu" };
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
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="studentPassword"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewStudent(string studentName, string studentPassword, string studentId)
        {
            var user = new ApplicationUser { UserName = studentName, Email = studentName + "@seattleu.edu" };
            var result = UserManager.Create(user, studentPassword);

            if (!result.Succeeded)
            {
                var findResult = FindUserByUserName(studentName);

                return null;
            }

            UserManager.AddClaim(user.Id, new Claim("StudentID", studentId));

            return user;
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
        /// Deletes the given user
        /// returns false if delete fails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(ApplicationUser user)
        {
            //var findResult = FindUserByUserName(user.UserName);

            var deleteResult = UserManager.Delete(user);

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