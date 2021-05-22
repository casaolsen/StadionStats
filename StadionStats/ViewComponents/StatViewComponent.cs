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

namespace StadionStats.ViewComponents
{
    public class StatViewComponent : ViewComponent
    {
        private StatContext _context;

        public StatViewComponent(StatContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
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


                    //string query = "SELECT TOP 3 a.HomeTeamId, b.Name, b.Logo, SUM(Attendance) AS HomeTotal, COUNT(*) AS HomeGames "
                    //    + "FROM Games a "
                    //    + "INNER JOIN Team2s b ON a.HomeTeamId = b.Id "
                    //    + "WHERE a.LigaID = 1 /*AND IsCorona = 0 */"
                    //    + "GROUP BY a. HomeTeamId, b.Name, b.Logo";

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

    }

}