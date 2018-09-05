using System;
using System.Collections.Generic;
using System.Linq;

using _5051.Models;

namespace _5051.Backend
{
    /*
    * Store the Student Record as fragments due to limit on Azure Table Storage of 1mb of data per table
    * rk+"Field"
    * 
    * Need to Clear the sub structures from Student before storing them
    * Then recreate them on retrieval

           AvatarCompositeModel AvatarComposite
           List<AvatarItemModel> AvatarInventory
           List<FactoryInventoryModel> Inventory
           GameResultViewModel TruckItems
           ShopTruckModel Truck
           List<AttendanceModel> Attendance
    */

    /// <summary>
    /// Backend Table DataSource for Students, to manage them
    /// </summary>
    public class StudentDataSourceTable : IStudentInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentDataSourceTable instance;
        private static object syncRoot = new Object();

        private StudentDataSourceTable() { }

        public static StudentDataSourceTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentDataSourceTable();
                            instance.Initialize();
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

        private const string ClassName = "StudentModel";
        /// <summary>
        /// Table Name used for data storage
        /// </summary>
        private string tableName = ClassName.ToLower();

        /// <summary>
        /// Partition Key used for data storage
        /// </summary>
        private string partitionKey = ClassName.ToLower();

        /// <summary>
        /// Makes a new Student
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Student Passed In</returns>
        public StudentModel Create(StudentModel data)
        {
            DataList.Add(data);

            var temp = new StudentModel(data);
            temp.Id = data.Id;
            temp.AvatarComposite = null;
            temp.AvatarInventory = null;
            temp.Inventory = null;
            temp.Attendance = null;
            temp.TruckItems = null;
            temp.Truck = null;

            // Add to Storage, the smaller temp student
            DataSourceBackendTable.Instance.Create<StudentModel>(tableName, "student", data.Id, temp);

            // Sub Components
            var tempData = new StudentModel(data);
            tempData.Id = data.Id;

            // Add to Storage, the smaller temp student
            DataSourceBackendTable.Instance.Create<StudentModel>(tableName, "student", temp.Id, temp);

            // Now store each of the Sub Structures as independent rows
            DataSourceBackendTable.Instance.Create<AvatarCompositeModel>(tableName, "composite", tempData.Id, tempData.AvatarComposite);

            DataSourceBackendTable.Instance.Create<List<AvatarItemModel>>(tableName, "avatarinventory", tempData.Id, tempData.AvatarInventory);

            DataSourceBackendTable.Instance.Create<List<FactoryInventoryModel>>(tableName, "inventory", tempData.Id, tempData.Inventory);

            DataSourceBackendTable.Instance.Create<List<AttendanceModel>>(tableName, "attendance", tempData.Id, tempData.Attendance);

            DataSourceBackendTable.Instance.Create<GameResultViewModel>(tableName, "truckitems", tempData.Id, tempData.TruckItems);

            DataSourceBackendTable.Instance.Create<ShopTruckModel>(tableName, "truck", tempData.Id, tempData.Truck);

            DataList = DataList.OrderBy(x => x.Name).ToList();

            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// Does not access storage, just reads from memeory
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public StudentModel Read(string id)
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
        public StudentModel Update(StudentModel data)
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
            var temp = new StudentModel(data);
            temp.Id = data.Id;
            temp.AvatarComposite = null;
            temp.AvatarInventory = null;
            temp.Inventory = null;
            temp.Attendance = null;
            temp.TruckItems = null;
            temp.Truck = null;

            // Add to Storage, the smaller temp student
            DataSourceBackendTable.Instance.Create<StudentModel>(tableName, "student", temp.Id , temp);


            // Sub components
            var tempData = new StudentModel(data);
            tempData.Id = data.Id;

            // Now store each of the Sub Structures as independent rows
            DataSourceBackendTable.Instance.Create<AvatarCompositeModel>(tableName, "composite", tempData.Id , tempData.AvatarComposite);

            DataSourceBackendTable.Instance.Create<List<AvatarItemModel>>(tableName, "avatarinventory", tempData.Id , tempData.AvatarInventory);

            DataSourceBackendTable.Instance.Create<List<FactoryInventoryModel>>(tableName, "inventory", tempData.Id , tempData.Inventory);

            DataSourceBackendTable.Instance.Create<List<AttendanceModel>>(tableName, "attendance", tempData.Id, tempData.Attendance);

            DataSourceBackendTable.Instance.Create<GameResultViewModel>(tableName, "truckitems", tempData.Id, tempData.TruckItems);

            DataSourceBackendTable.Instance.Create<ShopTruckModel>(tableName, "truck", tempData.Id, tempData.Truck);

            DataList = DataList.OrderBy(x => x.Name).ToList();

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

            var data = DataList.Find(n => n.Id == Id);
            if (DataList.Remove(data) == false)
            {
                return false;
            }

            // Storage Delete

            var temp = new StudentModel(data);
            temp.AvatarComposite = null;
            temp.AvatarInventory = null;
            temp.Inventory = null;
            temp.Attendance = null;
            temp.TruckItems = null;
            temp.Truck = null;

            // Add to Storage, the smaller temp student
            DataSourceBackendTable.Instance.Delete<StudentModel>(tableName, "student", data.Id , temp);

            // Sub components
            var tempData = new StudentModel(data);
            tempData.Id = data.Id;

            // Now store each of the Sub Structures as independent rows
            DataSourceBackendTable.Instance.Delete<AvatarCompositeModel>(tableName, "composite", tempData.Id, tempData.AvatarComposite);
            
            DataSourceBackendTable.Instance.Create<List<AvatarItemModel>>(tableName, "avatarinventory", tempData.Id  , tempData.AvatarInventory);

            DataSourceBackendTable.Instance.Delete<List<FactoryInventoryModel>>(tableName, "inventory", tempData.Id , tempData.Inventory);

            DataSourceBackendTable.Instance.Delete<List<AttendanceModel>>(tableName, "attendance", tempData.Id  , tempData.Attendance);

            DataSourceBackendTable.Instance.Delete<GameResultViewModel>(tableName, "truckitems", tempData.Id , tempData.TruckItems);

            DataSourceBackendTable.Instance.Delete<ShopTruckModel>(tableName, "truck", tempData.Id , tempData.Truck);

            return true;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Students</returns>
        public List<StudentModel> Index()
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

            // Make a call to LoadAll, and pass false for convert, this will return the raw object
            var DataSetList = DataSourceBackendTable.Instance.LoadAll<DataSourceBackendTableEntity>(tableName, "student",false);

            // Loop through each DataSetList, and reload them with the sub fields
            foreach (var temp in DataSetList)
            {
                // Only parse the Student items
                if (!temp.PartitionKey.Contains("student") )
                {
                    continue;   
                }

                // Now store each of the Sub Structures as independent rows

                var TempData = new StudentModel();

                TempData = DataSourceBackendTable.Instance.Load<StudentModel>(tableName, "student", temp.RowKey);

                TempData.AvatarComposite = DataSourceBackendTable.Instance.Load<AvatarCompositeModel>(tableName, "composite", temp.RowKey );

                TempData.AvatarInventory = DataSourceBackendTable.Instance.Load<List<AvatarItemModel>>(tableName, "avatarinventory", temp.RowKey );

                TempData.Inventory = DataSourceBackendTable.Instance.Load<List<FactoryInventoryModel>>(tableName, "inventory", temp.RowKey );

                TempData.Attendance = DataSourceBackendTable.Instance.Load<List<AttendanceModel>>(tableName, "attendance", temp.RowKey );

                TempData.TruckItems = DataSourceBackendTable.Instance.Load<GameResultViewModel>(tableName, "truckitems", temp.RowKey );

                TempData.Truck = DataSourceBackendTable.Instance.Load<ShopTruckModel>(tableName, "truck", temp.RowKey );

                var newData = new StudentModel(TempData);
                newData.Id = temp.RowKey;   //Set the ID to the item loaded
                DataList.Add(newData);
            }


            // If Storage is Empty, then Create.
            if (DataList.Count < 1)
            {
                CreateDataSetDefault();
            }

            // Need to order the return, because the azure table returns based on rk, which is not helpfull. So ordering by Name instead
            DataList = DataList.OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Get the Default data set, and then add it to the current
        /// </summary>
        private void CreateDataSetDefault()
        {
            var dataSet = StudentDataSourceHelper.Instance.GetDefaultDataSet();
            foreach (var item in dataSet)
            {
                Create(item);
            }
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