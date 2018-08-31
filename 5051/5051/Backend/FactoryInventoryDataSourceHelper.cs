using System;
using System.Collections.Generic;

using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for AvatarItems, to manage them
    /// </summary>
    public class FactoryInventoryDataSourceHelper
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile FactoryInventoryDataSourceHelper instance;
        private static object syncRoot = new Object();

        private FactoryInventoryDataSourceHelper() { }

        public static FactoryInventoryDataSourceHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new FactoryInventoryDataSourceHelper();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The AvatarItem List
        /// </summary>
        private List<FactoryInventoryModel> DataList = new List<FactoryInventoryModel>();

        /// <summary>
        /// Clear the Data List, and build up a new one
        /// </summary>
        /// <returns></returns>
        public List<FactoryInventoryModel> GetDefaultDataSet()
        {
            DataList.Clear();

            //DataList.Add(new FactoryInventoryModel("guitar.png", "Guitar", "6 strings and all", FactoryInventoryCategoryEnum.Music, 10, 10, true ));
            //DataList.Add(new FactoryInventoryModel("saxophone.png", "Saxophone", "Smooth Sounds", FactoryInventoryCategoryEnum.Music, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("trumpet.png", "Trumpet", "Love the Horn", FactoryInventoryCategoryEnum.Music, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("violin.png", "Violin", "Sweet Melody", FactoryInventoryCategoryEnum.Music, 10, 10, true));

            //DataList.Add(new FactoryInventoryModel("radio.png", "Radio", "Play some Tunes", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("telephone.png", "Telephone", "Talking with my buds", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("television.png", "Television", "I love lucy again?", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("headphone.png", "Headphone", "Just relazing", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("moviecamera.png", "Movie Camera", "Making Movies", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));

            DataList.Add(new FactoryInventoryModel("GChocolate.png", "Chocolate Gallon", "Chocolate Ice Cream", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("GCoffee.png", "Coffee Gallon", "Coffee Ice Cream", FactoryInventoryCategoryEnum.Food, 20, 10, true));
            DataList.Add(new FactoryInventoryModel("GCookiedough.png", "Cookie Dough Gallon", "Cookie Dough Ice Cream", FactoryInventoryCategoryEnum.Food, 20, 10, true));
            DataList.Add(new FactoryInventoryModel("GMintChip.png", "Mint Chip Gallon", "Mint Chip Ice Cream", FactoryInventoryCategoryEnum.Food, 20, 10, true));
            DataList.Add(new FactoryInventoryModel("GSherbert.png", "Sherbert Gallon", "Sherbert", FactoryInventoryCategoryEnum.Food, 20, 10, true));
            DataList.Add(new FactoryInventoryModel("GStrawberry.png", "Strawberry Gallon", "Strawberry Ice Cream", FactoryInventoryCategoryEnum.Food, 10, 10, false));

            DataList.Add(new FactoryInventoryModel("Cheese.png", "Cheese", "Cheese", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Avacadoes.png", "Avacadoes", "Avacadoes", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Cilantro.png", "Cilantro", "Cilantro", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Onions.png", "Onions", "Onions", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Meat.png", "Meat", "Meat", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Lime.png", "Lime", "Lime", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Tomatoes.png", "Tomatoes", "Tomatoes", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Tortillas.png", "Tortillas", "Tortillas", FactoryInventoryCategoryEnum.Food, 10, 10, false));

            //DataList.Add(new FactoryInventoryModel("FBarstool.png", "Barstool", "Barstool", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("FBench.png", "Bench", "Bench", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("FCounter.png", "Counter", "Counter", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("FHangingfern.png", "Hanging Fern", "Hanging Fern Plant", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));
            //DataList.Add(new FactoryInventoryModel("FOttomantable.png", "Ottoman Table", "Ottoman Table", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));

            //DataList.Add(new FactoryInventoryModel("Wheels0.png", "No Wheel", "No Wheels", FactoryInventoryCategoryEnum.Wheels, 1, 1, false));
            //DataList.Add(new FactoryInventoryModel("Trailer0.png", "No Trailer", "No Trailer", FactoryInventoryCategoryEnum.Trailer, 1, 1, false));
            //DataList.Add(new FactoryInventoryModel("Truck0.png", "No Truck", "No Truck", FactoryInventoryCategoryEnum.Truck, 1, 1, false));
            DataList.Add(new FactoryInventoryModel("Menu0.png", "No Menu", "No Menu", FactoryInventoryCategoryEnum.Menu, 1, 1, false));
            DataList.Add(new FactoryInventoryModel("Topper0.png", "No Topper", "No Topper", FactoryInventoryCategoryEnum.Topper, 1, 1, false));
            DataList.Add(new FactoryInventoryModel("Sign0.png", "No Sign", "No Sign", FactoryInventoryCategoryEnum.Sign, 1, 1, false));

            DataList.Add(new FactoryInventoryModel("Wheels1.png", "Slick Wheel", "Slick Wheels", FactoryInventoryCategoryEnum.Wheels, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Trailer1.png", "Slick Trailer", "Slick Trailer", FactoryInventoryCategoryEnum.Trailer, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Truck1.png", "Slick Truck", "Slick Truck", FactoryInventoryCategoryEnum.Truck, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Menu1.png", "Slick Menu", "Slick Menu", FactoryInventoryCategoryEnum.Menu, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Topper1.png", "Slick Topper", "Slick Topper", FactoryInventoryCategoryEnum.Topper, 10, 10, false));
            DataList.Add(new FactoryInventoryModel("Sign1.png", "Slick Sign", "Slick Sign", FactoryInventoryCategoryEnum.Sign, 10, 10, false));

            DataList.Add(new FactoryInventoryModel("Trailer2.png", "Taco Fire Trailer", "Tack FireTrailer", FactoryInventoryCategoryEnum.Trailer, 15, 10, false));
            DataList.Add(new FactoryInventoryModel("Truck2.png", "Taco Fire Truck", "Taco Fire Truck", FactoryInventoryCategoryEnum.Truck, 15, 10, false));
            DataList.Add(new FactoryInventoryModel("Menu2.png", "Taco Fire Menu", "Taco Fire Menu", FactoryInventoryCategoryEnum.Menu, 15, 10, false));
            DataList.Add(new FactoryInventoryModel("Topper2.png", "Taco Fire Topper", "Taco Fire Topper", FactoryInventoryCategoryEnum.Topper, 15, 10, false));
            DataList.Add(new FactoryInventoryModel("Sign2.png", "Taco Fire Sign", "Taco Fire Sign", FactoryInventoryCategoryEnum.Sign, 15, 10, false));

            DataList.Add(new FactoryInventoryModel("Trailer3.png", "Bubble Trailer", "Bubble Trailer", FactoryInventoryCategoryEnum.Trailer, 20, 10, false));
            DataList.Add(new FactoryInventoryModel("Truck3.png", "Bubble Truck", "Bubble Truck", FactoryInventoryCategoryEnum.Truck, 20, 10, false));

            return DataList;
        }

    }
}