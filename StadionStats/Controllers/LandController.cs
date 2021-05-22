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
    [Authorize(Roles = "Admin")]
    public class LandController : Controller
    {
        private readonly StatContext _context;

        public LandController(StatContext context)
        {
            _context = context;
        }

        // GET: Land
        public async Task<IActionResult> Index()
        {
            var statContext = _context.Land.Include(g => g.Liga);
            return View(await statContext.ToListAsync());
        }

        // GET: Land/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var land = await _context.Land
                .Include(l => l.Liga)
                .FirstOrDefaultAsync(m => m.LandID == id);
            if (land == null)
            {
                return NotFound();
            }

            return View(land);
        }

        // GET: Land/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Land/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LandID,Landenavn")] Land land)
        {
            if (ModelState.IsValid)
            {
                _context.Add(land);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(land);
        }

        // GET: Land/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var land = await _context.Land.FindAsync(id);
            if (land == null)
            {
                return NotFound();
            }
            return View(land);
        }

        // POST: Land/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LandID,Landenavn")] Land land)
        {
            if (id != land.LandID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(land);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LandExists(land.LandID))
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
            return View(land);
        }

        // GET: Land/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var land = await _context.Land
                .FirstOrDefaultAsync(m => m.LandID == id);
            if (land == null)
            {
                return NotFound();
            }

            return View(land);
        }

        // POST: Land/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var land = await _context.Land.FindAsync(id);
            _context.Land.Remove(land);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LandExists(int id)
        {
            return _context.Land.Any(e => e.LandID == id);
        }
    }
}
