using System;
using System.Linq;
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
        /// Calendar Default resets the date to defaults for that date type
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Default(string id = null)
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

            myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.SetDefault(id);

            return RedirectToAction("Update", "Calendar", new { id });
        }

        /// <summary>
        /// Calendar Update
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Update(string id = null)
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

        /// <summary>
        /// This updates the Calendar based on the information posted from the update page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Avatar/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id,"+
                                        "Modified,"+
                                        "Date,"+
                                        "TimeMax,"+
                                        "TimeStart,"+
                                        "TimeEnd,"+
                                        "DayStart,"+
                                        "DayEnd,"+
                                        "")] SchoolCalendarModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to error page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            // Validate Date and Times
            if (data.TimeStart.TotalHours < 1 || data.TimeStart.TotalHours > 24)
            {
                // Must be between 0 and 24
                ModelState.AddModelError("TimeStart", "Enter Valid Start Time");
                return View(data);
            }

            // Validate Date and Times
            if (data.TimeEnd.TotalHours < 1 || data.TimeEnd.TotalHours > 24)
            {
                // Must be between 0 and 24
                ModelState.AddModelError("TimeEnd", "Enter Valid End Time");
                return View(data);
            }

            // Validate Date and Times
            if (data.TimeEnd.Subtract(data.TimeStart).Ticks < 1)
            {
                // End is before Start
                ModelState.AddModelError("TimeStart", "Start Time must be before End Time");
                return View(data);
            }

            Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Update(data);

            return RedirectToAction("Index");
        }
    }
}