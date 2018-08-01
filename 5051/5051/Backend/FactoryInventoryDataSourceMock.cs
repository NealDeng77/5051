using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for FactoryInventorys, to manage them
    /// </summary>
    public class FactoryInventoryDataSourceMock : IFactoryInventoryInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile FactoryInventoryDataSourceMock instance;
        private static object syncRoot = new Object();

        private FactoryInventoryDataSourceMock() { }

        public static FactoryInventoryDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new FactoryInventoryDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The FactoryInventory List
        /// </summary>
        private List<FactoryInventoryModel> FactoryInventoryList = new List<FactoryInventoryModel>();

        /// <summary>
        /// Makes a new FactoryInventory
        /// </summary>
        /// <param name="data"></param>
        /// <returns>FactoryInventory Passed In</returns>
        public FactoryInventoryModel Create(FactoryInventoryModel data)
        {
            FactoryInventoryList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public FactoryInventoryModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = FactoryInventoryList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public FactoryInventoryModel Update(FactoryInventoryModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = FactoryInventoryList.Find(n => n.Id == data.Id);
            if (myReturn == null)
            {
                return null;
            }

            myReturn.Update(data);

            return myReturn;
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = FactoryInventoryList.Find(n => n.Id == Id);
            var myReturn = FactoryInventoryList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of FactoryInventorys</returns>
        public List<FactoryInventoryModel> Index()
        {
            return FactoryInventoryList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            LoadDataSet(DataSourceDataSetEnum.Default);
        }

        /// <summary>
        /// Clears the Data
        /// </summary>
        private void DataSetClear()
        {
            FactoryInventoryList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();
            Create(new FactoryInventoryModel("guitar.png", "Guitar", "6 strings and all", FactoryInventoryCategoryEnum.Music, 10, 10, true ));
            Create(new FactoryInventoryModel("saxophone.png", "Saxophone", "Smooth Sounds", FactoryInventoryCategoryEnum.Music, 10, 10, true));
            Create(new FactoryInventoryModel("trumpet.png", "Trumpet", "Love the Horn", FactoryInventoryCategoryEnum.Music, 10, 10, true));
            Create(new FactoryInventoryModel("violin.png", "Violin", "Sweet Melody", FactoryInventoryCategoryEnum.Music, 10, 10, true));

            Create(new FactoryInventoryModel("radio.png", "Radio", "Play some Tunes", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));
            Create(new FactoryInventoryModel("telephone.png", "Telephone", "Talking with my buds", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));
            Create(new FactoryInventoryModel("television.png", "Television", "I love lucy again?", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));
            Create(new FactoryInventoryModel("headphone.png", "Headphone", "Just relazing", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));
            Create(new FactoryInventoryModel("moviecamera.png", "Movie Camera", "Making Movies", FactoryInventoryCategoryEnum.Entertainment, 10, 10, true));

            Create(new FactoryInventoryModel("GChocolate.png", "Chocolate Gallon", "Chocolate Ice Cream", FactoryInventoryCategoryEnum.Food, 10, 10, false));
            Create(new FactoryInventoryModel("GCoffee.png", "Coffee Gallon", "Coffee Ice Cream", FactoryInventoryCategoryEnum.Food, 10, 10, true));
            Create(new FactoryInventoryModel("GCookiedough.png", "Cookie Dough Gallon", "Cookie Dough Ice Cream", FactoryInventoryCategoryEnum.Food, 10, 10, true));
            Create(new FactoryInventoryModel("GMintChip.png", "Mint Chip Gallon", "Mint Chip Ice Cream", FactoryInventoryCategoryEnum.Food, 10, 10, true));
            Create(new FactoryInventoryModel("GSherbert.png", "Sherbert Gallon", "Sherbert", FactoryInventoryCategoryEnum.Food, 10, 10, true));
            Create(new FactoryInventoryModel("GStrawberry.png", "Strawberry Gallon", "Strawberry Ice Cream", FactoryInventoryCategoryEnum.Food, 10, 10, true));

            Create(new FactoryInventoryModel("FBarstool.png", "Barstool", "Barstool", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));
            Create(new FactoryInventoryModel("FBench.png", "Bench", "Bench", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));
            Create(new FactoryInventoryModel("FCounter.png", "Counter", "Counter", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));
            Create(new FactoryInventoryModel("FHangingfern.png", "Hanging Fern", "Hanging Fern Plant", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));
            Create(new FactoryInventoryModel("FOttomantable.png", "Ottoman Table", "Ottoman Table", FactoryInventoryCategoryEnum.Furniture, 10, 10, true));

            Create(new FactoryInventoryModel("Wheels0.png", "No Wheel", "No Wheels", FactoryInventoryCategoryEnum.Wheels, 1, 1, false));
            Create(new FactoryInventoryModel("Trailer0.png", "No Trailer", "No Trailer", FactoryInventoryCategoryEnum.Trailer, 1, 1, false));
            Create(new FactoryInventoryModel("Truck0.png", "No Truck", "No Truck", FactoryInventoryCategoryEnum.Truck, 1, 1, false));
            Create(new FactoryInventoryModel("Menu0.png", "No Menu", "No Menu", FactoryInventoryCategoryEnum.Menu, 1, 1, false));
            Create(new FactoryInventoryModel("Topper0.png", "No Topper", "No Topper", FactoryInventoryCategoryEnum.Topper, 1, 1, false));
            Create(new FactoryInventoryModel("Sign0.png", "No Sign", "No Sign", FactoryInventoryCategoryEnum.Sign, 1, 1, false));

            Create(new FactoryInventoryModel("Wheels1.png", "Slick Wheel", "Slick Wheels", FactoryInventoryCategoryEnum.Wheels, 10, 10, false));
            Create(new FactoryInventoryModel("Trailer1.png", "Slick Trailer", "Slick Trailer", FactoryInventoryCategoryEnum.Trailer, 10, 10, false));
            Create(new FactoryInventoryModel("Truck1.png", "Slick Truck", "Slick Truck", FactoryInventoryCategoryEnum.Truck, 10, 10, false));
            Create(new FactoryInventoryModel("Menu1.png", "Slick Menu", "Slick Menu", FactoryInventoryCategoryEnum.Menu, 10, 10, false));
            Create(new FactoryInventoryModel("Topper1.png", "Slick Topper", "Slick Topper", FactoryInventoryCategoryEnum.Topper, 10, 10, false));
            Create(new FactoryInventoryModel("Sign1.png", "Slick Sign", "Slick Sign", FactoryInventoryCategoryEnum.Sign, 10, 10, false));

            Create(new FactoryInventoryModel("Trailer2.png", "Taco Fire Trailer", "Tack FireTrailer", FactoryInventoryCategoryEnum.Trailer, 10, 10, false));
            Create(new FactoryInventoryModel("Truck2.png", "Taco Fire Truck", "Taco Fire Truck", FactoryInventoryCategoryEnum.Truck, 10, 10, false));
            Create(new FactoryInventoryModel("Menu2.png", "Taco Fire Menu", "Taco Fire Menu", FactoryInventoryCategoryEnum.Menu, 10, 10, false));
            Create(new FactoryInventoryModel("Topper2.png", "Taco Fire Topper", "Taco Fire Topper", FactoryInventoryCategoryEnum.Topper, 10, 10, false));
            Create(new FactoryInventoryModel("Sign2.png", "Taco Fire Sign", "Taco Fire Sign", FactoryInventoryCategoryEnum.Sign, 10, 10, false));
        }

        /// <summary>
        /// Data set for demo
        /// </summary>
        private void DataSetDemo()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Unit Test data set
        /// </summary>
        private void DataSetUnitTest()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Set which data to load
        /// </summary>
        /// <param name="setEnum"></param>
        public void LoadDataSet(DataSourceDataSetEnum setEnum)
        {
            switch (setEnum)
            {
                case DataSourceDataSetEnum.Demo:
                    DataSetDemo();
                    break;

                case DataSourceDataSetEnum.UnitTest:
                    DataSetUnitTest();
                    break;

                case DataSourceDataSetEnum.Default:
                default:
                    DataSetDefault();
                    break;
            }
        }
    }
}