using System;
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

        public ShopTruckModel()
        {
            // New Models, set default data

            Truck = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck).Id;

            Wheels = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Wheels).Id;

            Topper = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Topper).Id;

            Trailer = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Trailer).Id;

            Sign = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Sign).Id;

            Menu = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Menu).Id;
        }
    }
}