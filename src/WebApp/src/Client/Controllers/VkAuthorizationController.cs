using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleWebRequests;
using Client.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Client.Controllers
{
    public class VkAuthorizationController : Controller
    {
        private ILogger<HomeController> logger;

        public VkAuthorizationController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        // GET: /<controller>/
        public IActionResult Index(Dictionary<string, string> parameters)
        {
            try
            {
                logger.LogWarning("Authorization started");

                if (!parameters.ContainsKey("code"))
                {
                    logger.LogDebug("Authorization declined");
                    return View("AuthorizationFail");
                }
                string code = parameters["code"];
                logger.LogWarning("Code recieved: " + code);

                dynamic tokenResponse = RESTRequest.PostAsUrlEncodedWithJsonResponse("https://oauth.vk.com/access_token", new Dictionary<string, string>()
                {
                    { "client_id", "5671241"},
                    { "client_secret", "7nHh8NwJJeaZrW7juOyZ" },
                    { "redirect_uri", "http://localhost:56230/VkAuthorization" },
                    { "code", code }
                });
                string accessToken = tokenResponse.access_token;
                string userId = tokenResponse.user_id.ToString();
                logger.LogWarning("Access token recieved: " + accessToken);
                HttpContext.Session.SetString("VkAccessToken", accessToken);
                HttpContext.Session.SetString("VkUserId", userId);
                return RedirectToAction("AuthorizationSucceeded", "Status");
            }
            catch (Exception e)
            {
                logger.LogError(new EventId(), e, "Error");
                return RedirectToAction("Exception", "Status", new { exception = JsonConvert.SerializeObject(e) });
            }
        }
    }
}
