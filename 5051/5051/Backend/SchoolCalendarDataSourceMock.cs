using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for SchoolCalendars, to manage them
    /// </summary>
    public class SchoolCalendarDataSourceMock : ISchoolCalendarInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SchoolCalendarDataSourceMock instance;
        private static object syncRoot = new Object();

        private SchoolCalendarDataSourceMock() { }

        public static SchoolCalendarDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SchoolCalendarDataSourceMock();
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
        private List<SchoolCalendarModel> SchoolCalendarList = new List<SchoolCalendarModel>();

        /// <summary>
        /// Makes a new SchoolCalendar
        /// </summary>
        /// <param name="data"></param>
        /// <returns>SchoolCalendar Passed In</returns>
        public SchoolCalendarModel Create(SchoolCalendarModel data)
        {
            SchoolCalendarList.Add(data);
            return data;
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

            var myReturn = SchoolCalendarList.Find(n => n.Id == id);
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
            var myReturn = SchoolCalendarList.Find(n => n.Id == data.Id);

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

            var myData = SchoolCalendarList.Find(n => n.Id == Id);
            var myReturn = SchoolCalendarList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of SchoolCalendars</returns>
        public List<SchoolCalendarModel> Index()
        {
            return SchoolCalendarList;
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
            SchoolCalendarList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();
            var count = 0;
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Police", "Happy Officer", 1));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Kunoichi", "Ninja Lady", 2));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Angry", "Angry, but happy", 1));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Playfull", "Anyone want a ride?", 1));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Pirate", "Where is my ship?", 2));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Blue", "Having a Blue Day", 3));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Pigtails", "Love my hair", 3));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Ninja", "Taste my Katana", 2));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Circus", "Swinging from the Trapeese", 4));
            //Create(new SchoolCalendarModel("SchoolCalendar" + count++.ToString() + ".png", "Chef", "I love to cook", 4));
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