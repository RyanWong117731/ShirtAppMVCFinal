using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShirtAppMVCFinal.Data;
using ShirtAppMVCFinal.Models;

namespace ShirtAppMVCFinal.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext _context;

        public OrderController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionID,Email,ShirtID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(transaction.ShirtID);
            return View(transaction);
        }

        private void PopulateDepartmentsDropDownList(object selectedShirt = null)
        {

            var shirtsQuery = from d in _context.Shirts
                              orderby d.ShirtName
                                   select d;
            ViewBag.ShirtID = new SelectList(shirtsQuery.AsNoTracking(), "ShirtID", "ShirtName", selectedShirt);
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }
    }
}
