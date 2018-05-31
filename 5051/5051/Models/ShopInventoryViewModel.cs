using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the ShopInventory Views to have the list of ShopInventorys
    /// </summary>
    public class ShopInventoryViewModel
    {
        /// <summary>
        /// The List of ShopInventorys
        /// </summary>
        public List<ShopInventoryModel> ShopInventoryList = new List<ShopInventoryModel>();
        public int ListLevel;
    }

    /// <summary>
    /// Adds a list of ShopInventory Lists per Level, making it easier to select
    /// </summary>
    public class ShopInventoryListViewModel : ShopInventoryViewModel
    {
        public List<ShopInventoryViewModel> ShopInventoryLevelList;
        public int MaxLevel;
    }

    /// <summary>
    /// Returns the selected ShopInventory and the ShopInventory List
    /// </summary>
    public class SelectedShopInventoryViewModel : ShopInventoryListViewModel
    {
        public ShopInventoryModel SelectedShopInventory;
    }

    /// <summary>
    /// Adds the Student Information to the View Model for the ShopInventorys availble for the student to select
    /// </summary>
    public class SelectedShopInventoryForStudentViewModel : SelectedShopInventoryViewModel
    {
        public StudentModel Student;
    }
}