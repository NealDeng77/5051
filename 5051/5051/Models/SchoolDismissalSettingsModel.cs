using System;

namespace _5051.Models
{
    /// <summary>
    /// Setting model to track the school Times for Start and End
    /// The Amin should be allowed to change these
    /// Save as defaults to the Database
    /// </summary>
    public class SchoolDismissalSettingsModel
    {
        // The Normal Start time.  8:55am
        public TimeSpan StartNormal { get; set; }

        // The Early Start time.  8:30am
        public TimeSpan StartEarly { get; set; }

        // The Late Start Time 10:55am (snow start)
        public TimeSpan StartLate { get; set; }

        // The Normal end time 3:45am
        public TimeSpan EndNormal { get; set; }

        // The Early dismissal (wed 2pm)
        public TimeSpan EndEarly { get; set; }

        // The Late Dismissal 4pm
        public TimeSpan EndLate { get; set; }

        public SchoolDismissalSettingsModel()
        {
            StartNormal = TimeSpan.Parse("8:55");
            StartEarly = TimeSpan.Parse("8:00");
            StartLate = TimeSpan.Parse("10:55");

            EndNormal = TimeSpan.Parse("15:45");
            EndEarly = TimeSpan.Parse("14:00");
            EndLate = TimeSpan.Parse("16:00");
        }
    }
}