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
        public IActionResult Code(Dictionary<string, string> parameters)
        {
            try
            {
                if (!parameters.ContainsKey("login") || !parameters.ContainsKey("password") || !parameters.ContainsKey("client_id") || !parameters.ContainsKey("redirect_uri"))
                    return Json(new { error = "Incorrect parameters", error_description = "There are not all of needed params" });

                string login = parameters["login"];
                string password = parameters["password"];
                string clientId = parameters["client_id"];
                string redirectUri = parameters["redirect_uri"];

                var authManager = new AuthorizationManager();
                if (!authManager.CorrectCredentials(clientId, login, password))
                    return Json(new { error = "Incorrect credentials", error_description = "One or few credentials are wrong" });

                string code = authManager.GetCode(clientId, redirectUri, login);

                return Ok(new { uri = redirectUri + "?code=" + code.Replace("+", "%2B") });
            }
            catch(Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }

        [HttpGet]
        public IActionResult Token(Dictionary<string, string> parameters)
        {
            try
            {
                if (!parameters.ContainsKey("code"))
                    return Json(new { error = "Incorrect parameters", error_description = "There must be a 'code' parameter in url" });
                if (!Request.Headers.ContainsKey("client_id") || !Request.Headers.ContainsKey("client_secret"))
                    return Json(new { error = "Incorrect parameters", error_description = "There must be headers 'client_id' and 'client_secret'" });

                string code = parameters["code"];
                string clientId = Request.Headers["client_id"];
                string clientSecret = Request.Headers["client_secret"];

                var token = new AuthorizationManager().GetToken(code, clientId, clientSecret);
                if (token == null)
                    return Json(new { error = "Incorrect credentials", error_description = "One or few credentials are wrong" });

                return Ok(new
                {
                    access_token = token.AuthorizationToken,
                    token_type = "bearer",
                    refresh_token = token.RefreshToken,
                    expires_in = Math.Round(token.AuthTokenExpiration.AddSeconds(20).Subtract(DateTime.Now).TotalSeconds).ToString(),
                });
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }

        [HttpGet]
        public IActionResult RefreshToken(Dictionary<string, string> parameters)
        {
            try
            {
                if (!parameters.ContainsKey("refresh_token"))
                    return Json(new { error = "Incorrect parameters", error_description = "There must be a 'refresh_token' parameter in url" });
                if (!Request.Headers.ContainsKey("client_id") || !Request.Headers.ContainsKey("client_secret"))
                    return Json(new { error = "Incorrect parameters", error_description = "There must be headers 'client_id' and 'client_secret'" });

                string refreshToken = parameters["refresh_token"];
                string clientId = Request.Headers["client_id"];
                string clientSecret = Request.Headers["client_secret"];

                var token = new AuthorizationManager().RefreshToken(refreshToken, clientId, clientSecret);
                if (token == null)
                    return Json(new { error = "Incorrect credentials", error_description = "One or few credentials are wrong" });

                return Ok(new
                {
                    access_token = token.AuthorizationToken,
                    token_type = "bearer",
                    refresh_token = token.RefreshToken,
                    expires_in = Math.Round(token.AuthTokenExpiration.AddSeconds(20).Subtract(DateTime.Now).TotalSeconds).ToString(),
                });
            }
            catch (Exception e)
            {
                return Json(new { error = "Internal server error", error_description = JsonConvert.SerializeObject(e, Formatting.Indented) });
            }
        }
    }
}
