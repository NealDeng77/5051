using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{
    public class IdentityDataSourceMockV2 : IIdentityInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile IdentityDataSourceMockV2 instance;
        private static object syncRoot = new Object();

        private IdentityDataSourceMockV2() { }

        public static IdentityDataSourceMockV2 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new IdentityDataSourceMockV2();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        private List<ApplicationUser> DataList = new List<ApplicationUser>();

        private string supportUserName = "su5051";
        private string supportPass = "su5051";
        private string teacherUserName = "teacher";
        private string teacherPass = "teacher";

        public ApplicationUser CreateNewSupportUser(string userName, string password, string supportId)
        {
            //fill in all fields needed
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

            return user;
        }

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

            return user;
        }

        public StudentModel CreateNewStudent(StudentModel student)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = student.Name, Email = student.Name + "@seattleu.edu", Id = student.Id };

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = "StudentUser",
                ClaimValue = "True"
            });

            //add to id datalist
            DataList.Add(user);

            //add to student list
            //DataSourceBackend.Instance.StudentBackend.Create(student);
            var createResult = StudentBackend.Instance.Create(student);
            if (createResult == null)
            {
                return null;
            }


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

            return student;
        }

        public bool ChangeUserName(string userId, string newName)
        {
            if (userId == null || newName == null)
            {
                return false;
            }
            var idFindStudent = FindUserByID(userId);
            if (idFindStudent == null)
            {
                return false;
            }

            var findStudent = GetStudentById(userId);
            if (findStudent == null)
            {
                return false;
            }

            //update for both student and id
            findStudent.Name = newName;           

            var studentUpdateResult =  StudentBackend.Instance.Update(findStudent);
            if (studentUpdateResult == null)
            {
                return false;
            }

            DataList.Remove(idFindStudent);
            idFindStudent.UserName = newName;
            DataList.Add(idFindStudent);

            return true;
        }

        public ApplicationUser FindUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            var myReturn = DataList.Find(n => n.UserName == userName);
            return myReturn;
        }

        public ApplicationUser FindUserByID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = DataList.Find(n => n.Id == id);
            return myReturn;
        }

        public StudentModel GetStudentById(string id)
        {
            var student = StudentBackend.Instance.Read(id);

            if (student == null)
            {
                return null;
            }

            return student;
        }

        public List<ApplicationUser> ListAllUsers()
        {
            return DataList;
        }

        public List<ApplicationUser> ListAllStudentUsers()
        {
            var myReturn = new List<ApplicationUser>() { };

            foreach (var user in DataList)
            {
                if (UserHasClaimOfValue(user.Id, "StudentUser", "True"))
                {
                    myReturn.Add(user);
                }
            }

            return myReturn;
        }

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

        public ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd)
        {
            var findResult = FindUserByID(userID);

            if (findResult == null)
            {
                return null;
            }

            findResult.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = claimTypeToAdd,
                ClaimValue = claimValueToAdd
            });

            return findResult;
        }

        public bool RemoveClaimFromUser(string userID, string claimTypeToRemove)
        {
            var findResult = FindUserByID(userID);
            if (findResult == null)
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

            return true;
        }

        public bool DeleteUser(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = DataList.Find(n => n.Id == Id);
            if (UserHasClaimOfValue(myData.Id, "StudentUser", "True"))
            {
                //delete the student from student table as well
                var deleteResult = StudentBackend.Instance.Delete(myData.Id);
            }

            //remove from list
            if (DataList.Remove(myData) == false)
            {
                return false;
            }

            return true;
        }

        public bool LogUserIn(string userName, string password, IdentityDataSourceTable.IdentityRole role)
        {
            if (userName == null && password == null)
            {
                return false;
            }

            var findResult = FindUserByUserName(userName);
            if (findResult == null)
            {
                return false;
            }

            //check that role is correct
            if (role == IdentityDataSourceTable.IdentityRole.Support)
            {
                if (!UserHasClaimOfValue(findResult.Id, "SupportUser", "True"))
                {
                    return false;
                }

                if (password == supportPass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (role == IdentityDataSourceTable.IdentityRole.Teacher)
            {
                if (!UserHasClaimOfValue(findResult.Id, "TeacherUser", "True"))
                {
                    return false;
                }

                if (password == teacherPass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            var student = GetStudentById(findResult.Id);
            if (student != null && student.Password == password)
            {
                return true;
            }

            return false;
        }


        public bool LogUserOut()
        {
            return false;
        }

        public void Reset()
        {
            Initialize();
        }

        public void Initialize()
        {
            LoadDataSet(DataSourceDataSetEnum.Default);
        }

        private void DataSetClear()
        {
            DataList.Clear();
        }

        private void DataSetDefault()
        {
            DataSetClear();

            //create support user
            var supportResult = CreateNewSupportUser(supportUserName, supportPass, supportUserName);

            //create teacher user
            var teacherResult = CreateNewTeacher(teacherUserName, teacherPass, teacherUserName);

            var dataSet = DataSourceBackend.Instance.StudentBackend.Index();
            foreach (var item in dataSet)
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

        /// <summary>
        /// Set which data to load
        /// </summary>
        /// <param name="setEnum"></param>
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