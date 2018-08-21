using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the GameResult Views to have the list of GameResults
    /// </summary>
    public class BusinessReportViewModel
    {
        /// <summary>
        /// The Report Parts
        /// </summary>
        public int income;
        public int outcome;
        public int profit;
        public List<TransactionModel> BusinessList;

    }
}