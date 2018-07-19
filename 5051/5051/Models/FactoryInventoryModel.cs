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
    /// FactoryInventorys for the system
    /// </summary>
    public class FactoryInventoryModel
    {
        [Display(Name = "Id", Description = "FactoryInventory Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Display(Name = "Uri", Description = "Picture to Show")]
        [Required(ErrorMessage = "Picture is required")]
        public string Uri { get; set; }

        [Display(Name = "Name", Description = "FactoryInventory Name")]
        [Required(ErrorMessage = "FactoryInventory Name is required")]
        public string Name { get; set; }

        [Display(Name = "Description", Description = "FactoryInventory Description")]
        [Required(ErrorMessage = "FactoryInventory Description is required")]
        public string Description { get; set; }

        [Display(Name = "Category", Description = "FactoryInventory Category")]
        [Required(ErrorMessage = "Category is required")]
        public FactoryInventoryCategoryEnum Category { get; set; }

        [Display(Name = "Tokens", Description = "Cost in Tokens")]
        [Required(ErrorMessage = "Tokens is required")]
        public int Tokens { get; set; }


        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Tokens = 1;
        }

        /// <summary>
        /// New FactoryInventory
        /// </summary>
        public FactoryInventoryModel()
        {
            Initialize();
        }

        /// <summary>
        /// Make an FactoryInventory from values passed in
        /// </summary>
        /// <param name="uri">The Picture path</param>
        /// <param name="name">FactoryInventory Name</param>
        /// <param name="description">FactoryInventory Description</param>
        public FactoryInventoryModel(string uri, string name, string description, FactoryInventoryCategoryEnum category, int tokens)
        {
            Initialize();

            Uri = uri;
            Name = name;
            Description = description;
            Category = category;
            Tokens = tokens;
        }

        /// <summary>
        /// Used to Update FactoryInventory Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(FactoryInventoryModel data)
        {
            if (data == null)
            {
                return;
            }

            Uri = data.Uri;
            Name = data.Name;
            Description = data.Description;
            Tokens = data.Tokens;
        }
    }
}