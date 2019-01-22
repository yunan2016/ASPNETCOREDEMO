using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AreaAndPolicy.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using static AreaAndPolicy.Startup;

namespace AreaAndPolicy.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("[controller]/{statusCode:int?}")]
        public IActionResult Error(int? statusCode = null)
        {

            if (statusCode.HasValue)
            {

               

                if (statusCode == (int)HttpStatusCode.BadRequest)
                {
                    return View("status404");
                }
            }

            return View();
        }
    }
}