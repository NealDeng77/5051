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

        // Will copy all the data from the Live Server to designated location
        public bool CopyFromServerLive(DataSourceEnum DestinationDataSourceEnum)
        {
            var connectionString = string.Empty;
            var StorageConnectionString = string.Empty;

            switch (DestinationDataSourceEnum)
            {
                case DataSourceEnum.Local:
                    StorageConnectionString = "StorageConnectionStringLocal";
                    break;

                case DataSourceEnum.ServerLive:
                    return false;  // Can't copy from live to live
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