using System;
using System.Collections.Generic;
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
    public class TablesCopy
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile TablesCopy instance;
        private static object syncRoot = new Object();

        private TablesCopy() { }

        public static TablesCopy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new TablesCopy();
                        }
                    }
                }

                return instance;
            }
        }


        public bool CopyFromServerLive(DataSourceEnum dataSourceServerMode)
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
                default:
                    StorageConnectionString = "StorageConnectionStringServerTest";
                    break;
            }

            return true;
        }
    }
}