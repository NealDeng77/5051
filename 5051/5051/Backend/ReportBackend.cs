using System;
using System.Linq;

using _5051.Models.Enums;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// Manages the Reports for Student and Admin
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
        /// Generate monthly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public StudentReportViewModel GenerateMonthlyReport(StudentReportViewModel report)
        {
            //set student
            report.Student = StudentBackend.Instance.Read(report.StudentId);

            //set start date and end date
            report.DateStart = new DateTime(report.Year, report.Month, 1);
            report.DateEnd = new DateTime(report.Year, report.Month, DateTime.DaysInMonth(report.Year, report.Month));


            GenerateReportFromStartToEnd(report);

            return report;
        }
        /// <summary>
        /// Generate overall report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public StudentReportViewModel GenerateOverallReport(StudentReportViewModel report)
        {
            //set student
            report.Student = StudentBackend.Instance.Read(report.StudentId);

            //set start date and end date
            report.DateStart = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst;
            report.DateEnd = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast;


            GenerateReportFromStartToEnd(report);

            return report;
        }

        /// <summary>
        /// Generate the report from the start date to the end date
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        private StudentReportViewModel GenerateReportFromStartToEnd(StudentReportViewModel report)
        {
            // Don't go beyond today, don't include today
            if (report.DateEnd.CompareTo(DateTime.UtcNow.Date) >= 0)
            {
                report.DateEnd = DateTime.UtcNow.Date.AddDays(-1);
            }

            var currentDate = report.DateStart;

            while (currentDate.CompareTo(report.DateEnd) < 0)
            {
                //create a new AttendanceReportViewmodel for each day
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
                    temp.HoursExpected = myToday.TimeDuration;
                    // Find out if the student attended that day, and add that in.  Because the student can check in/out multiple times add them together.
                    // Todo: need to confirm: durations are accumulated, other stats are overwriten, i.e. only the last check in/out is effective
                    var myRange = report.Student.Attendance.Where(m => m.In.DayOfYear == currentDate.DayOfYear).ToList();

                    //if no attendance record on this day, set attendance status to absent
                    if (!myRange.Any())
                    {
                        temp.AttendanceStatus = AttendanceStatusEnum.AbsentUnexcused;
                        report.Stats.DaysAbsentUnexcused++;
                    }
                    else
                    {
                        //loop through all attendance records in my range
                        foreach (var item in myRange)
                        {
                            //calculations for each attendance record

                            //calculate effective duration
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
                            temp.TimeIn = item.In;
                            temp.TimeOut = item.Out;
                            temp.HoursAttended += tempDuration;
                            temp.Emotion = item.Emotion;
                            CalculateDaysInOutStats(temp, report.Stats);
                        }

                        //calculations for present records
                        temp.PercentAttended = (int)(temp.HoursAttended.TotalMinutes * 100 / temp.HoursExpected.TotalMinutes);

                    }
                    //calculations for both absent and present records                    
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

            //if there is at least one school days in this report, calculate the following stats
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

            return report;
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

            //trim the effective start time to actual arrive time only if the student is late
            if (attendance.In.TimeOfDay.CompareTo(schoolDay.TimeStart) > 0)
            {
                start = attendance.In.TimeOfDay;
                attendanceReport.CheckInStatus = CheckInStatusEnum.ArriveLate;
            }
            else
            {
                attendanceReport.CheckInStatus = CheckInStatusEnum.ArriveOnTime;
            }
            //trim the effective end time to actual out time only if the student leave early
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
        /// Calculate the stats about days in/out
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="stats"></param>
        private void CalculateDaysInOutStats(AttendanceReportViewModel temp, StudentReportStatsModel stats)
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
    }
}