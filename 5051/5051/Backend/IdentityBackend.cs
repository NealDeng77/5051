using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;
using System.Web.Mvc;

namespace _5051.Backend
{
    public class IdentityBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile IdentityBackend instance;
        private static object syncRoot = new Object();

        private IdentityBackend() { }

        public static IdentityBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new IdentityBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IIdentityInterface DataSource;


        /// <summary>
        /// Sets the Datasource to be Mock or SQL
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            switch (dataSourceEnum)
            {
                case DataSourceEnum.SQL:
                    break;

                case DataSourceEnum.Local:
                case DataSourceEnum.ServerLive:
                case DataSourceEnum.ServerTest:
                    DataSourceBackendTable.Instance.SetDataSourceServerMode(dataSourceEnum);
                    DataSource = IdentityDataSourceTable.Instance;
                    break;

                case DataSourceEnum.Mock:
                default:
                    // Default is to use the Mock
                    DataSource = null;
                    break;
            }

        }


        public ApplicationUser CreateNewSupportUser(string userName, string password, string id)
        {
            var result = DataSource.CreateNewSupportUser(userName, password, id);
            return result;
        }

    }
}