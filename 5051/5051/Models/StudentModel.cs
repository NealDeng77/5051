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


        public StudentModel()
        {
            Id = Guid.NewGuid().ToString();
            Tokens = 0;
            AvatarLevel = 0;
        }

        /// <summary>
        /// Constructor for Student.  Call this when making a new student
        /// </summary>
        /// <param name="name">The Name to call the student</param>
        /// <param name="avatarId">The avatar to use, if not specified, will call the backend to get an ID</param>
        public StudentModel(string name, string avatarId)
        {
            Name = name;

            // If no avatar ID is sent in, then go and get the first avatar ID from the backend data as the default to use.
            if (string.IsNullOrEmpty(avatarId))
            {
                avatarId = AvatarBackend.Instance.GetFirstAvatarId();
            }
            AvatarId = avatarId;
        }
    }
}