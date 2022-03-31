﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace asp_test.Controllers
{
    public class HelloWorldController : Controller
    {

        //Get: /HelloWorld/

        public IActionResult Index()
        {
            return View();
        }

        //
        //GET: /HelloWorld/Welcome/
        //Requires using System.Text.Encodings.Web;

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
