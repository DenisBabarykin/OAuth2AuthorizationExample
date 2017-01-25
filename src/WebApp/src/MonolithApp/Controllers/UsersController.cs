using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MonolithApp.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        [Route("api/me")]
        public IActionResult Me()
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

                var userInfo = authManager.GetUserInfo(token);
                return Ok(userInfo);
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }
    }
}
