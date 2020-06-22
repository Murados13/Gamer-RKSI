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
    public class PlayersController : APIController
    {
        private readonly ApplicationContext _context;

        public PlayersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string code)
        {
            using (ApplicationContext db = this._context)
            {
                if (IsTokenFalse(db, "API_PLAYERS", code)) return BadRequest();
                return new OkObjectResult(JsonConvert.SerializeObject(db.players.ToList()));
            }
        }
    }
}
