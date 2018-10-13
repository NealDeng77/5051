using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Backend;
using _5051.Models;

namespace _5051.Maintain
{


    /*
     * Allen
     * 
     * So investigate what is wrong with the current calendar data
     * I am assuming each calendar record needs to be inspected and potentialy fixed up
     * 
     * Best way to do this is to make Unit Tests for each of the issues that can be fixed, write the unit test first,  then the code to fix it.
     * Repeat untill all issues are fixed
     * 
     */

    public class CalendarMaintenance
    {
        public bool ResetCalendar()
        {

            var calendarSet = DataSourceBackend.Instance.SchoolCalendarBackend.Index();
            foreach (var item in calendarSet)
            {
                // Check the calendar against what the default would have been.
                var temp = new SchoolCalendarModel(item.Date);
                // Compare Item with Temp, to see what is different ?

                // I noticed the setdefault code in SchoolCalendar is commented out, not sure why, but that would be where I would investigate.
            }

            return true;
        }
    }
}