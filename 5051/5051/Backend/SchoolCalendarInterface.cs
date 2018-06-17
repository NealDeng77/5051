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
    public interface ISchoolCalendarInterface
    {
        SchoolCalendarModel Create(SchoolCalendarModel data);
        SchoolCalendarModel Read(string id);
        SchoolCalendarModel Update(SchoolCalendarModel data);
        bool Delete(string id);
        List<SchoolCalendarModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);

        SchoolCalendarModel ReadDate(DateTime date);
    }
}