using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using BookingManagement;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingServer.Controllers
{
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        // GET api/values/5
        [HttpGet("{userLogin}")]
        public IActionResult Get(string userLogin)
        {
            try
            {
                return Json(new BookingManager().GetUserBookings(userLogin));
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            try
            {
                return Json(new BookingManager().AddBooking(booking));
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }
    }
}

