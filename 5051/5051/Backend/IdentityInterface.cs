using System.Collections.Generic;
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

        ApplicationUser FindUserByUserName(string userName);

        ApplicationUser FindUserByID(string id);

        StudentModel GetStudentById(string id);

        List<ApplicationUser> ListAllUsers();

        List<ApplicationUser> ListAllStudentUsers();

        List<ApplicationUser> ListAllTeacherUsers();

        List<ApplicationUser> ListAllSupportUsers();

        bool UserHasClaimOfValue(string userID, string claimType, string claimValue);

        ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd);

        bool RemoveClaimFromUser(string userID, string claimTypeToRemove);

        bool DeleteUser(string id);

        bool LogUserIn(string userName, string password, IdentityDataSourceTable.IdentityRole role);

        bool LogUserOut();

        void Reset();

        void LoadDataSet(DataSourceDataSetEnum setEnum);
    }
}