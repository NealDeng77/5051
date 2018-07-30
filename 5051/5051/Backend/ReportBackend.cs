using System;
using System.Linq;

using _5051.Models.Enums;
using _5051.Models;
using System.Collections.Generic;
using System.Web.Mvc;

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

        #region GenerateWeeklyReportRegion
        /// <summary>
        /// Generate Weekly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public WeeklyReportViewModel GenerateWeeklyReport(WeeklyReportViewModel report)
        {
            //set student
            report.Student = StudentBackend.Instance.Read(report.StudentId);

            var dayFirst = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst;
            var dayLast = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast;
            //todo: convert this to kiosk timezone here
            var dayNow = UTCConversionsBackend.UtcToKioskTime(DateTime.UtcNow).Date;

            //The first valid week(Monday's date) for the dropdown
            var FirstWeek = dayFirst.AddDays(-((dayNow.DayOfWeek - DayOfWeek.Monday + 7) % 7)); //added this mod operation to make sure it's the previous monday not the next monday
            //The last valid month for the dropdown
            var LastWeek = dayLast.AddDays(-((dayNow.DayOfWeek - DayOfWeek.Monday + 7) % 7));
            //The month of today
            var WeekNow = dayNow.AddDays(-((dayNow.DayOfWeek - DayOfWeek.Monday + 7) % 7)); //if today is sunday, dayNow.DayOfWeek - DayOfWeek.Monday = -1

            //do not go beyond the week of today
            if (LastWeek > WeekNow)
            {
                LastWeek = WeekNow;
            }

            //Set the current week (loop variable) to the last valid week
            var currentWeek = LastWeek;


            //initialize the dropdownlist
            report.Weeks = new List<SelectListItem>();

            // the week id
            int weekId = 1;

            //loop backwards in time so that the week select list items are in time reversed order
            while (currentWeek >= FirstWeek)
            {
                //the friday's date of the current week
                var currentWeekFriday = currentWeek.AddDays(4);

                //make a list item for the current week
                var week = new SelectListItem { Value = "" + weekId, Text = "" + currentWeek.ToShortDateString() + " to " + currentWeekFriday.ToShortDateString() };

                //add to the select list
                report.Weeks.Add(week);

                //if current week is the selected month, set the start date and end date for this report
                if (weekId == report.SelectedWeekId)
                {
                    //set start date and end date
                    report.DateStart = currentWeek;
                    report.DateEnd = currentWeekFriday;
                }

                weekId++;
                currentWeek = currentWeek.AddDays(-7);

            }


            //Generate report for this month
            GenerateReportFromStartToEnd(report);

            return report;
        }


        #endregion
        #region GenerateMonthlyReportRegion
        /// <summary>
        /// Generate monthly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public MonthlyReportViewModel GenerateMonthlyReport(MonthlyReportViewModel report)
        {
            //set student
            report.Student = StudentBackend.Instance.Read(report.StudentId);

            //
            var dayFirst = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst;
            var dayLast = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast;
            var dayNow = UTCConversionsBackend.UtcToKioskTime(DateTime.UtcNow).Date;

            //The first valid month for the dropdown
            var monthFirst = new DateTime(dayFirst.Year, dayFirst.Month, 1);
            //The last valid month for the dropdown
            var monthLast = new DateTime(dayLast.Year, dayLast.Month, 1);
            //The month of today
            var monthNow = new DateTime(dayNow.Year, dayNow.Month, 1);

            //do not go beyond the month of today
            if (monthLast > monthNow)
            {
                monthLast = monthNow;
            }

            //Set the current month (loop variable) to the last valid month
            var currentMonth = monthLast;

            //initialize the dropdownlist
            report.Months = new List<SelectListItem>();

            // the month id
            int monthId = 1;

            //loop backwards in time so that the month select list items are in time reversed order
            while (currentMonth >= monthFirst)
            {
                //make a list item for the current month
                var month = new SelectListItem { Value = "" + monthId, Text = currentMonth.ToString("MMMM yyyy") };

                //add to the select list
                report.Months.Add(month);

                //if current month is the selected month, set the start date and end date for this report
                if (monthId == report.SelectedMonthId)
                {
                    //set start date and end date
                    report.DateStart = currentMonth;
                    report.DateEnd = new DateTime(currentMonth.Year, currentMonth.Month, DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month));
                }

                monthId++;
                currentMonth = currentMonth.AddMonths(-1);

            }


            //Generate report for this month
            GenerateReportFromStartToEnd(report);

            return report;
        }
        #endregion
        #region GenerateSemesterReportRegion
        
        /// <summary>
        /// Generate Weekly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public SemesterReportViewModel GenerateSemesterReport(SemesterReportViewModel report)
        {
            //set student
            report.Student = StudentBackend.Instance.Read(report.StudentId);

            report.Semesters = new List<SelectListItem>();

            //todo: remove hardcoding
            var spring = new SelectListItem { Value = "1", Text = "Spring semester 2018" };

            //add to the select list
            report.Semesters.Add(spring);

            if (report.SelectedSemesterId == 1)
            {
                report.DateStart = SchoolDismissalSettingsBackend.Instance.GetDefault().SpringFirstClassDay;
                report.DateEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().SpringLastClassDay;
            }

            var fall = new SelectListItem { Value = "2", Text = "Fall semester 2017" };

            //add to the select list
            report.Semesters.Add(fall);

            if (report.SelectedSemesterId == 2)
            {
                report.DateStart = SchoolDismissalSettingsBackend.Instance.GetDefault().FallFirstClassDay;
                report.DateEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().FallLastClassDay;
            }

 

            //Generate report for this semester
            GenerateReportFromStartToEnd(report);

            return report;
        }


        #endregion
        #region GenerateQuarterReportRegion
        /// <summary>
        /// Generate Weekly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public QuarterReportViewModel GenerateQuarterReport(QuarterReportViewModel report)
        {
            //set student
            report.Student = StudentBackend.Instance.Read(report.StudentId);

            report.Quarters = new List<SelectListItem>();

            //todo: remove hardcoding
            var summer = new SelectListItem { Value = "1", Text = "Summer quarter 2018" };

            //add to the select list
            report.Quarters.Add(summer);

            if (report.SelectedQuarterId == 1)
            {
                report.DateStart = SchoolDismissalSettingsBackend.Instance.GetDefault().SummerQuarterFirstClassDay;
                report.DateEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().SummerQuarterLastClassDay;
            }

            var spring = new SelectListItem { Value = "2", Text = "Spring quarter 2018" };

            //add to the select list
            report.Quarters.Add(spring);

            if (report.SelectedQuarterId == 2)
            {
                report.DateStart = SchoolDismissalSettingsBackend.Instance.GetDefault().SpringQuarterFirstClassDay;
                report.DateEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().SpringQuarterLastClassDay;
            }

            var winter = new SelectListItem { Value = "3", Text = "Winter quarter 2018" };

            //add to the select list
            report.Quarters.Add(winter);

            if (report.SelectedQuarterId == 3)
            {
                report.DateStart = SchoolDismissalSettingsBackend.Instance.GetDefault().WinterQuarterFirstClassDay;
                report.DateEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().WinterQuarterLastClassDay;
            }

            var fall = new SelectListItem { Value = "4", Text = "Fall quarter 2017" };

            //add to the select list
            report.Quarters.Add(fall);

            if (report.SelectedQuarterId == 4)
            {
                report.DateStart = SchoolDismissalSettingsBackend.Instance.GetDefault().FallQuarterFirstClassDay;
                report.DateEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().FallQuarterLastClassDay;
            }

            //Generate report for this semester
            GenerateReportFromStartToEnd(report);

            return report;
        }
        #endregion

        /// <summary>
        /// Generate overall report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public BaseReportViewModel GenerateOverallReport(BaseReportViewModel report)
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
        private void GenerateReportFromStartToEnd(BaseReportViewModel report)
        {
            // Don't go beyond today
            if (report.DateEnd.CompareTo(UTCConversionsBackend.UtcToKioskTime(DateTime.UtcNow).Date) > 0)
            {
                report.DateEnd = UTCConversionsBackend.UtcToKioskTime(DateTime.UtcNow).Date;
            }

            var currentDate = report.DateStart;

            while (currentDate.CompareTo(report.DateEnd) <= 0)
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
                    var myRange = report.Student.Attendance.Where(m => UTCConversionsBackend.UtcToKioskTime(m.In).Date == currentDate.Date).ToList();

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

                            //        // Log the student out as well.
                            //        report.Student.Status = StudentStatusEnum.Out;

                            //        // Update the change for that item be rewriting the student record back to the datastore
                            //        DataSourceBackend.Instance.StudentBackend.Update(report.Student);
                            //    }
                            //    else
                            //    {
                            //        // If the person is still checked in, and it is today, use now and add up till then.
                            //        tempDuration = DateTime.UtcNow.Subtract(item.In);
                            //    }
                            //}

                            temp.AttendanceStatus = AttendanceStatusEnum.Present;
                            temp.TimeIn = UTCConversionsBackend.UtcToKioskTime(item.In);
                            temp.TimeOut = UTCConversionsBackend.UtcToKioskTime(item.Out);
                            temp.HoursAttended += tempDuration;
                            temp.Emotion = item.Emotion;
                            
                        }
                        CalculateDaysInOutStats(temp, report.Stats);
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
                report.Stats.PercPresent = (int)Math.Round((double)report.Stats.DaysPresent * 100 / report.Stats.NumOfSchoolDays);
                report.Stats.PercAttendedHours =
                    (int)Math.Round(report.Stats.AccumlatedTotalHours.TotalHours * 100 / report.Stats.AccumlatedTotalHoursExpected.TotalHours);
                report.Stats.PercExcused = (int)Math.Round((double)report.Stats.DaysAbsentExcused * 100 / report.Stats.NumOfSchoolDays);
                report.Stats.PercUnexcused = (int)Math.Round((double)report.Stats.DaysAbsentUnexcused * 100 / report.Stats.NumOfSchoolDays);
                if (report.Stats.DaysPresent > 0)
                {
                    report.Stats.PercInLate = (int)Math.Round((double)report.Stats.DaysLate * 100 / report.Stats.DaysPresent);
                    report.Stats.PercOutEarly = (int)Math.Round((double)report.Stats.DaysOutEarly * 100 / report.Stats.DaysPresent);
                }
            }
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
            if (UTCConversionsBackend.UtcToKioskTime(attendance.In).TimeOfDay.CompareTo(schoolDay.TimeStart) > 0)
            {
                start = UTCConversionsBackend.UtcToKioskTime(attendance.In).TimeOfDay;
                attendanceReport.CheckInStatus = CheckInStatusEnum.ArriveLate;
            }
            else
            {
                attendanceReport.CheckInStatus = CheckInStatusEnum.ArriveOnTime;
            }
            //trim the effective end time to actual out time only if the student leave early
            if (UTCConversionsBackend.UtcToKioskTime(attendance.Out).TimeOfDay.CompareTo(schoolDay.TimeEnd) < 0)
            {
                end = UTCConversionsBackend.UtcToKioskTime(attendance.Out).TimeOfDay;
                attendanceReport.CheckOutStatus = CheckOutStatusEnum.DoneEarly;
            }
            else
            {
                attendanceReport.CheckOutStatus = CheckOutStatusEnum.DoneAuto;
            }
            var duration = end.Subtract(start);

            //If time-in is later than time out, just return 0
            //Todo: prevent it from happening in kiosk
            if (duration < TimeSpan.Zero)
            {
                return TimeSpan.Zero;
            }

            return duration;
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