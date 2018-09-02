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

            StudentModel data;


            // Mike has Full Truck and Tokens
            data = new StudentModel("Mike", null);
            data = FactoryInventoryBackend.Instance.GetDefaultFullTruck(data);
            data.Tokens = 1000;
            DataList.Add(data);

            // Doug has full truck, but no Tokens
            data = new StudentModel("Doug", null);
            data = FactoryInventoryBackend.Instance.GetDefaultFullTruck(data);
            data.Tokens = 0;
            DataList.Add(data);

            // Jea has No truck, and Tokens
            data = new StudentModel("Jea", null);
            data = FactoryInventoryBackend.Instance.GetDefaultEmptyTruck(data);
            data.Tokens = 1000;
            DataList.Add(data);

            // Jea has No truck, and No Tokens
            data = new StudentModel("Sue", null);
            data = FactoryInventoryBackend.Instance.GetDefaultEmptyTruck(data);
            data.Tokens = 0;
            DataList.Add(data);

            // Mike has Full Truck and 1 Token
            data = new StudentModel("Stan", null);
            data = FactoryInventoryBackend.Instance.GetDefaultFullTruck(data);
            data.Tokens = 1;
            DataList.Add(data);

            return DataList;
        }

    }
}