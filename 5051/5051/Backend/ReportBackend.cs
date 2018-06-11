using System;
using System.Linq;

using _5051.Models.Enums;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// Manages the Reports for Student and Admin
    ///  Set the Student, Date Start, Date End
    ///  Then call to Generate the report
    /// </summary>

    public class ReportBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile ReportBackend instance;
        private static object syncRoot = new Object();

        private ReportBackend() { }

        public static ReportBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ReportBackend();
                        }
                    }
                }

                return instance;
            }
        }

        // Hold the current report
        private StudentReportViewModel Report = new StudentReportViewModel();
        
        /// <summary>
        ///  Generate the Report fo a Student
        /// </summary>
        /// <param name="student"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns>The Student Report View Model</returns>
        public StudentReportViewModel GenerateStudentReport(StudentModel student, DateTime dateStart, DateTime dateEnd)
        {
            // Confirm a student was passed in
            if (student == null)
            {
                return null;
            }

            // Set the Student and date range
            Report.Student = student;
            Report.DateStart = dateStart;
            Report.DateEnd = dateEnd;

            // Call for the report to be generated
            GenerateAttendance();
            GenerateDateRange();
            GenerateHoursAttended();
            GenerateOther();

            return Report;
        }

        /// <summary>
        /// Walk the official school calendar and tally up what is expected
        /// </summary>
        private void GenerateAttendance()
        {
            DateTime currentDate = new DateTime();
            DateTime dateStart = new DateTime();
            DateTime dateEnd = new DateTime();

            dateStart = DateTime.Parse("09/01/2017"); //Todo swap out with a data structure that models the school calendar
            dateEnd = DateTime.Parse("07/01/2018"); //Todo swap out with a data structure that models the school calendar

            // Don't go beyond today
            if (dateEnd.CompareTo(DateTime.UtcNow) > 0)
            {
                dateEnd = DateTime.UtcNow;
            }

            // Reset the values to be 0, then add to them.
            Report.Stats.AccumlatedTotalHoursExpected = TimeSpan.Zero;
            Report.Stats.AccumlatedTotalHours = TimeSpan.Zero;

            currentDate = dateStart;
            while (currentDate.CompareTo(dateEnd) < 0)
            {
                var temp = new AttendanceReportViewModel();
                temp.Date = currentDate;

                temp.HoursExpected = TimeSpan.Parse("5:00");  //Todo, replace with actual hours from school calendar

                // Find out if the student attended that day, and add that in.  Because the student can check in/out multiple times add them together.
                var myRange = Report.Student.Attendance.Where(m => m.In.DayOfYear == currentDate.DayOfYear).ToList();
                foreach (var item in myRange)
                {
                    temp.HoursAttended += item.Duration;
                }

                Report.Stats.AccumlatedTotalHoursExpected += temp.HoursExpected;
                Report.Stats.AccumlatedTotalHours += temp.HoursAttended;

                // Need to add the totals back to the temp, because the temp is new each iteration
                temp.TotalHoursExpected += Report.Stats.AccumlatedTotalHoursExpected;
                temp.TotalHours = Report.Stats.AccumlatedTotalHours;

                Report.AttendanceList.Add(temp);

                // Look to the next day
                currentDate = currentDate.AddDays(1);
            }
        }

        /// <summary>
        /// Walk the Dates between the Start and End, and only keep the ones to show.
        /// </summary>
        private void GenerateDateRange()
        {
            var accumulativeHoursAttended = new TimeSpan();
            var accumulativeHoursExpected = new TimeSpan();

            accumulativeHoursAttended = TimeSpan.Zero;
            accumulativeHoursExpected = TimeSpan.Zero;

            // Pull out just the date range between Start and End
            var myData = Report.AttendanceList.Where(m => m.Date.CompareTo(Report.DateStart.AddDays(-1)) > 0 && m.Date.CompareTo(Report.DateEnd.AddDays(1)) < 1).ToList();

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
            Report.AttendanceList = myData;

            Report.Stats.AccumlatedTotalHours = accumulativeHoursAttended;
            Report.Stats.AccumlatedTotalHoursExpected = accumulativeHoursExpected;
        }

        private void GenerateHoursAttended()
        {

            //foreach (var item in Report.Student.Attendance)
            //{
            //    accumulativeHoursAttended += item.Duration;
            //}

            //for (int i = 0; i < Report.Student.Attendance.Count; i++)
            //{
            //    //AttendanceModel att = Report.Student.Attendance[i];
            //    double hours = 0;
            //    double hoursExpected = 0;
            //    //for (int j = 0; j < att.Report.Student.AttendanceCheckIns.Count; j++)
            //    //{
            //    //    Report.Student.AttendanceCheckInModel checkIn = att.Report.Student.AttendanceCheckIns[j];
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

        private void GenerateOther()
        {
            if (Report.Student.Attendance.Count != 0)
            {
                foreach (var item in Report.Student.Attendance)
                {
                    // Count up the Data Totals for Excused, Present, Unexcused
                    switch (item.AttendanceStatus)
                    {
                        case AttendanceStatusEnum.AbsentExcused:
                            Report.Stats.DaysAbsentExcused++;
                            break;
                        case AttendanceStatusEnum.AbsentUnexcused:
                            Report.Stats.DaysAbsentUnexcused++;
                            break;

                        case AttendanceStatusEnum.Present:
                            Report.Stats.DaysPresent++;
                            break;
                    }

                    if (item.CheckInStatus == CheckInStatusEnum.ArriveLate)
                    {
                        Report.Stats.DaysLateStayed++;
                        Report.Stats.DaysLate++;
                    }
                    if (item.CheckOutStatus == CheckOutStatusEnum.DoneAuto)
                    {
                        Report.Stats.DaysLateLeft++;
                        Report.Stats.DaysLeftEarly++;
                    }
                    if (item.CheckOutStatus == CheckOutStatusEnum.DoneEarly)
                    {
                        Report.Stats.DaysOnTimeLeft++;
                        Report.Stats.DaysOnTime++;
                    }
                }

                Report.Stats.DaysPresent = Report.Student.Attendance.Count - Report.Stats.DaysAbsentExcused - Report.Stats.DaysAbsentUnexcused;

                Report.Stats.DaysOnTime = Report.Stats.DaysPresent - Report.Stats.DaysLate;
                Report.Stats.DaysStayed = Report.Stats.DaysPresent - Report.Stats.DaysLeftEarly;

                Report.Stats.TotalHoursAttended = Report.Stats.AccumlatedTotalHours.TotalDays;
                Report.Stats.TotalHoursMissing = Report.Stats.AccumlatedTotalHoursExpected.Subtract(Report.Stats.AccumlatedTotalHours).TotalDays;

                Report.Stats.PercPresent = 100 * Report.Stats.DaysPresent / Report.Student.Attendance.Count;
                Report.Stats.PercExcused = 100 * Report.Stats.DaysAbsentExcused / Report.Student.Attendance.Count;
                Report.Stats.PercUnexcused = 100 * Report.Stats.DaysAbsentUnexcused / Report.Student.Attendance.Count;
                Report.Stats.PercAttendedHours = (int)(100 * Report.Stats.TotalHoursAttended / (Report.Stats.TotalHoursMissing + Report.Stats.TotalHoursAttended));
            }

        }
    }
}