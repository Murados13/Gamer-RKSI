using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using app.Models;
using app.Extensions;
using System.Data;
using Microsoft.AspNetCore.Mvc.Filters;


namespace app.Controllers
{
    public class HomeController : Controller
    {
        public String sid { get => this.ClaimSid(); }

        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.sid = sid;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Orders()
        {
            using (ApplicationContext db = this._context)
            {
                ViewBag.list = db.orders.ToList();
            }
            return View();
        }

        [Authorize]
        public IActionResult Order()
        {
            using (ApplicationContext db = this._context)
            {
                var l = db.currencies.ToList();
                foreach (CurrencyClass cur in l)
                {
                    var b = db.balances.FirstOrDefault(p => p.Code == cur.Code && p.UserId == cur.UserId);
                    cur.Amount = b == null ? 0 : b.Amount;
                }
                ViewBag.list = l;
            }

            return View();
        }
        [Authorize]
        public IActionResult MakeOrder(OrderClass order)
        {

                using (ApplicationContext db = this._context)
                {
                    var w = db.players.FirstOrDefault(p => p.Email == sid);
                    order.UserId = w.id;

                    db.orders.Add(order);
                    db.SaveChanges();
                }

                return Redirect("/Home/Orders");
        }
        

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Registered(PlayerClass player)
        {
            try
            {
                using (ApplicationContext db = this._context)
                {
                    player.Email = player.Email.ToLower();

                    var w = db.players.FirstOrDefault(p => p.Email == player.Email);

                    if (w != null)
                    {
                        ViewBag.message = "Игрок с таким email уже зарегистрирован";
                        return View();
                    }

                    db.players.Add(player);
                    db.SaveChanges();
                }

                ViewBag.message = $"Пользователь <b>{player.Email}</b> теперь в игре!";
            }
            catch (Exception e)
            {
                ViewBag.message = e.ToString();
            }

            return View();
        }
        [Authorize]
        public IActionResult Players()
        {
            using (ApplicationContext db = this._context)
            {
                ViewBag.players = db.players.ToList();
            }
            return View();
        }
        [Authorize]
        public IActionResult Logs()
        {
            using (ApplicationContext db = this._context)
            {
                ViewBag.list = db.logs.OrderByDescending(p => p.id).ToList();
            }
            return View();
        }


        public IActionResult Player(int id)
        {
            using (ApplicationContext db = this._context)
            {
                var w = db.players.FirstOrDefault(p => p.id == id);
                var l = db.currencies.ToList();

                foreach (CurrencyClass cur in l)
                {
                    var b = db.balances.FirstOrDefault(p => p.Code == cur.Code && p.UserId == w.id);
                    cur.Amount = b == null ? 0 : b.Amount;
                }

                ViewBag.player = w;
                ViewBag.list = l;
            }
            return View();
        }

        public IActionResult Details(int id, string c)
        {
            using (ApplicationContext db = this._context)
            {
                var player = db.players.FirstOrDefault(p => p.id == id);
                var cur = db.currencies.FirstOrDefault(p => p.Code == c);

                if (c == null)
                {
                    ViewBag.list = db.ledger.Where(p => p.UserId == id).ToList();
                }
                else
                {
                    var b = db.balances.FirstOrDefault(p => p.Code == cur.Code && p.UserId == player.id);
                    player.Amount = b == null ? 0 : b.Amount;

                    ViewBag.list = db.ledger.Where(p => p.UserId == id && p.Code == c).ToList();
                    ViewBag.cur = cur;
                }
                ViewBag.player = player;
            }
            return View();
        }

        public IActionResult Companies()
        {
            using (ApplicationContext db = this._context)
            {
                var l = db.currencies.ToList();
                foreach (CurrencyClass cur in l)
                {
                    var b = db.balances.FirstOrDefault(p => p.Code == cur.Code && p.UserId == cur.UserId);
                    cur.Amount = b == null ? 0 : b.Amount;
                }
                ViewBag.list = l;
            }
            return View();
        }

        public IActionResult Confidentiality()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        //Games IActionResults

        public IActionResult SteamExistence()
        {
            return View("Games/SteamExistence");
        }
        public IActionResult ReactiveSteal()
        {
            return View("Games/ReactiveSteal");
        }
        public IActionResult DragonFlex()
        {
            return View("Games/DragonFlex");
        }
        public IActionResult Dungeon()
        {
            return View("Games/Dungeon");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
