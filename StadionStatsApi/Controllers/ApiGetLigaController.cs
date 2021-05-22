using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StadionstatsApi.Data;
using StadionstatsApi.Models;
using Microsoft.AspNetCore.Authorization;
using StadionstatsApi.ViewModels;

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