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
        /// Elapsed time (delta of Out-In)
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Track if the student was present, or absent
        /// </summary>
        public AttendanceStatusEnum AttendanceStatus { get; set; }

        /// <summary>
        /// Track how the student did on the Check In and Out
        /// </summary>
        public CheckInStatusEnum CheckInStatus { get; set; }

        /// <summary>
        /// Track how the student did on the Check In and Out
        /// </summary>
        public CheckOutStatusEnum CheckOutStatus { get; set; }

        /// <summary>
        /// The emotion state
        /// </summary>
        public EmotionStatusEnum Emotion { get; set; }

        public AttendanceModel()
        {
            AttendanceStatus = AttendanceStatusEnum.Present;
            In = DateTime.UtcNow;
            Status = StudentStatusEnum.In;
            CheckInStatus = CheckInStatusEnum.ArriveOnTime;
            CheckOutStatus = CheckOutStatusEnum.Unknown;
            Emotion = EmotionStatusEnum.Neutral;
        }
    }
}