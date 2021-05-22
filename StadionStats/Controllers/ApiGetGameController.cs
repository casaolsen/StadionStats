using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StadionStats.Data;
using StadionStats.Models;
using Microsoft.AspNetCore.Authorization;

namespace Stadionstats.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGetGameController : ControllerBase
    {
        private readonly StatContext _context;

        public ApiGetGameController(StatContext context)
        {
            _context = context;
        }

        // GET: Liste med kampe
        [HttpGet]
        public IQueryable<Object> GetGames()
        {
            return _context.Games;
        }
    }
}