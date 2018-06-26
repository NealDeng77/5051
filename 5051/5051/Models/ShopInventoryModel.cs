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
    /// ShopInventorys for the system
    /// </summary>
    public class ShopInventoryModel
    {
        [Display(Name = "Id", Description = "ShopInventory Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Display(Name = "Uri", Description = "Picture to Show")]
        [Required(ErrorMessage = "Picture is required")]
        public string Uri { get; set; }

        [Display(Name = "Name", Description = "ShopInventory Name")]
        [Required(ErrorMessage = "ShopInventory Name is required")]
        public string Name { get; set; }

        [Display(Name = "Description", Description = "ShopInventory Description")]
        [Required(ErrorMessage = "ShopInventory Description is required")]
        public string Description { get; set; }

        [Display(Name = "Category", Description = "ShopInventory Category")]
        [Required(ErrorMessage = "Category is required")]
        public ShopInventoryCategoryEnum Category{ get; set; }

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
        /// New ShopInventory
        /// </summary>
        public ShopInventoryModel()
        {
            Initialize();
        }

        /// <summary>
        /// Make an ShopInventory from values passed in
        /// </summary>
        /// <param name="uri">The Picture path</param>
        /// <param name="name">ShopInventory Name</param>
        /// <param name="description">ShopInventory Description</param>
        public ShopInventoryModel(string uri, string name, string description, ShopInventoryCategoryEnum category, int tokens)
        {
            Initialize();

            Uri = uri;
            Name = name;
            Description = description;
            Category = category;
            Tokens = tokens;
        }

        /// <summary>
        /// Used to Update ShopInventory Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(ShopInventoryModel data)
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