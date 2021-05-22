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
    public class StadionsController : Controller
    {
        private readonly StatContext _context;

        public StadionsController(StatContext context)
        {
            _context = context;
        }

        // GET: Stadions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stadion.ToListAsync());
        }

        // GET: Stadions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadion = await _context.Stadion
                .FirstOrDefaultAsync(m => m.StadionID == id);
            if (stadion == null)
            {
                return NotFound();
            }

            return View(stadion);
        }

        // GET: Stadions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stadions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StadionID,Navn,AttendanceCapacity")] Stadion stadion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stadion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stadion);
        }

        // GET: Stadions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadion = await _context.Stadion.FindAsync(id);
            if (stadion == null)
            {
                return NotFound();
            }
            return View(stadion);
        }

        // POST: Stadions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StadionID,Navn,AttendanceCapacity")] Stadion stadion)
        {
            if (id != stadion.StadionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stadion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StadionExists(stadion.StadionID))
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
            return View(stadion);
        }

        // GET: Stadions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadion = await _context.Stadion
                .FirstOrDefaultAsync(m => m.StadionID == id);
            if (stadion == null)
            {
                return NotFound();
            }

            return View(stadion);
        }

        // POST: Stadions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stadion = await _context.Stadion.FindAsync(id);
            _context.Stadion.Remove(stadion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StadionExists(int id)
        {
            return _context.Stadion.Any(e => e.StadionID == id);
        }
    }
}
