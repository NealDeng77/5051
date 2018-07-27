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

        // The StudentID for this truck, used to simplify the models
        public string StudentId { get; set; }

        // Make a new view model based on the current data set
        public ShopTruckViewModel(ShopTruckModel data)
        {
            Wheels = "Wheels0.png";
            Topper = "Topper0.png";
            Trailer = "Trailer0.png";
            Menu = "Menu0.png";
            Sign = "Sign0.png";
            Truck = "Truck0.png";

            StudentId = data.StudentId;

            if (data.Wheels != null)
            { 
                Wheels = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Wheels);
            }

            if (data.Topper != null)
            {
                Topper = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Topper);
            }

            if (data.Trailer != null)
            {
                Trailer = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Trailer);
            }

            if (data.Menu != null)
            {
                Menu = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Menu);
            }

            if (data.Sign != null)
            {
                Sign = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Sign);
            }

            if (data.Truck != null)
            {
                Truck = DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(data.Truck);
            }
        }
    }
}