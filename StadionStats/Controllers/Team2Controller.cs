using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StadionStats.Data;
using StadionStats.Models;
using StadionStats.ViewModels.StatViewModels;
using Microsoft.AspNetCore.Authorization;

namespace StadionStats.Controllers
{
    public class Team2Controller : Controller
    {
        private readonly StatContext _context;

        public Team2Controller(StatContext context)
        {
            _context = context;
        }

        // GET: Team2
        public async Task<IActionResult> Index()
        {
            var statContext = _context.Team2s.Include(t => t.Stadion).Include(t => t.Liga);
            return View(await statContext.ToListAsync());
        }

        // GET: Team2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team2 = await _context.Team2s
                .Include(t => t.Stadion)
                .Include(t => t.Sponsor)
                .Include(t => t.Liga)
                .Include(s => s.HomeGames)
                .ThenInclude(g => g.AwayTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team2 == null)
            {
                return NotFound();
            }

            return View(team2);
        }

        // GET: Team2/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["StadionID"] = new SelectList(_context.Stadion, "StadionID", "Navn");
            ViewData["LigaID"] = new SelectList(_context.Liga, "LigaID", "Navn");
            return View();
        }

        // POST: Team2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,StadionID,Seasontickets,Image,Logo,Sponsortext,LigaID")] Team2 team2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StadionID"] = new SelectList(_context.Stadion, "StadionID", "Navn", team2.StadionID);
            ViewData["LigaID"] = new SelectList(_context.Liga, "LigaID", "Navn", team2.LigaID);
            return View(team2);
        }

        // GET: Team2/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team2 = await _context.Team2s.FindAsync(id);
            if (team2 == null)
            {
                return NotFound();
            }
            ViewData["StadionID"] = new SelectList(_context.Stadion, "StadionID", "Navn", team2.StadionID);
            ViewData["LigaID"] = new SelectList(_context.Liga, "LigaID", "Navn", team2.LigaID);
            return View(team2);
        }

        // POST: Team2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,StadionID,Seasontickets,Image,Logo,Sponsortext,LigaID")] Team2 team2)
        {
            if (id != team2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Team2Exists(team2.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StadionID"] = new SelectList(_context.Stadion, "StadionID", "Navn", team2.StadionID);
            ViewData["LigaID"] = new SelectList(_context.Liga, "LigaID", "Navn", team2.LigaID);
            return View(team2);
        }

        // GET: Team2/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team2 = await _context.Team2s
                .Include(t => t.Stadion)
                .Include(t => t.Liga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team2 == null)
            {
                return NotFound();
            }

            return View(team2);
        }

        // POST: Team2/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team2 = await _context.Team2s.FindAsync(id);
            _context.Team2s.Remove(team2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Team2Exists(int id)
        {
            return _context.Team2s.Any(e => e.Id == id);
        }
    }
}
