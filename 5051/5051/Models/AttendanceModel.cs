using System;
using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// Data to Track Attendance
    /// </summary>
    public class AttendanceModel
    {
        /// <summary>
        /// ID of the Student
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// When Logged in
        /// </summary>
        public DateTime In { get; set; }

        /// <summary>
        /// When Logged Out
        /// </summary>
        public DateTime Out { get; set; }

        /// <summary>
        /// Status of the transaction (in, out, hold)
        /// </summary>
        public StudentStatusEnum Status { get; set; }

        /// <summary>
        /// The emotion state
        /// </summary>
        public EmotionStatusEnum Emotion { get; set; }

        public AttendanceModel()
        {
            In = DateTime.UtcNow;
            Status = StudentStatusEnum.In;
            Emotion = EmotionStatusEnum.Neutral;
        }
    }
}