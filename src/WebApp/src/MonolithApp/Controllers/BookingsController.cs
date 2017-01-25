using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Authorization;
using BookingManagement;
using Newtonsoft.Json;
using Billing;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MonolithApp.Controllers
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
                if (!Request.Headers.ContainsKey("access_token"))
                    return Unauthorized();

                string token = Request.Headers["access_token"];
                var authManager = new AuthorizationManager();
                if (!authManager.CheckTokenBelonging(token, userLogin))
                    return Json(new { error = "Invalid token", error_description = "Given token doesn't belong to this user" });
                var tokenStatus = authManager.CheckToken(token);
                if (tokenStatus == TokenStatusesEnum.Invalid)
                    return Json(new { error = "Invalid token", error_description = "Given token is invalid" });
                else if (tokenStatus == TokenStatusesEnum.Expired)
                    return Json(new { error = "Expired token", error_description = "Given token is expired. Please refresh it." });

                var bookings = new BookingManager().GetUserBookings(userLogin);
                var billManager = new BillingManager();
                foreach (var booking in bookings)
                    booking.Bill = billManager.GetBill(booking.Id);

                return Json(bookings);
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Booking booking)
        {
            try
            {
                if (!Request.Headers.ContainsKey("access_token"))
                    return Unauthorized();

                string token = Request.Headers["access_token"];
                var authManager = new AuthorizationManager();
                var tokenStatus = authManager.CheckToken(token);
                if (tokenStatus == TokenStatusesEnum.Invalid)
                    return Json(new { error = "Invalid token", error_description = "Given token is invalid" });
                else if (tokenStatus == TokenStatusesEnum.Expired)
                    return Json(new { error = "Expired token", error_description = "Given token is expired. Please refresh it." });

                //var booking = JsonConvert.DeserializeObject<Booking>(bookingStr);
                booking.InitId();
                var savedBooking = new BookingManager().AddBooking(booking);
                var bill = new BillingManager().AddBill(booking.DaysCount * 1500, "OOO Sberbank 91929393234923", savedBooking.Id);
                savedBooking.Bill = bill;
                return Json(savedBooking);
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }
    }
}
