using System;
using System.Collections.Generic;
using System.Linq;

using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// The Full set of Reports for a single Student
    /// </summary>
    public class StudentReportViewModel
    {

        /// <summary>
        /// The Student record
        /// </summary>
        public StudentModel Student { get; set; }

        /// <summary>
        /// Date Start passed in for the filter for the report
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Date end passed in for the filter for the report
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// The attendance Record for each date to show on the report
        /// </summary>
        public List<AttendanceReportViewModel> AttendanceList { get; set; }

        /// <summary>
        /// The single value for the total hours from school start till now attended
        /// </summary>
        public TimeSpan AccumlatedTotalHours { get; set; }

        /// <summary>
        /// The single value for the total hours from school start till now expected to attend
        /// </summary>
        public TimeSpan AccumlatedTotalHoursExpected { get; set; }


        public int DaysPresent { get; set; }
        public int DaysAbsentExcused { get; set; }
        public int DaysAbsentUnexcused { get; set; }

        public double TotalHoursAttended { get; set; }
        public double TotalHoursMissing { get; set; }

        public int DaysOnTime { get; set; }
        public int DaysLate { get; set; }
        public int DaysStayed { get; set; }
        public int DaysLeftEarly { get; set; }
        public int DaysOnTimeStayed { get; set; }
        public int DaysOnTimeLeft { get; set; }
        public int DaysLateStayed { get; set; }
        public int DaysLateLeft { get; set; }
        public int PercPresent { get; set; }
        public int PercAttendedHours { get; set; }
        public int PercExcused { get; set; }
        public int PercUnexcused { get; set; }

        public StudentReportViewModel(StudentModel student, DateTime dateStart, DateTime dateEnd)
        {
            Student = student;
            DateStart = dateStart;
            DateEnd = dateEnd;

            AttendanceList = new List<AttendanceReportViewModel>();

            Calculate();
        }

        private void Calculate()
        {
            CalculateAttendance();
            CalculateDateRange();
            CalculateHoursAttended();
            CalculateOther();
        }

        /// <summary>
        /// Walk the official school calendar and tally up what is expected
        /// </summary>
        private void CalculateAttendance()
        {
            DateTime currentDate = new DateTime();
            DateTime dateStart = new DateTime();
            DateTime dateEnd = new DateTime();

            dateStart = DateTime.Parse("09/01/2017"); //Todo swap out with a data structure that models the school calendar
            dateEnd = DateTime.Parse("07/01/2018"); //Todo swap out with a data structure that models the school calendar

            // Don't go beyond today
            if (dateEnd.CompareTo(DateTime.UtcNow)>0)
            {
                dateEnd = DateTime.UtcNow;
            }

            // Reset the values to be 0, then add to them.
            AccumlatedTotalHoursExpected = TimeSpan.Zero;
            AccumlatedTotalHours = TimeSpan.Zero;

            currentDate = dateStart;
            while (currentDate.CompareTo(dateEnd) < 0)
            {
                var temp = new AttendanceReportViewModel();
                temp.Date = currentDate;

                temp.HoursExpected = TimeSpan.Parse("5:00");  //Todo, replace with actual hours from school calendar

                // Find out if the student attended that day, and add that in.  Because the student can check in/out multiple times add them together.
                var myRange = Student.Attendance.Where(m => m.In.DayOfYear == currentDate.DayOfYear).ToList();
                foreach (var item in myRange)
                {
                    temp.HoursAttended += item.Duration;
                }

                AccumlatedTotalHoursExpected += temp.HoursExpected;
                AccumlatedTotalHours += temp.HoursAttended;

                // Need to add the totals back to the temp, because the temp is new each iteration
                temp.TotalHoursExpected += AccumlatedTotalHoursExpected;
                temp.TotalHours = AccumlatedTotalHours;

                AttendanceList.Add(temp);

                // Look to the next day
                currentDate = currentDate.AddDays(1);
            }
        }

        /// <summary>
        /// Walk the Dates between the Start and End, and only keep the ones to show.
        /// </summary>
        private void CalculateDateRange()
        {
            var accumulativeHoursAttended = new TimeSpan();
            var accumulativeHoursExpected = new TimeSpan();

            accumulativeHoursAttended = TimeSpan.Zero;
            accumulativeHoursExpected = TimeSpan.Zero;

            // Pull out just the date range between Start and End
            var myData = AttendanceList.Where(m => m.Date.CompareTo(DateStart.AddDays(-1)) > 0 && m.Date.CompareTo(DateEnd.AddDays(1)) < 1).ToList();

            // Tally up the actual hours
            foreach (var item in myData)
            {
                accumulativeHoursAttended += item.HoursAttended;
                accumulativeHoursExpected += item.HoursExpected;

                // Need to reset the values to reflect the date range
                item.TotalHours = accumulativeHoursAttended;
                item.TotalHoursExpected = accumulativeHoursExpected;
            }

            //Trim the AttendanceList down to be just the MyData list
            AttendanceList = myData;

            AccumlatedTotalHours = accumulativeHoursAttended;
            AccumlatedTotalHoursExpected = accumulativeHoursExpected;
        }

        private void CalculateHoursAttended()
        {

            //foreach (var item in Student.Attendance)
            //{
            //    accumulativeHoursAttended += item.Duration;
            //}

            //for (int i = 0; i < Student.Attendance.Count; i++)
            //{
            //    //AttendanceModel att = Student.Attendance[i];
            //    double hours = 0;
            //    double hoursExpected = 0;
            //    //for (int j = 0; j < att.Student.AttendanceCheckIns.Count; j++)
            //    //{
            //    //    Student.AttendanceCheckInModel checkIn = att.Student.AttendanceCheckIns[j];
            //    //    hours += checkIn.CheckOut.Subtract(checkIn.CheckIn).TotalHours;

            //    //}
            //    //hoursExpected = Backend.SchoolDayBackend.Instance.Read(att.SchoolDayId).ExpectedHours.TotalHours;
            //    //accumulativeHoursAttended += hours;
            //    //accumulativeHoursExpected += hoursExpected;
            //    //Date.Add(Backend.SchoolDayBackend.Instance.Read(att.SchoolDayId).Date.ToString("dd"));
            //    HoursAttended.Add(hours);
            //    HoursExpected.Add(hoursExpected);
            //    AccumulativeHoursAttended.Add(accumulativeHoursAttended.TotalHours);
            //    AccumulativeHoursExpected.Add(accumulativeHoursExpected.TotalHours);
            //}
        }

        private void CalculateOther()
        {
            if (Student.Attendance.Count != 0)
            {
                foreach (var item in Student.Attendance)
                {
                    // Count up the Data Totals for Excused, Present, Unexcused
                    switch (item.AttendanceStatus)
                    {
                        case AttendanceStatusEnum.AbsentExcused:
                            DaysAbsentExcused++;
                            break;
                        case AttendanceStatusEnum.AbsentUnexcused:
                            DaysAbsentUnexcused++;
                            break;

                        case AttendanceStatusEnum.Present:
                            DaysPresent++;
                            break;
                    }

                    if (item.CheckInStatus == CheckInStatusEnum.ArriveLate)
                    {
                        DaysLateStayed++;
                        DaysLate++;
                    }
                    if (item.CheckOutStatus == CheckOutStatusEnum.DoneAuto)
                    {
                        DaysLateLeft++;
                        DaysLeftEarly++;
                    }
                    if (item.CheckOutStatus == CheckOutStatusEnum.DoneEarly)
                    {
                        DaysOnTimeLeft++;
                        DaysOnTime++;
                    }
                }

                DaysPresent = Student.Attendance.Count - DaysAbsentExcused - DaysAbsentUnexcused;

                DaysOnTime = DaysPresent - DaysLate;
                DaysStayed = DaysPresent - DaysLeftEarly;

                TotalHoursAttended = AccumlatedTotalHours.TotalDays;
                TotalHoursMissing = AccumlatedTotalHoursExpected.Subtract(AccumlatedTotalHours).TotalDays;

                PercPresent = 100 * DaysPresent / Student.Attendance.Count;
                PercExcused = 100 * DaysAbsentExcused / Student.Attendance.Count;
                PercUnexcused = 100 * DaysAbsentUnexcused / Student.Attendance.Count;
                PercAttendedHours = (int)(100 * TotalHoursAttended / (TotalHoursMissing + TotalHoursAttended));
            }

        }
    }

}
