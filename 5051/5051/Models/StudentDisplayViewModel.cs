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
    /// For the Index View, add the Avatar URI to the Student, so it shows the student with the picture
    /// </summary>
    public class StudentDisplayViewModel : StudentModel
    {
        /// <summary>
        /// The path to the local image for the avatar
        /// </summary>
        [Display(Name = "Avatar Picture", Description = "Avatar Picture to Show")]
        public string AvatarUri { get; set; }

        /// <summary>
        /// Display name for the Avatar on the student information (Friendly Police etc.)
        /// </summary>
        [Display(Name = "Avatar Name", Description = "Avatar Name")]
        public string AvatarName { get; set; }

        /// <summary>
        /// Description of the Avatar to show on the student information
        /// </summary>
        [Display(Name = "Avatar Description", Description = "Avatar Description")]
        public string AvatarDescription { get; set; }

        /// <summary>
        /// DateTime of last transaction recorded, used for login and logout
        /// </summary>
        [Display(Name = "Date", Description = "Date and Time")]
        public DateTime LastDateTime { get; set; }

        /// <summary>
        /// DateTime of last transaction recorded, used for login and logout
        /// </summary>
        [Display(Name = "Last Login", Description = "Last Login")]
        public DateTime LastLogIn { get; set; }

        /// <summary>
        /// DateTime of last transaction recorded, used for login and logout
        /// </summary>
        [Display(Name = "Emotion Image URI", Description = "Emotion Image URI")]
        public string EmotionImgUri { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public StudentDisplayViewModel() { }

        /// <summary>
        /// Creates a Student Display View Model from a Student Model
        /// </summary>
        /// <param name="data">The Student Model to create</param>
        public StudentDisplayViewModel(StudentModel data)
        {
            if (data == null)
            {
                // Nothing to convert
                return;
            }

            Id = data.Id;
            Name = data.Name;
            Tokens = data.Tokens;
            AvatarLevel = data.AvatarLevel;
            AvatarId = data.AvatarId;
            Status = data.Status;
            ExperiencePoints = data.ExperiencePoints;
            Password = data.Password;
            Inventory = data.Inventory;
            Attendance = data.Attendance;
            EmotionCurrent = data.EmotionCurrent;

            var myDataAvatar = AvatarBackend.Instance.Read(AvatarId);
            if (myDataAvatar == null)
            {
                // Nothing to convert
                return;
            }

            AvatarName = myDataAvatar.Name;
            AvatarDescription = myDataAvatar.Description;
            AvatarUri = myDataAvatar.Uri;
        }
    }
}