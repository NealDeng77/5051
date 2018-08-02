using _5051.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Backend
{
    /// <summary>
    /// Game Backend handles the business logic and data for Games
    /// </summary>
    public class GameBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile GameBackend instance;
        private static object syncRoot = new Object();

        private static object Lock = new Object();

        private GameBackend() { }

        public static GameBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new GameBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IGameInterface DataSource;

        /// <summary>
        /// Sets the Datasource to be Mock or SQL
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            if (dataSourceEnum == DataSourceEnum.SQL)
            {
                // SQL not hooked up yet...
                // throw new NotImplementedException();
            }

            // Default is to use the Mock
            DataSource = GameDataSourceMock.Instance;
        }

        /// <summary>
        /// Makes a new Game
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Game Passed In</returns>
        public GameModel Create(GameModel data)
        {
            DataSource.Create(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public GameModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = DataSource.Read(id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public GameModel Update(GameModel data)
        {
            if (data == null)
            {
                return null;
            }

            var myReturn = DataSource.Update(data);

            return myReturn;
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myReturn = DataSource.Delete(Id);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Games</returns>
        public List<GameModel> Index()
        {
            var myData = DataSource.Index();
            return myData;
        }

        /// <summary>
        /// Returns the First record
        /// </summary>
        /// <returns>Null or valid data</returns>
        public GameModel GetDefault()
        {
            var myReturn = DataSource.Index().First();
            return myReturn;
        }

        /// <summary>
        /// Switch the data set between Demo, Default and Unit Test
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            GameDataSourceMock.Instance.LoadDataSet(SetEnum);
        }

        /// <summary>
        /// Helper function that resets the DataSource, and rereads it.
        /// </summary>
        public void Reset()
        {
            DataSource.Reset();
        }

        /// <summary>
        /// Run the Simulation
        /// Returns the number of Iterations the simulator ran
        /// </summary>
        public int Simulation()
        {
            // Run for each time between now and the last time ran
            var timeNow = DateTime.UtcNow;

            // Get Time last Ran
            var currentData = GetDefault();

            // Get the current delta, and see if anyting is needed
            var shouldRun = currentData.RunDate.AddTicks(currentData.TimeIteration.Ticks).CompareTo(timeNow);
            if (shouldRun >= 0)
            {
                // the number of iteration that the simulator has run
                return currentData.IterationNumber;
            }

            lock (Lock)
            {
                do
                {
                    // If time lapsed in > time Threshold, then Run Simulaton for one Cycle
                    shouldRun = currentData.RunDate.AddTicks(currentData.TimeIteration.Ticks).CompareTo(timeNow);
                    if (shouldRun < 0)
                    {
                        // Run Iteration
                        RunIteration();

                        // Increment the RunDate
                        currentData.RunDate = currentData.RunDate.AddTicks(currentData.TimeIteration.Ticks);
                        currentData.IterationNumber++;
                        Update(currentData);
                    }
                }
                while (shouldRun < 0);
            }

            // Until Simulation Time = Current Time
            return currentData.IterationNumber;
        }

        /// <summary>
        /// Simulation Results
        /// </summary>
        public GameResultViewModel GetResult(string id = null)
        {
            // Get the Store results for this student
            // Return the Student Results
            var result = new GameResultViewModel();

            var StudentData = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (StudentData == null)
            {
                return result;
            }

            result.Truck = DataSourceBackend.Instance.FactoryInventoryBackend.Read(StudentData.Truck.Truck).Uri;
            result.Trailer = DataSourceBackend.Instance.FactoryInventoryBackend.Read(StudentData.Truck.Trailer).Uri;
            result.Wheels = DataSourceBackend.Instance.FactoryInventoryBackend.Read(StudentData.Truck.Wheels).Uri;
            result.Topper = DataSourceBackend.Instance.FactoryInventoryBackend.Read(StudentData.Truck.Topper).Uri;
            result.Sign = DataSourceBackend.Instance.FactoryInventoryBackend.Read(StudentData.Truck.Sign).Uri;
            result.Menu = DataSourceBackend.Instance.FactoryInventoryBackend.Read(StudentData.Truck.Menu).Uri;

            result.IterationNumber = GetDefault().IterationNumber;

            // TODO: Add CustomersTotal to the Truck Record on Student...
            result.CustomersTotal = 1; //StudentData.Truck.CustomersTotal;

            result.Tokens = StudentData.Tokens;
            result.Experience = StudentData.ExperiencePoints;

            // Todo: Add isClosed to the Truck record on Student ...
            result.isClosed = false;

            if (StudentData.Tokens < 1)
            {
                result.isClosed = true;
            }

            return result;
        }

        /// <summary>
        /// Run a Single Iteration of the Game
        /// </summary>
        public void RunIteration()
        {
            // Run a single iteration
            foreach (var student in StudentBackend.Instance.Index())
            {
                // calculate student iteration
                CalculateStudentIteration(student);

                // Update Student
                DataSourceBackend.Instance.StudentBackend.Update(student);
            }
            
        }

        public void CalculateStudentIteration(StudentModel student)
        {
            // check if it is necessary to do the iteration 
            if (student.Tokens > 0)
            {
                // pay rent once per day
                PayRentPerDay(student);
                // closed the truck once the token less than 1
                ClosedTruck(student);
                // customer arrives
                CustomerPassBy(student);
                // did customer buy something when they pass by
                CustomerPurchase(student);
                // data change if customer did purchase
                DataChangeAfterPurchase(student);
            }
            else
            {
                // quit iteration calculation
            }
                   
        }

        public void PayRentPerDay(StudentModel student)
        {
            // Run for each time between now and the last time ran
            var timeNow = DateTime.UtcNow;

            // Get Time last Ran
            var currentData = GetDefault();

            // Get the current delta, and see if student need to pay for the rent
            var shouldPay = currentData.RunDate.AddHours(24).CompareTo(timeNow);
            if (shouldPay > 0)
            {
                // means they have paied for the rent today
            }

            lock (Lock)
            {
                do
                {
                    // If time lapsed in > time Threshold, then pay for the rent
                    shouldPay = currentData.RunDate.AddHours(24).CompareTo(timeNow);
                    if (shouldPay <= 0)
                    {
                        if(student.Tokens > 0)
                        {
                            // the tokens of the rent is 1
                            student.Tokens -= 1;
                        }
                        else
                        {
                            // stop the iteration
                            shouldPay = 0;
                        }
                        
                        // Increment the RunDate
                        currentData.RunDate = currentData.RunDate.AddHours(24);
                        Update(currentData);
                    }
                }
                while (shouldPay < 0);
            }
            
            return;
        }

        public void ClosedTruck(StudentModel student)
        {
            
            return;
        }

        public void CustomerPassBy(StudentModel student)
        {
           
            return;
        }

        public void CustomerPurchase(StudentModel student)
        {
            
            return;
        }

        public void DataChangeAfterPurchase(StudentModel student)
        {
            
            return;
        }
    }
}