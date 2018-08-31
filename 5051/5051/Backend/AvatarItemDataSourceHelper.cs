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
    public class AvatarItemDataSourceHelper
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile AvatarItemDataSourceHelper instance;
        private static object syncRoot = new Object();

        private AvatarItemDataSourceHelper() { }

        public static AvatarItemDataSourceHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AvatarItemDataSourceHelper();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The AvatarItem List
        /// </summary>
        private List<AvatarItemModel> DataList = new List<AvatarItemModel>();

        /// <summary>
        /// Clear the Data List, and build up a new one
        /// </summary>
        /// <returns></returns>
        public List<AvatarItemModel> GetDefaultDataSet()
        {
            DataList.Clear();

            DataList.Add(new AvatarItemModel("Accessory0.png", "None", "None", AvatarItemCategoryEnum.Accessory, 1, 10, false));
            DataList.Add(new AvatarItemModel("Accessory1.png", "Glasses", "Horn-Rimmed", AvatarItemCategoryEnum.Accessory, 5, 10, false));
            DataList.Add(new AvatarItemModel("Accessory2.png", "Glasses", "Round", AvatarItemCategoryEnum.Accessory, 10, 10, false));
            DataList.Add(new AvatarItemModel("Accessory3.png", "Glasses", "Rectangle", AvatarItemCategoryEnum.Accessory, 20, 10, false));
            DataList.Add(new AvatarItemModel("Accessory4.png", "Earrings", "Stud", AvatarItemCategoryEnum.Accessory, 40, 10, false));
            DataList.Add(new AvatarItemModel("Accessory5.png", "Earrings", "Hoop", AvatarItemCategoryEnum.Accessory, 80, 10, false));

            DataList.Add(new AvatarItemModel("Cheeks0.png", "None", "None", AvatarItemCategoryEnum.Cheeks, 1, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks1.png", "Blush", "Light", AvatarItemCategoryEnum.Cheeks, 5, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks2.png", "Blush", "Dark", AvatarItemCategoryEnum.Cheeks, 10, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks3.png", "Blush", "Darker", AvatarItemCategoryEnum.Cheeks, 20, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks4.png", "Freckles", "Dark", AvatarItemCategoryEnum.Cheeks, 40, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks5.png", "Freckles", "Light", AvatarItemCategoryEnum.Cheeks, 80, 10, false));

            // DataList.Add(new AvatarItemModel("Expression0.png", "None", "None", AvatarItemCategoryEnum.Expression, 1, 10, false));
            DataList.Add(new AvatarItemModel("Expression1.png", "Smile", "Happy", AvatarItemCategoryEnum.Expression, 1, 10, false));
            DataList.Add(new AvatarItemModel("Expression2.png", "Smile", "Lashy", AvatarItemCategoryEnum.Expression, 10, 10, false));
            DataList.Add(new AvatarItemModel("Expression3.png", "Eyes", "Femme Smile", AvatarItemCategoryEnum.Expression, 20, 10, false));
            DataList.Add(new AvatarItemModel("Expression4.png", "Eyes", "Smiley", AvatarItemCategoryEnum.Expression, 40, 10, false));
            DataList.Add(new AvatarItemModel("Expression5.png", "Meh", "Moody", AvatarItemCategoryEnum.Expression, 80, 10, false));
            DataList.Add(new AvatarItemModel("Expression6.png", "Meh", "Feeme Moody", AvatarItemCategoryEnum.Expression, 100, 10, false));

            DataList.Add(new AvatarItemModel("Head0.png", "Head", "None", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head1.png", "Head", "Light", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head2.png", "Head", "Olive", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head3.png", "Head", "Netural", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head4.png", "Head", "Dark", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head5.png", "Head", "Darker", AvatarItemCategoryEnum.Head, 1, 10, false));

            DataList.Add(new AvatarItemModel("Shirt_white.png", "Shirt", "White", AvatarItemCategoryEnum.ShirtFull, 1, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_black.png", "Shirt", "Black", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_blue.png", "Shirt", "Blue", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_green.png", "Shirt", "Green", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_orange.png", "Shirt", "Orange", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_red.png", "Shirt", "Red", AvatarItemCategoryEnum.ShirtFull, 50, 10, false));

            DataList.Add(new AvatarItemModel("Pants.png", "Pants", "Blue", AvatarItemCategoryEnum.Pants, 1, 10, false));

            #region FrontHair

            DataList.Add(new AvatarItemModel("placeholder.png", "Bangs", "Bald", AvatarItemCategoryEnum.HairFront, 1, 30, false));

            DataList.Add(new AvatarItemModel("Hair1_straight_white.png", "Bangs", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_black.png", "Bangs", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_blonde.png", "Bangs", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_brown.png", "Bangs", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_chestnut.png", "Bangs", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_red.png", "Bangs", "Straight", AvatarItemCategoryEnum.HairFront, 1, 30, false));

            DataList.Add(new AvatarItemModel("Hair1_short_white.png", "Bangs", "Crew Cut", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_black.png", "Bangs", "Crew Cut", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_blonde.png", "Bangs", "Crew Cut", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_brown.png", "Bangs", "Crew Cut", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_chestnut.png", "Bangs", "Crew Cut", AvatarItemCategoryEnum.HairFront, 20, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_red.png", "Bangs", "Crew Cut", AvatarItemCategoryEnum.HairFront, 20, 20, false));

            DataList.Add(new AvatarItemModel("Hair1_hairline_black.png", "Bangs", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_blonde.png", "Bangs", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_brown.png", "Bangs", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_chestnut.png", "Bangs", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_red.png", "Bangs", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_white.png", "Bangs", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));

            DataList.Add(new AvatarItemModel("Hair1_loose_black.png", "Bangs", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_blonde.png", "Bangs", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_brown.png", "Bangs", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_chestnut.png", "Bangs", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_red.png", "Bangs", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_white.png", "Bangs", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));

            DataList.Add(new AvatarItemModel("Hair1_swept_black.png", "Bangs", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_blonde.png", "Bangs", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_brown.png", "Bangs", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_chestnut.png", "Bangs", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_red.png", "Bangs", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_white.png", "Bangs", "Swept", AvatarItemCategoryEnum.HairFront, 30, 30, false));

            #endregion FrontHair

            #region BackHair
            DataList.Add(new AvatarItemModel("Hair0.png", "Length", "Very Short", AvatarItemCategoryEnum.HairBack, 1, 10, false));

            DataList.Add(new AvatarItemModel("Hair2_short_white.png", "Length", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_black.png", "Length", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_blonde.png", "Length", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_brown.png", "Length", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_chestnut.png", "Length", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_red.png", "Length", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));

            DataList.Add(new AvatarItemModel("Hair2_kinky_black.png", "Length", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_blonde.png", "Length", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_brown.png", "Length", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_chestnut.png", "Length", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_red.png", "Length", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_white.png", "Length", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));

            DataList.Add(new AvatarItemModel("Hair2_long_black.png", "Length", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_blonde.png", "Length", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_brown.png", "Length", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_chestnut.png", "Length", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_red.png", "Length", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_white.png", "Length", "Long", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            #endregion BackHair

            return DataList;
        }

    }
}