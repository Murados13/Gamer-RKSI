using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;


namespace app.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrderController : APIController
    {
        private readonly ApplicationContext _context;

        const string API_ORDER = "API_ORDER";

        public OrderController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("process/{id}")]
        public ActionResult Process(int id)
        {
            using (ApplicationContext db = this._context)
            {
                var order = db.orders.Find(id);

                if (order == null)
                {
                    db.logs.Add(new LogClass(API_ORDER, $"Заявка {id} не найдена"));
                    db.SaveChanges();
                    return BadRequest();
                }

                if (order.State == OrderClass.STATE_PROCESSED)
                {
                    // Не обрабатываем уже обраотанные заказы
                    return BadRequest();
                }

                using (var trans = db.Database.BeginTransaction())
                {
                    db.logs.Add(new LogClass(API_ORDER, $"Изменение состояния заявки с {order.State} на {OrderClass.STATE_PROCESSED}"));
                    order.State = OrderClass.STATE_PROCESSED;
                    db.SaveChanges();

                    trans.Commit();
                }

                return new OkResult();
            }
        }
    }
}
