using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Authorization;
using Newtonsoft.Json;
using SimpleWebRequests;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontApp.Controllers
{
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        private readonly AppSettings appSettings;

        public BookingsController(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

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

                var bookings = RESTRequest.GetWithJsonResponse<List<Booking>>(appSettings.BookingServerUri + $"api/bookings/{userLogin}");
                foreach (var booking in bookings)
                    booking.Bill = RESTRequest.GetWithJsonResponse<Bill>(appSettings.BillingServerUri + $"api/bills/{booking.Id}");

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

                booking.InitId();
                var savedBooking = RESTRequest.PostAsJsonWithJsonResponse<Booking>(appSettings.BookingServerUri + "api/bookings", booking);
                Bill bill = null;
                try
                {
                    bill = RESTRequest.PostAsJsonWithJsonResponse<Bill>(appSettings.BillingServerUri + "api/bills", new Bill()
                        {
                            Price = booking.DaysCount * 1500,
                            Requisites = "OOO Sberbank 91929393234923",
                            BookingId = savedBooking.Id
                        });
                    if (bill == null)
                        throw new JsonException("Bill remote saving failed");
                }
                catch(Exception ex)
                {
                    RESTRequest.DeleteWithJsonResponse<Booking>(appSettings.BookingServerUri + $"api/bookings/{savedBooking.Id}");
                    throw;
                }
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
