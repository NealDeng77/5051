﻿using System;
using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// The base report view model, all types of report view models(weekly, monthly, semester, school year) inherit this class
    /// Contains info about who the student is, start date and end date, a list of view models of individual day's attendance, and overall stats of the report.
    /// </summary>
    public class BaseReportViewModel
    {
        /// <summary>
        /// The Student id
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// The Student record
        /// </summary>
        public StudentModel Student { get; set; }

        /// <summary>
        /// Date Start 
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Date end 
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

        /// <summary>
        /// The goal percentage for total attended time
        /// </summary>
        public int Goal { get; set; }

    }
}
