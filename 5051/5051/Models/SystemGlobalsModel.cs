using System;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// System wide Global variables
    /// </summary>
    public class SystemGlobalsModel
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SystemGlobalsModel instance;
        private static object syncRoot = new Object();

        private SystemGlobalsModel() { }

        public static SystemGlobalsModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SystemGlobalsModel();
                            Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        // The Enum to use for the current data source
        // Default to Mock
        private static DataSourceEnum _DataSourceValue;
        private static HttpContext _HttpContext;
        private static HttpContext _DefaultHttpContext;

        //The current date
        public DateTime CurrentDate = DateTime.MinValue;

        //The Target Site

        public DataSourceEnum DataSourceValue => _DataSourceValue;
        public HttpContext HttpContext  => _HttpContext;
        public HttpContext DefaultHttpContext => _DefaultHttpContext;

        public static void SetHttpContext(HttpContext httpContext)
        {
            _HttpContext = httpContext;
        }

        public static void RestoreDefaultHttpContext()
        {
            _HttpContext = _DefaultHttpContext;
        }

        public static void Initialize()
        {
            if (HttpContext.Current== null)
            {
                // UT
                SetDataSourceEnum(DataSourceEnum.Mock);
                return;
            }

            string host = System.Web.HttpContext.Current.Request.Url.Host;
            if (host.Contains("mchs.azurewebsites.net"))
            {
                SetDataSourceEnum(DataSourceEnum.ServerLive);
                return;
            }

            if (host.Contains("azurewebsites.net"))
            {
                SetDataSourceEnum(DataSourceEnum.ServerTest);
                return;
            }

            SetDataSourceEnum(DataSourceEnum.Mock);
            return;
        }

        public static void SetDataSourceEnum(DataSourceEnum SetDataSourceValue)
        {
            _DataSourceValue = SetDataSourceValue;
        }
    }
}