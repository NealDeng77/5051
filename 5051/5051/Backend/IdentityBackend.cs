using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;
using System.Web.Mvc;

namespace _5051.Backend
{
    public class IdentityBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile IdentityBackend instance;
        private static object syncRoot = new Object();

        private IdentityBackend() { }

        public static IdentityBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new IdentityBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IIdentityInterface DataSource;


        /// <summary>
        /// Sets the Datasource to be Mock or SQL
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            switch (dataSourceEnum)
            {
                case DataSourceEnum.SQL:
                    break;

                case DataSourceEnum.Local:
                case DataSourceEnum.ServerLive:
                case DataSourceEnum.ServerTest:
                    DataSourceBackendTable.Instance.SetDataSourceServerMode(dataSourceEnum);
                    DataSource = IdentityDataSourceTable.Instance;
                    break;

                case DataSourceEnum.Mock:
                default:
                    // Default is to use the Mock
                    DataSource = IdentityDataSourceMockV2.Instance;
                    break;
            }

        }


        public ApplicationUser CreateNewSupportUser(string userName, string password, string id)
        {
            var result = DataSource.CreateNewSupportUser(userName, password, id);
            return result;
        }

        public StudentModel CreateNewStudentUser(StudentModel student)
        {
            var myReturn = DataSource.CreateNewStudent(student);
            return myReturn;
        }

        public StudentModel CreateNewStudentUserIdRecordOnly(StudentModel student)
        {
            var myReturn = DataSource.CreateNewStudentIdRecordOnly(student);
            return myReturn;
        }

        public List<ApplicationUser> ListAllStudentUsers()
        {
            var result = DataSource.ListAllStudentUsers();
            return result;
        }

        public List<ApplicationUser> ListAllTeacherUsers()
        {
            return DataSource.ListAllTeacherUsers();
        }

        public List<ApplicationUser> ListAllSupportUsers()
        {
            return DataSource.ListAllSupportUsers();
        }

        public List<ApplicationUser> ListAllUsers()
        {
            return DataSource.ListAllUsers();
        }

        public ApplicationUser FindUserByID(string id)
        {
            var findResult = DataSource.FindUserByID(id);
            return findResult;
        }

        public bool UserHasClaimOfValue(string userID, string claimType, string claimValue)
        {
            var myReturn = DataSource.UserHasClaimOfValue(userID, claimType, claimValue);
            return myReturn;
        }

        public bool LogUserIn(string userName, string password, IdentityDataSourceTable.IdentityRole role)
        {
            var myReturn = DataSource.LogUserIn(userName, password, role);
            return myReturn;
        }

        public bool ChangeUserName(string userId, string newName)
        {

            return DataSource.ChangeUserName(userId, newName);
        }

        public void Reset()
        {
            DataSource.Reset();
        }
    }
}