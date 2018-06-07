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
        public ShopInventoryCategoryEnum Category = ShopInventoryCategoryEnum.Unknown;
    }

    /// <summary>
    /// Adds a list of ShopInventory Lists per Category, making it easier to select
    /// </summary>
    public class ShopInventoryListViewModel
    {
        public List<ShopInventoryViewModel> ShopInventoryCategoryList = new List<ShopInventoryViewModel>();
    }

    /// <summary>
    /// Adds the Student Information to the View Model for the ShopInventorys availble for the student to select
    /// </summary>
    public class SelectedShopInventoryForStudentViewModel : ShopInventoryListViewModel
    {
        public StudentModel Student = new StudentModel();
    }
}