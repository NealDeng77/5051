using System;
using System.Collections.Generic;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for AvatarItems, to manage them
    /// </summary>
    public class AvatarItemDataSourceMock : IAvatarItemInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile AvatarItemDataSourceMock instance;
        private static object syncRoot = new Object();

        private AvatarItemDataSourceMock() { }

        public static AvatarItemDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AvatarItemDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The AvatarItem List
        /// </summary>
        private List<AvatarItemModel> AvatarItemList = new List<AvatarItemModel>();

        /// <summary>
        /// Makes a new AvatarItem
        /// </summary>
        /// <param name="data"></param>
        /// <returns>AvatarItem Passed In</returns>
        public AvatarItemModel Create(AvatarItemModel data)
        {
            AvatarItemList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public AvatarItemModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = AvatarItemList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public AvatarItemModel Update(AvatarItemModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = AvatarItemList.Find(n => n.Id == data.Id);
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

            var myData = AvatarItemList.Find(n => n.Id == Id);
            var myReturn = AvatarItemList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of AvatarItems</returns>
        public List<AvatarItemModel> Index()
        {
            return AvatarItemList;
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
            AvatarItemList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();

            Create(new AvatarItemModel("Accessory0.png", "None", "None", AvatarItemCategoryEnum.Accessory, 1, 10, false));
            Create(new AvatarItemModel("Accessory1.png", "Glasses", "Light Glasses", AvatarItemCategoryEnum.Accessory, 5, 10, false));
            Create(new AvatarItemModel("Accessory2.png", "Glasses", "Nice Glasses", AvatarItemCategoryEnum.Accessory, 10, 10, false));
            Create(new AvatarItemModel("Accessory3.png", "Glasses", "Samll Glasses", AvatarItemCategoryEnum.Accessory, 20, 10, false));
            Create(new AvatarItemModel("Accessory4.png", "Earrings", "Small Earrings", AvatarItemCategoryEnum.Accessory, 40, 10, false));
            Create(new AvatarItemModel("Accessory5.png", "Earrings", "Loop Earrings", AvatarItemCategoryEnum.Accessory, 80, 10, false));

            Create(new AvatarItemModel("Cheeks0.png", "None", "None", AvatarItemCategoryEnum.Cheeks, 1, 10, false));
            Create(new AvatarItemModel("Cheeks1.png", "Cheeks", "Circle", AvatarItemCategoryEnum.Cheeks, 5, 10, false));
            Create(new AvatarItemModel("Cheeks2.png", "Cheeks", "Light", AvatarItemCategoryEnum.Cheeks, 10, 10, false));
            Create(new AvatarItemModel("Cheeks3.png", "Cheeks", "Dark", AvatarItemCategoryEnum.Cheeks, 20, 10, false));
            Create(new AvatarItemModel("Cheeks4.png", "Blush", "Light", AvatarItemCategoryEnum.Cheeks, 40, 10, false));
            Create(new AvatarItemModel("Cheeks5.png", "Blush", "Dark", AvatarItemCategoryEnum.Cheeks, 80, 10, false));

           // Create(new AvatarItemModel("Expression0.png", "None", "None", AvatarItemCategoryEnum.Expression, 1, 10, false));
            Create(new AvatarItemModel("Expression1.png", "Smile", "Smile", AvatarItemCategoryEnum.Expression, 1, 10, false));
            Create(new AvatarItemModel("Expression2.png", "Smile", "Big Smile", AvatarItemCategoryEnum.Expression, 10, 10, false));
            Create(new AvatarItemModel("Expression3.png", "Eyes", "Happy Eyes", AvatarItemCategoryEnum.Expression, 20, 10, false));
            Create(new AvatarItemModel("Expression4.png", "Eyes", "Big Eyes", AvatarItemCategoryEnum.Expression, 40, 10, false));
            Create(new AvatarItemModel("Expression5.png", "Meh", "Meh", AvatarItemCategoryEnum.Expression, 80, 10, false));
            Create(new AvatarItemModel("Expression6.png", "Angry", "Angry", AvatarItemCategoryEnum.Expression, 100, 10, false));

            Create(new AvatarItemModel("Head0.png", "Head", "None", AvatarItemCategoryEnum.Head, 1, 10, false));
            Create(new AvatarItemModel("Head1.png", "Head", "Lighter", AvatarItemCategoryEnum.Head, 1, 10, false));
            Create(new AvatarItemModel("Head2.png", "Head", "Light", AvatarItemCategoryEnum.Head, 1, 10, false));
            Create(new AvatarItemModel("Head3.png", "Head", "Medium", AvatarItemCategoryEnum.Head, 1, 10, false));
            Create(new AvatarItemModel("Head4.png", "Head", "Dark", AvatarItemCategoryEnum.Head, 1, 10, false));
            Create(new AvatarItemModel("Head5.png", "Head", "Darker", AvatarItemCategoryEnum.Head, 1, 10, false));

            Create(new AvatarItemModel("Shirt_white.png", "Shirt", "White", AvatarItemCategoryEnum.ShirtFull, 1, 10, false));
            Create(new AvatarItemModel("Shirt_black.png", "Shirt", "Black", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));
            Create(new AvatarItemModel("Shirt_blue.png", "Shirt", "Blue", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));
            Create(new AvatarItemModel("Shirt_green.png", "Shirt", "Green", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));
            Create(new AvatarItemModel("Shirt_orange.png", "Shirt", "Orange", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));
            Create(new AvatarItemModel("Shirt_red.png", "Shirt", "Red", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));

            //Create(new AvatarItemModel("Shirt_white_short", "Shirt", "White", AvatarItemCategoryEnum.ShirtShort, 1, 10, false));
            //Create(new AvatarItemModel("Shirt_black_short", "Shirt", "Black", AvatarItemCategoryEnum.ShirtShort, 50, 10, false));
            //Create(new AvatarItemModel("Shirt_blue_short", "Shirt", "Blue", AvatarItemCategoryEnum.ShirtShort, 50, 10, false));
            //Create(new AvatarItemModel("Shirt_green_short", "Shirt", "Green", AvatarItemCategoryEnum.ShirtShort, 50, 10, false));
            //Create(new AvatarItemModel("Shirt_orange_short", "Shirt", "Orange", AvatarItemCategoryEnum.ShirtShort, 50, 10, false));
            //Create(new AvatarItemModel("Shirt_red_short", "Shirt", "Red", AvatarItemCategoryEnum.ShirtShort, 50, 10, false));

            Create(new AvatarItemModel("Pants.png", "Pants", "Pants", AvatarItemCategoryEnum.Pants, 1, 10, false));

            #region FrontHair
            Create(new AvatarItemModel("Hair1_straight_white.png", "Front", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            Create(new AvatarItemModel("Hair1_straight_black.png", "Front", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            Create(new AvatarItemModel("Hair1_straight_blonde.png", "Front", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            Create(new AvatarItemModel("Hair1_straight_brown.png", "Front", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            Create(new AvatarItemModel("Hair1_straight_chestnut.png", "Front", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            Create(new AvatarItemModel("Hair1_straight_red.png", "Front", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            
            Create(new AvatarItemModel("Hair1_short_white.png", "Front", "Short", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            Create(new AvatarItemModel("Hair1_short_black.png", "Front", "Short", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            Create(new AvatarItemModel("Hair1_short_blonde.png", "Front", "Short", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            Create(new AvatarItemModel("Hair1_short_brown.png", "Front", "Short", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            Create(new AvatarItemModel("Hair1_short_chestnut.png", "Front", "Short", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            Create(new AvatarItemModel("Hair1_short_red.png", "Front", "Short", AvatarItemCategoryEnum.HairFront, 20, 20, false));

            Create(new AvatarItemModel("Hair1_hairline_black.png", "Front", "Hairline", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_hairline_blonde.png", "Front", "Hairline", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_hairline_brown.png", "Front", "Hairline", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_hairline_chestnut.png", "Front", "Hairline", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_hairline_red.png", "Front", "Hairline", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_hairline_white.png", "Front", "Hairline", AvatarItemCategoryEnum.HairFront, 10, 10, false));

            Create(new AvatarItemModel("Hair1_loose_black.png", "Front", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_loose_blonde.png", "Front", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_loose_brown.png", "Front", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_loose_chestnut.png", "Front", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_loose_red.png", "Front", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            Create(new AvatarItemModel("Hair1_loose_white.png", "Front", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));

            Create(new AvatarItemModel("Hair1_swept_black.png", "Front", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            Create(new AvatarItemModel("Hair1_swept_blonde.png", "Front", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            Create(new AvatarItemModel("Hair1_swept_brown.png", "Front", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            Create(new AvatarItemModel("Hair1_swept_chestnut.png", "Front", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            Create(new AvatarItemModel("Hair1_swept_red.png", "Front", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            Create(new AvatarItemModel("Hair1_swept_white.png", "Front", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));

            #endregion FrontHair

            #region BackHair
            Create(new AvatarItemModel("Hair0.png", "Back", "Very Short", AvatarItemCategoryEnum.HairBack, 1, 10, false));

            Create(new AvatarItemModel("Hair2_short_white.png", "Back", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_short_black.png", "Back", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_short_blonde.png", "Back", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_short_brown.png", "Back", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_short_chestnut.png", "Back", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_short_red.png", "Back", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));

            Create(new AvatarItemModel("Hair2_kinky_black.png", "Back", "Kinky", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_kinky_blonde.png", "Back", "Kinky", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_kinky_brown.png", "Back", "Kinky", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_kinky_chestnut.png", "Back", "Kinky", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_kinky_red.png", "Back", "Kinky", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_kinky_white.png", "Back", "Kinky", AvatarItemCategoryEnum.HairBack, 10, 10, false));

            Create(new AvatarItemModel("Hair2_long_black.png", "Back", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_long_blonde.png", "Back", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_long_brown.png", "Back", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_long_chestnut.png", "Back", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_long_red.png", "Back", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            Create(new AvatarItemModel("Hair2_long_white.png", "Back", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            #endregion BackHair

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