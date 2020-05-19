using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using Newtonsoft.Json;

namespace app.Controllers
{
    [Route("api/v1/[controller]")]
    public class CurrenciesController : APIController
    {
        [HttpGet]
        public IActionResult Get(string code)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (IsTokenFalse(db, "API_CURRENCIES", code)) return BadRequest();
                return new OkObjectResult(JsonConvert.SerializeObject(db.currencies.ToList()));
            }
        }
    }
}
