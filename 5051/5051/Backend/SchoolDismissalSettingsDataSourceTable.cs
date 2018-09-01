﻿using System;
using System.Collections.Generic;

using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for AvatarItems, to manage them
    /// </summary>
    public class SchoolDismissalSettingsDataSourceTable : ISchoolDismissalSettingsInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SchoolDismissalSettingsDataSourceTable instance;
        private static object syncRoot = new Object();

        private SchoolDismissalSettingsDataSourceTable() { }

        public static SchoolDismissalSettingsDataSourceTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SchoolDismissalSettingsDataSourceTable();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The AvatarItem List
        /// </summary>
        private List<SchoolDismissalSettingsModel> DataList = new List<SchoolDismissalSettingsModel>();

        private const string ClassName = "SchoolDismissalSettingsModel";
        /// <summary>
        /// Table Name used for data storage
        /// </summary>
        private string tableName = ClassName.ToLower();

        /// <summary>
        /// Partition Key used for data storage
        /// </summary>
        private string partitionKey = ClassName.ToLower();

        /// <summary>
        /// Makes a new AvatarItem
        /// </summary>
        /// <param name="data"></param>
        /// <returns>AvatarItem Passed In</returns>
        public SchoolDismissalSettingsModel Create(SchoolDismissalSettingsModel data)
        {
            DataList.Add(data);

            // Add to Storage
            var myResult = DataSourceBackendTable.Instance.Create<SchoolDismissalSettingsModel>(tableName, partitionKey, data.Id, data);

            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// Does not access storage, just reads from memeory
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public SchoolDismissalSettingsModel Read(string id)
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
        public SchoolDismissalSettingsModel Update(SchoolDismissalSettingsModel data)
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
            var myResult = DataSourceBackendTable.Instance.Create<SchoolDismissalSettingsModel>(tableName, partitionKey, data.Id, data);

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
            var myReturn = DataSourceBackendTable.Instance.Delete<SchoolDismissalSettingsModel>(tableName, partitionKey, myData.Id, myData);

            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of AvatarItems</returns>
        public List<SchoolDismissalSettingsModel> Index()
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
        public void CreateDataSetDefaultData()
        {

            // Storage Load all rows
            var DataSetList = DataSourceBackendTable.Instance.LoadAll<SchoolDismissalSettingsModel>(tableName, partitionKey);

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
            // Call over to the Settings Model itself to have it set it's default settings
            var Temp = new SchoolDismissalSettingsModel();
            Temp.SetDefault();
            Create(Temp);
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