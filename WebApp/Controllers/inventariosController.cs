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
    public class inventariosController : Controller
    {
        private readonly DBContext _context;

        public inventariosController(DBContext context)
        {
            _context = context;
        }

        // GET: inventarios
        public async Task<IActionResult> Index()
        {
              return _context.inventario != null ? 
                          View(await _context.inventario.ToListAsync()) :
                          Problem("Entity set 'DBContext.inventario'  is null.");
        }

        // GET: inventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.inventario == null)
            {
                return NotFound();
            }

            var inventario = await _context.inventario
                .FirstOrDefaultAsync(m => m.id == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: inventarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: inventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_corte,id_item,lote,serie,fecha_venc,ubicacion,cantidad,estado,created_at,updated_at")] inventario inventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventario);
        }

        // GET: inventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.inventario == null)
            {
                return NotFound();
            }

            var inventario = await _context.inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            return View(inventario);
        }

        // POST: inventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_corte,id_item,lote,serie,fecha_venc,ubicacion,cantidad,estado,created_at,updated_at")] inventario inventario)
        {
            if (id != inventario.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!inventarioExists(inventario.id))
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
            return View(inventario);
        }

        // GET: inventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.inventario == null)
            {
                return NotFound();
            }

            var inventario = await _context.inventario
                .FirstOrDefaultAsync(m => m.id == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: inventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.inventario == null)
            {
                return Problem("Entity set 'DBContext.inventario'  is null.");
            }
            var inventario = await _context.inventario.FindAsync(id);
            if (inventario != null)
            {
                _context.inventario.Remove(inventario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool inventarioExists(int id)
        {
          return (_context.inventario?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
