﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    // Where on the Truck Items are positions
    public class ShopTruckModel
    {
        // The Inventory ID for the Wheels
        public string Wheels { get; set; }

        // The Inventory ID for the Wheels
        public string Topper { get; set; }

        // The Inventory ID for the Wheels
        public string Trailer { get; set; }

        // The Inventory ID for the Wheels
        public string Menu { get; set; }

        // The Inventory ID for the Wheels
        public string Sign { get; set; }

        // The Inventory ID for the Wheels
        public string Truck { get; set; }

        // Customer Count
        public int CustomersTotal { get; set; }

        // Track the status of th Truck
        public bool IsClosed { get; set;}

        public List<string> TransactionList { get; set; }


        public ShopTruckModel()
        {
            // New Models, set default data

            IsClosed = false; // Trucks Open to start with
            TransactionList = new List<string>();   // Empty List of Transactions
            CustomersTotal = 0; // No customers to start off with

            Truck = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck).Id;

            Wheels = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Wheels).Id;

            Topper = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Topper).Id;

            Trailer = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Trailer).Id;

            Sign = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Sign).Id;

            Menu = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Menu).Id;
        }
    }
}