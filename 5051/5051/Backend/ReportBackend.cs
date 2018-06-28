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

        /// <summary>
        ///  Generate the Report fo a Student
        /// </summary>
        /// <param name="student"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns>The Student Report View Model</returns>
        public StudentReportViewModel GenerateStudentReport(StudentModel student, DateTime dateStart, DateTime dateEnd)
        {
            // Hold the current report
            StudentReportViewModel Report = new StudentReportViewModel();

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
            if (GenerateAttendance(Report) == false)
            {
                return null;
            }

            GenerateDateRange(Report);
            GenerateHoursAttended(Report);
            GenerateOther(Report);

            return Report;
        }

        /// <summary>
        /// Walk the official school calendar and tally up what is expected
        /// </summary>
        private bool GenerateAttendance(StudentReportViewModel Report)
        {
            DateTime currentDate = new DateTime();

            // Don't go beyond today
            if (Report.DateEnd.CompareTo(DateTime.UtcNow) > 0)
            {
                Report.DateEnd = DateTime.UtcNow;
            }

            // Reset the values to be 0, then add to them.
            Report.Stats.AccumlatedTotalHoursExpected = TimeSpan.Zero;
            Report.Stats.AccumlatedTotalHours = TimeSpan.Zero;

            currentDate = Report.DateStart;

            // Set current date to be 1 less, because will get added in the for loop
            currentDate = currentDate.AddDays(-1);

            while (currentDate.CompareTo(Report.DateEnd) < 0)
            {
                // Look to the next day
                currentDate = currentDate.AddDays(1);

                var temp = new AttendanceReportViewModel();
                temp.Date = currentDate;

                var myToday = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(currentDate);

                // if the day is not a school day, set IsSchoolDay to false
                if (myToday == null || myToday.SchoolDay == false)
                {
                    temp.IsSchoolDay = false;
                }

                // if the day is a school day, perform calculations
                else
                {

                    temp.HoursExpected = myToday.TimeDuration;

                    // Find out if the student attended that day, and add that in.  Because the student can check in/out multiple times add them together.
                    var myRange = Report.Student.Attendance.Where(m => m.In.DayOfYear == currentDate.DayOfYear).ToList();

                    foreach (var item in myRange)
                    {
                        var tempDuration = item.Duration;
                        //if (item.Status == StudentStatusEnum.In)
                        //{
                        //    // Todo, refactor this rule based check out to a general location, and then call it when needed to force a checkout.
                        //    var myItemDefault = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(item.In);

                        //    // If the person is still checked in, and the day is over, then check them out.
                        //    if (item.In.DayOfYear <= DateTime.UtcNow.DayOfYear && myItemDefault.TimeEnd.Ticks < DateTime.UtcNow.Ticks)
                        //    {

                        //        var myDate = item.In.ToShortDateString() + " " + myItemDefault.TimeEnd.ToString();
                        //        //Add the current date of Item, with the end time for the default date, and return that back as a date time.
                        //        item.Out = DateTime.Parse(myDate);
                        //        item.Status = StudentStatusEnum.Out;
                        //        item.Duration = item.Out.Subtract(item.In);

                        //        // Log the student out as well.
                        //        Report.Student.Status = StudentStatusEnum.Out;

                        //        // Update the change for that item be rewriting the student record back to the datastore
                        //        DataSourceBackend.Instance.StudentBackend.Update(Report.Student);
                        //    }
                        //    else
                        //    {
                        //        // If the person is still checked in, and it is today, use now and add up till then.
                        //        tempDuration = DateTime.UtcNow.Subtract(item.In);
                        //    }
                        //}
                        temp.TimeIn = item.In;
                        temp.TimeOut = item.Out;
                        temp.HoursAttended += tempDuration;
                        temp.PercentAttended = (int)(temp.HoursAttended.TotalMinutes / temp.HoursExpected.TotalMinutes * 100);
                        temp.AttendanceStatus = item.AttendanceStatus;
                        temp.CheckInStatus = item.CheckInStatus;
                        temp.CheckOutStatus = item.CheckOutStatus;
                    }

                    Report.Stats.AccumlatedTotalHoursExpected += temp.HoursExpected;
                    Report.Stats.AccumlatedTotalHours += temp.HoursAttended;

                    // Need to add the totals back to the temp, because the temp is new each iteration
                    temp.TotalHoursExpected += Report.Stats.AccumlatedTotalHoursExpected;
                    temp.TotalHours = Report.Stats.AccumlatedTotalHours;
                }

                Report.AttendanceList.Add(temp);
            }

            return true;
        }

        /// <summary>
        /// Walk the Dates between the Start and End, and only keep the ones to show.
        /// </summary>
        private void GenerateDateRange(StudentReportViewModel Report)
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

        private void GenerateHoursAttended(StudentReportViewModel Report)
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

        private void GenerateOther(StudentReportViewModel Report)
        {
            if (Report.AttendanceList.Count != 0 && !Report.AttendanceList.Any())
            {
                foreach (var item in Report.AttendanceList)
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