using StadionStats.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StadionStats.ViewComponents
{
    public class TeamViewComponent: ViewComponent
    {
        private StatContext _context;
        public TeamViewComponent(StatContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Team2s.ToListAsync());
        }

    }
}
