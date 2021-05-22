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
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGetLigaController : ControllerBase
    {
        private readonly StatContext _context;

        public ApiGetLigaController(StatContext context)
        {
            _context = context;
        }

        // GET: Liste med ligaer
        [HttpGet]
        public IQueryable<Object> GetLiga()
        {
            return _context.Liga;
        }

    }
}