using _5051.Models;
using System;

namespace _5051.Backend
{
    /// <summary>
    /// Class that manages the overall data sources
    /// </summary>
    public class DataSourceBackend
    {
        /// <summary>
        /// Hold one of each of the DataSources as an instance to the datasource
        /// </summary>
        public AvatarBackend AvatarBackend = AvatarBackend.Instance;

        public AvatarItemBackend AvatarItemBackend = AvatarItemBackend.Instance;
        public StudentBackend StudentBackend = StudentBackend.Instance;
        public FactoryInventoryBackend FactoryInventoryBackend = FactoryInventoryBackend.Instance;
        public SchoolCalendarBackend SchoolCalendarBackend = SchoolCalendarBackend.Instance;
        public SchoolDismissalSettingsBackend SchoolDismissalSettingsBackend = SchoolDismissalSettingsBackend.Instance;
        public KioskSettingsBackend KioskSettingsBackend = KioskSettingsBackend.Instance;
        public GameBackend GameBackend = GameBackend.Instance;

        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile DataSourceBackend instance;
        private static object syncRoot = new Object();

        private static bool isTestingMode = false;

        private DataSourceBackend()
        {
            // Avatar must be before Student, because Student needs the default avatarID
            AvatarBackend = AvatarBackend.Instance;

            AvatarItemBackend = AvatarItemBackend.Instance;
            StudentBackend = StudentBackend.Instance;
            FactoryInventoryBackend = FactoryInventoryBackend.Instance;
            SchoolCalendarBackend = SchoolCalendarBackend.Instance;
            SchoolDismissalSettingsBackend = SchoolDismissalSettingsBackend.Instance;
            KioskSettingsBackend = KioskSettingsBackend.Instance;
            GameBackend = GameBackend.Instance;
        }

        public static DataSourceBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DataSourceBackend();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Call for all data sources to reset
        /// </summary>
        public void Reset()
        {
            GameBackend.Reset();
            KioskSettingsBackend.Reset();
            SchoolCalendarBackend.Reset();
            SchoolDismissalSettingsBackend.Reset();
            FactoryInventoryBackend.Reset();

            AvatarBackend.Reset();

            AvatarItemBackend.Reset();
            StudentBackend.Reset();

            //if (!isTestingMode)
            //{
            //    var identityBackend = new IdentityBackend();
            //    identityBackend.Reset();
            //}

            SetTestingMode(false);
        }

        /// <summary>
        /// Changes the data source, does not call for a reset, that allows for how swapping but keeping the original data in place
        /// </summary>
        public void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            GameBackend.SetDataSource(dataSourceEnum);
            KioskSettingsBackend.SetDataSource(dataSourceEnum);
            SchoolDismissalSettingsBackend.SetDataSource(dataSourceEnum);
            SchoolCalendarBackend.SetDataSource(dataSourceEnum);

            FactoryInventoryBackend.SetDataSource(dataSourceEnum);

            // Avatar must be reset before Student, because Student needs the default avatarID
            AvatarBackend.SetDataSource(dataSourceEnum);

            AvatarItemBackend.SetDataSource(dataSourceEnum);
            StudentBackend.SetDataSource(dataSourceEnum);
        }

        /// <summary>
        /// Change between demo, default, and UT data sets
        /// </summary>
        /// <param name="SetEnum"></param>
        public void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            GameBackend.SetDataSourceDataSet(SetEnum);
            KioskSettingsBackend.SetDataSourceDataSet(SetEnum);
            SchoolDismissalSettingsBackend.SetDataSourceDataSet(SetEnum);
            SchoolCalendarBackend.SetDataSourceDataSet(SetEnum);
            FactoryInventoryBackend.SetDataSourceDataSet(SetEnum);

            // Avatar must be reset before Student, because Student needs the default avatarID
            AvatarBackend.SetDataSourceDataSet(SetEnum);

            AvatarItemBackend.SetDataSourceDataSet(SetEnum);
            StudentBackend.SetDataSourceDataSet(SetEnum);
        }

        public bool GetTestingMode()
        {
            return isTestingMode;
        }

        public static bool SetTestingMode(bool mode)
        {
            isTestingMode = mode;

            //set the testing mode for other backends
            Backend.StudentDataSourceMock.SetTestingMode(mode);

            return isTestingMode;
        }

        public bool IsUserNotInRole(string userID, string role)
        {
            if (isTestingMode)
            {
                return false; // all OK
            }

            var IdentityBackend = new IdentityBackend();

            if (IdentityBackend.UserHasClaimOfValue(userID, role, "True"))
            {
                return false;
            }
            return true; // Not in role, so error
        }
    }
}