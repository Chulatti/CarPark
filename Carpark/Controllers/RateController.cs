using System;
using Microsoft.AspNetCore.Mvc;

namespace Carpark.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RateController : ControllerBase
    {
        private readonly RateCalculator _calculator;

        public RateController(RateCalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpPost]
        public IActionResult CalculateRate(ParkingDates dates)
        {
            return Ok(_calculator.CalculateRate(dates.Entry, dates.Exit));
        }

        public class ParkingDates
        {
            public DateTime Entry { get; set; }
            public DateTime Exit { get; set; }
        }
    }
}
