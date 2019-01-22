using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using corewithIdentity.Models;
using System.Security.Claims;

namespace corewithIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {

            ClaimsPrincipal principal = User as ClaimsPrincipal;
            ClaimsIdentity identity = principal.Identities.SingleOrDefault();
            identity.AddClaim(new Claim(ClaimTypes.Role, "RoleName"));



            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            return View(new Task());
        }
        [HttpPost]
        public IActionResult Edit(Task task)
        {
            // ...
            return View(task);
        }
        public class Task
        {
            public int TaskId { get; set; }
        }

        [HttpGet]
        public ActionResult Contact(string id)
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        [HttpPost]
        public IActionResult Contact(Task task)
        {
            // ...
            return View(task);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
