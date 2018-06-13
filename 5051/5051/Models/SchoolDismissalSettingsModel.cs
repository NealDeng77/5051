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
        // Used to access the settings instance, even if there is just one
        public string Id { get; set; }

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
        }

        /// <summary>
        /// Create a copy of the item, used for updates.
        /// </summary>
        /// <param name="data"></param>
        public SchoolDismissalSettingsModel(SchoolDismissalSettingsModel data)
        {
            if (data == null)
            {
                return;
            }

            StartNormal = data.StartNormal;
            StartEarly = data.StartEarly;
            StartLate = data.StartLate;
            EndNormal = data.EndNormal;
            EndEarly = data.EndEarly;
            EndLate = data.EndLate;
        }


        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            SetDefault();
        }

        /// <summary>
        /// Sets the default values for the Item
        /// Because it is set here, there is no need to set defaults over in the Mock, call this instead
        /// </summary>
        public void SetDefault()
        {
            Id = Guid.NewGuid().ToString();
            StartNormal = TimeSpan.Parse("8:55");
            StartEarly = TimeSpan.Parse("8:00");
            StartLate = TimeSpan.Parse("10:55");

            EndNormal = TimeSpan.Parse("15:45");
            EndEarly = TimeSpan.Parse("14:00");
            EndLate = TimeSpan.Parse("16:00");
        }

        /// <summary>
        /// Used to Update Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(SchoolDismissalSettingsModel data)
        {
            if (data == null)
            {
                return;
            }

            StartNormal = data.StartNormal;
            StartEarly = data.StartEarly;
            StartLate = data.StartLate;
            EndNormal = data.EndNormal;
            EndEarly = data.EndEarly;
            EndLate = data.EndLate;
        }
    }
}