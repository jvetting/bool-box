using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoolBox.Data;
using BoolBox.Models;

namespace BoolBox.Controllers
{
    public class BoolsController : Controller
    {
        private readonly BoolBoxContext _context;

        public BoolsController(BoolBoxContext context)
        {
            _context = context;
        }

        // GET: Bools
        public async Task<IActionResult> Index(string boolType, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Bool
                                            orderby m.Type
                                            select m.Type;

            var bools = from m in _context.Bool
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                bools = bools.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(boolType))
            {
                bools = bools.Where(x => x.Type == boolType);
            }

            var boolTypeVM = new BoolTypeViewModel
            {
                Types = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Bools = await bools.ToListAsync()
            };

            return View(boolTypeVM);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Bools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @bool = await _context.Bool
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@bool == null)
            {
                return NotFound();
            }

            return View(@bool);
        }

        // GET: Bools/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,Type,Duration,SpotifyID,Repeat")] Bool @bool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@bool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@bool);
        }

        // GET: Bools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @bool = await _context.Bool.FindAsync(id);
            if (@bool == null)
            {
                return NotFound();
            }
            return View(@bool);
        }

        // POST: Bools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,Type,Duration,SpotifyID,Repeat")] Bool @bool)
        {
            if (id != @bool.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@bool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoolExists(@bool.Id))
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
            return View(@bool);
        }

        // GET: Bools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @bool = await _context.Bool
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@bool == null)
            {
                return NotFound();
            }

            return View(@bool);
        }

        // POST: Bools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @bool = await _context.Bool.FindAsync(id);
            _context.Bool.Remove(@bool);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoolExists(int id)
        {
            return _context.Bool.Any(e => e.Id == id);
        }
    }
}
