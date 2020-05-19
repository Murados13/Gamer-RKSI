using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using app.Models;
using System.Data;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace app.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            ViewBag.sid = null;
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<IActionResult> enter(String email, String password)
        {
            dynamic res;

            try
            {
                using (ApplicationContext db = this._context)
                {
                    var _email = email.ToLower();
                    var w = db.players.FirstOrDefault(p => p.Email == _email);

                    if (w == null)
                    {
                        res = new { code = "401", description = "Пользователь не зарегистрирован" };
                        return new OkObjectResult(JsonConvert.SerializeObject(res));
                    }

                    if (password == w.Password)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Sid, email)
                        };

                        var userIdentity = new ClaimsIdentity(claims, "auth");

                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(principal);
                        ViewBag.sid = email;

                        res = new { code = "200", description = "ok" };
                    }
                    else
                    {
                        res = new { code = "400", description = "Пароли не совпадают" };
                    }
                }
            }
            catch (Exception e)
            {
                res = new { code = "400", description = e.ToString() };
            }
            return new OkObjectResult(JsonConvert.SerializeObject(res));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
