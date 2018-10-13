using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Backend;
using _5051.Models;

namespace _5051.Maintain
{

    public class CalendarMaintenance
    {
        public void ResetCalendar()
        {
            var calendarSet = DataSourceBackend.Instance.SchoolCalendarBackend.Index();
            foreach (var item in calendarSet)
            {
                // Check the calendar against what the default would have been.
                var temp = new SchoolCalendarModel(item.Date);
                // Compare Item with Temp, to see what is different ?

                // I noticed the setdefault code in SchoolCalendar is commented out, not sure why, but that would be where I would investigate.
            }
        }
    }
}