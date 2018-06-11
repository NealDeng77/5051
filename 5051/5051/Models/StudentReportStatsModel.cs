using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    public class StudentReportStatsModel
    {
        /// <summary>
        /// The single value for the total hours from school start till now attended
        /// </summary>
        public TimeSpan AccumlatedTotalHours { get; set; }

        /// <summary>
        /// The single value for the total hours from school start till now expected to attend
        /// </summary>
        public TimeSpan AccumlatedTotalHoursExpected { get; set; }


        public int DaysPresent { get; set; }
        public int DaysAbsentExcused { get; set; }
        public int DaysAbsentUnexcused { get; set; }

        public double TotalHoursAttended { get; set; }
        public double TotalHoursMissing { get; set; }

        public int DaysOnTime { get; set; }
        public int DaysLate { get; set; }
        public int DaysStayed { get; set; }
        public int DaysLeftEarly { get; set; }
        public int DaysOnTimeStayed { get; set; }
        public int DaysOnTimeLeft { get; set; }
        public int DaysLateStayed { get; set; }
        public int DaysLateLeft { get; set; }
        public int PercPresent { get; set; }
        public int PercAttendedHours { get; set; }
        public int PercExcused { get; set; }
        public int PercUnexcused { get; set; }
    }
}