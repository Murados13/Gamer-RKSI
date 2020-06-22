using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;

namespace app.Controllers
{
    public class APIController : Controller
    {
        public Boolean IsTokenFalse(ApplicationContext db, string group, string code)
        {
            if (code == null)
            {
                db.logs.Add(new LogClass(group, "Попытка анонимной авторизации"));
                db.SaveChanges();
                return true;
            }

            var cur = db.currencies.FirstOrDefault(p => p.TokenId == code);

            if (cur == null)
            {
                db.logs.Add(new LogClass(group, "Неверный код авторизации"));
                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
