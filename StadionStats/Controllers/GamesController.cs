using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StadionStats.Data;
using StadionStats.Models;
using Microsoft.AspNetCore.Authorization;

namespace StadionStats.Controllers
{
    public class GamesController : Controller
    {
        private readonly StatContext _context;

        public GamesController(StatContext context)
        {
            _context = context;
        }

        //GET: Games
        public async Task<IActionResult> Index()
        {
            var statContext = _context.Games.Include(g => g.AwayTeam).Include(g => g.HomeTeam).Include(g => g.Liga).Include(g => g.Stadion);
            return View(await statContext.ToListAsync());
        }

        //GET: Games
        public async Task<IActionResult> Nordicbet()
        {
            var statContext = _context.Games.Include(g => g.AwayTeam).Include(g => g.HomeTeam).Include(g => g.Liga).Include(g => g.Stadion);
            return View(await statContext.ToListAsync());
        }


        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.AwayTeam)
                .Include(g => g.HomeTeam)
                .Include(g => g.Liga)
                .Include(g => g.Stadion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AwayTeamId"] = new SelectList(_context.Team2s, "Id", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.Team2s, "Id", "Name");
            ViewData["LigaID"] = new SelectList(_context.Liga, "LigaID", "Navn");
            ViewData["StadionID"] = new SelectList(_context.Stadion, "StadionID", "Navn");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,HomeTeamId,AwayTeamId,HomeScore,GuestScore,Attendance,Tv,StadionID,LigaID,IsCorona")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Team2s, "Id", "Name", game.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team2s, "Id", "Name", game.HomeTeamId);
            ViewData["LigaID"] = new SelectList(_context.Liga, "LigaID", "Navn", game.LigaID);
            ViewData["StadionID"] = new SelectList(_context.Stadion, "StadionID", "Navn", game.StadionID);
            return View(game);
        }

        // GET: Games/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Team2s, "Id", "Name", game.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team2s, "Id", "Name", game.HomeTeamId);
            ViewData["LigaID"] = new SelectList(_context.Liga, "LigaID", "Navn", game.LigaID);
            ViewData["StadionID"] = new SelectList(_context.Stadion, "StadionID", "Navn", game.StadionID);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,HomeTeamId,AwayTeamId,HomeScore,GuestScore,Attendance,Tv,StadionID,LigaID,IsCorona")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["AwayTeamId"] = new SelectList(_context.Team2s, "Id", "Name", game.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team2s, "Id", "Name", game.HomeTeamId);
            ViewData["LigaID"] = new SelectList(_context.Liga, "LigaID", "Navn", game.LigaID);
            ViewData["StadionID"] = new SelectList(_context.Stadion, "StadionID", "Navn", game.StadionID);
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.AwayTeam)
                .Include(g => g.HomeTeam)
                .Include(g => g.Liga)
                .Include(g => g.Stadion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
