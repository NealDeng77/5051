using System;
using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// Track the School Calendar
    /// </summary>
    public class SchoolCalendarModel
    {
        /// <summary>
        /// The Date including year
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The number of hours and minutes in the school day
        /// </summary>
        public TimeSpan TimeMax { get; set; }

        /// <summary>
        /// The start of school time
        /// </summary>
        public TimeSpan TimeStart { get; set; }

        /// <summary>
        /// The End of school time  (full day, or part time)
        /// </summary>
        public TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// School day Starts normal, early, or late (snow day)
        /// </summary>
        public SchoolCalendarDismissalEnum DayStart { get; set; }

        /// <summary>
        /// School day Ends normal, early (wed early dismissal), or late
        /// </summary>
        public SchoolCalendarDismissalEnum DayEnd { get; set; }
    }
}