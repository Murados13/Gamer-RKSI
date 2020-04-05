using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rksi.Models;

namespace rksi.Controllers
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
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult SteamExistence()
        {
            return View();
        }

        public IActionResult DragonFlex()
        {
            return View();
        }

        //public IActionResult Registered(PlayerClass player)
        //{
        //    try
        //    {
        //        using (ApplicationContext db = new ApplicationContext())
        //        {
        //            player.Email = player.Email.ToLower();

        //            var w = db.players.FirstOrDefault(p => p.Email == player.Email);

        //            if (w != null)
        //            {
        //                ViewBag.message = "Игрок с таким email уже зарегистрирован";
        //                return View();
        //            }

        //            db.players.Add(player);
        //            db.SaveChanges();
        //        }

        //        ViewBag.message = $"Пользователь <b>{player.Email}</b> теперь в игре!";
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.message = e.ToString();
        //    }

        //    return View();
        //}

        public IActionResult Companies()
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
