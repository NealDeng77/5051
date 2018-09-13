using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using System;

namespace _5051.Controllers
{
    /// <summary>
    /// Controller for the Admin section of the website
    /// </summary>
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Reports
        /// </summary>
        /// <returns>All the Students that can have a report</returns>
        // GET: Report
        public ActionResult Report()
        {
            // Load the list of data into the StudentList
            var myDataList = DataSourceBackend.Instance.StudentBackend.Index();

            var myReturn = new AdminReportIndexViewModel(myDataList);

            myReturn.Leaderboard = ReportBackend.Instance.GenerateLeaderboard();
            

            return View(myReturn);
        }

        // GET: WeeklyReport
        /// <summary>
        /// Returns an individual weekly report for a student
        /// </summary>
        /// <param name="id">Student ID to generate Report for</param>
        /// <returns>Report data</returns>
        public ActionResult WeeklyReport(string id = null)
        {
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReport = new WeeklyReportViewModel()
            {
                StudentId = id,
                SelectedWeekId = 1
            };

            var myReturn = ReportBackend.Instance.GenerateWeeklyReport(myReport);

            return View(myReturn);
        }

        /// <summary>
        /// Generate report using the WeeklyReportViewModel passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WeeklyReport([Bind(Include=
            "StudentId,"+
            "SelectedWeekId"+
            "")] WeeklyReportViewModel data)
        {

            if (data == null)
            {
                // Send to error page
                return RedirectToAction("Error", "Home");
            }

            //generate the report
            var myReturn = ReportBackend.Instance.GenerateWeeklyReport(data);

            return View(myReturn);
        }

        // GET: Report
        /// <summary>
        /// Returns an individual monthly report for a student
        /// </summary>
        /// <param name="id">Student ID to generate Report for</param>
        /// <returns>Report data</returns>
        public ActionResult MonthlyReport(string id = null)
        {
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);

            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }


            var myReport = new MonthlyReportViewModel()
            {
                StudentId = id,
                SelectedMonthId = 1
            };

            var myReturn = ReportBackend.Instance.GenerateMonthlyReport(myReport);

            return View(myReturn);
        }
        /// <summary>
        /// Generate report using the StudentReportViewModel passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MonthlyReport([Bind(Include=
            "StudentId,"+
            "SelectedMonthId"+
            "")] MonthlyReportViewModel data)
        {

            if (data == null)
            {
                // Send to error page
                return RedirectToAction("Error", "Home");
            }

            //generate the report
            var myReturn = ReportBackend.Instance.GenerateMonthlyReport(data);

            return View(myReturn);
        }

        // GET: SemesterReport
        /// <summary>
        /// Returns an individual semester report for a student
        /// </summary>
        /// <param name="id">Student ID to generate Report for</param>
        /// <returns>Report data</returns>
        public ActionResult SemesterReport(string id = null)
        {
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);

            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }


            var myReport = new SemesterReportViewModel()
            {
                StudentId = id,
                SelectedSemesterId = 1
            };

            var myReturn = ReportBackend.Instance.GenerateSemesterReport(myReport);

            return View(myReturn);
        }

        /// <summary>
        /// Generate report using the SemesterReportViewModel passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SemesterReport([Bind(Include=
            "StudentId,"+
            "SelectedSemesterId"+
            "")] SemesterReportViewModel data)
        {
            if (data == null)
            {
                // Send to error page
                return RedirectToAction("Error", "Home");
            }

            //generate the report
            var myReturn = ReportBackend.Instance.GenerateSemesterReport(data);

            return View(myReturn);
        }

        // GET: QuarterReport
        /// <summary>
        /// Returns an individual quarter report for a student
        /// </summary>
        /// <param name="id">Student ID to generate Report for</param>
        /// <returns>Report data</returns>
        public ActionResult QuarterReport(string id = null)
        {
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);

            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }


            var myReport = new QuarterReportViewModel()
            {
                StudentId = id,
                SelectedQuarterId = 1
            };

            var myReturn = ReportBackend.Instance.GenerateQuarterReport(myReport);

            return View(myReturn);
        }

        /// <summary>
        /// Generate report using the QuarterReportViewModel passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QuarterReport([Bind(Include=
            "StudentId,"+
            "SelectedQuarterId"+
            "")] QuarterReportViewModel data)
        {
            if (data == null)
            {
                // Send to error page
                return RedirectToAction("Error", "Home");
            }

            //generate the report
            var myReturn = ReportBackend.Instance.GenerateQuarterReport(data);

            return View(myReturn);
        }

        // GET: Report
        /// <summary>
        /// Returns an individual overall report for a student
        /// </summary>
        /// <param name="id">Student ID to generate Report for</param>
        /// <returns>Report data</returns>
        public ActionResult OverallReport(string id = null)
        {
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);

            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }


            var myReport = new SchoolYearReportViewModel()
            {
                StudentId = id,
            };

            var myReturn = ReportBackend.Instance.GenerateOverallReport(myReport);

            return View(myReturn);
        }

        /// <summary>
        /// Settings page for the admin, allows for data reset, switching between data modes etc.
        /// </summary>
        /// <returns></returns>
        // GET: Settings
        public ActionResult Settings()
        {
            return View();
        }

        //GET
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel user)
        {
            user.Email = "teacher";

            //var loginResult = IdentityDataSourceTable.Instance.LogUserIn(user.Email, user.Password, IdentityDataSourceTable.IdentityRole.Teacher);
            var loginResult = IdentityBackend.Instance.LogUserIn(user.Email, user.Password, IdentityDataSourceTable.IdentityRole.Teacher);
            if (!loginResult)
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View(user);
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}