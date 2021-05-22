using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadionstatsApi.Models;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using StadionstatsApi.Data;

namespace StadionstatsApi.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}