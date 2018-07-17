using System;

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
                        }
                    }
                }

                return instance;
            }
        }

        // The Enum to use for the current data source
        // Default to Mock
        public DataSourceEnum DataSourceValue = DataSourceEnum.Mock;

        //The current date
        public DateTime CurrentDate = DateTime.MinValue;
    }
}