using System;
using System.Collections.Generic;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// Holds the Student Data as a Mock Data set, used for Unit Testing, System Testing, Offline Development etc.
    /// </summary>
    public class StudentDataSourceMock : IStudentInterface
    {
        /// <summary>
        /// Make into a singleton
        /// </summary>
        private static volatile StudentDataSourceMock instance;
        private static object syncRoot = new Object();

        private static bool isTestingMode = false;
        //IdentityBackend identityBackend = new IdentityBackend();

        private StudentDataSourceMock() { }

        public static StudentDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Data for the Students
        /// </summary>
        private List<StudentModel> StudentList = new List<StudentModel>();

        /// <summary>
        /// Makes a new Student
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Student Passed In</returns>
        public StudentModel Create(StudentModel data)
        {
            //if (!isTestingMode)
            //{
            //    var identityBackend = new IdentityBackend();
            //    identityBackend.CreateNewStudent(data);
            //}

            StudentList.Add(data);

            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public StudentModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = StudentList.Find(n => n.Id == id);
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
            var myReturn = StudentList.Find(n => n.Id == data.Id);
            if (myReturn == null)
            {
                return null;
            }

            myReturn.Update(data);

            return myReturn;
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

            var myData = StudentList.Find(n => n.Id == Id);
            var myReturn = StudentList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Students</returns>
        public List<StudentModel> Index()
        {
            return StudentList;
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
            StudentList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();
            StudentModel data;

            // Mike has Full Truck and Tokens
            data = new StudentModel("Mike", null);
            data = FactoryInventoryBackend.Instance.GetDefaultFullTruck(data);
            data.Tokens = 1000;
            Create(data);

            // Doug has full truck, but no Tokens
            data = new StudentModel("Doug", null);
            data = FactoryInventoryBackend.Instance.GetDefaultFullTruck(data);
            data.Tokens = 0;
            Create(data);

            // Jea has No truck, and Tokens
            data = new StudentModel("Jea", null);
            data = FactoryInventoryBackend.Instance.GetDefaultEmptyTruck(data);
            data.Tokens = 1000;
            Create(data);

            // Jea has No truck, and No Tokens
            data = new StudentModel("Sue", null);
            data = FactoryInventoryBackend.Instance.GetDefaultEmptyTruck(data);
            data.Tokens = 0;
            Create(data);

            // Mike has Full Truck and 1 Token
            data = new StudentModel("Stan", null);
            data = FactoryInventoryBackend.Instance.GetDefaultFullTruck(data);
            data.Tokens = 1;
            Create(data);
        }

        /// <summary>
        /// Data set for demo
        /// </summary>
        private void DataSetDemo()
        {
            DataSetClear();
            StudentBackendHelper.CreateDemoStudent();
            StudentBackendHelper.CreateDemoAttendance();     
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
            //if (!isTestingMode)
            //{
            //    var identityBackend = new IdentityBackend();
            //    identityBackend.Reset();
            //    //create support user
            //    identityBackend.CreateNewSupportUser("su5051", "su5051", "su5051");
            //    //create teacher user
            //    identityBackend.CreateNewTeacher("testTeacher", "teacherTest", "testTeacherID");
            //}
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