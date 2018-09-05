using System;
using System.Collections.Generic;
using System.Linq;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for SchoolCalendars, to manage them
    /// </summary>
    public class SchoolCalendarDataSourceTable : ISchoolCalendarInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SchoolCalendarDataSourceTable instance;
        private static object syncRoot = new Object();

        private SchoolCalendarDataSourceTable() { }

        public static SchoolCalendarDataSourceTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SchoolCalendarDataSourceTable();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The SchoolCalendar List
        /// </summary>
        private List<SchoolCalendarModel> DataList = new List<SchoolCalendarModel>();

        private const string ClassName = "SchoolCalendarModel";
        /// <summary>
        /// Table Name used for data storage
        /// </summary>
        private string tableName = ClassName.ToLower();

        /// <summary>
        /// Partition Key used for data storage
        /// </summary>
        private string partitionKey = ClassName.ToLower();

        /// <summary>
        /// Makes a new SchoolCalendar
        /// </summary>
        /// <param name="data"></param>
        /// <returns>SchoolCalendar Passed In</returns>
        public SchoolCalendarModel Create(SchoolCalendarModel data)
        {
            DataList.Add(data);

            // Add to Storage
            var myResult = DataSourceBackendTable.Instance.Create<SchoolCalendarModel>(tableName, partitionKey, data.Id, data);

            //sort by date
            DataList = DataList.OrderBy(x => x.Date).ToList();

            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// Does not access storage, just reads from memeory
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public SchoolCalendarModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = DataList.Find(n => n.Id == id);
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
            var myReturn = DataList.Find(n => n.Id == data.Id);
            if (myReturn == null)
            {
                return null;
            }

            myReturn.Update(data);

            // Update Storage
            var myResult = DataSourceBackendTable.Instance.Create<SchoolCalendarModel>(tableName, partitionKey, data.Id, data);

            //sort by date
            DataList = DataList.OrderBy(x => x.Date).ToList();

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

            var myData = DataList.Find(n => n.Id == Id);
            if (DataList.Remove(myData) == false)
            {
                return false;
            }

            // Storage Delete
            var myReturn = DataSourceBackendTable.Instance.Delete<SchoolCalendarModel>(tableName, partitionKey, myData.Id, myData);

            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of SchoolCalendars</returns>
        public List<SchoolCalendarModel> Index()
        {
            return DataList;
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
            DataList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();
            CreateDataSetDefaultData();
        }

        /// <summary>
        /// Load the data from the server, and then default data if needed.
        /// </summary>
        public void CreateDataSetDefaultData() { 
            
            // Storage Load all rows
            var DataSetList = DataSourceBackendTable.Instance.LoadAll<SchoolCalendarModel>(tableName, partitionKey);

            foreach (var item in DataSetList)
            {
                DataList.Add(item);
            }

            // If Storage is Empty, then Create.
            if (DataList.Count < 1)
            {
                CreateDataSetDefault();
            }
        }

        /// <summary>
        /// Get the Default data set, and then add it to the current
        /// </summary>
        private void CreateDataSetDefault()
        {
            DateTime currentDate = new DateTime();
            DateTime dateStart = new DateTime();
            DateTime dateEnd = new DateTime();

            dateStart = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst;
            dateEnd = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast;

            currentDate = dateStart;

            // For every day from the start of the school year, until the end of the school year or now...
            while (currentDate.CompareTo(dateEnd) < 0)
            {
                var temp = new SchoolCalendarModel(currentDate);

                //Add the new date to the list of dates
                Create(temp);

                currentDate = currentDate.AddDays(1);
            }

            //sort by date
            DataList = DataList.OrderBy(x => x.Date).ToList();

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

        /// <summary>
        /// Find a date in the data store
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public SchoolCalendarModel ReadDate(DateTime date)
        {
            // Use the short date string because only checking dd.mm.yy not time...
            var myData = DataList.Find(n => n.Date.ToShortDateString() == date.ToShortDateString());
            return myData;
        }
    }
}