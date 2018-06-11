using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    public class AttendanceReportViewModel
    {
        /// <summary>
        /// The Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Total Hours attended from the start of school till now
        /// </summary>
        public TimeSpan TotalHours { get; set; }

        /// <summary>
        /// Total hours Expected to attend at this point in time
        /// </summary>
        public TimeSpan TotalHoursExpected { get; set; }

        /// <summary>
        /// Hours attended Today
        /// </summary>
        public TimeSpan HoursAttended { get; set; }

        /// <summary>
        /// Hours Expected to Attend Today
        /// </summary>
        public TimeSpan HoursExpected { get; set; }

    }
}