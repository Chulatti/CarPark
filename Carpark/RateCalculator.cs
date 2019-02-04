using Carpark.Model;
using System;

namespace Carpark
{
    public class RateCalculator
    {
        public Rate CalculateRate(DateTime entryDate, DateTime exitDate)
        {
            decimal price;
            TimeSpan entryTime = entryDate.TimeOfDay;
            TimeSpan exitTime = exitDate.TimeOfDay;

            //Early bird
            if (entryTime >= new TimeSpan(6, 0, 0) && entryTime <= new TimeSpan(9, 0, 0)
                && exitTime >= new TimeSpan(15, 30, 0) && exitTime <= new TimeSpan(23, 30, 0)
                && entryDate.Day == exitDate.Day)
            {
                return new Rate { Name = "Early bird", Price = 13.00M };
            }

            //Night rate            
            if (entryTime >= new TimeSpan(18, 0, 0) && entryTime <= new TimeSpan(24, 0, 0)
                && exitTime <= new TimeSpan(6, 0, 0)
                && (entryDate.Date == exitDate.Date.AddDays(-1) || entryDate.DayOfWeek == DayOfWeek.Friday))
            {
                return new Rate { Name = "Night rate", Price = 6.50M };
            }

            //Weekend rate
            var monday = entryDate.Date.AddDays(entryDate.DayOfWeek == DayOfWeek.Saturday ? 2 : 1);

            if ((entryDate.DayOfWeek == DayOfWeek.Saturday || entryDate.DayOfWeek == DayOfWeek.Sunday)
                //left before midnight on Sunday
                && exitDate < monday)
            {
                return new Rate { Name = "Weekend rate", Price = 10.00M };
            }

            //Standard rate          
            var totalTime = (exitDate - entryDate).TotalHours;

            if (totalTime <= 1)
            {
                price = 5.00M;
            }
            else if (totalTime <= 2)
            {
                price = 10.00M;
            }
            else if (totalTime <= 3)
            {
                price = 15.00M;
            }
            else
            {
                var totalNumberOfDays = (exitDate.Date - entryDate.Date).TotalDays + 1;
                price = 20.00M * (decimal)totalNumberOfDays;
            }

            return new Rate { Name = "Standard rate", Price = price };
        }
    }
}
