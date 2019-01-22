﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AreaAndPolicy.Models;
using Microsoft.AspNetCore.Authorization;
using static AreaAndPolicy.Startup;

namespace AreaAndPolicy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Policy = "AtLeast21")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Route("/home/custom-error-response")]
        public IActionResult CustomErrorResponse()
        {
            var customErrorResponseFeature = HttpContext.Features.Get<ICustomErrorResponseFeature>();

            var view = View(customErrorResponseFeature);
            view.StatusCode = customErrorResponseFeature.StatusCode;
            return view;
        }

        public IActionResult Contact()
        {
                
           
            ViewData["Message"] = "Your contact page.";

            return View();
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
