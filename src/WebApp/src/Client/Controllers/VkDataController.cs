using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Microsoft.Extensions.Logging;
using SimpleWebRequests;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class VkDataController : Controller
    {
        private ILogger<VkDataController> logger;

        public VkDataController(ILogger<VkDataController> logger)
        {
            this.logger = logger;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                if (!HttpContext.Session.Keys.Contains("VkAccessToken") || !HttpContext.Session.Keys.Contains("VkUserId"))
                    return RedirectToAction("AuthorizationFail", "Status");

                string accessToken = HttpContext.Session.GetString("VkAccessToken");
                string userId = HttpContext.Session.GetString("VkUserId");

                dynamic userInfoResponse = RESTRequest.PostAsUrlEncodedWithJsonResponse("https://api.vk.com/method/users.get", new Dictionary<string, string>()
                {
                    { "access_token", accessToken },
                    { "v", "5.59" },
                    { "user_ids", userId },
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

                return View("VkUserInfoPage", userInfo);
            }
            catch (Exception e)
            {
                logger.LogError(new EventId(), e, "Error");
                return RedirectToAction("Exception", "Status", new { exception = JsonConvert.SerializeObject(e) });
            }
        }
    }
}
