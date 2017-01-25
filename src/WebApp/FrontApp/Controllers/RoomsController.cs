using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Authorization;
using Newtonsoft.Json;
using SimpleWebRequests;
using Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontApp.Controllers
{
    public class RoomsController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Route("api/rooms/{lowerBorder}/{upBorder}")]
        public IActionResult Get(int lowerBorder, int upBorder)
        {
            try
            {
                return Json(RESTRequest.GetWithJsonResponse<List<Room>>($"http://localhost:26547/api/rooms/{lowerBorder}/{upBorder}"));
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }
    }
}
