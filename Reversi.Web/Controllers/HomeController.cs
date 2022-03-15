using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reversi.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Reversi.Logic;

namespace Reversi.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var game = new Game();

            //testing game
            game.PlaceCounter(4, 4);
            game.PlaceCounter(3, 3);
            game.PlaceCounter(3, 4);
            game.PlaceCounter(4, 3);
            //--test code

            return View(game.GetOutput());
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
