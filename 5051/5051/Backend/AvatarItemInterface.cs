using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{

    /// <summary>
    /// Datasource Interface for AvatarItems
    /// </summary>
    public interface IAvatarItemInterface
    {
        AvatarItemModel Create(AvatarItemModel data);
        AvatarItemModel Read(string id);
        AvatarItemModel Update(AvatarItemModel data);
        bool Delete(string id);
        List<AvatarItemModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);
    }
}