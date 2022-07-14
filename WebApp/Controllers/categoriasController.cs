using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class categoriasController : Controller
    {
        private readonly DBContext _context;

        public categoriasController(DBContext context)
        {
            _context = context;
        }

        // GET: categorias
        public async Task<IActionResult> Index()
        {
            //Authenticacion
            try
            {
                if(HttpContext.Session.GetString("UserSession") == null)
                {
                    return RedirectToAction("Login", "Acceso");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Acceso");
            }
            return _context.categoria != null ? 
                        View(await _context.categoria.ToListAsync()) :
                        Problem("Entity set 'DBContext.categoria'  is null.");
        }

        // GET: categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.categoria == null)
            {
                return NotFound();
            }

            var categoria = await _context.categoria
                .FirstOrDefaultAsync(m => m.id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,codigo,descripcion,estado")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.categoria == null)
            {
                return NotFound();
            }

            var categoria = await _context.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,codigo,descripcion,estado")] categoria categoria)
        {
            if (id != categoria.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!categoriaExists(categoria.id))
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
            return View(categoria);
        }

        // GET: categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.categoria == null)
            {
                return NotFound();
            }

            var categoria = await _context.categoria
                .FirstOrDefaultAsync(m => m.id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.categoria == null)
            {
                return Problem("Entity set 'DBContext.categoria'  is null.");
            }
            var categoria = await _context.categoria.FindAsync(id);
            if (categoria != null)
            {
                _context.categoria.Remove(categoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool categoriaExists(int id)
        {
          return (_context.categoria?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
