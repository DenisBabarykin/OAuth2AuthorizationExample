using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Authorization;
using Billing;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MonolithApp.Controllers
{
    [Route("api/[controller]")]
    public class BillsController : Controller
    {
        // GET api/values/5
        [HttpGet("{bookingId}")]
        public IActionResult Get(Guid bookingId)
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

                return Json(new BillingManager().GetBill(bookingId));
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Bill bill)
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

                return Json(new BillingManager().AddBill(bill.Price, bill.Requisites, bill.BookingId));
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid billId)
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

                var billManager = new BillingManager();
                return Json(new BillingManager().RemoveBill(billId));
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }
    }
}
