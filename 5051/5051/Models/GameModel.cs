using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;

namespace _5051.Models
{
    /// <summary>
    /// Games for the system
    /// </summary>
    public class GameModel
    {
        [Display(Name = "Id", Description = "Game Id")]
        public string Id { get; set; }

        [Display(Name = "Date", Description = "Date and Time Iteration Ran")]
        public DateTime RunDate { get; set; }

        [Display(Name = "Time Iteration", Description = "How long between iterations")]
        public TimeSpan TimeIteration { get; set; }

        [Display(Name = "Refresh Rate", Description = "How often the student's game refreshes")]
        public TimeSpan RefreshRate { get; set; }

        [Display(Name = "Iteration", Description = "Iteration Number")]
        public int IterationNumber { get; set; }

        [Display(Name = "Enabled", Description = "Enable Game")]
        public bool Enabled { get; set; }

        [Display(Name = "Feed", Description = "Feed")]
        public List<String> Feed { get; set; }

        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            IterationNumber = 0;
            Enabled = true;
            Feed = new List<String> { };
            TimeIteration = TimeSpan.ParseExact("00:01:00", @"hh\:mm\:ss", CultureInfo.InvariantCulture, TimeSpanStyles.None);  // default to 1 minute
            RunDate = DateTime.UtcNow;
            RefreshRate = TimeSpan.ParseExact("00:01:00", @"hh\:mm\:ss", CultureInfo.InvariantCulture, TimeSpanStyles.None);  // default to 1 minute
        }

        /// <summary>
        /// New Game
        /// </summary>
        public GameModel()
        {
            Initialize();
        }

        /// <summary>
        /// New Game, with data passed in
        /// </summary>
        public GameModel(GameModel data)
        {
            Initialize();
            Update(data);
        }

        /// <summary>
        /// Used to Update Game Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(GameModel data)
        {
            if (data == null)
            {
                return;
            }

            Feed = data.Feed;
            IterationNumber = data.IterationNumber;
            RunDate = data.RunDate;
            Enabled = data.Enabled;
            TimeIteration = data.TimeIteration;
            RefreshRate = data.RefreshRate;
        }
    }
}