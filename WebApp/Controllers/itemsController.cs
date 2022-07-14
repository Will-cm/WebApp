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
    public class itemsController : Controller
    {
        private readonly DBContext _context;

        public itemsController(DBContext context)
        {
            _context = context;
        }

        // GET: items
        public async Task<IActionResult> Index()
        {
              return _context.item != null ? 
                          View(await _context.item.ToListAsync()) :
                          Problem("Entity set 'DBContext.item'  is null.");
        }

        // GET: items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.item == null)
            {
                return NotFound();
            }

            var item = await _context.item
                .FirstOrDefaultAsync(m => m.id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,codigo,descripcion,unidad_med,precio,maximo,minimo,vida_util,estado,cod_sbcategoria,created_at,updated_at")] item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.item == null)
            {
                return NotFound();
            }

            var item = await _context.item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,codigo,descripcion,unidad_med,precio,maximo,minimo,vida_util,estado,cod_sbcategoria,created_at,updated_at")] item item)
        {
            if (id != item.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!itemExists(item.id))
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
            return View(item);
        }

        // GET: items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.item == null)
            {
                return NotFound();
            }

            var item = await _context.item
                .FirstOrDefaultAsync(m => m.id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.item == null)
            {
                return Problem("Entity set 'DBContext.item'  is null.");
            }
            var item = await _context.item.FindAsync(id);
            if (item != null)
            {
                _context.item.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool itemExists(int id)
        {
          return (_context.item?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
