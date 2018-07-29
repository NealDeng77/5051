using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    // Used for Truck Editing Controller and View
    public class ShopTruckItemViewModel
    {
        // Current Item
        public FactoryInventoryModel Item = new FactoryInventoryModel();

        // List of Options to Pick...
        public List<FactoryInventoryModel> ItemList = new List<FactoryInventoryModel>();

        /// <summary>
        /// Build out the View used by Edit.
        /// If anything is wrong, don't init the studentID.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="InventoryId"></param>
        /// <param name="defaultUri"></param>
        public ShopTruckItemViewModel(string studentId, string inventoryId)
        {
            /* 
             * Pass in the inventory Id for the Item.
             * 
             * Retrieve the Id item
             * 
             * Put the returned Factory Item in the Item Slot
             * 
             * Get the Category
             * 
             * Get the StudentId Inventory
             * 
             * Find all the Items that match the Category
             * 
             * Add them to the Item List
             * 
             */


            if (string.IsNullOrEmpty(studentId))
            {
                return;
            }

            if (string.IsNullOrEmpty(inventoryId))
            {
                return;
            }

            var InventoryData = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.Read(inventoryId);
            if (InventoryData == null)
            {
                return;
            }

            var StudentData = Backend.DataSourceBackend.Instance.StudentBackend.Read(studentId);
            if (StudentData == null)
            {
                return;
            }

            var InventoryListData = StudentData.Inventory.Where(m => m.Category == InventoryData.Category).ToList();

            if (InventoryListData == null)
            {
                return;
            }

            // Found the Item, and Found the Inventory List for the Item
            Item = InventoryData;
            ItemList = InventoryListData;
        }
    }
}