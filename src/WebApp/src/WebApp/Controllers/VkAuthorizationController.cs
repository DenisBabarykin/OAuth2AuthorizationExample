using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            return View("AuthorizationSucces");
        }
    }
}
