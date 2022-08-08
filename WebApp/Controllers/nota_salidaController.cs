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
    public class nota_salidaController : Controller
    {
        private readonly DBContext _context;

        public nota_salidaController(DBContext context)
        {
            _context = context;
        }

        // GET: nota_salida
        public async Task<IActionResult> Index()
        {
            var nota_salida = _context.nota_salida
                .Include(c => c.sucursal)
                .OrderByDescending(c => c.id)
                .AsNoTracking();   //add
            return _context.nota_salida != null ? 
                          View(await nota_salida.ToListAsync()) :
                          Problem("Entity set 'DBContext.nota_salida'  is null.");
        }

        // GET: nota_salida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.nota_salida == null)
            {
                return NotFound();
            }

            var nota_salida = await _context.nota_salida
                .FirstOrDefaultAsync(m => m.id == id);
            if (nota_salida == null)
            {
                return NotFound();
            }
            var detalle_salida = _context.detalle_salida  //add
                .Include(c => c.item)
                .Where(d => d.id_nsalida == id)
                .OrderByDescending(c => c.id)
                .AsNoTracking();   //add
            ViewData["detalle_salida"] = detalle_salida; //add
            return View(nota_salida);
        }

        // GET: nota_salida/Create
        public IActionResult Create()
        {
            ViewData["cod_vendedor"] = new SelectList(_context.users, "nombre", "nombre"); //add
            ViewData["id_sucursal"] = new SelectList(_context.sucursal, "id", "descripcion"); //add
            return View();
        }

        // POST: nota_salida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nro_doc,fecha_emis,prioridad,programacion,cod_cliente,raz_social,cod_vendedor,estado,id_sucursal,created_at,updated_at")] nota_salida nota_salida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota_salida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nota_salida);
        }

        // GET: nota_salida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.nota_salida == null)
            {
                return NotFound();
            }

            var nota_salida = await _context.nota_salida.FindAsync(id);
            if (nota_salida == null)
            {
                return NotFound();
            }
            ViewData["cod_vendedor"] = new SelectList(_context.users, "nombre", "nombre"); //add
            ViewData["id_sucursal"] = new SelectList(_context.sucursal, "id", "descripcion"); //add
            return View(nota_salida);
        }

        // POST: nota_salida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nro_doc,fecha_emis,prioridad,programacion,cod_cliente,raz_social,cod_vendedor,estado,id_sucursal,created_at,updated_at")] nota_salida nota_salida)
        {
            if (id != nota_salida.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota_salida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!nota_salidaExists(nota_salida.id))
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
            return View(nota_salida);
        }

        // GET: nota_salida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.nota_salida == null)
            {
                return NotFound();
            }

            var nota_salida = await _context.nota_salida
                .FirstOrDefaultAsync(m => m.id == id);
            if (nota_salida == null)
            {
                return NotFound();
            }

            return View(nota_salida);
        }

        // POST: nota_salida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.nota_salida == null)
            {
                return Problem("Entity set 'DBContext.nota_salida'  is null.");
            }
            var nota_salida = await _context.nota_salida.FindAsync(id);
            if (nota_salida != null)
            {
                _context.nota_salida.Remove(nota_salida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool nota_salidaExists(int id)
        {
          return (_context.nota_salida?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
