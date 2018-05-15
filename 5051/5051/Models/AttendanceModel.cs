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
        public string StudentId;
        public DateTime ClockIn;
        public DateTime ClockOut;
        public TimeSpan Time;
    }
}