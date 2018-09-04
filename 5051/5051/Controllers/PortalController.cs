using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class PortalController : BaseController
    {
        /// <summary>
        /// The list of all the active students in the class, so they can Roster
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Roster()
        {
            var myDataList = DataSourceBackend.Instance.StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        /// <summary>
        /// The Roster in page for the Portal, shows all the Students
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Login(string id = null)
        {
            var myStudent = StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Roster", "Portal");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);

            return View(myReturn);
        }

        /// <summary>
        /// Login for the student, take the ID, the rest of the fields are required but not used
        /// </summary>
        /// <param name="data"></param>
        /// <returns>if all is OK, then redirect to the student protal page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "AvatarLevel,"+
                                        "Tokens,"+
                                        "Status,"+
                                        "Password,"+
                                        "ExperiencePoints,"+
                                        "Password,"+
                                        "")] StudentDisplayViewModel data)
        {
            // Any password is accepted for now. does not really login...

            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.Id);
            if (myStudent == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            //if (!IdentityDataSourceTable.Instance.LogUserIn(myStudent.Name, data.Password, IdentityDataSourceTable.IdentityRole.Student))
            //{
            //    ModelState.AddModelError("", "Invalid password");
            //    return View(data);
            //}

            // all is OK, so redirect to the student index page and pass in the student ID for now.
            return RedirectToAction("Index", "Portal", new { id = data.Id });
        }

        /// <summary>
        /// Index Page
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult Index(string id = null)
        {
            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = id;

            var myStudent = StudentBackend.Instance.Read(id);

            if (myStudent == null)
            {
                return RedirectToAction("Roster", "Portal");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);

            //Set the last log in time and emotion status img uri
            if (myReturn.Attendance.Any())
            {
                myReturn.LastLogIn = UTCConversionsBackend.UtcToKioskTime(myReturn.Attendance.OrderByDescending(m => m.In).FirstOrDefault().In);
            }

            var myWeeklyReport = new WeeklyReportViewModel()
            {
                StudentId = id,
                SelectedWeekId = 1
            };

            var myMonthlyReport = new MonthlyReportViewModel()
            {
                StudentId = id,
                SelectedMonthId = 1
            };

            myReturn.WeeklyAttendanceScore = ReportBackend.Instance.GenerateWeeklyReport(myWeeklyReport).Stats.PercAttendedHours;
            myReturn.MonthlyAttendanceScore = ReportBackend.Instance.GenerateMonthlyReport(myMonthlyReport).Stats.PercAttendedHours;

            return View(myReturn);
        }

        /// <summary>
        /// Attendance Page
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult Attendance(string id = null)
        {
            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = id;

            var myStudent = StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Roster", "Portal");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);

            var attendanceListOrdered = myReturn.Attendance.OrderByDescending(m => m.In);

            //Set the last log in time and emotion status img uri
            if (attendanceListOrdered.Any())
            {
                myReturn.LastLogIn = UTCConversionsBackend.UtcToKioskTime(attendanceListOrdered.FirstOrDefault().In);
            }

            //Deep copy Attendance list and convert time zone
            var myAttendanceModels = new List<AttendanceModel>();

            foreach (var item in attendanceListOrdered)
            {
                var myAttendance = new AttendanceModel()
                {
                    //deep copy the AttendanceModel and convert time zone
                    In = UTCConversionsBackend.UtcToKioskTime(item.In),
                    Out = UTCConversionsBackend.UtcToKioskTime(item.Out),

                    Emotion = item.Emotion,
                    EmotionUri = Emotion.GetEmotionURI(item.Emotion)
                };

                myAttendance.Id = item.Id;

                myAttendanceModels.Add(myAttendance);
            }

            myReturn.Attendance = myAttendanceModels;

            return View(myReturn);
        }
        
        /// <summary>
        ///  My Settings
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult Settings(string id = null)
        {
            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = id;

            var myStudent = StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);

            return View(myReturn);
        }


        /// <summary>
        /// Student's Avatar page
        /// </summary>
        /// <returns></returns>
        // Post: Portal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "AvatarLevel,"+
                                        "Tokens,"+
                                        "Status,"+
                                        "AvatarUri,"+
                                        "ExperiencePoints,"+
                                        "Password,"+

                                        "")] StudentDisplayViewModel data)
        {
            // If data passed up is not valid, go back to the Index page so the user can try again
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

            // If the Student Id is black, error out
            if (string.IsNullOrEmpty(data.Id))
            {
                return RedirectToAction("Error", "Home");
            }

            // Lookup the student id, will just replace the Avatar Id on it if it is valid
            var myStudent = StudentBackend.Instance.Read(data.Id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Set the Avatar ID on the Student and update in data store
            myStudent.Name = data.Name;
            StudentBackend.Instance.Update(myStudent);

            // Editing is done, so go back to the Student Portal and pass the Student Id
            return RedirectToAction("Index", "Portal", new { Id = myStudent.Id });
        }

        // GET: WeeklyReport
        /// <summary>
        /// Returns an individual weekly report for a student
        /// </summary>
        /// <param name="id">Student ID to generate Report for</param>
        /// <returns>Report data</returns>
        public ActionResult WeeklyReport(string id = null)
        {
            var myStudent = StudentBackend.Instance.Read(id);
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
            var myStudent = StudentBackend.Instance.Read(id);

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
            var myStudent = StudentBackend.Instance.Read(id);

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
            var myStudent = StudentBackend.Instance.Read(id);

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
            var myStudent = StudentBackend.Instance.Read(id);

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

        ///// <summary>
        ///// My Attendance Reports
        ///// </summary>
        ///// <param name="id">Student Id</param>
        ///// <returns>Student Record as a Student View Model</returns>
        //// GET: Portal
        //public ActionResult Report(string id = null)
        //{
        //    var myStudent = StudentBackend.Instance.Read(id);

        //    if (myStudent == null)
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }


        //    //var myReport = new StudentReportViewModel
        //    //{
        //    //    StudentId = id,
        //    //    Year = DateTime.UtcNow.Year,
        //    //    Month = DateTime.UtcNow.Month
        //    //};

        //    //var myReturn = ReportBackend.Instance.GenerateMonthlyReport(myReport);

        //    return RedirectToAction("Index", "Portal", new { id = myStudent.Id });

        //    // return RedirectToAction("MonthlyReport", "Admin", new { id });
        //}
    }
}