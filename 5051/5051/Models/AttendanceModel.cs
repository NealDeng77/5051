using System;
using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// Data to Track Attendance, In and Out are in UTC
    /// </summary>
    public class AttendanceModel
    {
        /// <summary>
        /// ID of the Student
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// When Logged in, in utc
        /// </summary>
        public DateTime In { get; set; }

        /// <summary>
        /// When Logged Out, in utc
        /// </summary>
        public DateTime Out { get; set; }

        /// <summary>
        /// Status of the transaction (in, out, hold)
        /// </summary>
        //public StudentStatusEnum Status { get; set; }  //removed this property for now because I don't see it useful

        /// <summary>
        /// The emotion state
        /// </summary>
        public EmotionStatusEnum Emotion { get; set; }


        public AttendanceModel()
        {
            In = DateTime.UtcNow;
            //Status = StudentStatusEnum.In;
            Emotion = EmotionStatusEnum.Neutral;
        }
    }
}