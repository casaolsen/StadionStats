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
    public class SeasonticketsViewComponent : ViewComponent
    {
        private StatContext _context;

        public SeasonticketsViewComponent(StatContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SeasonticketsCount> groups3 = new List<SeasonticketsCount>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    string query = "SELECT TOP 3 Id, Name, Seasontickets, Logo "
                        + "FROM Team2s "
                        + "ORDER BY Seasontickets DESC";

                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new SeasonticketsCount { Team = reader.GetInt32(0), TeamName = reader.GetString(1), SeasonticketsSales = reader.GetInt32(2), TeamImage = reader.GetString(3) };
                            groups3.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return View(groups3);

        }

    }

}