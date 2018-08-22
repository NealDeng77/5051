using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;


namespace _5051.Backend
{
    public class KioskSettingsBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile KioskSettingsBackend instance;
        private static object syncRoot = new Object();

        private static bool isTestingMode = false;

        private KioskSettingsBackend() { }

        public static KioskSettingsBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new KioskSettingsBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IKioskSettingsInterface DataSource;

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
            DataSource = KioskSettingsDataSourceMock.Instance;
        }

        /// <summary>
        /// Switch the data set between Demo, Default and Unit Test
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            StudentDataSourceMock.Instance.LoadDataSet(SetEnum);
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
        public KioskSettingsModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = DataSource.Read(id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public KioskSettingsModel Update(KioskSettingsModel data)
        {
            if (data == null)
            {
                return null;
            }

            var myReturn = DataSource.Update(data);

            //Todo: not ready yet, need to work out how to call for Resetting of the Calendar properly
            //if (myReturn != null)
            //{
            //    // Changes to the Update, means a setting on the calendar is different.
            //    // Need to go through the calendar and change things.  That way a change to the dismissal time is reflected if changed etc.
            //    SchoolCalendarBackend.Instance.ResetDefaults();
            //}
            
            return myReturn;
        }

        /// <summary>
        /// Returns the First record
        /// </summary>
        /// <returns>Null or valid data</returns>
        public KioskSettingsModel GetDefault()
        {
            var myReturn = DataSource.Index().First();
            return myReturn;
        }

        public bool GetTestingMode()
        {
            return isTestingMode;
        }

        public bool SetTestingMode(bool mode)
        {
            isTestingMode = mode;

            return isTestingMode;
        }
    }
}