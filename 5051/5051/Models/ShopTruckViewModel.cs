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
        // Positions, with current item.
        public ShopTruckItemViewModel TruckItem;
        public ShopTruckItemViewModel WheelsItem;
        public ShopTruckItemViewModel TopperItem;
        public ShopTruckItemViewModel TrailerItem;
        public ShopTruckItemViewModel MenuItem;
        public ShopTruckItemViewModel SignItem;

        // The StudentID for this truck, used to simplify the models
        public string StudentId { get; set; }

        // Make a new view model based on the current data set
        public ShopTruckViewModel(ShopTruckModel data)
        {
            StudentId = data.StudentId;

            // Load the data set for each type
            TruckItem = new ShopTruckItemViewModel(StudentId, data.Truck);
            WheelsItem = new ShopTruckItemViewModel(StudentId, data.Wheels);
            TopperItem = new ShopTruckItemViewModel(StudentId, data.Topper);
            TrailerItem = new ShopTruckItemViewModel(StudentId, data.Trailer);
            MenuItem = new ShopTruckItemViewModel(StudentId, data.Menu);
            SignItem = new ShopTruckItemViewModel(StudentId, data.Sign);
        }
    }
}