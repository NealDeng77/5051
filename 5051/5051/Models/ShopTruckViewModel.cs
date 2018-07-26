using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Backend;

namespace _5051.Models
{
    // The URI for the Images for the Truck
    public class ShopTruckViewModel
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
        public string Truck{ get; set; }

        // Make a new view model based on the current data set
        public ShopTruckViewModel(ShopTruckModel data)
        {
            Wheels = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Wheels);
            Topper = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Topper);
            Trailer = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Trailer);
            Menu = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Menu);
            Sign = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Sign);
            Truck = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Truck);
        }
    }
}