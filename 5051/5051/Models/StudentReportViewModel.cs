using System;
using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// The Full set of Reports for a single Student
    /// </summary>
    public class StudentReportViewModel
    {
        /// <summary>
        /// The Student id
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// The year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// The Student record
        /// </summary>
        public StudentModel Student { get; set; }

        /// <summary>
        /// Date Start passed in for the filter for the report
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Date end passed in for the filter for the report
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// The attendance Record for each date to show on the report
        /// </summary>
        public List<AttendanceReportViewModel> AttendanceList = new List<AttendanceReportViewModel>();

        /// <summary>
        /// The Statistics for this report
        /// </summary>
        public StudentReportStatsModel Stats = new StudentReportStatsModel();
    }
}
