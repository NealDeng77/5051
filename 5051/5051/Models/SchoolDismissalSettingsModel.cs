using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// Setting model to track the school Times for Start and End
    /// The Amin should be allowed to change these
    /// Save as defaults to the Database
    /// </summary>
    public class SchoolDismissalSettingsModel
    {
        // Used to access the settings instance, even if there is just one
        public string Id { get; set; }

        // The Normal Start time.  8:55am
        [Display(Name = "Start Normal", Description = "Time Class Usualy Starts")]
        public TimeSpan StartNormal { get; set; }

        // The Early Start time.  8:30am
        [Display(Name = "Start Early", Description = "Time Class Starts Early")]
        public TimeSpan StartEarly { get; set; }

        // The Late Start Time 10:55am (snow start)
        [Display(Name = "Start Late", Description = "Late Start Time")]
        public TimeSpan StartLate { get; set; }

        // The Normal end time 3:45am
        [Display(Name = "End Normal", Description = "Time Class Usualy Ends")]
        public TimeSpan EndNormal { get; set; }

        // The Early dismissal (wed 2pm)
        [Display(Name = "End Early", Description = "Time Class Ends Early")]
        public TimeSpan EndEarly { get; set; }

        // The Late Dismissal 4pm
        [Display(Name = "End Late", Description = "Time Class Ends if Late End")]
        public TimeSpan EndLate { get; set; }

        // Day start time drop down list: normal, early, late
        public List<SelectListItem> DayStartDropDown { get; set; }

        // Day end time drop down list: normal, early, late
        public List<SelectListItem> DayEndDropDown { get; set; }

        //Selected start time and end time type id for each day of week.
        public int MonStartSelected { get; set; }        
        public int MonEndSelected { get; set; }

        public int TueStartSelected { get; set; }
        public int TueEndSelected { get; set; }

        public int WedStartSelected { get; set; }
        public int WedEndSelected { get; set; }

        public int ThuStartSelected { get; set; }
        public int ThuEndSelected { get; set; }

        public int FriStartSelected { get; set; }
        public int FriEndSelected { get; set; }

        public int SatStartSelected { get; set; }
        public int SatEndSelected { get; set; }

        public int SunStartSelected { get; set; }
        public int SunEndSelected { get; set; }

        public SchoolCalendarDismissalEnum MonStartType { get; set; }
        public SchoolCalendarDismissalEnum MonEndType { get; set; }
        public SchoolCalendarDismissalEnum TueStartType { get; set; }
        public SchoolCalendarDismissalEnum TueEndType { get; set; }
        public SchoolCalendarDismissalEnum WedStartType { get; set; }
        public SchoolCalendarDismissalEnum WedEndType { get; set; }
        public SchoolCalendarDismissalEnum ThuStartType { get; set; }
        public SchoolCalendarDismissalEnum ThuEndType { get; set; }
        public SchoolCalendarDismissalEnum FriStartType { get; set; }
        public SchoolCalendarDismissalEnum FriEndType { get; set; }
        public SchoolCalendarDismissalEnum SatStartType { get; set; }
        public SchoolCalendarDismissalEnum SatEndType { get; set; }
        public SchoolCalendarDismissalEnum SunStartType { get; set; }
        public SchoolCalendarDismissalEnum SunEndType { get; set; }

        // First day of school
        [Display(Name = "First Day", Description = "First Day of School")]
        public DateTime DayFirst { get; set; }

        // Last day of school
        [Display(Name = "Last Day", Description = "Last Day of School")]
        public DateTime DayLast { get; set; }

        // Fall First Class Day
        [Display(Name = "Fall Semester First Class Day", Description = "Fall Semester First Class Day")]
        public DateTime FallFirstClassDay { get; set; }

        // Fall Last Class Day
        [Display(Name = "Fall Semester Last Class Day", Description = "Fall Semester Last Class Day")]
        public DateTime FallLastClassDay { get; set; }


        // Spring First Class Day
        [Display(Name = "Spring Semester First Class Day", Description = "Spring Semester First Class Day")]
        public DateTime SpringFirstClassDay { get; set; }

        // Spring Last Class Day
        [Display(Name = "Spring Semester Last Class Day", Description = "Spring Semester Last Class Day")]
        public DateTime SpringLastClassDay { get; set; }

        // Fall Quarter First Class Day
        [Display(Name = "Fall Quarter First Class Day", Description = "Fall Quarter First Class Day")]
        public DateTime FallQuarterFirstClassDay { get; set; }

        // Fall Quarter Last Class Day
        [Display(Name = "Fall Quarter Last Class Day", Description = "Fall Quarter Last Class Day")]
        public DateTime FallQuarterLastClassDay { get; set; }

        // Winter Quarter First Class Day
        [Display(Name = "Winter Quarter First Class Day", Description = "Winter Quarter First Class Day")]
        public DateTime WinterQuarterFirstClassDay { get; set; }

        // Winter Quarter Last Class Day
        [Display(Name = "Winter Quarter Last Class Day", Description = "Winter Quarter Last Class Day")]
        public DateTime WinterQuarterLastClassDay { get; set; }


        // Spring Quarter First Class Day
        [Display(Name = "Spring Quarter First Class Day", Description = "Spring Quarter First Class Day")]
        public DateTime SpringQuarterFirstClassDay { get; set; }

        // Spring Quarter Last Class Day
        [Display(Name = "Spring Quarter Last Class Day", Description = "Spring Quarter Last Class Day")]
        public DateTime SpringQuarterLastClassDay { get; set; }

        // Summer Quarter First Class Day
        [Display(Name = "Summer Quarter First Class Day", Description = "Summer Quarter First Class Day")]
        public DateTime SummerQuarterFirstClassDay { get; set; }

        // Summer Quarter Last Class Day
        [Display(Name = "Summer Quarter Last Class Day", Description = "Summer Quarter Last Class Day")]
        public DateTime SummerQuarterLastClassDay { get; set; }

        // The current setting for goal percentage
        [Display(Name = "Attendance Goal Percentage", Description = "Attendance Goal Percentage")]
        public int Goal { get; set; }

        // The period of time before school during which time attended by student is counted into attendance duration
        // while time attended before this window starts is not counted into attendance duration
        [Display(Name = "Early Window", Description = "The period of time before school during which time attended by student is counted into attendance duration")]
        public TimeSpan EarlyWindow { get; set; }

        // The period of time after school during which time attended by student is counted into attendance duration
        // while time attended after this window ends is not counted into attendance duration
        [Display(Name = "Late Window", Description = "The period of time after school during which time attended by student is counted into attendance duration")]
        public TimeSpan LateWindow { get; set; }

        public SchoolDismissalSettingsModel()
        {
            Initialize();
        }

        /// <summary>
        /// Create a copy of the item, used for updates.
        /// </summary>
        /// <param name="data"></param>
        public SchoolDismissalSettingsModel(SchoolDismissalSettingsModel data)
        {
            Initialize();
            if (data == null)
            {
                return;
            }

            StartNormal = data.StartNormal;
            StartEarly = data.StartEarly;
            StartLate = data.StartLate;
            EndNormal = data.EndNormal;
            EndEarly = data.EndEarly;
            EndLate = data.EndLate;

            DayStartDropDown = data.DayStartDropDown;
            DayEndDropDown = data.DayEndDropDown;

            MonStartSelected = data.MonStartSelected;
            MonEndSelected = data.MonEndSelected;
            TueStartSelected = data.TueStartSelected;
            TueEndSelected = data.TueEndSelected;
            WedStartSelected = data.WedStartSelected;
            WedEndSelected = data.WedEndSelected;
            ThuStartSelected = data.ThuStartSelected;
            ThuEndSelected = data.ThuEndSelected;
            FriStartSelected = data.FriStartSelected;
            FriEndSelected = data.FriEndSelected;
            SatStartSelected = data.SatStartSelected;
            SatEndSelected = data.SatEndSelected;
            SunStartSelected = data.SunStartSelected;
            SunEndSelected = data.SunEndSelected;

            MonStartType = setType(MonStartSelected);
            MonEndType = setType(MonEndSelected);
            TueStartType = setType(TueStartSelected);
            TueEndType = setType(TueEndSelected);
            WedStartType = setType(WedStartSelected);
            WedEndType = setType(WedEndSelected);
            ThuStartType = setType(ThuStartSelected);
            ThuEndType = setType(ThuEndSelected);
            FriStartType = setType(FriStartSelected);
            FriEndType = setType(FriEndSelected);
            SatStartType = setType(SatStartSelected);
            SatEndType = setType(SatEndSelected);
            SunStartType = setType(SunStartSelected);
            SunEndType = setType(SunEndSelected);

            DayFirst = data.DayFirst;
            DayLast = data.DayLast;

            FallFirstClassDay = data.FallFirstClassDay;
            FallLastClassDay = data.FallLastClassDay;
            SpringFirstClassDay = data.SpringFirstClassDay;
            SpringLastClassDay = data.SpringLastClassDay;
            FallQuarterFirstClassDay = data.FallQuarterFirstClassDay;
            FallQuarterLastClassDay = data.FallQuarterLastClassDay;
            WinterQuarterFirstClassDay = data.WinterQuarterFirstClassDay;
            WinterQuarterLastClassDay = data.WinterQuarterLastClassDay;
            SpringQuarterFirstClassDay = data.SpringQuarterFirstClassDay;
            SpringQuarterLastClassDay = data.SpringQuarterLastClassDay;
            SummerQuarterLastClassDay = data.SummerQuarterFirstClassDay;
            SummerQuarterLastClassDay = data.SummerQuarterLastClassDay;

            Goal = data.Goal;
            EarlyWindow = data.EarlyWindow;
            LateWindow = data.LateWindow;
        }

        /// <summary>
        /// Create the default values
        /// </summary>
        private void Initialize()
        {
            SetDefault();
        }

        /// <summary>
        /// Sets the default values for the Item
        /// Because it is set here, there is no need to set defaults over in the Mock, call this instead
        /// </summary>
        public void SetDefault()
        {
            Id = Guid.NewGuid().ToString();
            StartNormal = TimeSpan.Parse("8:55");
            StartEarly = TimeSpan.Parse("8:00");
            StartLate = TimeSpan.Parse("10:55");

            EndNormal = TimeSpan.Parse("15:45");
            EndEarly = TimeSpan.Parse("14:00");
            EndLate = TimeSpan.Parse("16:00");

            DayStartDropDown = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "No School" },
                new SelectListItem { Value = "1", Text = "Start Normal" },
                new SelectListItem { Value = "2", Text = "Start Early" },
                new SelectListItem { Value = "3", Text = "Start Late" }
            };

            DayEndDropDown = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "No School" },
                new SelectListItem { Value = "1", Text = "End Normal" },
                new SelectListItem { Value = "2", Text = "End Early" },
                new SelectListItem { Value = "3", Text = "End Late" }
            };

            MonStartSelected = 1;
            MonEndSelected = 1;
            TueStartSelected = 1;
            TueEndSelected = 1;
            WedStartSelected = 1;
            WedEndSelected = 2;
            ThuStartSelected = 1;
            ThuEndSelected = 1;
            FriStartSelected = 3;
            FriEndSelected = 1;
            SatStartSelected = 0;
            SatEndSelected = 0;
            SunStartSelected =0;
            SunEndSelected = 0;

            MonStartType = SchoolCalendarDismissalEnum.Normal;
            MonEndType = SchoolCalendarDismissalEnum.Normal;
            TueStartType = SchoolCalendarDismissalEnum.Normal;
            TueEndType = SchoolCalendarDismissalEnum.Normal;
            WedStartType = SchoolCalendarDismissalEnum.Normal;
            WedEndType = SchoolCalendarDismissalEnum.Early;
            ThuStartType = SchoolCalendarDismissalEnum.Normal;
            ThuEndType = SchoolCalendarDismissalEnum.Normal;
            FriStartType = SchoolCalendarDismissalEnum.Late;
            FriEndType = SchoolCalendarDismissalEnum.Normal;
            SatStartType = SchoolCalendarDismissalEnum.Unknown;
            SatEndType = SchoolCalendarDismissalEnum.Unknown;
            SunStartType = SchoolCalendarDismissalEnum.Unknown;
            SunEndType = SchoolCalendarDismissalEnum.Unknown;

            var Year = DateTime.UtcNow.Year;

            DayFirst = DateTime.Parse("09/01/" + Year);
            DayLast = DateTime.Parse("08/31/" + (Year + 1));

            //The following specifies the start and end date of semesters
            FallFirstClassDay = DateTime.Parse("09/05/" + Year);
            FallLastClassDay = DateTime.Parse("01/31/" + (Year + 1));

            SpringFirstClassDay = DateTime.Parse("02/01/" + (Year + 1));
            SpringLastClassDay = DateTime.Parse("08/31/" + (Year + 1));

            //The following specifies the start and end date of quarters
            FallQuarterFirstClassDay = DateTime.Parse("09/05/" + Year);
            FallQuarterLastClassDay = DateTime.Parse("12/1/" + Year);

            WinterQuarterFirstClassDay = DateTime.Parse("01/08/" + (Year + 1));
            WinterQuarterLastClassDay = DateTime.Parse("03/19/" + (Year + 1));

            SpringQuarterFirstClassDay= DateTime.Parse("04/03/" + (Year + 1));
            SpringQuarterLastClassDay = DateTime.Parse("06/11/" + (Year + 1));

            SummerQuarterFirstClassDay = DateTime.Parse("06/19/" + (Year + 1));
            SummerQuarterLastClassDay = DateTime.Parse("08/26/" + (Year + 1));

            Goal = 85;
            EarlyWindow = new TimeSpan(0, 30, 0);
            LateWindow = new TimeSpan(0, 30, 0);
        }

        /// <summary>
        /// Used to Update Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(SchoolDismissalSettingsModel data)
        {
            if (data == null)
            {
                return;
            }

            StartNormal = data.StartNormal;
            StartEarly = data.StartEarly;
            StartLate = data.StartLate;
            EndNormal = data.EndNormal;
            EndEarly = data.EndEarly;
            EndLate = data.EndLate;

            DayStartDropDown = data.DayStartDropDown;
            DayEndDropDown = data.DayEndDropDown;

            MonStartSelected = data.MonStartSelected;
            MonEndSelected = data.MonEndSelected;
            TueStartSelected = data.TueStartSelected;
            TueEndSelected = data.TueEndSelected;
            WedStartSelected = data.WedStartSelected;
            WedEndSelected = data.WedEndSelected;
            ThuStartSelected = data.ThuStartSelected;
            ThuEndSelected = data.ThuEndSelected;
            FriStartSelected = data.FriStartSelected;
            FriEndSelected = data.FriEndSelected;
            SatStartSelected = data.SatStartSelected;
            SatEndSelected = data.SatEndSelected;
            SunStartSelected = data.SunStartSelected;
            SunEndSelected = data.SunEndSelected;

            MonStartType = setType(MonStartSelected);
            MonEndType = setType(MonEndSelected);
            TueStartType = setType(TueStartSelected);
            TueEndType = setType(TueEndSelected);
            WedStartType = setType(WedStartSelected);
            WedEndType = setType(WedEndSelected);
            ThuStartType = setType(ThuStartSelected);
            ThuEndType = setType(ThuEndSelected);
            FriStartType = setType(FriStartSelected);
            FriEndType = setType(FriEndSelected);
            SatStartType = setType(SatStartSelected);
            SatEndType = setType(SatEndSelected);
            SunStartType = setType(SunStartSelected);
            SunEndType = setType(SunEndSelected);

            DayFirst = data.DayFirst;
            DayLast = data.DayLast;
            FallFirstClassDay = data.FallFirstClassDay;
            FallLastClassDay = data.FallLastClassDay;
            SpringFirstClassDay = data.SpringFirstClassDay;
            SpringLastClassDay = data.SpringLastClassDay;
            FallQuarterFirstClassDay = data.FallQuarterFirstClassDay;
            FallQuarterLastClassDay = data.FallQuarterLastClassDay;
            WinterQuarterFirstClassDay = data.WinterQuarterFirstClassDay;
            WinterQuarterLastClassDay = data.WinterQuarterLastClassDay;
            SpringQuarterFirstClassDay = data.SpringQuarterFirstClassDay;
            SpringQuarterLastClassDay = data.SpringQuarterLastClassDay;
            SummerQuarterLastClassDay = data.SummerQuarterLastClassDay;
            SummerQuarterLastClassDay = data.SummerQuarterLastClassDay;
            Goal = data.Goal;
            EarlyWindow = data.EarlyWindow;
            LateWindow = data.LateWindow;
        }

        /// <summary>
        /// set dismissal type according to selectedId
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        private SchoolCalendarDismissalEnum setType(int selectedId)
        {
            switch (selectedId)
            {
                case 0:
                    return SchoolCalendarDismissalEnum.Unknown;
                case 1:
                    return SchoolCalendarDismissalEnum.Normal;
                case 2:
                    return SchoolCalendarDismissalEnum.Early;
                case 3:
                    return SchoolCalendarDismissalEnum.Late;
                default:
                    return SchoolCalendarDismissalEnum.Normal;
            }
        }
    }
}
