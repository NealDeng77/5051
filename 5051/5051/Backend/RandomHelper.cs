using System;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// Get Random Number Function helper functions
    /// </summary>
    public class RandomHelper
    {
        private static volatile RandomHelper instance;
        private static object syncRoot = new Object();

        private static int ForcedRandomNumber;
        private static bool isSetForcedNumber;

        public RandomHelper ()
        {
            ForcedRandomNumber = -1;
            isSetForcedNumber = false;
        }

        public static void SetForcedNumber()
        {
            // generate random number
            var randObj = new Random();
            ForcedRandomNumber = randObj.Next(0, 30);
            isSetForcedNumber = true;
        }
        public static int GetRandomNumber()
        {
            if (isSetForcedNumber)
            {
                return ForcedRandomNumber;
            } else
            {
                return -1;
            }
        }

    }
}



