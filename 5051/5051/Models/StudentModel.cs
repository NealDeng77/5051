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
        [Key]
        [Display(Name = "Id", Description = "Student Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Display(Name = "Name", Description = "Nick Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "AvatarId", Description = "Avatar")]
        [Required(ErrorMessage = "Avatar is required")]
        public string AvatarId { get; set; }

        [Display(Name = "Avatar Level", Description = "Level of the Avatar")]
        [Required(ErrorMessage = "Level is required")]
        public int AvatarLevel { get; set; }

        [Display(Name = "Tokens", Description = "Tokens Saved")]
        [Required(ErrorMessage = "Tokens are required")]
        public int Tokens { get; set; }

        /// <summary>
        /// The defaults for a new student
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Tokens = 0;
            AvatarLevel = 0;
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
    }

    /// <summary>
    /// For the Index View, add the Avatar URI to the Student, so it shows the student with the picture
    /// </summary>
    public class StudentDisplayViewModel:StudentModel
    {
        [Display(Name = "Avatar Uri", Description = "Avatar Picture to Show")]
        [Required(ErrorMessage = "Picture is required")]
        public string AvatarUri { get; set; }

        public StudentDisplayViewModel(StudentModel data)
        {
            Id = data.Id;
            Name = data.Name;
            Tokens = data.Tokens;
            AvatarLevel = data.AvatarLevel;
            AvatarId = data.AvatarId;
            AvatarUri = AvatarBackend.Instance.GetAvatarUri(AvatarId);
        }
    }
}