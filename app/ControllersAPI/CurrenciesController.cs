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
        private readonly ApplicationContext _context;

        public CurrenciesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string code)
        {
            using (ApplicationContext db =this._context)
            {
                if (IsTokenFalse(db, "API_CURRENCIES", code)) return BadRequest();
                return new OkObjectResult(JsonConvert.SerializeObject(db.currencies.ToList()));
            }
        }
    }
}
