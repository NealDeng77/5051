using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;

namespace _5051.Controllers
{
    /// <summary>
    /// The Calendar crudi
    /// </summary>
    public class CalendarController : Controller
    {
        /// <summary>
        /// Calendar Index
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Index()
        {
            var mySchoolDaysData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Index();
            if (mySchoolDaysData == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            var myData = new SchoolCalendarViewModel
            {
                SchoolDays = mySchoolDaysData,
                CurrentDate = DateTime.UtcNow,
            };

            myData.FirstDay = myData.SchoolDays.First().Date;
            myData.LastDay = myData.SchoolDays.Last().Date;

            //Get the First and last month, and set the dates to the first of those months.
            var dateStart = DateTime.Parse(myData.SchoolDays.First().Date.Month.ToString() + "/01/" + myData.SchoolDays.First().Date.Year.ToString());

            // Go to the month after the last date
            var dateEnd = DateTime.Parse((myData.SchoolDays.Last().Date.Month + 1).ToString() + "/01/" + myData.SchoolDays.Last().Date.Year.ToString());
            dateEnd.AddDays(-1); // Back up 1 day to be the last day of the month

            DateTime currentDate = new DateTime();

            currentDate = dateStart;

            // For every day from the start of the school year, until the end of the school year or now...
            while (currentDate.CompareTo(dateEnd) < 0)
            {
                if (currentDate.Day == 1)
                {
                    // For each month, build a list of the days for that month
                    var temp = myData.SchoolDays.Where(m => m.Date.Month == currentDate.Month).ToList();
                    myData.Months.Add(temp);
                }

                currentDate = currentDate.AddDays(1);
            }

            return View(myData);
        }

        /// <summary>
        /// Calendar Edit
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Edit(string id=null)
        {
            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Read(id);
            if (myData == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            return View(myData);
        }
    }
}