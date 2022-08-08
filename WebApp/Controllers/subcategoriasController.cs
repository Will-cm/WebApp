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
    public class subcategoriasController : Controller
    {
        private readonly DBContext _context;

        public subcategoriasController(DBContext context)
        {
            _context = context;
        }

        // GET: subcategorias
        public async Task<IActionResult> Index()
        {
            var subcat = _context.subcategoria
                .Include(c => c.categoria)
                .OrderByDescending(c => c.id)
                .AsNoTracking();   //add
            return _context.subcategoria != null ? 
                          View(await subcat.ToListAsync()) :
                          Problem("Entity set 'DBContext.subcategoria'  is null.");
        }

        // GET: subcategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.subcategoria == null)
            {
                return NotFound();
            }

            var subcategoria = await _context.subcategoria
                .FirstOrDefaultAsync(m => m.id == id);
            if (subcategoria == null)
            {
                return NotFound();
            }

            return View(subcategoria);
        }

        // GET: subcategorias/Create
        public IActionResult Create()
        {
            ViewData["cod_categoria"] = new SelectList(_context.categoria, "id", "descripcion"); //add
            return View();
        }

        // POST: subcategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,codigo,descripcion,estado,cod_categoria")] subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subcategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subcategoria);
        }

        // GET: subcategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.subcategoria == null)
            {
                return NotFound();
            }

            var subcategoria = await _context.subcategoria.FindAsync(id);
            if (subcategoria == null)
            {
                return NotFound();
            }
            ViewData["cod_categoria"] = new SelectList(_context.categoria, "id", "descripcion"); //add
            return View(subcategoria);
        }

        // POST: subcategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,codigo,descripcion,estado,cod_categoria")] subcategoria subcategoria)
        {
            if (id != subcategoria.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!subcategoriaExists(subcategoria.id))
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
            return View(subcategoria);
        }

        // GET: subcategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.subcategoria == null)
            {
                return NotFound();
            }

            var subcategoria = await _context.subcategoria
                .FirstOrDefaultAsync(m => m.id == id);
            if (subcategoria == null)
            {
                return NotFound();
            }

            return View(subcategoria);
        }

        // POST: subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.subcategoria == null)
            {
                return Problem("Entity set 'DBContext.subcategoria'  is null.");
            }
            var subcategoria = await _context.subcategoria.FindAsync(id);
            if (subcategoria != null)
            {
                _context.subcategoria.Remove(subcategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool subcategoriaExists(int id)
        {
          return (_context.subcategoria?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
