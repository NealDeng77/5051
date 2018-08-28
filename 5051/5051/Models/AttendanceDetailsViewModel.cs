﻿using _5051.Backend;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> Attendance Model ID</param>
        public AttendanceDetailsViewModel(string id = null)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Initialize(id);
            }
        }

        public AttendanceDetailsViewModel Initialize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var attendanceData = StudentBackend.Instance.ReadAttendance(id);

            if (attendanceData == null)
            {
                return null;
            }

            var studentId = attendanceData.StudentId;

            var student = StudentBackend.Instance.Read(studentId);

            if (student == null)
            {
                return null;
            }

            var ret = new AttendanceDetailsViewModel
            {
                Attendance = new AttendanceModel
                {
                    StudentId = attendanceData.StudentId,
                    Id = attendanceData.Id,
                    In = UTCConversionsBackend.UtcToKioskTime(attendanceData.In),
                    Out = UTCConversionsBackend.UtcToKioskTime(attendanceData.Out),
                    Emotion = attendanceData.Emotion,
                    EmotionUri = Emotion.GetEmotionURI(attendanceData.Emotion),

                    IsNew = attendanceData.IsNew
                },
                Name = student.Name,
                AvatarId = student.AvatarId,
                AvatarComposite = student.AvatarComposite
            };

            return ret;
        }


    }
}