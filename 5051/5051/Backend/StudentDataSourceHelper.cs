using System;
using System.Collections.Generic;

using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for Students, to manage them
    /// </summary>
    public class StudentDataSourceHelper
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentDataSourceHelper instance;
        private static object syncRoot = new Object();

        private StudentDataSourceHelper() { }

        public static StudentDataSourceHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentDataSourceHelper();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Student List
        /// </summary>
        private List<StudentModel> DataList = new List<StudentModel>();

        /// <summary>
        /// Clear the Data List, and build up a new one
        /// </summary>
        /// <returns></returns>
        public List<StudentModel> GetDefaultDataSet()
        {
            DataList.Clear();

            DataList.Add(new StudentModel("Mike", null));
            DataList.Add(new StudentModel("Doug", null));
            DataList.Add(new StudentModel("Jea", null));
            DataList.Add(new StudentModel("Sue", null));
            DataList.Add(new StudentModel("Stan", null));

            return DataList;
        }

    }
}