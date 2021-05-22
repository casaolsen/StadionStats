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
    public class LigaController : Controller
    {
        private readonly StatContext _context;

        public LigaController(StatContext context)
        {
            _context = context;
        }

        // GET: Liga
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var statContext = _context.Liga.Include(l => l.Land);
            return View(await statContext.ToListAsync());
        }

        // GET: Liga
        public async Task<IActionResult> Ligaer()
        {
            var statContext = _context.Liga.Include(l => l.Land);
            return View(await statContext.ToListAsync());
        }


        // GET: Liga/Details/5
        public async Task<IActionResult> Liga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Liga
                .Include(l => l.Land)
                .Include(e => e.Games).ThenInclude(g => g.Stadion)
                .Include(e => e.Games).ThenInclude(f => f.HomeTeam)
                .Include(e => e.Games).ThenInclude(f => f.AwayTeam)
                .FirstOrDefaultAsync(m => m.LigaID == id);

            if (liga == null)
            {
                return NotFound();
            }

            return View(liga);
        }

        // GET: Liga/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Liga
                .Include(l => l.Land)
                .FirstOrDefaultAsync(m => m.LigaID == id);

            if (liga == null)
            {
                return NotFound();
            }

            return View(liga);
        }



        // GET: Liga/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["LandID"] = new SelectList(_context.Land, "LandID", "Landenavn");
            return View();
        }

        // POST: Liga/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LigaID,Navn,Logo,LandID")] Liga liga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(liga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LandID"] = new SelectList(_context.Land, "LandID", "Landenavn", liga.LandID);
            return View(liga);
        }

        // GET: Liga/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Liga.FindAsync(id);
            if (liga == null)
            {
                return NotFound();
            }
            ViewData["LandID"] = new SelectList(_context.Land, "LandID", "Landenavn", liga.LandID);
            return View(liga);
        }

        // POST: Liga/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LigaID,Navn,Logo,LandID")] Liga liga)
        {
            if (id != liga.LigaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(liga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LigaExists(liga.LigaID))
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
            ViewData["LandID"] = new SelectList(_context.Land, "LandID", "Landenavn", liga.LandID);
            return View(liga);
        }

        // GET: Liga/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Liga
                .Include(l => l.Land)
                .FirstOrDefaultAsync(m => m.LigaID == id);
            if (liga == null)
            {
                return NotFound();
            }

            return View(liga);
        }

        // POST: Liga/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var liga = await _context.Liga.FindAsync(id);
            _context.Liga.Remove(liga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LigaExists(int id)
        {
            return _context.Liga.Any(e => e.LigaID == id);
        }
    }
}
