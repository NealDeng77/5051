using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Backend;
using _5051.Models;

namespace _5051.Controllers
{
    //The controller that handles attendance crudi
    public class AttendanceController : Controller
    {
        // GET: Attendance. Select a student here
        public ActionResult Index()
        {
            // Load the list of data into the StudentList
            var myDataList = DataSourceBackend.Instance.StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        // GET: Attendance/Read/. Read the attendance history of the student
        public ActionResult Read(string id)
        {
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);

            var attendanceListOrdered = myReturn.Attendance.OrderByDescending(m => m.In);

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

        // GET: Attendance/Detail
        // Read the details of the attendance(time in, time out).
        public ActionResult Details(string id)
        {
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(id);

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
                EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion),

                IsNew = myAttendance.IsNew
            };

            return View(myReturn);
        }



        // GET: Attendance/Create
        //Create a new attendance
        public ActionResult Create(string id)
        {

            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myStudent == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            //current date
            var myDate = UTCConversionsBackend.UtcToKioskTime(DateTime.UtcNow).Date;
            //the school day model
            var schoolDay = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(myDate);
            DateTime defaultStart;
            DateTime defaultEnd;

            if (schoolDay == null)
            {
                defaultStart = myDate.Add(SchoolDismissalSettingsBackend.Instance.GetDefault().StartNormal);
                defaultEnd = myDate.Add(SchoolDismissalSettingsBackend.Instance.GetDefault().EndNormal);
            }
            else
            {
                defaultStart = myDate.Add(schoolDay.TimeStart);
                defaultEnd = myDate.Add(schoolDay.TimeEnd);
            }



            var myData = new AttendanceModel
            {
                StudentId = id,
                In = defaultStart,
                Out = defaultEnd,
            };

            return View(myData);
        }

        // POST: Attendance/Create
        [HttpPost]
        public ActionResult Create([Bind(Include=
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
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for edit
                return View(data);
            }

            //create a new attendance using the data
            var myAttendance = new AttendanceModel
            {
                StudentId = data.StudentId,
                //update the time
                In = UTCConversionsBackend.KioskTimeToUtc(data.In),
                Out = UTCConversionsBackend.KioskTimeToUtc(data.Out),
                Emotion = data.Emotion,
                EmotionUri = Emotion.GetEmotionURI(data.Emotion),

                IsNew = data.IsNew
            };

            //add the attendance to the student's attendance
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(myAttendance.StudentId);

            myStudent.Attendance.Add(myAttendance);

            return RedirectToAction("Read", new { id = myAttendance.StudentId });
        }

        // GET: Attendance/Update
        // Update in and out times.
        public ActionResult Update(string id)
        {
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(id);

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
                EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion),

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
                return RedirectToAction("Error", "Home");
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
                return RedirectToAction("Error", "Home");
            }

            //update the time
            myAttendance.In = UTCConversionsBackend.KioskTimeToUtc(data.In);
            myAttendance.Out = UTCConversionsBackend.KioskTimeToUtc(data.Out);

            //update the emotion
            myAttendance.Emotion = data.Emotion;
            myAttendance.EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion);

            return RedirectToAction("Details", new { id = myAttendance.Id });
        }

        // GET: Attendance/Delete/5
        // Remove the attendance
        public ActionResult Delete(string id)
        {
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(id);

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
                EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion),

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
                return RedirectToAction("Error", "Home");
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
                return RedirectToAction("Error", "Home");
            }

            //get the student, then remove the attendance from his attendance list
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(myAttendance.StudentId);

            myStudent.Attendance.Remove(myAttendance);

            return RedirectToAction("Read", new { id = myAttendance.StudentId });
        }
    }
}
