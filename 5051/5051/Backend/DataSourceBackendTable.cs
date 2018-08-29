using System;
using System.Collections.Generic;
using _5051.Models;

using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

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
            try
            {
                var connectionString = string.Empty;
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
                        StorageConnectionString = "StorageConnectionStringServerTest";
                        break;

                    default:
                        throw new NotImplementedException();
                }

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
        public List<DataSourceBackendTableEntity> LoadAll(string tableName, string pk)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return null;
            }

            var Table = tableClient.GetTableReference(tableName);
            Table.CreateIfNotExists();

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

            return result;
        }

        /// <summary>
        /// Call Update, because Update is Insert or Replace
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataSourceBackendTableEntity Create(string tableName, DataSourceBackendTableEntity data)
        {
            return Update(tableName, data);
        }

        /// <summary>
        /// Load the record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pk"></param>
        /// <param name="rk"></param>
        /// <returns></returns>
        public DataSourceBackendTableEntity Load(string tableName, string pk, string rk)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return null;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return null;
            }

            var Table = tableClient.GetTableReference(tableName);
            Table.CreateIfNotExists();

            // Retrieve
            var retrieveOperation = TableOperation.Retrieve<DataSourceBackendTableEntity>(pk, rk);
            var query = Table.Execute(retrieveOperation);
            if (query.Result == null)
            {
                return null;
            }

            var myReturn = (DataSourceBackendTableEntity)query.Result;
            return myReturn;
        }

        /// <summary>
        /// Insert or Replace the Record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataSourceBackendTableEntity Update(string tableName, DataSourceBackendTableEntity data)
        {
            if (data == null)
            {
                return null;
            }

            var Table = tableClient.GetTableReference(tableName);
            Table.CreateIfNotExists();

            var updateOperation = TableOperation.InsertOrReplace(data);
            var query = Table.Execute(updateOperation);
            if (query.Result == null)
            {
                return null;
            }

            return data;
        }

        /// <summary>
        /// Delete the record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete(string tableName, DataSourceBackendTableEntity data)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }

            if (data == null)
            {
                return false;
            }

            var Table = tableClient.GetTableReference(tableName);
            Table.CreateIfNotExists();

            // Delete
            data.ETag = "*";

            var Operation = TableOperation.Delete(data);
            var query = Table.Execute(Operation);
            if (query.Result == null)
            {
                return false;
            }

            return true;
        }

    }
}


//connectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;";
