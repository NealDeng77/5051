using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace _5051.Backend
{
    public class IdentityDataSourceTable : IIdentityInterface
    {



        private static volatile IdentityDataSourceTable instance;
        private static object syncRoot = new Object();

        private IdentityDataSourceTable() { }

        public static IdentityDataSourceTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new IdentityDataSourceTable();
                        instance.Reset();
                    }
                }

                return instance;
            }
        }



        private List<ApplicationUser> DataList = new List<ApplicationUser>();

        private const string ClassName = "IdentityModel";

        private string tableName = ClassName.ToLower();

        private string partitionKey = ClassName.ToLower();



        /// <summary>
        /// Creates a new Support User
        /// returns the newly created user
        /// returns null if failed
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewSupportUser(string userName, string password, string supportId)
        {
            var user = new ApplicationUser { UserName = userName, Email = userName + "@seattleu.edu", Id = supportId };

            //need to add claims / role / something to distinguish user

            DataList.Add(user);

            //add to storage
            var myResult = DataSourceBackendTable.Instance.Create<ApplicationUser>(tableName, partitionKey, user.Id, user);

            return myResult;
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

            //var result = idBackend.UserManager.Create(user, teacherPassword);

            //if (!result.Succeeded)
            //{
            //    return null;
            //}

            //var claimResult = AddClaimToUser(user.Id, "TeacherUser", "True");
            //claimResult = AddClaimToUser(user.Id, "TeacherID", teacherId);

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
            if (student == null)
            {
                return false;
            }

            //var findResult = FindUserByID(student.Id);

            //if (findResult == null)
            //{
            //    return false;
            //}

            ////update all fields in db to match given student record
            //findResult.UserName = student.Name;

            //var updateResult = idBackend.UserManager.Update(findResult);

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
            //var findResult = idBackend.UserManager.FindByName(userName);

            //return findResult;
            return null;
        }

        /// <summary>
        /// Finds and returns the user using the given id
        /// if user does not exist, returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationUser FindUserByID(string id)
        {
            //var findResult = idBackend.UserManager.FindById(id);

            //return findResult;
            return null;
        }

        public StudentModel GetStudentById(string id)
        {
            var studentBackend = StudentBackend.Instance;

            var studentResult = studentBackend.Read(id);

            if (studentResult == null)
            {
                return null;
            }

            return studentResult;
        }

        /// <summary>
        /// Lists all the users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllUsers()
        {
            return DataList;
        }

        /// <summary>
        /// Lists all the student users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllStudentUsers()
        {
            //var userList = ListAllUsers();
            //var studentList = new List<ApplicationUser>();

            //foreach (var user in userList)
            //{
            //    if (UserHasClaimOfValue(user.Id, "StudentUser", "True"))
            //    {
            //        studentList.Add(user);
            //    }
            //}

            //return studentList;
            return DataList;
        }

        /// <summary>
        /// Lists all the teacher user
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllTeacherUsers()
        {
            //var userList = ListAllUsers();
            //var teacherList = new List<ApplicationUser>();

            //foreach (var user in userList)
            //{
            //    if (UserHasClaimOfValue(user.Id, "TeacherUser", "True"))
            //    {
            //        teacherList.Add(user);
            //    }
            //}

            //return teacherList;
            return DataList;
        }


        /// <summary>
        /// lists all the support users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllSupportUsers()
        {
            //var userList = ListAllUsers();
            //var supportList = new List<ApplicationUser>();

            //foreach (var user in userList)
            //{
            //    if (UserHasClaimOfValue(user.Id, "SupportUser", "True"))
            //    {
            //        supportList.Add(user);
            //    }
            //}

            //return supportList;
            return DataList;
        }

        /// <summary>
        /// checks if user has the given claim type and value
        /// returns false if not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public bool UserHasClaimOfValue(string userID, string claimType, string claimValue)
        {
            var user = FindUserByID(userID);

            if (user == null)
            {
                return false;
            }

            var claims = user.Claims.ToList();

            foreach (var item in claims)
            {
                if (item.ClaimType == claimType)
                {
                    if (item.ClaimValue == claimValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the given claim type and value to the user
        /// returns null if failure to add
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="claimTypeToAdd"></param>
        /// <param name="claimValueToAdd"></param>
        /// <returns></returns>
        public ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd)
        {
            //var findResult = FindUserByID(userID);

            //if (findResult == null)
            //{
            //    return null;
            //}

            //var claimAddResult = idBackend.UserManager.AddClaim(userID, new Claim(claimTypeToAdd, claimValueToAdd));

            //if (!claimAddResult.Succeeded)
            //{
            //    return null;
            //}

            //return findResult;
            return null;
        }

        /// <summary>
        /// removes a claim from the user
        /// returns false if failure to remove
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="claimTypeToRemove"></param>
        /// <returns></returns>
        public bool RemoveClaimFromUser(string userID, string claimTypeToRemove)
        {
            //var claims = idBackend.UserManager.GetClaims(userID);

            //if (claims == null)
            //{
            //    return false;
            //}

            //var lastAccessedClaim = claims.FirstOrDefault(t => t.Type == claimTypeToRemove);

            //var resultDelete = (lastAccessedClaim == null) ? null : idBackend.UserManager.RemoveClaim(userID, lastAccessedClaim);

            //if (!resultDelete.Succeeded)
            //{
            //    return false;
            //}

            //return true;
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
            if (string.IsNullOrEmpty(user.Id))
            {
                return false;
            }

            var myData = DataList.Find(n => n.Id == user.Id);
            if (DataList.Remove(myData) == false)
            {
                return false;
            }

            // Storage Delete
            var myReturn = DataSourceBackendTable.Instance.Delete<ApplicationUser>(tableName, partitionKey, myData.Id, myData);

            return myReturn;
        }

        /// <summary>
        /// Deletes the user with the given id
        /// returns false if delete fails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = DataList.Find(n => n.Id == Id);
            if (DataList.Remove(myData) == false)
            {
                return false;
            }

            // Storage Delete
            var myReturn = DataSourceBackendTable.Instance.Delete<ApplicationUser>(tableName, partitionKey, myData.Id, myData);

            return myReturn;
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
            //var result = idBackend.SignInManager.PasswordSignIn(user.UserName, password, isPersistent: false, shouldLockout: false);

            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return true;
            //    case SignInStatus.Failure:
            //    default:
            //        return false;
            //}
            return false;
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
            //if (password == null)
            //{
            //    return false;
            //}

            //var result = idBackend.SignInManager.PasswordSignIn(userName, password, isPersistent: false, shouldLockout: false);

            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return true;
            //    case SignInStatus.Failure:
            //    default:
            //        return false;
            //}
            return false;
        }

        /// <summary>
        /// logs the currently logged in user out
        /// </summary>
        /// <returns></returns>
        public bool LogUserOut()
        {
            //idBackend.SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            //return true;
            return false;
        }

        public void Reset()
        {
            var userList = ListAllUsers();

            foreach (var item in userList)
            {
                var deleteResult = DeleteUser(item);
            }

            LoadDataSet(DataSourceDataSetEnum.Default);
        }

        private void DataSetClear()
        {
            DataList.Clear();
        }


        private void DataSetDefault()
        {
            DataSetClear();

            // Storage Load all rows
            var DataSetList = DataSourceBackendTable.Instance.LoadAll<ApplicationUser>(tableName, partitionKey);

            foreach (var item in DataSetList)
            {
                DataList.Add(item);
            }

            // If Storage is Empty, then Create.
            if (DataList.Count < 1)
            {
                CreateDataSetDefault();
            }
        }

        private void CreateDataSetDefault()
        {
            CreateNewSupportUser("su5051", "su5051", "su5051");
        }

        /// <summary>
        /// Data set for demo
        /// </summary>
        private void DataSetDemo()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Unit Test data set
        /// </summary>
        private void DataSetUnitTest()
        {
            DataSetDefault();
        }


        public void LoadDataSet(DataSourceDataSetEnum setEnum)
        {
            switch (setEnum)
            {
                case DataSourceDataSetEnum.Demo:
                    DataSetDemo();
                    break;

                case DataSourceDataSetEnum.UnitTest:
                    DataSetUnitTest();
                    break;

                case DataSourceDataSetEnum.Default:
                default:
                    DataSetDefault();
                    break;
            }
        }

    }
}