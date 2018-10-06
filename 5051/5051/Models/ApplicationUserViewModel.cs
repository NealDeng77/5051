using _5051.Backend;
using System;
using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// List of Users by Role
    /// </summary>
    public class ApplicationUserViewModel
    {
        public List<UserRoleEnum> UserRole;
        public StudentDisplayViewModel Student;

        public ApplicationUserViewModel(ApplicationUser User) 
        {
            if (User == null)
            {
                return;
            }

            UserRole = new List<UserRoleEnum>();
            foreach (UserRoleEnum RoleEnum in Enum.GetValues(typeof(UserRoleEnum)))
            {
                //if (IdentityDataSourceTable.Instance.UserHasClaimOfValue(User.Id, RoleEnum.ToString(), "True"))
                if(IdentityBackend.Instance.UserHasClaimOfType(User.Id, RoleEnum.ToString()))
                {
                    UserRole.Add(RoleEnum);
                }
            }

            var StudentData = DataSourceBackend.Instance.StudentBackend.Read(User.Id);
            Student = new StudentDisplayViewModel(StudentData);
            Student.Name = User.UserName;
            Student.Id = User.Id;
        }
    }
}