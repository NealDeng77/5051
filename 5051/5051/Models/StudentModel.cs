using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        [Display(Name = "Tokens", Description = "Tokens Saved")]
        [Required(ErrorMessage = "Tokens are required")]
        public int Tokens { get; set; }


        public StudentModel()
        {
            Id = Guid.NewGuid().ToString();
            Tokens = 0;
        }

        public StudentModel(string name, string avatarId)
        {
            Name = name;

            if (string.IsNullOrEmpty(avatarId))
            {
                avatarId = ""
            }
            AvatarId = avatarId;
        }
    }
}