using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Backend;
using _5051.Models;

namespace _5051.Controllers
{
    public class AttendanceController : Controller
    {
        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        // GET: Attendance
        public ActionResult Index()
        {
            // Load the list of data into the StudentList
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        // GET: Attendance/Read/
        public ActionResult Read(string id)
        {
            var myStudent = StudentBackend.Instance.Read(id);

            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);

            //Set the last log in time and emotion status img uri
            if (myReturn.Attendance.Any())
            {
                myReturn.LastLogIn = UTCConversionsBackend.UtcToKioskTime(myReturn.Attendance.OrderByDescending(m => m.In).FirstOrDefault().In);
                switch (myReturn.EmotionCurrent)
                {
                    case EmotionStatusEnum.VeryHappy:
                        myReturn.EmotionImgUri = "EmotionVeryHappy.png";
                        break;
                    case EmotionStatusEnum.Happy:
                        myReturn.EmotionImgUri = "EmotionHappy.png";
                        break;
                    case EmotionStatusEnum.Neutral:
                        myReturn.EmotionImgUri = "EmotionNeutral.png";
                        break;
                    case EmotionStatusEnum.Sad:
                        myReturn.EmotionImgUri = "EmotionSad.png";
                        break;
                    case EmotionStatusEnum.VerySad:
                        myReturn.EmotionImgUri = "EmotionVerySad.png";
                        break;
                }
            }

            var attendanceListOrdered = myReturn.Attendance.OrderByDescending(m => m.In);
            //Deep copy Attendance list and convert time zone
            var myAttendanceModels = new List<AttendanceModel>();

            foreach (var item in attendanceListOrdered)
            {
                var myAttendance = new AttendanceModel()
                {
                    //deep copy the AttendanceModel and convert time zone
                    In = UTCConversionsBackend.UtcToKioskTime(item.In),


                    Emotion = item.Emotion
                };

                if (item.Out == DateTime.MinValue)
                {
                    //if out is auto, set time out to today's dismissal time
                    myAttendance.Out = item.Out.Add(DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(myAttendance.In.Date).TimeEnd);
                }
                else
                {
                    myAttendance.Out = UTCConversionsBackend.UtcToKioskTime(item.Out);
                }

                myAttendance.Id = item.Id;

                myAttendanceModels.Add(myAttendance);
            }

            myReturn.Attendance = myAttendanceModels;

            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = myStudent.Id;

            return View(myReturn);
        }

        // GET: Attendance/Detail
        public ActionResult Details(string id)
        {
            var myAttendance = StudentBackend.ReadAttendance(id);

            if (myAttendance == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Create a new attendance to hold converted times
            var myReturn = new AttendanceModel
            {
                StudentId = myAttendance.StudentId,
                Id = myAttendance.Id,
                In = UTCConversionsBackend.UtcToKioskTime(myAttendance.In),
                Out = UTCConversionsBackend.UtcToKioskTime(myAttendance.Out),
                Emotion = myAttendance.Emotion,
                IsNew = myAttendance.IsNew
            };

            return View(myReturn);
        }



        // GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attendance/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Attendance/Update
        public ActionResult Update(string id)
        {
            var myAttendance = StudentBackend.ReadAttendance(id);

            if (myAttendance == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Create a new attendance to hold converted times
            var myReturn = new AttendanceModel
            {
                StudentId = myAttendance.StudentId,
                Id = myAttendance.Id,
                In = UTCConversionsBackend.UtcToKioskTime(myAttendance.In),
                Out = UTCConversionsBackend.UtcToKioskTime(myAttendance.Out),
                Emotion = myAttendance.Emotion,
                IsNew = myAttendance.IsNew
            };

            return View(myReturn);
        }

        // POST: Attendance/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
            "Id,"+
            "StudentId,"+
            "In,"+
            "Out,"+
            "Emotion,"+
            "IsNew,"+
            "")] AttendanceModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for edit
                return View(data);
            }

            //get the attendance with given id
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(data.Id);

            //update the time
            myAttendance.In = UTCConversionsBackend.KioskTimeToUtc(data.In);
            myAttendance.Out = UTCConversionsBackend.KioskTimeToUtc(data.Out);

            return RedirectToAction("Read", new { id = myAttendance.StudentId });
        }

        // GET: Attendance/Delete/5
        public ActionResult Delete(string id)
        {
            var myAttendance = StudentBackend.ReadAttendance(id);

            if (myAttendance == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Create a new attendance to hold converted times
            var myReturn = new AttendanceModel
            {
                StudentId = myAttendance.StudentId,
                Id = myAttendance.Id,
                In = UTCConversionsBackend.UtcToKioskTime(myAttendance.In),
                Out = UTCConversionsBackend.UtcToKioskTime(myAttendance.Out),
                Emotion = myAttendance.Emotion,
                IsNew = myAttendance.IsNew
            };

            return View(myReturn);
        }

        // POST: Attendance/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include =
            "Id," +
            "")] AttendanceModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for edit
                return View(data);
            }

            //get the attendance with given id
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(data.Id);
            if (myAttendance == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            //get the student, then remove the attendance from his attendance list
            var myStudent = StudentBackend.Instance.Read(myAttendance.StudentId);
            if (myStudent == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            myStudent.Attendance.Remove(myAttendance);

            return RedirectToAction("Read", new { id = myAttendance.StudentId });
        }
    }
}
