using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleWebRequests;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class VkAuthorizationController : Controller
    {
        // GET: /<controller>/
        public IActionResult VkAuthorization(Dictionary<string, string> parameters)
        {
            if (!parameters.ContainsKey("code"))
                return View("AuthorizationFail");
            string code = parameters["code"];
            dynamic response = RESTRequest.PostAsUrlEncodedWithJsonResponse("https://oauth.vk.com/access_token", new Dictionary<string, string>()
            {
                { "client_id", "5671241"},
                { "client_secret", "7nHh8NwJJeaZrW7juOyZ" },
                { "redirect_uri", "http://localhost:56230/VkAuthorization" },
                { "code", code }
            });
            return View("AuthorizationSucces");
        }
    }
}
