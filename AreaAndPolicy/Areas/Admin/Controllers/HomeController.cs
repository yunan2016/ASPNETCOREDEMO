using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AreaAndPolicy.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AreaAndPolicy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}