using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgramWeb.Data;
using ProgramWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace ProgramWeb.Controllers
{
    public class ValuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Values
        public async Task<IActionResult> Index()
        {
            return View(await _context.Value.ToListAsync());
        }

        // GET: Values/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: Values/ShowSearchResult
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Value.Where(j=>j.ValQue.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Values/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var value = await _context.Value
                .FirstOrDefaultAsync(m => m.Id == id);
            if (value == null)
            {
                return NotFound();
            }

            return View(value);
        }

        // GET: Values/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Values/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ValQue,ValAns")] Value value)
        {
            if (ModelState.IsValid)
            {
                value.Email = User.Identity.Name;
                _context.Add(value);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(value);
        }

        // GET: Values/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var value = await _context.Value.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return View(value);
        }

        // POST: Values/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ValQue,ValAns")] Value value)
        {
            if (id != value.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    value.Email = User.Identity.Name;
                    _context.Update(value);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValueExists(value.Id))
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
            return View(value);
        }

        // GET: Values/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var value = await _context.Value
                .FirstOrDefaultAsync(m => m.Id == id);
            if (value == null)
            {
                return NotFound();
            }

            return View(value);
        }

        // POST: Values/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var value = await _context.Value.FindAsync(id);
            _context.Value.Remove(value);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValueExists(int id)
        {
            return _context.Value.Any(e => e.Id == id);
        }
    }
}
