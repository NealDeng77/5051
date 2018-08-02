﻿using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the GameResult Views to have the list of GameResults
    /// </summary>
    public class GameResultViewModel
    {
        /// <summary>
        /// The Truck Parts
        /// </summary>
        public string Truck;
        public string Trailer;
        public string Sign;
        public string Wheels;
        public string Topper;
        public string Menu;

        /// <summary>
        /// Iteratoin Number
        /// </summary>
        public int IterationNumber;

        public bool IsClosed;
        public int CustomersTotal;
        public List<string> TransactionList;

        public int Tokens;
        public int Experience;
    }
}