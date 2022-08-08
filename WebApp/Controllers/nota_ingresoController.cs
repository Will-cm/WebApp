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
    public class nota_ingresoController : Controller
    {
        private readonly DBContext _context;

        public nota_ingresoController(DBContext context)
        {
            _context = context;
        }

        // GET: nota_ingreso
        public async Task<IActionResult> Index()
        {
            var nota_ingreso = _context.nota_ingreso
                .Include(c => c.sucursal)
                .OrderByDescending(c => c.id)
                .AsNoTracking();   //add
            return _context.nota_ingreso != null ? 
                          View(await nota_ingreso.ToListAsync()) :
                          Problem("Entity set 'DBContext.nota_ingreso'  is null.");
        }

        // GET: nota_ingreso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.nota_ingreso == null)
            {
                return NotFound();
            }

            var nota_ingreso = await _context.nota_ingreso
                .FirstOrDefaultAsync(m => m.id == id);
            if (nota_ingreso == null)
            {
                return NotFound();
            }
            var detalle_ingreso = _context.detalle_ingreso  //add
                .Include(c => c.item)
                .Where(d => d.id_ningreso == id)
                .OrderByDescending(c => c.id)
                .AsNoTracking();   //add
            ViewData["detalle_ingreso"] = detalle_ingreso; //add
            return View(nota_ingreso);
        }

        // GET: nota_ingreso/Create
        public IActionResult Create()
        {
            ViewData["id_sucursal"] = new SelectList(_context.sucursal, "id", "descripcion"); //add
            return View();
        }

        // POST: nota_ingreso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nro_doc,movimiento,usuario,estado,id_sucursal,id_suc_origen,created_at,updated_at")] nota_ingreso nota_ingreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota_ingreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nota_ingreso);
        }

        // GET: nota_ingreso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.nota_ingreso == null)
            {
                return NotFound();
            }

            var nota_ingreso = await _context.nota_ingreso.FindAsync(id);
            if (nota_ingreso == null)
            {
                return NotFound();
            }
            ViewData["id_sucursal"] = new SelectList(_context.sucursal, "id", "descripcion"); //add
            return View(nota_ingreso);
        }

        // POST: nota_ingreso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nro_doc,movimiento,usuario,estado,id_sucursal,id_suc_origen,created_at,updated_at")] nota_ingreso nota_ingreso)
        {
            if (id != nota_ingreso.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota_ingreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!nota_ingresoExists(nota_ingreso.id))
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
            return View(nota_ingreso);
        }

        // GET: nota_ingreso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.nota_ingreso == null)
            {
                return NotFound();
            }

            var nota_ingreso = await _context.nota_ingreso
                .FirstOrDefaultAsync(m => m.id == id);
            if (nota_ingreso == null)
            {
                return NotFound();
            }

            return View(nota_ingreso);
        }

        // POST: nota_ingreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.nota_ingreso == null)
            {
                return Problem("Entity set 'DBContext.nota_ingreso'  is null.");
            }
            var nota_ingreso = await _context.nota_ingreso.FindAsync(id);
            if (nota_ingreso != null)
            {
                _context.nota_ingreso.Remove(nota_ingreso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool nota_ingresoExists(int id)
        {
          return (_context.nota_ingreso?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
