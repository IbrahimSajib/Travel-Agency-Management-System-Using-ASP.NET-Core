using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Management_System.Models;

namespace Travel_Agency_Management_System.Controllers
{
    public class SpotsController : Controller
    {
        private readonly TravelDbContext db;

        public SpotsController(TravelDbContext db)
        {
            this.db = db;
        }

        // GET: Spots
        public async Task<IActionResult> Index(int page=1)
        {
            ViewBag.totalPages = (int)Math.Ceiling((double)db.Spots.Count() / 5);
            ViewBag.currentPage = page;
            return View(await db.Spots.OrderBy(x => x.SpotId).Skip((page - 1) * 5).Take(5).ToListAsync());
        }

        // GET: Spots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpotId,SpotName")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Add(spot);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spot);
        }

        // GET: Spots/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Spots == null)
            {
                return NotFound();
            }

            var spot = await db.Spots.FindAsync(id);
            if (spot == null)
            {
                return NotFound();
            }
            return View(spot);
        }

        // POST: Spots/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpotId,SpotName")] Spot spot)
        {
            if (id != spot.SpotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(spot);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpotExists(spot.SpotId))
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
            return View(spot);
        }

        // GET: Spots/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Spots == null)
            {
                return NotFound();
            }

            var spot = await db.Spots
                .FirstOrDefaultAsync(m => m.SpotId == id);
            if (spot == null)
            {
                return NotFound();
            }

            return View(spot);
        }

        // POST: Spots/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Spots == null)
            {
                return Problem("Entity set 'TravelDbContext.Spots'  is null.");
            }
            var spot = await db.Spots.FindAsync(id);
            if (spot != null)
            {
                db.Spots.Remove(spot);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpotExists(int id)
        {
          return (db.Spots?.Any(e => e.SpotId == id)).GetValueOrDefault();
        }
    }
}
