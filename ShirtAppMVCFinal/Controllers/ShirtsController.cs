using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShirtAppMVCFinal.Data;
using ShirtAppMVCFinal.Models;

namespace ShirtAppMVCFinal
{
    public class ShirtsController : Controller
    {
        private readonly ApplicationContext _context;

        public ShirtsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Shirts
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            var shirts = from s in _context.Shirts
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    shirts = shirts.OrderByDescending(s => s.ShirtName);
                    break;
                case "Price":
                    shirts = shirts.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    shirts = shirts.OrderByDescending(s => s.Price);
                    break;
                default:
                    shirts = shirts.OrderBy(s => s.ShirtName);
                    break;
            }
            return View(await _context.Shirts.ToListAsync());
        }

        // GET: Shirts/Details/5    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts
                .FirstOrDefaultAsync(m => m.ShirtID == id);
            if (shirt == null)
            {
                return NotFound();
            }

            return View(shirt);
        }

        // GET: Shirts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shirts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShirtID,ShirtName,Price,Size")] Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shirt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shirt);
        }

        // GET: Shirts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts.FindAsync(id);
            if (shirt == null)
            {
                return NotFound();
            }
            return View(shirt);
        }

        // POST: Shirts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShirtID,ShirtName,Price,Size")] Shirt shirt)
        {
            if (id != shirt.ShirtID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shirt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShirtExists(shirt.ShirtID))
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
            return View(shirt);
        }

        // GET: Shirts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts
                .FirstOrDefaultAsync(m => m.ShirtID == id);
            if (shirt == null)
            {
                return NotFound();
            }

            return View(shirt);
        }

        // POST: Shirts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shirt = await _context.Shirts.FindAsync(id);
            _context.Shirts.Remove(shirt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShirtExists(int id)
        {
            return _context.Shirts.Any(e => e.ShirtID == id);
        }
    }
}
