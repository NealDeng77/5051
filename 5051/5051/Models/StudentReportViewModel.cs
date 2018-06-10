using System.Collections.Generic;
using System.Linq;

using _5051.Models.Enums;

namespace _5051.Models
{
    public class StudentReportViewModel
    {
        public List<AttendanceModel> Attendance { get; set; }
        public List<string> Date { get; set; }
        public List<double> HoursExpected { get; set; }
        public List<double> HoursAttended { get; set; }
        public List<double> AccumulativeHoursExpected { get; set; }
        public List<double> AccumulativeHoursAttended { get; set; }
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

        public StudentReportViewModel(StudentModel student)
        {
            Attendance = student.Attendance;
            Date = new List<string>();
            HoursExpected = new List<double>();
            HoursAttended = new List<double>();
            AccumulativeHoursExpected = new List<double>();
            AccumulativeHoursAttended = new List<double>();
            Calculate();
        }

        private void Calculate()
        {
            CalculateData();
            CalculateOther();
        }

        private void CalculateData()
        {
            double accumulativeHoursAttended = 0;
            double accumulativeHoursExpected = 0;
            for (int i = 0; i < Attendance.Count; i++)
            {
                AttendanceModel att = Attendance[i];
                double hours = 0;
                double hoursExpected = 0;
                //for (int j = 0; j < att.AttendanceCheckIns.Count; j++)
                //{
                //    AttendanceCheckInModel checkIn = att.AttendanceCheckIns[j];
                //    hours += checkIn.CheckOut.Subtract(checkIn.CheckIn).TotalHours;

                //}
                //hoursExpected = Backend.SchoolDayBackend.Instance.Read(att.SchoolDayId).ExpectedHours.TotalHours;
                //accumulativeHoursAttended += hours;
                //accumulativeHoursExpected += hoursExpected;
                //Date.Add(Backend.SchoolDayBackend.Instance.Read(att.SchoolDayId).Date.ToString("dd"));
                HoursAttended.Add(hours);
                HoursExpected.Add(hoursExpected);
                AccumulativeHoursAttended.Add(accumulativeHoursAttended);
                AccumulativeHoursExpected.Add(accumulativeHoursExpected);
            }
        }

        private void CalculateOther()
        {
            if (Attendance.Count != 0)
            {
                foreach (var item in Attendance)
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
                DaysPresent = Attendance.Count - DaysAbsentExcused - DaysAbsentUnexcused;
                DaysOnTime = DaysPresent - DaysLate;
                DaysStayed = DaysPresent - DaysLeftEarly;
                TotalHoursAttended = AccumulativeHoursAttended.Last();
                TotalHoursMissing = AccumulativeHoursExpected.Last() - AccumulativeHoursAttended.Last();
                PercPresent = 100 * DaysPresent / Attendance.Count;
                PercExcused = 100 * DaysAbsentExcused / Attendance.Count;
                PercUnexcused = 100 * DaysAbsentUnexcused / Attendance.Count;
                PercAttendedHours = (int)(100 * TotalHoursAttended / (TotalHoursMissing + TotalHoursAttended));
            }

        }
    }

}
