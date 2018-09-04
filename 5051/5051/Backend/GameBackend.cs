﻿using _5051.Models;
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
            DataSource.LoadDataSet(SetEnum);
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

            result.Tokens = StudentData.Tokens;
            result.Experience = StudentData.ExperiencePoints;
            result.TruckName = StudentData.Truck.TruckName;

            if (StudentData.Tokens < 1)
            {
                // Update the Backend, and close the store
                StudentData.Truck.IsClosed = true;
            } else {
                StudentData.Truck.IsClosed = false;
            }

            result.IsClosed = StudentData.Truck.IsClosed;
            result.IterationNumber = GetDefault().IterationNumber;
            result.CustomersTotal = StudentData.Truck.CustomersTotal;

            result.TransactionList = StudentData.Truck.TransactionList;

            // display recent 5 messages
            //if (StudentData.Truck.TransactionList.Count <= 5)
            //{
            //    result.TransactionList = StudentData.Truck.TransactionList;
            //} else
            //{
            //    var RecentFiveMessagesList = new List<TransactionModel>();
            //    for (int i = StudentData.Truck.TransactionList.Count - 5; i < StudentData.Truck.TransactionList.Count; i++)
            //    {
            //        RecentFiveMessagesList.Add(StudentData.Truck.TransactionList[i]);
            //    }
            //    result.TransactionList = RecentFiveMessagesList;
            //}           

            // Clear the Student TransactionList
            //StudentData.Truck.TransactionList = new List<TransactionModel>();
            DataSourceBackend.Instance.StudentBackend.Update(StudentData);

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

                // check if need to close the store

            }
        }

        public void CalculateStudentIteration(StudentModel student)
        {
            // check if it is necessary to do the iteration 
            if (student.Truck.IsClosed == false)
            {                            
                // pay rent once per day
                PayRentPerDay(student);

                // customer arrives
                CustomerPassBy(student);

                // Check if customer buy something or not
                var ifBuy = WillCustomerBuyOrNot(student);
                if (ifBuy == true)
                {
                    // customer buy something
                    CustomerPurchase(student);
                }                                       
            }
            else
            {
                // quit iteration calculation
            }
            var leaderBoard = new List<LeaderBoardModel>();
            leaderBoard = UpdatedLeaderBoard();
        }

        public void PayRentPerDay(StudentModel student)
        {
            // Run for each time between now and the last time ran
            var timeNow = DateTime.UtcNow;

            // Get Time last Ran
            var currentData = GetDefault();

            var RunDateTime = currentData.RunDate.AddHours(24);
            
            // Get the current delta, and see if student need to pay for the rent
            var shouldPay = RunDateTime.CompareTo(timeNow);
            if (shouldPay > 0)
            {
                // means they have paied for the rent today

                // Paid the rent already, so can exit.
                return;
            }

            lock (Lock)
            {
                do
                {
                    // If time lapsed in > time Threshold, then pay for the rent
                    RunDateTime = currentData.RunDate.AddHours(24);
                    shouldPay = RunDateTime.CompareTo(timeNow);
                    if (shouldPay <= 0)
                    {
                        if(student.Tokens > 0)
                        {
                            var RentAmount = 1;
                            // the tokens of the rent is 1
                            student.Tokens -= RentAmount;
                            // increase outcome
                            student.Truck.Outcome += 1;

                            TransactionModel myTransaction = new TransactionModel();
                            myTransaction.Name = "Paid Rent " + RentAmount + " for " + RunDateTime.Date.ToShortDateString();
                            myTransaction.Uri = null;
                            student.Truck.TransactionList.Add(myTransaction);
                            student.Truck.BusinessList.Add(myTransaction);

                        }
                        else
                        {
                            // stop the iteration
                            shouldPay = 0;
                            // close the truck
                            student.Truck.IsClosed = true;
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

        public void CustomerPassBy(StudentModel student)
        {
            // TODO Replace with real code
            // Pretend a Customer PassBy.
            var passByPercent = .1d;

            // extra precent
            var levelExtra = student.AvatarLevel * .01d;

            // Add a Transaction of the Customer passBy           
            var studentPoints = (int)Math.Ceiling((passByPercent + levelExtra) * 100);

            // generate random number
            //RandomHelper.Instance.SetForcedNumber();
            var criterion = RandomHelper.Instance.GetRandomNumber(30);

            // show pass by message            
            if (studentPoints >= criterion)
            {
                TransactionModel myTransaction = new TransactionModel();
                myTransaction.Name = "Customer is coming";
                myTransaction.Uri = null;
                student.Truck.TransactionList.Add(myTransaction);
            }
            return;
        }

        public bool WillCustomerBuyOrNot(StudentModel student)
        {
            var buy = false;
            // TODO Replace with real code
            // Pretend a Customer buy or not.
            var passByPercent = .1d;

            // extra precent
            var levelExtra = student.AvatarLevel * .01d;

            // Add a Transaction of the Customer passBy           
            var studentPoints = (int)Math.Ceiling((passByPercent + levelExtra) * 100);

            // generate random number
            //RandomHelper.SetForcedNumber();
            var criterion = RandomHelper.Instance.GetRandomNumber(30);

            // show pass by message
            if (studentPoints >= criterion)
            {
                buy = true;
            }

            return buy;
        }

        public void CustomerPurchase(StudentModel student)
        {
            // TODO Replace with real code
            // Pretend a Customer buys something.

            // Choose an item from the inventory to buy
            // Check that there are enough quanties.
            // Sell it (-1 to quantity)
            // If quantity is 0, remove from the student inventory list
            // Calculate Tokens. Need Profit Markup factor, does student experienc help here?
            var ProfitPercent = .1d;                                                                                    

            // Update Tokens for Student
            // Add a Transaction of the Sale
            // Increment Customer Total

            var Item = student.Inventory.FirstOrDefault(m => m.Category == FactoryInventoryCategoryEnum.Food);

            if (Item != null)
            {
                // Sell Item
                Item.Quantities--;

                // Add Tokens to the Student
                var profit = (int)Math.Ceiling(Item.Tokens * ProfitPercent);
                student.Tokens += Item.Tokens + profit;
                // increase income
                student.Truck.Income += Item.Tokens + profit;

                student.Truck.CustomersTotal++;

                TransactionModel myTransaction = new TransactionModel();
                myTransaction.Name = "Sold " + Item.Name + " for profit of " + profit;
                myTransaction.Uri = Item.Uri;
                student.Truck.TransactionList.Add(myTransaction);
                student.Truck.BusinessList.Add(myTransaction);


                // Remove from list
                student.Inventory.Remove(Item);

            }
            else
            {
                TransactionModel myTransaction = new TransactionModel();
                myTransaction.Name = "No Inventory to Sell";
                myTransaction.Uri = null;
                student.Truck.TransactionList.Add(myTransaction);
            }

            return;
        }

        public List<LeaderBoardModel> UpdatedLeaderBoard()
        {
            var board = new List<LeaderBoardModel>();
            var data = StudentBackend.Instance.Index();

            // Get Student
            foreach(var student in data)
            {
                var temp = new LeaderBoardModel();
                temp.Id = student.Id;
                temp.Name = student.Name;
                temp.Profit = student.Truck.Income - student.Truck.Outcome;
                board.Add(temp);
            }

            // Sort
            int max = 0;
            for (int i = 0; i < 3; i++)
            {
                max = i;
                for (int j = i + 1; j < board.Count; j++)
                {
                    if (board[j].Profit > board[i].Profit)
                    {
                        var temp = board[i];
                        board[i] = board[j];
                        board[j] = temp;
                    }
                }
            }

            return board;
        }
       
    }
}