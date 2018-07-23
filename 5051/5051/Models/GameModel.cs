using System;
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
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Display(Name = "Uri", Description = "Picture to Show")]
        [Required(ErrorMessage = "Picture is required")]
        public string Uri { get; set; }

        [Display(Name = "Name", Description = "Game Name")]
        [Required(ErrorMessage = "Game Name is required")]
        public string Name { get; set; }

        [Display(Name = "Description", Description = "Game Description")]
        [Required(ErrorMessage = "Game Description is required")]
        public string Description { get; set; }

        [Display(Name = "Level", Description = "Game Level")]
        [Required(ErrorMessage = "Game Level is required")]
        public int Level { get; set; }

        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Level = 1;
        }

        /// <summary>
        /// New Game
        /// </summary>
        public GameModel()
        {
            Initialize();
        }

        /// <summary>
        /// Make an Game from values passed in
        /// </summary>
        /// <param name="uri">The Picture path</param>
        /// <param name="name">Game Name</param>
        /// <param name="description">Game Description</param>
        public GameModel(string uri, string name, string description, int level)
        {
            Initialize();

            Uri = uri;
            Name = name;
            Description = description;
            Level = level;
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

            Uri = data.Uri;
            Name = data.Name;
            Description = data.Description;
            Level = data.Level;
        }
    }
}