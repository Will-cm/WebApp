using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class racksController : Controller
    {
        private readonly DBContext _context;

        public racksController(DBContext context)
        {
            _context = context;
        }

        // GET: racks
        public async Task<IActionResult> Index()
        {
              return _context.rack != null ? 
                          View(await _context.rack.ToListAsync()) :
                          Problem("Entity set 'DBContext.rack'  is null.");
        }

        // GET: racks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.rack == null)
            {
                return NotFound();
            }

            var rack = await _context.rack
                .FirstOrDefaultAsync(m => m.id == id);
            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }

        // GET: racks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: racks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,codigo,descripcion,capacidad_max,cant_filas,cant_colum,estado,id_sucursal,created_at,updated_at")] rack rack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rack);
        }

        // GET: racks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.rack == null)
            {
                return NotFound();
            }

            var rack = await _context.rack.FindAsync(id);
            if (rack == null)
            {
                return NotFound();
            }
            return View(rack);
        }

        // POST: racks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,codigo,descripcion,capacidad_max,cant_filas,cant_colum,estado,id_sucursal,created_at,updated_at")] rack rack)
        {
            if (id != rack.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!rackExists(rack.id))
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
            return View(rack);
        }

        // GET: racks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.rack == null)
            {
                return NotFound();
            }

            var rack = await _context.rack
                .FirstOrDefaultAsync(m => m.id == id);
            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }

        // POST: racks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.rack == null)
            {
                return Problem("Entity set 'DBContext.rack'  is null.");
            }
            var rack = await _context.rack.FindAsync(id);
            if (rack != null)
            {
                _context.rack.Remove(rack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool rackExists(int id)
        {
          return (_context.rack?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
