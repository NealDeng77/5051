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
        private static IStudentInterface DataSource;

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
            DataSource = StudentDataSourceMock.Instance;
        }

        /// <summary>
        /// Switch the data set between Demo, Default and Unit Test
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            StudentDataSourceMock.Instance.LoadDataSet(SetEnum);
        }

        // Add the School Calendar Backend here
    }
}