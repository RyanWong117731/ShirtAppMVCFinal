using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShirtAppMVCFinal.Models;
using ShirtAppMVCFinal.Data;
using Microsoft.EntityFrameworkCore;

namespace ShirtAppMVCFinal.Controllers
{
    public class BrowseController : Controller
    {
        private readonly ApplicationContext _context;

        public BrowseController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Browse()
        {
            return View(await _context.Shirts.ToListAsync());
        }

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
    }
}
