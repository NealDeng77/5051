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
        /// Generate the report for a Student
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public StudentReportViewModel GenerateStudentReport(StudentReportViewModel report)
        {
            // Generate attendance records
            if (GenerateAttendance(report) == false)
            {
                return null;
            }

            return report;
        }

        /// <summary>
        /// Walk the official school calendar and tally up what is expected
        /// </summary>
        private bool GenerateAttendance(StudentReportViewModel report)
        {
            //set student
            report.Student = StudentBackend.Instance.Read(report.StudentId);
            //set start date and end date
            report.DateStart = new DateTime(report.Year, report.Month, 1);
            report.DateEnd = new DateTime(report.Year, report.Month, DateTime.DaysInMonth(report.Year, report.Month));

            // Don't go beyond today, don't include today
            if (report.DateEnd.CompareTo(DateTime.UtcNow.Date) >= 0)
            {
                report.DateEnd = DateTime.UtcNow.Date.AddDays(-1);
            }

            var currentDate = report.DateStart;

            while (currentDate.CompareTo(report.DateEnd) < 0)
            {

                var temp = new AttendanceReportViewModel
                {
                    Date = currentDate
                };

                //get today's school calendar model
                var myToday = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(currentDate);

                // if the day is not a school day, set IsSchoolDay to false
                if (myToday == null || myToday.SchoolDay == false)
                {
                    temp.IsSchoolDay = false;
                }

                // if the day is a school day, perform calculations
                else
                {
                    // Find out if the student attended that day, and add that in.  Because the student can check in/out multiple times add them together.
                    // Todo: need to confirm: durations are accumulated, other stats are overwriten, i.e. only the last check in/out is effective
                    var myRange = report.Student.Attendance.Where(m => m.In.DayOfYear == currentDate.DayOfYear).ToList();

                    //if no attendance record on this day, set attendance status to absent
                    if (!myRange.Any())
                    {
                        temp.AttendanceStatus = AttendanceStatusEnum.AbsentUnexcused;
                        temp.HoursExpected = myToday.TimeDuration;
                        report.Stats.DaysAbsentUnexcused++;
                    }
                    else
                    {
                        foreach (var item in myRange)
                        {
                            var tempDuration = CalculateDurationAndInOutStatus(item, myToday, temp);

                            if (item.Status == StudentStatusEnum.In)
                            {
                                // Todo, refactor this rule based check out to a general location, and then call it when needed to force a checkout.
                                var myItemDefault = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(item.In);

                                // If the person is still checked in, and the day is over, then check them out.
                                if (item.In.DayOfYear <= DateTime.UtcNow.DayOfYear && myItemDefault.TimeEnd.Ticks < DateTime.UtcNow.Ticks)
                                {

                                    var myDate = item.In.ToShortDateString() + " " + myItemDefault.TimeEnd.ToString();
                                    //Add the current date of Item, with the end time for the default date, and return that back as a date time.
                                    item.Out = DateTime.Parse(myDate);
                                    item.Status = StudentStatusEnum.Out;

                                    // Log the student out as well.
                                    report.Student.Status = StudentStatusEnum.Out;

                                    // Update the change for that item be rewriting the student record back to the datastore
                                    DataSourceBackend.Instance.StudentBackend.Update(report.Student);
                                }
                                else
                                {
                                    // If the person is still checked in, and it is today, use now and add up till then.
                                    tempDuration = DateTime.UtcNow.Subtract(item.In);
                                }
                            }

                            temp.AttendanceStatus = AttendanceStatusEnum.Present;
                            temp.HoursExpected = myToday.TimeDuration;
                            temp.TimeIn = item.In;
                            temp.TimeOut = item.Out;
                            temp.HoursAttended += tempDuration;
                            temp.Emotion = item.Emotion;

                            CalculateDaysInOutStatus(temp, report.Stats);
                        }
                        temp.PercentAttended = (int)(temp.HoursAttended.TotalMinutes / temp.HoursExpected.TotalMinutes * 100);

                    }

                    report.Stats.NumOfSchoolDays++;

                    report.Stats.AccumlatedTotalHoursExpected += temp.HoursExpected;
                    report.Stats.AccumlatedTotalHours += temp.HoursAttended;

                    // Need to add the totals back to the temp, because the temp is new each iteration
                    temp.TotalHoursExpected += report.Stats.AccumlatedTotalHoursExpected;
                    temp.TotalHours = report.Stats.AccumlatedTotalHours;
                }

                report.AttendanceList.Add(temp);
                currentDate = currentDate.AddDays(1);
            }

            if (report.Stats.NumOfSchoolDays > 0)
            {
                report.Stats.PercPresent = report.Stats.DaysPresent * 100 / report.Stats.NumOfSchoolDays;
                report.Stats.PercAttendedHours =
                    (int)(report.Stats.AccumlatedTotalHours.TotalHours * 100 / report.Stats.AccumlatedTotalHoursExpected.TotalHours);
                report.Stats.PercExcused = report.Stats.DaysAbsentExcused * 100 / report.Stats.NumOfSchoolDays;
                report.Stats.PercUnexcused = report.Stats.DaysAbsentUnexcused * 100 / report.Stats.NumOfSchoolDays;
                report.Stats.PercInLate = report.Stats.DaysLate * 100 / report.Stats.NumOfSchoolDays;
                report.Stats.PercOutEarly = report.Stats.DaysOutEarly * 100 / report.Stats.NumOfSchoolDays;
            }

            return true;
        }

        private void CalculateDaysInOutStatus(AttendanceReportViewModel temp, StudentReportStatsModel stats)
        {
            stats.DaysPresent++;

            if (temp.CheckInStatus == CheckInStatusEnum.ArriveOnTime)
            {
                stats.DaysOnTime++;
            }

            stats.DaysLate = stats.DaysPresent - stats.DaysOnTime;

            if (temp.CheckOutStatus == CheckOutStatusEnum.DoneAuto)
            {
                stats.DaysOutAuto++;
            }

            stats.DaysOutEarly = stats.DaysPresent - stats.DaysOutAuto;
        }

        /// <summary>
        /// Calculate the effective duration, in/out status of the given attendance record
        /// </summary>
        /// <param name="attendance"></param>
        /// <param name="schoolDay"></param>
        /// <returns></returns>
        private TimeSpan CalculateDurationAndInOutStatus(AttendanceModel attendance, SchoolCalendarModel schoolDay, AttendanceReportViewModel attendanceReport)
        {
            var start = schoolDay.TimeStart;
            var end = schoolDay.TimeEnd;



            if (attendance.In.TimeOfDay.CompareTo(schoolDay.TimeStart) > 0)
            {
                start = attendance.In.TimeOfDay;
                attendanceReport.CheckInStatus = CheckInStatusEnum.ArriveLate;
            }
            else
            {
                attendanceReport.CheckInStatus = CheckInStatusEnum.ArriveOnTime;
            }
            if (attendance.Out.TimeOfDay.CompareTo(schoolDay.TimeEnd) < 0)
            {
                end = attendance.Out.TimeOfDay;
                attendanceReport.CheckOutStatus = CheckOutStatusEnum.DoneEarly;
            }
            else
            {
                attendanceReport.CheckOutStatus = CheckOutStatusEnum.DoneAuto;
            }

            return end.Subtract(start);
        }

        /// <summary>
        /// Walk the Dates between the Start and End, and only keep the ones to show.
        /// </summary>
        private void GenerateDateRange(StudentReportViewModel Report)
        {
            //var accumulativeHoursAttended = new TimeSpan();
            //var accumulativeHoursExpected = new TimeSpan();

            //accumulativeHoursAttended = TimeSpan.Zero;
            //accumulativeHoursExpected = TimeSpan.Zero;

            //// Pull out just the date range between Start and End
            //var myData = Report.AttendanceList.Where(m => m.Date.CompareTo(Report.DateStart.AddDays(-1)) > 0 && m.Date.CompareTo(Report.DateEnd.AddDays(1)) < 1).ToList();

            //// Tally up the actual hours
            //foreach (var item in myData)
            //{
            //    accumulativeHoursAttended += item.HoursAttended;
            //    accumulativeHoursExpected += item.HoursExpected;

            //    // Need to reset the values to reflect the date range
            //    item.TotalHours = accumulativeHoursAttended;
            //    item.TotalHoursExpected = accumulativeHoursExpected;
            //}

            ////Trim the AttendanceList down to be just the MyData list
            //Report.AttendanceList = myData;

            //Report.Stats.AccumlatedTotalHours = accumulativeHoursAttended;
            //Report.Stats.AccumlatedTotalHoursExpected = accumulativeHoursExpected;
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
            //if (Report.AttendanceList.Count != 0 && !Report.AttendanceList.Any())
            //{
            //    foreach (var item in Report.AttendanceList)
            //    {
            //        // Count up the Data Totals for Excused, Present, Unexcused
            //        switch (item.AttendanceStatus)
            //        {
            //            case AttendanceStatusEnum.AbsentExcused:
            //                Report.Stats.DaysAbsentExcused++;
            //                break;
            //            case AttendanceStatusEnum.AbsentUnexcused:
            //                Report.Stats.DaysAbsentUnexcused++;
            //                break;

            //            case AttendanceStatusEnum.Present:
            //                Report.Stats.DaysPresent++;
            //                break;
            //        }

            //        if (item.CheckInStatus == CheckInStatusEnum.ArriveLate)
            //        {
            //            Report.Stats.DaysLateStayed++;
            //            Report.Stats.DaysLate++;
            //        }
            //        if (item.CheckOutStatus == CheckOutStatusEnum.DoneAuto)
            //        {
            //            Report.Stats.DaysLateLeft++;
            //            Report.Stats.DaysLeftEarly++;
            //        }
            //        if (item.CheckOutStatus == CheckOutStatusEnum.DoneEarly)
            //        {
            //            Report.Stats.DaysOnTimeLeft++;
            //            Report.Stats.DaysOnTime++;
            //        }
            //    }

            //    Report.Stats.DaysPresent = Report.Student.Attendance.Count - Report.Stats.DaysAbsentExcused - Report.Stats.DaysAbsentUnexcused;

            //    Report.Stats.DaysOnTime = Report.Stats.DaysPresent - Report.Stats.DaysLate;
            //    Report.Stats.DaysStayed = Report.Stats.DaysPresent - Report.Stats.DaysLeftEarly;

            //    Report.Stats.TotalHoursAttended = Report.Stats.AccumlatedTotalHours.TotalDays;
            //    Report.Stats.TotalHoursMissing = Report.Stats.AccumlatedTotalHoursExpected.Subtract(Report.Stats.AccumlatedTotalHours).TotalDays;

            //    Report.Stats.PercPresent = 100 * Report.Stats.DaysPresent / Report.Student.Attendance.Count;
            //    Report.Stats.PercExcused = 100 * Report.Stats.DaysAbsentExcused / Report.Student.Attendance.Count;
            //    Report.Stats.PercUnexcused = 100 * Report.Stats.DaysAbsentUnexcused / Report.Student.Attendance.Count;
            //    Report.Stats.PercAttendedHours = (int)(100 * Report.Stats.TotalHoursAttended / (Report.Stats.TotalHoursMissing + Report.Stats.TotalHoursAttended));
            //}
        }
    }
}