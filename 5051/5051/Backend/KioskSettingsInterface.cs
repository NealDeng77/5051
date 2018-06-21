using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{

    /// <summary>
    /// Datasource Interface for Avatars
    /// </summary>
    public interface IKioskSettingsInterface
    {
        KioskSettingsModel Create(KioskSettingsModel data);
        KioskSettingsModel Read(string id);
        KioskSettingsModel Update(KioskSettingsModel data);
        bool Delete(string id);
        List<KioskSettingsModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);
    }
}