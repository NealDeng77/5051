using System;
using System.Collections.Generic;
using System.Linq;
using _5051.Models;

using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for AvatarItems, to manage them
    /// </summary>
    public class DataSourceBackendTable
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile DataSourceBackendTable instance;
        private static object syncRoot = new Object();

        private DataSourceBackendTable() { }

        public static DataSourceBackendTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DataSourceBackendTable();
                        }
                    }
                }

                return instance;
            }
        }

        public CloudStorageAccount storageAccount;
        public CloudTableClient tableClient;
        public int takeCount = 1000;
        public DataSourceEnum DataSourceServerMode = DataSourceEnum.Local;

        public bool SetDataSourceServerMode(DataSourceEnum dataSourceServerMode)
        {
            var connectionString = string.Empty;
            var StorageConnectionString = GetDataSourceConnectionString(dataSourceServerMode);

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return true;
            }

            return SetDataSourceServerModeDirect(StorageConnectionString);
        }

        public string GetDataSourceConnectionString(DataSourceEnum dataSourceServerMode)
        {
            var StorageConnectionString = string.Empty;
            switch (dataSourceServerMode)
            {
                case DataSourceEnum.Local:
                    StorageConnectionString = "StorageConnectionStringLocal";
                    break;

                case DataSourceEnum.ServerLive:
                    StorageConnectionString = "StorageConnectionStringServerLive";
                    break;

                case DataSourceEnum.ServerTest:
                default:
                    StorageConnectionString = "StorageConnectionStringServerTest";
                    break;
            }
            return StorageConnectionString;
        }

        public bool SetDataSourceServerModeDirect(string StorageConnectionString)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                        CloudConfigurationManager.GetSetting(StorageConnectionString));

                tableClient = storageAccount.CreateCloudTableClient();

                // Test if the connection is working by trying to access a table
                var Table = tableClient.GetTableReference("initaltable");
                Table.CreateIfNotExists();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Load All Rows that match the PK
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public List<T> LoadAll<T>(string tableName, string pk, bool convert = true, CloudTable Table = null)
        {
            var myReturnList = new List<T>();

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturnList;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturnList;
            }

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return myReturnList;
            }

            return LoadAllDirect<T>(tableName, pk, Table, convert);
        }

        public List<T> LoadAllDirect<T>(string tableName, 
                                        string pk,
                                        CloudTable Table,
                                        bool convert = true)
        {
            var myReturnList = new List<T>();

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturnList;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturnList;
            }

            try
            {
                // If not Table Client is passed in, then assume the global one.
                if (Table == null)
                {
                    Table = GetTable(DataSourceServerMode, tableName);
                }

                var result = new List<DataSourceBackendTableEntity>();
                var query =
                    new TableQuery<DataSourceBackendTableEntity>().Where(
                        TableQuery.GenerateFilterCondition(
                            "PartitionKey", QueryComparisons.Equal, pk));

                query.TakeCount = takeCount;

                TableContinuationToken tableContinuationToken = null;
                do
                {
                    var queryResponse = Table.ExecuteQuerySegmented(query, tableContinuationToken);
                    tableContinuationToken = queryResponse.ContinuationToken;
                    result.AddRange(queryResponse.Results);
                } while (tableContinuationToken != null);

                foreach (var item in result)
                {
                    if (convert)
                    {
                        myReturnList.Add(ConvertFromEntity<T>(item));
                    }
                    else
                    {
                        myReturnList.Add((T)((Object)item));
                    }
                }
            }
            catch (Exception ex) { }

            return myReturnList;
        }

        /// <summary>
        /// Call Update, because Update is Insert or Replace
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Create<T>(string tableName, string pk, string rk, T data)
        {
            return Update<T>(tableName, pk, rk, data);
        }

        /// <summary>
        /// Load the record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pk"></param>
        /// <param name="rk"></param>
        /// <returns></returns>
        public T Load<T>(string tableName, string pk, string rk)
        {
            var myReturn = default(T);

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return myReturn;
            }

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return myReturn;
            }

            return LoadDirect<T>(tableName, pk, rk);
        }

        public T LoadDirect<T>(string tableName, string pk, string rk)
        {
            var myReturn = default(T);

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return myReturn;
            }

            try
            {
                var Table = tableClient.GetTableReference(tableName);
                Table.CreateIfNotExists();

                // Retrieve
                var retrieveOperation = TableOperation.Retrieve<DataSourceBackendTableEntity>(pk, rk);
                var query = Table.Execute(retrieveOperation);
                if (query.Result == null)
                {
                    return myReturn;
                }

                var data = (DataSourceBackendTableEntity)query.Result;

                myReturn = ConvertFromEntity<T>(data);
                return myReturn;
            }
            catch (Exception ex)
            {
            }

            return myReturn;
        }

        /// <summary>
        /// Insert or Replace the Record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Update<T>(string tableName, string pk, string rk, T data)
        {
            var myReturn = default(T);

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return myReturn;
            }

            if (data == null)
            {
                return myReturn;
            }

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return myReturn;
            }

            return UpdateDirect<T>(tableName, pk, rk, data);
        }

        public T UpdateDirect<T>(string tableName, string pk, string rk, T data)
        {
            var myReturn = default(T);

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return myReturn;
            }

            if (data == null)
            {
                return myReturn;
            }

            try
            {
                var Table = tableClient.GetTableReference(tableName);
                Table.CreateIfNotExists();

                // Add to Storage
                var entity = DataSourceBackendTable.Instance.ConvertToEntity<T>(data, pk, rk);

                var updateOperation = TableOperation.InsertOrReplace(entity);
                var query = Table.Execute(updateOperation);
                if (query.Result == null)
                {
                    return myReturn;
                }

                myReturn = DataSourceBackendTable.Instance.ConvertFromEntity<T>(entity);
            }
            catch (Exception ex) { }

            return myReturn;
        }

        /// <summary>
        /// Delete the record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete<T>(string tableName, string pk, string rk, T data, CloudTableClient TableClient = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return false;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return false;
            }

            if (data == null)
            {
                return false;
            }

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return true;
            }

            return DeleteDirect<T>(tableName, pk, rk, data);
        }

        public bool DeleteDirect<T>(string tableName, string pk, string rk, T data)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return false;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return false;
            }

            if (data == null)
            {
                return false;
            }

            try
            {
                var Table = tableClient.GetTableReference(tableName);
                Table.CreateIfNotExists();

                // Delete
                var entity = DataSourceBackendTable.Instance.ConvertToEntity<T>(data, pk, rk);
                entity.ETag = "*";

                var Operation = TableOperation.Delete(entity);
                var query = Table.Execute(Operation);
                if (query.Result == null)
                {
                    return false;
                }
            }
            catch (Exception ex) { }

            return true;
        }

        /// <summary>
        /// Convert the data item to an entity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataSourceBackendTableEntity ConvertToEntity<T>(T data, string pk, string rk)
        {
            var entity = new DataSourceBackendTableEntity();
            entity.PartitionKey = pk;
            entity.RowKey = rk;
            entity.Blob = JsonConvert.SerializeObject(data);

            return entity;
        }

        /// <summary>
        /// Convert an entity to a data item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T ConvertFromEntity<T>(DataSourceBackendTableEntity data)
        {
            var myReturn = JsonConvert.DeserializeObject<T>(data.Blob);
            return myReturn;
        }

        /// <summary>
        /// Convert all the entities to data items
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public List<T> ConvertFromEntityList<T>(List<DataSourceBackendTableEntity> dataList)
        {
            var myReturn = new List<T>();

            if (dataList == null)
            {
                return myReturn;
            }

            foreach (var item in dataList)
            {
                var temp = JsonConvert.DeserializeObject<T>(item.Blob);
                myReturn.Add(temp);
            }

            return myReturn;
        }

        /// <summary>
        ///  Call this method to copy from one data source to another, this does the write of the copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSourceServerMode"></param>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CopyDataDirect<T>(DataSourceEnum dataSourceServerMode, string tableName)
        {
            var pk = tableName.ToLower();

            // Read all the records from the Source using current database defaults
            var SourceData = LoadAllDirect<T>(tableName, pk,null,true);
            if (!SourceData.Any())
            {
                return false;
            }

            // Write all the records to the destination
            // Set the path to the destination

            CloudTableClient DestinationTableClient;

            var DestinationStorageConnectionString = GetDataSourceConnectionString(dataSourceServerMode);

            CloudStorageAccount DestinationStorageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting(DestinationStorageConnectionString));

            DestinationTableClient = DestinationStorageAccount.CreateCloudTableClient();
            var DestinationTable = DestinationTableClient.GetTableReference(tableName);
            DestinationTable.CreateIfNotExists();

            // TODO
            // Empty out Destination Table
            // Get all rows in the destination Table
            // Walk and delete each item, because delete table takes too long...
            var SourceDataAll = LoadAllDirect<T>(tableName, pk, DestinationTable,true);
            foreach (var data in SourceDataAll)
            {

            }

            // Load new Data into it
            foreach (var data in SourceData)
            {

                var rkObject  = data.GetType().GetProperty("Id").GetValue(data, null);
                var rk = rkObject.ToString();

                // Add to Storage
                var entity = ConvertToEntity<T>(data, pk, rk);

                var updateOperation = TableOperation.InsertOrReplace(entity);
                var query = DestinationTable.Execute(updateOperation);
            }

            return true;
        }

        public CloudTable GetTable(DataSourceEnum dataSourceServerMode, string tableName)
        {
            CloudTableClient DestinationTableClient;

            var DestinationStorageConnectionString = GetDataSourceConnectionString(dataSourceServerMode);

            var Connection = CloudConfigurationManager.GetSetting(DestinationStorageConnectionString);

            CloudStorageAccount DestinationStorageAccount = CloudStorageAccount.Parse(Connection
            );

            DestinationTableClient = DestinationStorageAccount.CreateCloudTableClient();
            var DestinationTable = DestinationTableClient.GetTableReference(tableName);
            DestinationTable.CreateIfNotExists();

            return DestinationTable;
        }
    }
}