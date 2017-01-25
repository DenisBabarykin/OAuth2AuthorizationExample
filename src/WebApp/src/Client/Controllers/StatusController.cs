using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class StatusController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult AuthorizationSucceeded()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult AuthorizationFail()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Exception(string exception)
        {
            return View(JsonConvert.DeserializeObject<Exception>(exception));
        }
    }
}
