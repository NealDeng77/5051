using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{

    /// <summary>
    /// Datasource Interface for ShopInventorys
    /// </summary>
    public interface IShopInventoryInterface
    {
        ShopInventoryModel Create(ShopInventoryModel data);
        ShopInventoryModel Read(string id);
        ShopInventoryModel Update(ShopInventoryModel data);
        bool Delete(string id);
        List<ShopInventoryModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);
    }
}