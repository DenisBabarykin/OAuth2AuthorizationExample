using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Authorization;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MonolithApp.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(Dictionary<string, string> parameters)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Token(Dictionary<string, string> parameters)
        {
            try
            {

                if (!parameters.ContainsKey("login") || !parameters.ContainsKey("password") || !parameters.ContainsKey("client_id") || !parameters.ContainsKey("redirect_uri"))
                    return BadRequest(new { error = "Incorrect parameters", error_description = "There are not all of needed params" });

                string login = parameters["login"];
                string password = parameters["password"];
                string clientId = parameters["client_id"];
                string redirectUri = parameters["redirect_uri"];

                var authManager = new AuthorizationManager();
                if (!authManager.CorrectCredentials(clientId, login, password))
                    return Ok(new { error = "Incorrect credentials", error_description = "One or few credentials are wrong" });

                authManager.CreateToken(clientId, redirectUri, login);

                return Ok();
            }
            catch(Exception e)
            {
                return Ok(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }
    }
}
