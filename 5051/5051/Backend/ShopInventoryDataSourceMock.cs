using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for ShopInventorys, to manage them
    /// </summary>
    public class ShopInventoryDataSourceMock : IShopInventoryInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile ShopInventoryDataSourceMock instance;
        private static object syncRoot = new Object();

        private ShopInventoryDataSourceMock() { }

        public static ShopInventoryDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ShopInventoryDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The ShopInventory List
        /// </summary>
        private List<ShopInventoryModel> ShopInventoryList = new List<ShopInventoryModel>();

        /// <summary>
        /// Makes a new ShopInventory
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ShopInventory Passed In</returns>
        public ShopInventoryModel Create(ShopInventoryModel data)
        {
            ShopInventoryList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public ShopInventoryModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = ShopInventoryList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public ShopInventoryModel Update(ShopInventoryModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = ShopInventoryList.Find(n => n.Id == data.Id);
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

            var myData = ShopInventoryList.Find(n => n.Id == Id);
            var myReturn = ShopInventoryList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of ShopInventorys</returns>
        public List<ShopInventoryModel> Index()
        {
            return ShopInventoryList;
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
            ShopInventoryList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();
            Create(new ShopInventoryModel("guitar.png", "Guitar", "6 strings and all",ShopInventoryCategoryEnum.Music,10 ));
            Create(new ShopInventoryModel("saxophone.png", "Saxophone", "Smooth Sounds", ShopInventoryCategoryEnum.Music,10));
            Create(new ShopInventoryModel("trumpet.png", "Trumpet", "Love the Horn", ShopInventoryCategoryEnum.Music,10));
            Create(new ShopInventoryModel("violin.png", "Violin", "Sweet Melody", ShopInventoryCategoryEnum.Music,10));

            Create(new ShopInventoryModel("radio.png", "Radio", "Play some Tunes", ShopInventoryCategoryEnum.Entertainment,10));
            Create(new ShopInventoryModel("telephone.png", "Telephone", "Talking with my buds", ShopInventoryCategoryEnum.Entertainment,10));
            Create(new ShopInventoryModel("television.png", "Television", "I love lucy again?", ShopInventoryCategoryEnum.Entertainment,10));
            Create(new ShopInventoryModel("headphone.png", "Headphone", "Just relazing", ShopInventoryCategoryEnum.Entertainment,10));
            Create(new ShopInventoryModel("moviecamera.png", "Movie Camera", "Making Movies", ShopInventoryCategoryEnum.Entertainment,10));
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