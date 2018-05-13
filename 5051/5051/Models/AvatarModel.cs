using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _5051.Models
{
    public class AvatarModel
    {
        [Display(Name = "Id", Description = "Avatar Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [DisplayName("Uri")]
        [Display(Name = "Uri", Description = "Picture to Show")]
        [Required(ErrorMessage = "Picture is required")]
        public string Uri { get; set; }

        [Display(Name = "Name", Description = "Avatar Name")]
        [Required(ErrorMessage = "Avatar Name is required")]
        public string Name { get; set; }

        [Display(Name = "Description", Description = "Avatar Description")]
        [Required(ErrorMessage = "Avatar Description is required")]
        public string Description { get; set; }

        public AvatarModel()
        {

        }

        public AvatarModel(string uri, string name, string description)
        {
            Id = Guid.NewGuid().ToString();
            Uri = uri;
            Name = name;
            Description = description;
        }
    }
}