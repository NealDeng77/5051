﻿using System;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// StudentBackend helper functions
    /// </summary>
    public class StudentBackendHelper
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentBackendHelper instance;
        private static object syncRoot = new Object();

        private StudentBackendHelper() { }

        public static StudentBackendHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentBackendHelper();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Create demo student
        /// </summary>
        public void CreateStudent()
        {
            StudentBackend.Instance.Create(new StudentModel("Mike", null));
            StudentBackend.Instance.Create(new StudentModel("Doug", null));
            StudentBackend.Instance.Create(new StudentModel("Jea", null));
            StudentBackend.Instance.Create(new StudentModel("Sue", null));
            StudentBackend.Instance.Create(new StudentModel("Stan", null));
        }

        /// <summary>
        /// Create demo attendance
        /// </summary>
        public void CreateAttendance()
        {
            DateTime dateStart = new DateTime();
            DateTime dateEnd = new DateTime();

            //Set the range to be from the first day of school to the last day of school
            dateStart = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst;
            dateEnd = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast;

            //Don't generate attendance for today and future days
            DateTime yesterday = DateTime.UtcNow.AddDays(-1);
            if (yesterday.CompareTo(dateEnd) < 0)
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
            var myStudent = StudentBackend.Instance.Index()[index];

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
    }
}



