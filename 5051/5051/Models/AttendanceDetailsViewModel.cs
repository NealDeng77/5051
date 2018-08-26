using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    public class AttendanceDetailsViewModel
    {
        /// <summary>
        /// The attendance record for the 
        /// </summary>
        public AttendanceModel Attendance { get; set; }

        /// <summary>
        /// The Friendly name for the student, does not need to be directly associated with the actual student name
        /// </summary>
        [Display(Name = "Name", Description = "Nick Name")]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the Avatar the student is associated with, this will convert to an avatar picture
        /// </summary>
        [Display(Name = "AvatarId", Description = "Avatar")]
        public string AvatarId { get; set; }

        /// <summary>
        ///  The composite for the Avatar
        /// </summary>
        public AvatarCompositeModel AvatarComposite { get; set; }


    }
}