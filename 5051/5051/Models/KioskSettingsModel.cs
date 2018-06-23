using System;
using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// Setting model for the kiosk password for login
    /// The Amin should be allowed to change these
    /// Save as defaults to the Database
    /// </summary>
    public class KioskSettingsModel
    {
        // Used to access the settings instance, even if there is just one
        public string Id { get; set; }

        // The Normal Start time.  8:55am
        [Display(Name = "Password", Description = "Kiosk Password")]
        public string Password { get; set; }

        public KioskSettingsModel()
        {
            Initialize();
        }

        /// <summary>
        /// Create a copy of the item, used for updates.
        /// </summary>
        /// <param name="data"></param>
        public KioskSettingsModel(KioskSettingsModel data)
        {
            if (data == null)
            {
                return;
            }

            Password = data.Password;
        }

        /// <summary>
        /// Create the default values
        /// </summary>
        private void Initialize()
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
            Password = "123";
        }

        /// <summary>
        /// Used to Update Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(KioskSettingsModel data)
        {
            if (data == null)
            {
                return;
            }

            Password = data.Password;
        }
    }
}
