using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;


namespace _5051.Backend
{
    public class SchoolCalendarBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SchoolCalendarBackend instance;
        private static object syncRoot = new Object();

        private SchoolCalendarBackend() { }

        public static SchoolCalendarBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SchoolCalendarBackend();
                            SetDataSource(SystemGlobals.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static ISchoolCalendarInterface DataSource;

        /// <summary>
        /// Switches between Live, and Mock Datasets
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            if (dataSourceEnum == DataSourceEnum.SQL)
            {
                // SQL not hooked up yet...
                //throw new NotImplementedException();
            }

            // Default is to use the Mock
            DataSource = SchoolCalendarDataSourceMock.Instance;
        }

        /// <summary>
        /// Switch the data set between Demo, Default and Unit Test
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            SchoolCalendarDataSourceMock.Instance.LoadDataSet(SetEnum);
        }

        /// <summary>
        /// Helper function that resets the DataSource, and rereads it.
        /// </summary>
        public void Reset()
        {
            DataSource.Reset();
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public SchoolCalendarModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = DataSource.Read(id);
            return myReturn;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public SchoolCalendarModel ReadDate(DateTime date)
        {
            // If the date is out of range, return null
            if (date.Year < DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst.Year)
            {
                return null;
            }

            var myReturn = DataSource.ReadDate(date);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public SchoolCalendarModel Update(SchoolCalendarModel data)
        {
            if (data == null)
            {
                return null;
            }

            // Set date as modified
            data.Modified = true;

            // See if the updates make it match default, if so, need to clear the modified flag...
            var myDefault = new SchoolCalendarModel(data.Date);
            myDefault.SetDefault();
            myDefault.SetSchoolDay();

            if (myDefault.TimeEnd == data.TimeEnd && myDefault.TimeStart == data.TimeStart)
            {
                // It is back to defaults, so turn off the modified flag.
                data.Modified = false;
            }

            if (myDefault.SchoolDay != data.SchoolDay)
            {
                data.Modified = true;
            }

            var myReturn = DataSource.Update(data);

            return myReturn;
        }

        /// <summary>
        /// Walks each existing date in the calendar, and resets the dates based on the defaults.
        /// </summary>
        public void ResetDefaults()
        {
            foreach (var item in instance.Index())
            {
                Update(item);
            }
        }

        /// <summary>
        /// Returns the First record
        /// </summary>
        /// <returns>Null or valid data</returns>
        public SchoolCalendarModel GetDefault()
        {
            var myReturn = DataSource.Index().Last();
            return myReturn;
        }

        /// <summary>
        /// Returns this record to default settings
        /// </summary>
        /// <returns>Null or valid data</returns>
        public SchoolCalendarModel SetDefault(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myData = DataSource.Read(id);
            if (myData == null)
            {
                return null;
            }

            myData.SetDefault();

            // Clear modified flag on date
            myData.Modified = false;

            var myReturn = DataSource.Update(myData);

            return myReturn;
        }

        /// <summary>
        /// Makes a new Student
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Student Passed In</returns>
        public SchoolCalendarModel Create(SchoolCalendarModel data)
        {
            DataSource.Create(data);
            return data;
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

            var myReturn = DataSource.Delete(Id);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Students</returns>
        public List<SchoolCalendarModel> Index()
        {
            var myData = DataSource.Index();
            return myData;
        }

        /// <summary>
        /// Returns the First Student in the system
        /// </summary>
        /// <returns>Null or valid data</returns>
        public SchoolCalendarModel GetToday()
        {
            var myReturn = DataSource.Index().LastOrDefault();
            return myReturn;
        }
    }
}