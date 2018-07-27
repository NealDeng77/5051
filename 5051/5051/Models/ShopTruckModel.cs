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

        // The StudentID for this truck, used to simplify the models
        public string StudentId { get; set; }

    }
}