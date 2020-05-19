using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace app.Controllers
{
    [Route("api/v1/[controller]")]
    public class LoginController : APIController
    {
        private readonly ApplicationContext _context;

        public LoginController(ApplicationContext context)
        {
            _context = context;
        }

        const string API_LOGIN = "API_LOGIN";

        [HttpGet]
        public IActionResult Get(string email, string password, string code)
        {
            using (ApplicationContext db = this._context)
            {
                if (IsTokenFalse(db,API_LOGIN,code)) return BadRequest();

                var w = db.players.FirstOrDefault(p => p.Email == email.ToLower());

                if (w == null)
                {
                    db.logs.Add(new LogClass(API_LOGIN, "Пользователь не найден"));
                    db.SaveChanges();
                    return BadRequest();
                }

                if (w.Password == password)
                {
                    var cur = db.currencies.FirstOrDefault(p => p.TokenId == code);
                    var b = db.balances.FirstOrDefault(p => p.Code == cur.Code && p.UserId == w.id);
                    w.Amount = b == null ? 0 : b.Amount;
                    db.logs.Add(new LogClass(API_LOGIN, $"Вход пользователя {w.id} в игру {cur.License}"));
                    db.SaveChanges();
                    return new OkObjectResult(JsonConvert.SerializeObject(w));
                }

                db.logs.Add(new LogClass(API_LOGIN,"Неверный пароль"));
                return BadRequest();

            }
        }
    }
}
