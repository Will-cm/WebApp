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
    public class detalle_ingresoController : Controller
    {
        private readonly DBContext _context;

        public detalle_ingresoController(DBContext context)
        {
            _context = context;
        }

        // GET: detalle_ingreso
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.detalle_ingreso.Include(d => d.item);
            return View(await dBContext.ToListAsync());
        }

        // GET: detalle_ingreso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.detalle_ingreso == null)
            {
                return NotFound();
            }

            var detalle_ingreso = await _context.detalle_ingreso
                .Include(d => d.item)
                .FirstOrDefaultAsync(m => m.id == id);
            if (detalle_ingreso == null)
            {
                return NotFound();
            }
            return View(detalle_ingreso);
        }

        // GET: detalle_ingreso/Create
        public IActionResult Create()
        {
            ViewData["id_item"] = new SelectList(_context.item, "id", "id");
            return View();
        }

        // POST: detalle_ingreso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_ningreso,id_item,lote,fecha_venc,serie,cantidad,estado")] detalle_ingreso detalle_ingreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle_ingreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", detalle_ingreso.id_item);
            return View(detalle_ingreso);
        }

        // GET: detalle_ingreso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.detalle_ingreso == null)
            {
                return NotFound();
            }

            var detalle_ingreso = await _context.detalle_ingreso.FindAsync(id);
            if (detalle_ingreso == null)
            {
                return NotFound();
            }
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", detalle_ingreso.id_item);
            return View(detalle_ingreso);
        }

        // POST: detalle_ingreso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_ningreso,id_item,lote,fecha_venc,serie,cantidad,estado")] detalle_ingreso detalle_ingreso)
        {
            if (id != detalle_ingreso.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle_ingreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!detalle_ingresoExists(detalle_ingreso.id))
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
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", detalle_ingreso.id_item);
            return View(detalle_ingreso);
        }

        // GET: detalle_ingreso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.detalle_ingreso == null)
            {
                return NotFound();
            }

            var detalle_ingreso = await _context.detalle_ingreso
                .Include(d => d.item)
                .FirstOrDefaultAsync(m => m.id == id);
            if (detalle_ingreso == null)
            {
                return NotFound();
            }

            return View(detalle_ingreso);
        }

        // POST: detalle_ingreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.detalle_ingreso == null)
            {
                return Problem("Entity set 'DBContext.detalle_ingreso'  is null.");
            }
            var detalle_ingreso = await _context.detalle_ingreso.FindAsync(id);
            if (detalle_ingreso != null)
            {
                _context.detalle_ingreso.Remove(detalle_ingreso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool detalle_ingresoExists(int id)
        {
          return (_context.detalle_ingreso?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
