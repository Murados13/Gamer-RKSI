using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace app.Controllers
{
    [Route("api/v1/[controller]")]
    public class BalanceController : APIController
    {
        private readonly ApplicationContext _context;

        public BalanceController(ApplicationContext context)
        {
            _context = context;
        }

        const string API_BALANCE = "API_BALANCE";

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post(int id, decimal amount, string code)
        {
            using (ApplicationContext db = this._context)
            {
                if (IsTokenFalse(db, API_BALANCE, code)) return BadRequest();

                if (id == 0)
                {
                    db.logs.Add(new LogClass(API_BALANCE, "Пользователь не задан"));
                    db.SaveChanges();
                    return BadRequest();
                }

                var u = db.players.FirstOrDefault(p => p.id == id);

                if (u == null)
                {
                    db.logs.Add(new LogClass(API_BALANCE, "Пользователь не найден"));
                    db.SaveChanges();
                    return BadRequest();
                }

                if (amount == 0)
                {
                    db.logs.Add(new LogClass(API_BALANCE, "Сумма не задана"));
                    db.SaveChanges();
                    return BadRequest();
                }

                var cur = db.currencies.FirstOrDefault(p => p.TokenId == code);

                var l1 = new LedgerClass();
                var l2 = new LedgerClass();

                using (var trans = db.Database.BeginTransaction())
                {
                    var x = db.ledger.OrderByDescending(x=>x.id).FirstOrDefault();
                    l1.id = x == null ? 1 : 1 + x.id;
                    l1.Code = cur.Code;
                    l1.Operation = amount> 0? 1 : 2;
                    l1.Amount = -amount;
                    l1.UserId = cur.UserId;
                    l1.Description = "Изменение баланса";
                    l1.CorOppId = l1.id + 1;
                    l1.RegUserId = cur.UserId;

                    l2.id = l1.CorOppId;
                    l2.Code = cur.Code;
                    l2.Operation = amount < 0 ? 2 : 1;
                    l2.Amount = amount;
                    l2.UserId = id;
                    l2.Description = "Изменение баланса";
                    l2.CorOppId = l1.id;
                    l2.RegUserId = cur.UserId;

                    db.ledger.Add(l1);
                    db.ledger.Add(l2);
                    db.logs.Add(new LogClass(API_BALANCE, $"Изменение баланса пользователя {u.id} на {amount} металл {cur.Code}"));
                    db.SaveChanges();

                    trans.Commit();
                }

                return new OkResult();
            }
        }
    }
}
