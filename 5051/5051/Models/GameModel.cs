﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        [Display(Name = "Iteration", Description = "Iteration Number")]
        public int IterationNumber { get; set; }

        [Display(Name = "Enabled", Description = "Enable Game")]
        public bool Enabled { get; set; }


        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            IterationNumber = 0;
            Enabled = true;
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

            IterationNumber = data.IterationNumber;
            RunDate = data.RunDate;
            Enabled = data.Enabled;
        }
    }
}