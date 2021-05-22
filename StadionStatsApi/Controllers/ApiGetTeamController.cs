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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGetTeamController : ControllerBase
    {
        private readonly StatContext _context;

        public ApiGetTeamController(StatContext context)
        {
            _context = context;
        }

        // GET: Liste med teams
        [HttpGet]
        public IQueryable<TeamViewModel> GetTeam2s()
        {
            return _context.Team2s
                .Include(p => p.Stadion)
                .Select(p => new TeamViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Seasontickets = p.Seasontickets,
                    Stadionnavn = p.Stadion.Navn,
                    AttendanceCapacity = p.Stadion.AttendanceCapacity

                })
                .AsQueryable();
        }

        // GET: Team detaljer
        [HttpGet("{id2}")]
        public async Task<ActionResult<Team2>> GetTeam2(int id2)
        {
            var team2 = await _context.Team2s

            .Include(s => s.Stadion)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id2);

            return team2;
        }
   
    }
}