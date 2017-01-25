using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoomManagement;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RoomServer.Controllers
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
                return Json(new RoomManager().GetRooms(lowerBorder, upBorder));
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
