using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleWebRequests;
using Client.Models;
using Microsoft.Extensions.Logging;

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
                logger.LogWarning("Access token recieved: " + accessToken);

                dynamic userInfoResponse = RESTRequest.PostAsUrlEncodedWithJsonResponse("https://api.vk.com/method/users.get", new Dictionary<string, string>()
                {
                    { "access_token", accessToken },
                    { "v", "5.59" },
                    { "user_ids", tokenResponse.user_id.ToString() },
                    { "fields", "sex, bdate, photo_100" },
                    { "name_case", "nom" }
                });
                logger.LogWarning("User info recieved");

                VkUserInfo userInfo = new VkUserInfo()
                {
                    Id = userInfoResponse.response[0].id.ToString(),
                    Name = userInfoResponse.response[0].first_name,
                    Surname = userInfoResponse.response[0].last_name,
                    Sex = userInfoResponse.response[0].sex == 1 ? "Female" : "Male",
                    BirthDate = userInfoResponse.response[0].bdate,
                    PhotoUrl = userInfoResponse.response[0].photo_100
                };

                return View("AuthorizationSucces", userInfo);
            }
            catch (Exception e)
            {
                logger.LogError(new EventId(), e, "Error");
                return View("Exception", e);
            }
        }
    }
}
