using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051;
using _5051.Controllers;
using _5051.Models;
using _5051.Backend;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        [TestMethod]
        public void Controller_Admin_Index_Default_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result,TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Report_Should_Return_New_Model()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            ViewResult result = controller.Report() as ViewResult;

            var resultStudentViewModel = result.Model as StudentViewModel;

            // Assert
            Assert.IsNotNull(resultStudentViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = StudentBackend.Instance.GetDefault().Id;

            // Act
            ViewResult result = controller.WeeklyReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "bogus";
            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            WeeklyReportViewModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Selected_ID_Is_2_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new WeeklyReportViewModel()
            {
                StudentId = StudentBackend.Instance.GetDefault().Id,
                SelectedWeekId = 2
            };

            // Act
            var result = controller.WeeklyReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = StudentBackend.Instance.GetDefault().Id; 

            // Act
            ViewResult result = controller.MonthlyReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "bogus"; 
            // Act
            var result = (RedirectToRouteResult)controller.MonthlyReport(id); 

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            MonthlyReportViewModel data = null;
            
            // Act
            var result = (RedirectToRouteResult)controller.MonthlyReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Post_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            MonthlyReportViewModel data = new MonthlyReportViewModel()
            {
                StudentId = StudentBackend.Instance.GetDefault().Id,
                SelectedMonthId = 2
            };

            // Act
            var result = controller.MonthlyReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }



        [TestMethod]
        public void Controller_Admin_Semester_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = StudentBackend.Instance.GetDefault().Id;

            // Act
            ViewResult result = controller.SemesterReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "bogus";
            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            SemesterReportViewModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new SemesterReportViewModel()
            {
                StudentId = StudentBackend.Instance.GetDefault().Id,
                SelectedSemesterId = 2
            };

            // Act
            var result = controller.SemesterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = StudentBackend.Instance.GetDefault().Id;

            // Act
            ViewResult result = controller.QuarterReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "bogus";
            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            QuarterReportViewModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_2_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new QuarterReportViewModel()
            {
                StudentId = StudentBackend.Instance.GetDefault().Id,
                SelectedQuarterId = 2
            };

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_3_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new QuarterReportViewModel()
            {
                StudentId = StudentBackend.Instance.GetDefault().Id,
                SelectedQuarterId = 3
            };

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_4_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new QuarterReportViewModel()
            {
                StudentId = StudentBackend.Instance.GetDefault().Id,
                SelectedQuarterId = 4
            };

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Overall_Report_Id_Is_Valid_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = StudentBackend.Instance.GetDefault().Id;

            // Act
            var result = controller.OverallReport(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Overall_ReportId_Is_Not_Valid_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "abc";

            // Act
            var result = (RedirectToRouteResult)controller.OverallReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }


        //[TestMethod]
        //public void Controller_Admin_Attendance_Default_Should_Pass()
        //{
        //    // Arrange
        //    AdminController controller = new AdminController();

        //    // Act
        //    ViewResult result = controller.Attendance() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result, TestContext.TestName);
        //}

        [TestMethod]
        public void Controller_Admin_Settings_Default_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            ViewResult result = controller.Settings() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Reset_DeFault_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_DataSourceSet_Id_Is_Null_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(null);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_DataSourceSet_Id_Is_Empty_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet("");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_DataSourceSet_Id_Equals_DeFault_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            var myId = "Default";
            
            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSourceSet is set to Default using avatar count
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);


        }

        [TestMethod]
        public void Controller_Admin_DataSourceSet_Id_Equals_Demo_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            var myId = "Demo";

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSourceSet is set to Demo using avatar count, Demo set not implemented yet, set to Default
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Admin_DataSourceSet_Id_Equals_UnitTest_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            var myId = "UnitTest";

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSourceSet is set to UnitTest using avatar count, UnitTest set not implemented yet, set to Default
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Admin_DataSource_Id_Is_Null_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(null);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_DataSource_Id_Is_Empty_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            var result = (RedirectToRouteResult)controller.DataSource("");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_DataSource_Id_Equals_Mock_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            var myId = "Mock";

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSource is set to Mock using avatar count
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Admin_DataSource_Id_Equals_SQL_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            var myId = "SQL";

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;


            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            //Check that the DataSource is set to SQL using avatar count, SQL DataSource not implemented yet, set to Mock
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Admin_DataSource_Id_Equals_Unknown_Should_Return_Index_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            var myId = "unknown";

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;


            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSource is set to unknown using avatar count, unknown DataSource not implemented yet, set to Mock
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);
        }

    }
}
