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
            Create(new StudentModel("Mike", null));
            Create(new StudentModel("Doug", null));
            Create(new StudentModel("Jea", null));
            Create(new StudentModel("Sue", null));
        }

        /// <summary>
        /// Data set for demo
        /// </summary>
        private void DataSetDemo()
        {
            DataSetClear();
            Create(new StudentModel("Mike", null));
            Create(new StudentModel("Doug", null));
            Create(new StudentModel("Jea", null));
            Create(new StudentModel("Sue", null));
            //attendance test data
            var myStudent = Backend.StudentBackend.Instance.GetDefault();


            DateTime dateStart = new DateTime();
            DateTime dateEnd = new DateTime();

            //Generate attendance for may 2018
            dateStart = DateTime.Parse("6/1/2018");
            dateEnd = DateTime.Parse("7/1/2018");

            // Set current date to be 1 less than the start day, because will get added in the for loop
            DateTime currentDate = dateStart.AddDays(-1);
            while (currentDate.CompareTo(dateEnd) < 0)
            {

                // Look to the next day
                currentDate = currentDate.AddDays(1);

                var myToday = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(currentDate);

                // if the day is a school day
                if (myToday != null && myToday.SchoolDay == true)
                {
                    var temp = new AttendanceModel
                    {
                        StudentId = myStudent.Id
                    };
                    //done early example
                    if (currentDate.Day == 7 || currentDate.Day == 25)
                    {
                        temp.In = currentDate.AddHours(9); //in 9:00
                        temp.Out = currentDate.AddHours(12);  // out 12:00                    
                        temp.Duration = temp.Out.Subtract(temp.In);
                        temp.AttendanceStatus = Models.Enums.AttendanceStatusEnum.Present;
                        temp.CheckInStatus = Models.Enums.CheckInStatusEnum.ArriveOnTime;
                        temp.CheckOutStatus = Models.Enums.CheckOutStatusEnum.DoneEarly;
                    }
                    //late  example
                    else if (currentDate.Day == 22)
                    {
                        temp.In = currentDate.AddHours(10); //in 10:00
                        temp.Out = currentDate.AddHours(15).AddMinutes(45); //out 15:45                 
                        temp.Duration = temp.Out.Subtract(temp.In);
                        temp.AttendanceStatus = Models.Enums.AttendanceStatusEnum.Present;
                        temp.CheckInStatus = Models.Enums.CheckInStatusEnum.ArriveLate;
                        temp.CheckOutStatus = Models.Enums.CheckOutStatusEnum.DoneAuto;
                    }
                    //wednesday early dismissal good attendance
                    else if (currentDate.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        temp.In = currentDate.AddHours(8).AddMinutes(55); //in 8:55
                        temp.Out = currentDate.AddHours(14); //out 14:00                
                        temp.Duration = temp.Out.Subtract(temp.In);
                        temp.AttendanceStatus = Models.Enums.AttendanceStatusEnum.Present;
                        temp.CheckInStatus = Models.Enums.CheckInStatusEnum.ArriveOnTime;
                        temp.CheckOutStatus = Models.Enums.CheckOutStatusEnum.DoneAuto;
                    }
                    //absent examples
                    else if (currentDate.Day == 8 || currentDate.Day == 14 || currentDate.Day == 25 || currentDate.Day == 29)
                    {
                        temp.Duration = temp.Out.Subtract(temp.In);
                        temp.AttendanceStatus = Models.Enums.AttendanceStatusEnum.AbsentUnexcused;
                    }
                    //good attendance 
                    else
                    {
                        temp.In = currentDate.AddHours(8).AddMinutes(55); //in 8:55
                        temp.Out = currentDate.AddHours(15).AddMinutes(45); //out 15:45                 
                        temp.Duration = temp.Out.Subtract(temp.In);
                        temp.AttendanceStatus = Models.Enums.AttendanceStatusEnum.Present;
                        temp.CheckInStatus = Models.Enums.CheckInStatusEnum.ArriveOnTime;
                        temp.CheckOutStatus = Models.Enums.CheckOutStatusEnum.DoneAuto;
                    }
                    myStudent.Attendance.Add(temp);
                }
            }
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