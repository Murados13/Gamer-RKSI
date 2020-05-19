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
    public class PlayerController : APIController
    {
        [HttpGet]
        public IActionResult Get(int id, string code)
        {
            const string API_PLAYER = "API_PLAYER";

            using (ApplicationContext db = new ApplicationContext())
            {
                if (IsTokenFalse(db, API_PLAYER, code)) return BadRequest();

                var w = db.players.FirstOrDefault(p => p.id == id);

                if (w == null)
                {
                    db.logs.Add(new LogClass(API_PLAYER, "Пользователь не найден"));
                    db.SaveChanges();
                    return BadRequest();
                }

                var cur = db.currencies.FirstOrDefault(p => p.TokenId == code);
                var b = db.balances.FirstOrDefault(p => p.Code == cur.Code && p.UserId == w.id);
                w.Amount = b == null ? 0 : b.Amount;

                return new OkObjectResult(JsonConvert.SerializeObject(w));
            }
        }
    }
}

