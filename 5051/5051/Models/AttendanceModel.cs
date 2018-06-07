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
        public string StudentId { get; set; }
        public DateTime Time { get; set; }
        public StudentStatusEnum Status { get; set; }
        public TimeSpan Duration { get; set; }
    }
}