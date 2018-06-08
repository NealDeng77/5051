using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}