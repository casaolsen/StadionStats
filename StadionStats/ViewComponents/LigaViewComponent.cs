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
    public class LigaViewComponent : ViewComponent
    {
        private StatContext _context;

        public LigaViewComponent(StatContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clubID)
        {

            List<LigaGroup> groups = new List<LigaGroup>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    string query = "SELECT a.LigaID, b.Navn, SUM(Attendance) AS HomeTotal, COUNT(*) AS LigaCount "
                        + "FROM Games a "
                        + "INNER JOIN Liga b ON a.LigaID = b.LigaID "
                        + "WHERE a.HomeTeamId = " +clubID
                        + "GROUP BY a.LigaID, b.Navn";
                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new LigaGroup { LigaID = reader.GetInt32(0), LigaName = reader.GetString(1), HomeTotal = reader.GetInt32(2), LigaCount = reader.GetInt32(3), HomeAvg = reader.GetInt32(2) / reader.GetInt32(3) };
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return View(groups);

        }

    }

}