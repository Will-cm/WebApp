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
    public class picking_nsalidaController : Controller
    {
        private readonly DBContext _context;

        public picking_nsalidaController(DBContext context)
        {
            _context = context;
        }

        // GET: picking_nsalida
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.picking_nsalida.Include(p => p.item).Include(p => p.nota_salida);
            return View(await dBContext.ToListAsync());
        }

        // GET: picking_nsalida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.picking_nsalida == null)
            {
                return NotFound();
            }

            var picking_nsalida = await _context.picking_nsalida
                .Include(p => p.item)
                .Include(p => p.nota_salida)
                .FirstOrDefaultAsync(m => m.id == id);
            if (picking_nsalida == null)
            {
                return NotFound();
            }

            return View(picking_nsalida);
        }

        // GET: picking_nsalida/Create
        public IActionResult Create()
        {
            ViewData["id_item"] = new SelectList(_context.item, "id", "id");
            ViewData["id_nsalida"] = new SelectList(_context.nota_salida, "id", "id");
            return View();
        }

        // POST: picking_nsalida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_nsalida,id_item,lote,fecha_venc,serie,cantidad,cant_picking,usuario,estado,created_at,updated_at")] picking_nsalida picking_nsalida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(picking_nsalida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", picking_nsalida.id_item);
            ViewData["id_nsalida"] = new SelectList(_context.nota_salida, "id", "id", picking_nsalida.id_nsalida);
            return View(picking_nsalida);
        }

        // GET: picking_nsalida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.picking_nsalida == null)
            {
                return NotFound();
            }

            var picking_nsalida = await _context.picking_nsalida.FindAsync(id);
            if (picking_nsalida == null)
            {
                return NotFound();
            }
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", picking_nsalida.id_item);
            ViewData["id_nsalida"] = new SelectList(_context.nota_salida, "id", "id", picking_nsalida.id_nsalida);
            return View(picking_nsalida);
        }

        // POST: picking_nsalida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_nsalida,id_item,lote,fecha_venc,serie,cantidad,cant_picking,usuario,estado,created_at,updated_at")] picking_nsalida picking_nsalida)
        {
            if (id != picking_nsalida.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(picking_nsalida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!picking_nsalidaExists(picking_nsalida.id))
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
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", picking_nsalida.id_item);
            ViewData["id_nsalida"] = new SelectList(_context.nota_salida, "id", "id", picking_nsalida.id_nsalida);
            return View(picking_nsalida);
        }

        // GET: picking_nsalida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.picking_nsalida == null)
            {
                return NotFound();
            }

            var picking_nsalida = await _context.picking_nsalida
                .Include(p => p.item)
                .Include(p => p.nota_salida)
                .FirstOrDefaultAsync(m => m.id == id);
            if (picking_nsalida == null)
            {
                return NotFound();
            }

            return View(picking_nsalida);
        }

        // POST: picking_nsalida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.picking_nsalida == null)
            {
                return Problem("Entity set 'DBContext.picking_nsalida'  is null.");
            }
            var picking_nsalida = await _context.picking_nsalida.FindAsync(id);
            if (picking_nsalida != null)
            {
                _context.picking_nsalida.Remove(picking_nsalida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool picking_nsalidaExists(int id)
        {
          return (_context.picking_nsalida?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
