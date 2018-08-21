using _5051.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Backend
{
    /// <summary>
    /// Student Backend handles the business logic and data for Students
    /// </summary>
    public class StudentBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentBackend instance;
        private static object syncRoot = new Object();

        private StudentBackend() { }

        public static StudentBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IStudentInterface DataSource;

        /// <summary>
        /// Switches between Live, and Mock Datasets
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            if (dataSourceEnum == DataSourceEnum.SQL)
            {
                // SQL not hooked up yet...
                //throw new NotImplementedException();
            }

            // Default is to use the Mock
            DataSource = StudentDataSourceMock.Instance;
        }

        /// <summary>
        /// Switch the data set between Demo, Default and Unit Test
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            StudentDataSourceMock.Instance.LoadDataSet(SetEnum);
        }

        /// <summary>
        /// Makes a new Student
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Student Passed In</returns>
        public StudentModel Create(StudentModel data)
        {
            DataSource.Create(data);
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

            var myReturn = DataSource.Read(id);

            if (myReturn == null)
            {
                return null;
            }

            myReturn.EmotionCurrent = EmotionStatusEnum.Neutral;

            //Todo: refactor this to a different location
            if (myReturn.Attendance.Any())
            {
                // Set the current emotion, to the last check in status emotion
                var temp = myReturn.Attendance.LastOrDefault(/*m => m.Status == StudentStatusEnum.In*/);

                if (temp != null)
                {
                    myReturn.EmotionCurrent = temp.Emotion;
                }
            }

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

            var myData = DataSource.Read(data.Id);
            if (myData == null)
            {
                // Not found
                return null;
            }

            if (myData.Status != data.Status)
            {
                // Status Changed, need to process the status change
                ToggleStatus(myData);
            }

            // Update the record
            var myReturn = DataSource.Update(data);

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

            var myReturn = DataSource.Delete(Id);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Students</returns>
        public List<StudentModel> Index()
        {
            var myData = DataSource.Index();
            return myData;
        }

        /// <summary>
        /// Sets the student to be logged In
        /// </summary>
        /// <param name="id">The Student ID</param>
        public void SetLogIn(StudentModel data)
        {
            if (data == null)
            {
                return;
            }

            data.Status = StudentStatusEnum.In;

            // Update the Attendance Log
            var temp = new AttendanceModel
            {
                In = DateTime.UtcNow,
                Emotion = data.EmotionCurrent
            };

            data.Attendance.Add(temp);

        }

        /// <summary>
        /// Sets the student to be logged Out
        /// </summary>
        /// <param name="id">The Student ID</param>
        public void SetLogOut(StudentModel data)
        {
            if (data == null)
            {
                return;
            }

            data.Status = StudentStatusEnum.Out;

            var myTimeData = data.Attendance.OrderByDescending(m => m.In).FirstOrDefault();
            if (myTimeData == null)
            {
                return;
            }

            // Add the new one it with the new data
            myTimeData.Out = DateTime.UtcNow;
        }

        /// <summary>
        /// Use the ID to toggle the emotion and status 
        /// </summary>
        /// <param name="id">Id of the student</param>
        public void ToggleEmotionStatusById(string id, EmotionStatusEnum emotion)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            var myData = DataSource.Read(id);
            if (myData == null)
            {
                return;
            }

            myData.EmotionCurrent = emotion;
            ToggleStatus(myData);
        }

        /// <summary>
        /// Use the ID to toggle the status
        /// </summary>
        /// <param name="id">Id of the student</param>
        public void ToggleStatusById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            var myData = DataSource.Read(id);
            if (myData == null)
            {
                return;
            }

            ToggleStatus(myData);
        }

        /// <summary>
        /// Change the Status of the student
        /// </summary>
        /// <param name="id">The Student ID</param>
        public void ToggleStatus(StudentModel data)
        {
            if (data == null)
            {
                return;
            }

            switch (data.Status)
            {
                case StudentStatusEnum.In:
                    SetLogOut(data);
                    break;

                case StudentStatusEnum.Out:
                    SetLogIn(data);
                    break;

                case StudentStatusEnum.Hold:
                    SetLogOut(data);
                    break;

            }

            DataSource.Update(data);
        }

        /// <summary>
        /// Get the attendance model with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AttendanceModel ReadAttendance(string id)
        {
            foreach (var student in Index())
            {
                foreach (var attendance in student.Attendance)
                {
                    if (attendance.Id == id)
                    {
                        return attendance;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// If date has changed, reset all students' status to out.
        /// </summary>
        public void ResetAllStatus()
        {
            if (DateTime.Compare(SystemGlobalsModel.Instance.CurrentDate.Date, UTCConversionsBackend.UtcToKioskTime(DateTime.UtcNow).Date) == 0)
            {
                //Reset all Student Status to "Out"
                foreach (var item in Index())
                {
                    item.Status = StudentStatusEnum.Out;
                }

                SystemGlobalsModel.Instance.CurrentDate = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Update token
        /// </summary>
        /// <param name="id"></param>
        public void UpdateToken(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            var data = DataSource.Read(id);
            if (data == null)
            {
                return;
            }

            //get the list of new attendances, for which token amount has not been added yet.
            var newLogIns = data.Attendance.Where(m => m.IsNew);

            //for each new attendance, calculate effective duration and according collected tokens,
            //add to current tokens of the student.
            foreach (var attendance in newLogIns)
            {
                var effectiveDuration = CalculateEffectiveDuration(attendance);

                //todo: since hours attended is rounded up, need to prevent the case where consecutive check-ins in a short period
                //todo: of time could add 1 tokens everytime
                var collectedTokens = (int)Math.Ceiling(effectiveDuration.TotalHours);
                data.Tokens += collectedTokens;

                //mark it as old attendance
                attendance.IsNew = false;
            }

        }

        /// <summary>
        /// private helper method to calculate effective duration
        /// </summary>
        /// <param name="attendance"></param>
        /// <returns></returns>
        private TimeSpan CalculateEffectiveDuration(AttendanceModel attendance)
        {
            //the school day model, will use the school day's start time and end time later
            var schoolDay = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(UTCConversionsBackend.UtcToKioskTime(attendance.In));

            
            var start = schoolDay.TimeStart.Add(-SchoolDismissalSettingsBackend.Instance.GetDefault().EarlyWindow); //the time from which duration starts to count

            var end = schoolDay.TimeEnd.Add(SchoolDismissalSettingsBackend.Instance.GetDefault().LateWindow); //the time that duration counts until

            var myIn = UTCConversionsBackend.UtcToKioskTime(attendance.In).TimeOfDay; //check-in time
            TimeSpan myOut; //check-out time

            //if out is auto, use today's dismissal time
            if (attendance.Out == DateTime.MinValue)
            {             
                myOut = schoolDay.TimeEnd;
            }
            else //otherwise, use attendance's check-out time
            {
                myOut = UTCConversionsBackend.UtcToKioskTime(attendance.Out).TimeOfDay;
            }

            //trim the start time to actual arrive time only if the student is late
            if (myIn.CompareTo(start) > 0)
            {
                start = myIn;
            }
            //trim the end time to actual out time only if the student leave early
            if (myOut.CompareTo(end) < 0)
            {
                end = myOut;
            }

            var duration = end.Subtract(start);

            //If time-in is later than time out, just return 0
            if (duration < TimeSpan.Zero)
            {
                return TimeSpan.Zero;
            }

            return duration;
        }

        /// <summary>
        /// Helper function that resets the DataSource, and rereads it.
        /// </summary>
        public void Reset()
        {
            DataSource.Reset();
        }

        /// <summary>
        /// Returns the First Student in the system
        /// </summary>
        /// <returns>Null or valid data</returns>
        public StudentModel GetDefault()
        {
            var myReturn = DataSource.Index().First();
            return myReturn;
        }
    }
}