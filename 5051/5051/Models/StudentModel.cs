using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using _5051.Backend;

namespace _5051.Models
{
    /// <summary>
    /// The Student, this holds the student id, name, tokens.  Other things about the student such as their attendance is pulled from the attendance data, or the owned items, from the inventory data
    /// </summary>
    public class StudentModel
    {
        /// <summary>
        /// The ID for the Student, this is the key, and a required field
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Student Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        /// <summary>
        /// The Friendly name for the student, does not need to be directly associated with the actual student name
        /// </summary>
        [Display(Name = "Name", Description = "Nick Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the Avatar the student is associated with, this will convert to an avatar picture
        /// </summary>
        [Display(Name = "AvatarId", Description = "Avatar")]
        [Required(ErrorMessage = "Avatar is required")]
        public string AvatarId { get; set; }

        /// <summary>
        /// The personal level for the Avatar, the avatar levels up.  switching the avatar ID (picture), does not change the level
        /// </summary>
        [Display(Name = "Avatar Level", Description = "Level of the Avatar")]
        [Required(ErrorMessage = "Level is required")]
        public int AvatarLevel { get; set; }

        /// <summary>
        /// The number of Tokens the student has, tokens are used in the store, and also to level up
        /// </summary>
        [Display(Name = "XP", Description = "Experience Points Earned")]
        [Required(ErrorMessage = "XP is required")]
        public int ExperiencePoints { get; set; }

        /// <summary>
        /// The number of Tokens the student has, tokens are used in the store, and also to level up
        /// </summary>
        [Display(Name = "Tokens", Description = "Tokens Saved")]
        [Required(ErrorMessage = "Tokens are required")]
        public int Tokens { get; set; }

        /// <summary>
        /// The status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Current Status", Description = "Status of the Student")]
        [Required(ErrorMessage = "Status is required")]
        public StudentStatusEnum Status { get; set; }

        /// <summary>
        /// The status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Password", Description = "Student Password")]
        [PasswordPropertyText]
        public string Password { get; set; }

        /// <summary>
        /// The current emotion status of the student
        /// </summary>
        public EmotionStatusEnum EmotionCurrent { get; set; }

        /// <summary>
        /// The inventory list for the student
        /// </summary>
        public List<FactoryInventoryModel> Inventory { get; set; }

        /// <summary>
        /// The Attendance list for the student
        /// </summary>
        public List<AttendanceModel> Attendance { get; set; }

        /// <summary>
        /// The defaults for a new student
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Tokens = 10;
            AvatarLevel = 1;
            Status = StudentStatusEnum.Out;
            ExperiencePoints = 0;
            Password = string.Empty;
            Inventory = new List<FactoryInventoryModel>();
            Attendance = new List<AttendanceModel>();
            EmotionCurrent = EmotionStatusEnum.Neutral;
        }

        /// <summary>
        /// Constructor for a student
        /// </summary>
        public StudentModel()
        {
            Initialize();
        }

        /// <summary>
        /// Constructor for Student.  Call this when making a new student
        /// </summary>
        /// <param name="name">The Name to call the student</param>
        /// <param name="avatarId">The avatar to use, if not specified, will call the backend to get an ID</param>
        public StudentModel(string name, string avatarId)
        {
            Initialize();

            Name = name;

            // If no avatar ID is sent in, then go and get the first avatar ID from the backend data as the default to use.
            if (string.IsNullOrEmpty(avatarId))
            {
                avatarId = AvatarBackend.Instance.GetFirstAvatarId();
            }
            AvatarId = avatarId;
        }

        /// <summary>
        /// Convert a Student Display View Model, to a Student Model, used for when passed data from Views that use the full Student Display View Model.
        /// </summary>
        /// <param name="data">The student data to pull</param>
        public StudentModel(StudentDisplayViewModel data)
        {
            Id = data.Id;
            Name = data.Name;

            AvatarId = data.AvatarId;
            AvatarLevel = data.AvatarLevel;
            Tokens = data.Tokens;
            Status = data.Status;
            ExperiencePoints = data.ExperiencePoints;
            Password = data.Password;
            Inventory = data.Inventory;
            Attendance = data.Attendance;
            EmotionCurrent = data.EmotionCurrent;
        }

        /// <summary>
        /// Update the Data Fields with the values passed in, do not update the ID.
        /// </summary>
        /// <param name="data">The values to update</param>
        /// <returns>False if null, else true</returns>
        public bool Update(StudentModel data)
        {
            if (data == null)
            {
                return false;
            }

            Name = data.Name;
            AvatarId = data.AvatarId;
            AvatarLevel = data.AvatarLevel;
            Tokens = data.Tokens;
            Status = data.Status;
            ExperiencePoints = data.ExperiencePoints;
            Password = data.Password;
            Inventory = data.Inventory;
            Attendance = data.Attendance;
            EmotionCurrent = data.EmotionCurrent;

            return true;
        }
    }
}