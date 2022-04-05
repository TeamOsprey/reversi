using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reversi.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Reversi.Logic;
using Reversi.Web.Services;
using Reversi.Web.Services.Interfaces;

namespace Reversi.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IGameService _gameService;

        public HomeController(ILogger<HomeController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            _gameService.PlaceCounter(4, 4);
            _gameService.PlaceCounter(3, 3);
            _gameService.PlaceCounter(3, 4);
            _gameService.PlaceCounter(4, 3);

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
