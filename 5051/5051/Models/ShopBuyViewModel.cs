using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    // What Stdents can purchage
    public class ShopBuyViewModel
    {
        /// <summary>
        /// The Student ID that is doing the purchage
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// The ItemID to purchage
        /// </summary>
        public string ItemId { get; set; }
    }
}