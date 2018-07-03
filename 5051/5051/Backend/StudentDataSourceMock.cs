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
            Create(new StudentModel("Stan", null));

            DateTime dateStart = new DateTime();
            DateTime dateEnd = new DateTime();

            //Set the range to be from the first day of school to the last day of school
            dateStart = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst;
            dateEnd = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast;

            //Don't generate attendance for today and future days
            DateTime yesterday = DateTime.UtcNow.AddDays(-1);
            if(yesterday.CompareTo(dateEnd) < 0)
            {
                dateEnd = yesterday;
            }

            //Generate attendance records for 5 student personas
            //The scenario of this demo data:
            //Student at index 0 has perfect attendance: always arrive on time and stay
            GenerateAttendance(0, dateStart, dateEnd);
            //Student at index 1 has good attendance: on average has 1 day late and 1 day leave early out of every 5 days
            GenerateAttendance(1, dateStart, dateEnd);
            //Student at index 2 has average attendance: on average has 1 day late, 1 day leave early and 1 day absent out of every 5 days
            GenerateAttendance(2, dateStart, dateEnd);
            //Student at index 3 has bad attendance: on average has 1 day late, 2 days very late, 1 day leave early and 2 days absent out of every 5 days
            GenerateAttendance(3, dateStart, dateEnd);
            //Student at index 4 has no attendance: always absent
            GenerateAttendance(4, dateStart, dateEnd);
            //To do: create scenario for multiple check-ins
        }

        /// <summary>
        /// Generate attendance records for student of given name from the start date to the end date
        /// </summary>
        /// <param name="index">The index of the student in studentBackend index</param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        private void GenerateAttendance(int index, DateTime dateStart, DateTime dateEnd)
        {
            var myStudent = Backend.StudentBackend.Instance.Index()[index];

            // Set current date to be 1 less than the start day, because will get added in the for loop
            DateTime currentDate = dateStart.AddDays(-1);

            //To generate random numbers, since seed is fixed, the numbers generated will be same in every run
            Random r = new Random(0);

            while (currentDate.CompareTo(dateEnd) < 0)
            {

                // Look to the next day
                currentDate = currentDate.AddDays(1);

                // Create an attendance model for this student
                var temp = new AttendanceModel
                {
                    StudentId = myStudent.Id,
                    Status = StudentStatusEnum.Out
                };

                // Get the school day info for current date
                var myToday = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(currentDate);

                // if the day is a school day
                if (myToday != null && myToday.SchoolDay == true)
                {
                    //generate a random number 0-4 inclusive, use this to randomly generate 5 scenarios
                    int rn = r.Next(0, 5);

                    switch (index)
                    {
                        case 0: //Perfect
                            temp.In = InGood(currentDate, r);
                            temp.Out = OutAuto(currentDate, r);
                            temp.Duration = temp.Out.Subtract(temp.In);
                            myStudent.Attendance.Add(temp);
                            break;
                        case 1: //Good
                            {
                                switch (rn)
                                {
                                    case 0:
                                        temp.In = InLate(currentDate, r);
                                        temp.Out = OutAuto(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                    case 1:
                                        temp.In = InGood(currentDate, r);
                                        temp.Out = OutEarly(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                    default:
                                        temp.In = InGood(currentDate, r);
                                        temp.Out = OutAuto(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                }
                            }
                            break;
                        case 2: //Average
                            {
                                switch (rn)
                                {
                                    case 0:
                                        temp.In = InLate(currentDate, r);
                                        temp.Out = OutEarly(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                    case 1:
                                        break;
                                    case 2:
                                        temp.In = InLate(currentDate, r);
                                        temp.Out = OutAuto(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                    default:
                                        temp.In = InGood(currentDate, r);
                                        temp.Out = OutAuto(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                }
                            }
                            break;
                        case 3: //Bad
                            {
                                switch (rn)
                                {
                                    case 0:
                                        temp.In = InVeryLate(currentDate, r);
                                        temp.Out = OutEarly(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                    case 1:
                                        temp.In = InVeryLate(currentDate, r);
                                        temp.Out = OutAuto(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                    case 2:
                                        temp.In = InLate(currentDate, r);
                                        temp.Out = OutAuto(currentDate, r);
                                        temp.Duration = temp.Out.Subtract(temp.In);
                                        myStudent.Attendance.Add(temp);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case 4: //None
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Check-in time is good, return a random time between 8:00am - 8:54am
        /// </summary>
        /// <param name="date"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private DateTime InGood(DateTime date, Random r)
        {
            return date.AddMinutes(r.Next(480, 535)); // in 8:00am - 8:54am
        }
        /// <summary>
        /// Check-in time is late, return a random time between 8:55am - 10:00am
        /// </summary>
        /// <param name="date"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private DateTime InLate(DateTime date, Random r)
        {
            return date.AddMinutes(r.Next(535, 600)); // in 8:55am - 10:00am
        }
        /// <summary>
        /// Check-in time is very late, return a random time between 10:00am - 2:00pm
        /// </summary>
        /// <param name="date"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private DateTime InVeryLate(DateTime date, Random r)
        {
            return date.AddMinutes(r.Next(600, 840)); // in 10:00am - 2:00pm
        }
        /// <summary>
        /// Check-out time is auto, return 15:45pm
        /// </summary>
        /// <param name="date"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private DateTime OutAuto(DateTime date, Random r)
        {
            return date.AddMinutes(945); // out 15:45pm
        }
        /// <summary>
        /// Check-out time is early, return a random time between 10:00am - 3:00pm
        /// </summary>
        /// <param name="date"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private DateTime OutEarly(DateTime date, Random r)
        {
            return date.AddMinutes(r.Next(600, 900)); // out 10:00am - 3:00pm
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