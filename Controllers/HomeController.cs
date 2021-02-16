using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebFront.Models;

namespace WebFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string date)
        {
            var querydate = "";
            if (date != null)
            {
                querydate = date.Replace("-", "/");
            }
            var endpoint = Environment.GetEnvironmentVariable("FUNCTIONS_URL");
            endpoint += querydate;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var res = client.SendAsync(request).Result;
            var jsonstring = res.Content.ReadAsStringAsync().Result;

            @ViewData["date"] = date;
            @ViewData["json"] = jsonstring;
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
