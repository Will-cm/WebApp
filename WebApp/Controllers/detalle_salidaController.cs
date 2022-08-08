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
    public class detalle_salidaController : Controller
    {
        private readonly DBContext _context;

        public detalle_salidaController(DBContext context)
        {
            _context = context;
        }

        // GET: detalle_salida
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.detalle_salida.Include(d => d.item);
            return View(await dBContext.ToListAsync());
        }

        // GET: detalle_salida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.detalle_salida == null)
            {
                return NotFound();
            }

            var detalle_salida = await _context.detalle_salida
                .Include(d => d.item)
                .FirstOrDefaultAsync(m => m.id == id);
            if (detalle_salida == null)
            {
                return NotFound();
            }

            return View(detalle_salida);
        }

        // GET: detalle_salida/Create
        public IActionResult Create()
        {
            ViewData["id_item"] = new SelectList(_context.item, "id", "id");
            return View();
        }

        // POST: detalle_salida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_nsalida,id_item,lote,fecha_venc,serie,cantidad,estado")] detalle_salida detalle_salida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle_salida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", detalle_salida.id_item);
            return View(detalle_salida);
        }

        // GET: detalle_salida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.detalle_salida == null)
            {
                return NotFound();
            }

            var detalle_salida = await _context.detalle_salida.FindAsync(id);
            if (detalle_salida == null)
            {
                return NotFound();
            }
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", detalle_salida.id_item);
            return View(detalle_salida);
        }

        // POST: detalle_salida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_nsalida,id_item,lote,fecha_venc,serie,cantidad,estado")] detalle_salida detalle_salida)
        {
            if (id != detalle_salida.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle_salida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!detalle_salidaExists(detalle_salida.id))
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
            ViewData["id_item"] = new SelectList(_context.item, "id", "id", detalle_salida.id_item);
            return View(detalle_salida);
        }

        // GET: detalle_salida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.detalle_salida == null)
            {
                return NotFound();
            }

            var detalle_salida = await _context.detalle_salida
                .Include(d => d.item)
                .FirstOrDefaultAsync(m => m.id == id);
            if (detalle_salida == null)
            {
                return NotFound();
            }

            return View(detalle_salida);
        }

        // POST: detalle_salida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.detalle_salida == null)
            {
                return Problem("Entity set 'DBContext.detalle_salida'  is null.");
            }
            var detalle_salida = await _context.detalle_salida.FindAsync(id);
            if (detalle_salida != null)
            {
                _context.detalle_salida.Remove(detalle_salida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool detalle_salidaExists(int id)
        {
          return (_context.detalle_salida?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
