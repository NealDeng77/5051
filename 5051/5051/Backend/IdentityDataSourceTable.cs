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

        public enum IdentityRole
        {
            // Not specified
            Student = 0,

            // Mock Dataset
            Teacher = 1,

            // SQL Dataset
            Support = 2
        }

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
                        instance.DataSetDefault();
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

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = "SupportUser",
                ClaimValue = "True"
            });
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = "TeacherUser",
                ClaimValue = "True"
            });

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

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = "TeacherUser",
                ClaimValue = "True"
            });

            DataList.Add(user);

            //add to storage
            var myResult = DataSourceBackendTable.Instance.Create<ApplicationUser>(tableName, partitionKey, user.Id, user);

            return myResult;
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
        public StudentModel CreateNewStudent(StudentModel student)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = student.Name, Email = student.Name + "@seattleu.edu", Id = student.Id };

            var createResult = StudentBackend.Instance.Create(student);

            if (createResult == null)
            {
                return null;
            }

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = "StudentUser",
                ClaimValue = "True"
            });

            DataList.Add(user);

            //add to storage
            var myResult = DataSourceBackendTable.Instance.Create<ApplicationUser>(tableName, partitionKey, user.Id, user);

            return student;
        }

        public StudentModel CreateNewStudentIdRecordOnly(StudentModel student)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = student.Name, Email = student.Name + "@seattleu.edu", Id = student.Id };

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = "StudentUser",
                ClaimValue = "True"
            });

            DataList.Add(user);

            //add to storage
            var myResult = DataSourceBackendTable.Instance.Create<ApplicationUser>(tableName, partitionKey, user.Id, user);

            return student;
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

            var findResult = FindUserByID(student.Id);

            if (findResult == null)
            {
                return false;
            }

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
            foreach (var item in DataList)
            {
                if (item.UserName == userName)
                {
                    return item;
                }
            }

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
            foreach (var item in DataList)
            {
                if(item.Id == id)
                {
                    return item;
                }
            }

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
            var myReturn = new List<ApplicationUser>() { };

            foreach (var user in DataList)
            {
                if(UserHasClaimOfValue(user.Id, "StudentUser", "True"))
                {
                    myReturn.Add(user);
                }
            }

            return myReturn;
        }

        /// <summary>
        /// Lists all the teacher user
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllTeacherUsers()
        {
            var myReturn = new List<ApplicationUser>() { };

            foreach (var user in DataList)
            {
                if (UserHasClaimOfValue(user.Id, "TeacherUser", "True"))
                {
                    myReturn.Add(user);
                }
            }

            return myReturn;
        }


        /// <summary>
        /// lists all the support users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllSupportUsers()
        {
            var myReturn = new List<ApplicationUser>() { };

            foreach (var user in DataList)
            {
                if (UserHasClaimOfValue(user.Id, "SupportUser", "True"))
                {
                    myReturn.Add(user);
                }
            }

            return myReturn;
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
            var findResult = FindUserByID(userID);
            if (findResult == null)
            {
                return false;
            }

            var claims = findResult.Claims.ToList();

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
        /// Adds the given claim type and value to the user
        /// returns null if failure to add
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="claimTypeToAdd"></param>
        /// <param name="claimValueToAdd"></param>
        /// <returns></returns>
        public ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd)
        {
            var findResult = FindUserByID(userID);

            if(findResult == null)
            {
                return null;
            }

            findResult.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = claimTypeToAdd,
                ClaimValue =claimValueToAdd
            });


            //storage update
            var updateResult = DataSourceBackendTable.Instance.Update(tableName, partitionKey, userID, findResult);

            return findResult;
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
            var findResult = FindUserByID(userID);
            if(findResult == null)
            {
                return false;
            }

            var claims = findResult.Claims.ToList();

            if (claims == null)
            {
                return false;
            }

            var lastAccessedClaim = claims.FirstOrDefault(t => t.ClaimType == claimTypeToRemove);

            var resultDelete = findResult.Claims.Remove(lastAccessedClaim);

            if (!resultDelete)
            {
                return false;
            }

            var updateResult = DataSourceBackendTable.Instance.Update(tableName, partitionKey, userID, findResult);

            return true;
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
            if(UserHasClaimOfValue(myData.Id, "StudentUser", "True"))
            {
                //delete the student from student table as well
                var deleteResult = StudentBackend.Instance.Delete(myData.Id);
            }

            if (DataList.Remove(myData) == false)
            {
                return false;
            }

            // Storage Delete
            var myReturn = DataSourceBackendTable.Instance.Delete<ApplicationUser>(tableName, partitionKey, myData.Id, myData);

            return myReturn;
        }

        /// <summary>
        /// Logs the user in using the given password
        /// returns false if login fails
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogUserIn(string userName, string password, IdentityRole role)
        {
            if(userName == null || password == null)
            {
                return false;
            }

            foreach (var item in DataList)
            {
                //check if user is in DataList
                if (item.UserName != userName)
                {
                    continue;
                }

                //check that role is correct
                if (role == IdentityRole.Support)
                {
                    if (!UserHasClaimOfValue(item.Id, "SupportUser", "True"))
                    {
                        return false;
                    }
                }

                if (role == IdentityRole.Teacher)
                {
                    if (!UserHasClaimOfValue(item.Id, "TeacherUser", "True"))
                    {
                        return false;
                    }
                }

                //check that password is correct
                if (item.UserName == password)
                {
                    return true;
                }
            }

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
            DataSetClear();

            var DataSetList = DataSourceBackendTable.Instance.LoadAll<ApplicationUser>(tableName, partitionKey);

            foreach (var item in DataSetList)
            {
                var deleteResult = DataSourceBackendTable.Instance.Delete(tableName, partitionKey, item.Id, item);
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
            //create support user
            var supportResult = CreateNewSupportUser("su5051", "su5051", "su5051ID");

            //create teacher user
            var teacherResult = CreateNewTeacher("teacher", "teacher", "teacherID");

            //create the student users
            var studentBackend = StudentBackend.Instance;

            studentBackend.Reset();

            var studentList = studentBackend.Index();

            foreach (var item in studentList)
            {
                var studentResult = CreateNewStudentIdRecordOnly(item);
            }

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