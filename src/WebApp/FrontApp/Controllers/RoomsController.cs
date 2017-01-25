using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Authorization;
using Newtonsoft.Json;
using SimpleWebRequests;
using Models;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly AppSettings appSettings;

        public RoomsController(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        // GET: api/values
        [HttpGet]
        [Route("api/rooms/{lowerBorder}/{upBorder}")]
        public IActionResult Get(int lowerBorder, int upBorder)
        {
            try
            {
                return Json(RESTRequest.GetWithJsonResponse<List<Room>>(appSettings.RoomServerUri + $"api/rooms/{lowerBorder}/{upBorder}"));
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }
    }
}
