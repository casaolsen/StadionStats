using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadionStats.Models;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using StadionStats.Data;
using StadionStats.ViewModels.StatViewModels;

namespace StadionStats.Controllers
{
    public class HomeController : Controller
    {
        private readonly StatContext _context;

        public HomeController(StatContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OmStadionStats()
        {
            return View();
        }

        public IActionResult Cookies()
        {
            return View();
        }

        public IActionResult Privatlivspolitik()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        // Home attendance stats SQL

        public async Task<ActionResult> Hjemmebanestatistik()
        {
            List<HjemmekampeCount> groups2 = new List<HjemmekampeCount>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    string query = "SELECT a.HomeTeamId, b.Name, b.logo, SUM(Attendance) AS HomeTotal, COUNT(*) AS HomeGames "
                        + "FROM Games a "
                        + "INNER JOIN Team2s b ON a.HomeTeamId = b.Id "
                        + "WHERE a.LigaID = 1 "
                        + "GROUP BY a.HomeTeamId, b.Name, b.logo ";

                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new HjemmekampeCount { HomeTeam = reader.GetInt32(0), HomeTeamName = reader.GetString(1), TeamImage = reader.GetString(2), HomeTotal = reader.GetInt32(3), HomeGames = reader.GetInt32(4), HomeAvg = reader.GetInt32(3) / reader.GetInt32(4) };
                            groups2.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return View(groups2);
        }


        // TV stats SQL

        public async Task<ActionResult> TVstatistik()
        {
            List<TVCount> groups2 = new List<TVCount>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    string query = "SELECT a.HomeTeamId, b.Name, b.logo, SUM(TV) AS HomeTotal, COUNT(*) AS HomeGames "
                        + "FROM Games a "
                        + "INNER JOIN Team2s b ON a.HomeTeamId = b.Id "
                        + "WHERE a.LigaID = 1 "
                        + "GROUP BY a.HomeTeamId, b.Name, b.logo ";

                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new TVCount { HomeTeam = reader.GetInt32(0), HomeTeamName = reader.GetString(1), TeamImage = reader.GetString(2), HomeTotal = reader.GetInt32(3), HomeGames = reader.GetInt32(4), HomeAvg = reader.GetInt32(3) / reader.GetInt32(4) };
                            groups2.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return View(groups2);

        }


        // Home Liga stats LINQ

        public async Task<ActionResult> LigaStatistik()
        {
            IQueryable<LigaGroup> data =
                from Game in _context.Games
                join Liga in _context.Liga on Game.LigaID equals Liga.LigaID
                select new {Liga.LigaID, Liga.Navn, Liga.Logo, Game.Attendance} into ligaGroup
                group ligaGroup by ligaGroup.LigaID into ligaGroup
                select new LigaGroup() 
                {
                    LigaID = ligaGroup.Key,
                    LigaName = ligaGroup.Select(Liga => Liga.Navn).FirstOrDefault(),
                    LigaLogo = ligaGroup.Select(Liga => Liga.Logo).FirstOrDefault(),
                    LigaCount = ligaGroup.Count(),
                    HomeTotal = ligaGroup.Sum(Game => Game.Attendance),
                    HomeAvg = ligaGroup.Sum(Game => Game.Attendance)/ligaGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}