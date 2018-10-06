﻿using System.Collections.Generic;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{
    public interface IIdentityInterface
    {
        ApplicationUser CreateNewSupportUser(string userName, string password, string supportId);

        ApplicationUser CreateNewTeacher(string teacherName, string teacherPassword, string teacherId);

        StudentModel CreateNewStudent(StudentModel student);

        StudentModel CreateNewStudentIdRecordOnly(StudentModel student);

        //bool UpdateStudent(StudentModel student);
        bool ChangeUserName(string userId, string newName);

        bool ChangeUserPassword(string userName, string newPass, string oldPass, IdentityDataSourceTable.IdentityRole role);

        ApplicationUser FindUserByUserName(string userName);

        ApplicationUser FindUserByID(string id);

        StudentModel GetStudentById(string id);

        List<ApplicationUser> ListAllUsers();

        List<ApplicationUser> ListAllStudentUsers();

        List<ApplicationUser> ListAllTeacherUsers();

        List<ApplicationUser> ListAllSupportUsers();

        bool UserHasClaimOfType(string userID, string claimType);

        ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd);

        bool RemoveClaimFromUser(string userID, string claimTypeToRemove);

        bool DeleteUser(string id);

        bool LogUserIn(string userName, string password, IdentityDataSourceTable.IdentityRole role, HttpContextBase context);

        bool BlockAccess(string userId, string requestedId, HttpContextBase context);

        string GetCurrentStudentID(HttpContextBase context);

        bool LogUserOut(HttpContextBase context);

        bool CreateCookie(string cookieName, string cookieValue, HttpContextBase context);

        string ReadCookieValue(string cookieName, HttpContextBase context);

        bool DeleteCookie(string cookiename, HttpContextBase context);

        void Reset();

        void LoadDataSet(DataSourceDataSetEnum setEnum);
    }
}