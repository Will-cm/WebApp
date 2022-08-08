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
    public class almacenController : Controller
    {
        private readonly DBContext _context;

        public almacenController(DBContext context)
        {
            _context = context;
        }

        // GET: almacen
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.almacen
                .Include(a => a.item)
                .Include(a => a.rack)
                .OrderByDescending(a => a.id);   //mod
            return View(await dBContext.ToListAsync());
        }

        // GET: almacen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.almacen == null)
            {
                return NotFound();
            }

            var almacen = await _context.almacen
                .Include(a => a.item)
                .FirstOrDefaultAsync(m => m.id == id);
            if (almacen == null)
            {
                return NotFound();
            }

            return View(almacen);
        }

        // GET: almacen/Create
        public IActionResult Create()
        {
            ViewData["id_item"] = new SelectList(_context.item, "id", "codigo");
            ViewData["id_rack"] = new SelectList(_context.rack, "id", "codigo"); //add
            return View();
        }

        // POST: almacen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_item,id_rack,lote,serie,fecha_venc,ubicacion,cantidad,estado,observacion,created_at,updated_at")] almacen almacen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(almacen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_item"] = new SelectList(_context.item, "id", "codigo", almacen.id_item);  //add
            ViewData["id_rack"] = new SelectList(_context.rack, "id", "codigo", almacen.id_rack);  //add
            return View(almacen);
        }

        // GET: almacen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.almacen == null)
            {
                return NotFound();
            }

            var almacen = await _context.almacen.FindAsync(id);
            if (almacen == null)
            {
                return NotFound();
            }
            ViewData["id_item"] = new SelectList(_context.item, "id", "codigo", almacen.id_item);
            ViewData["id_rack"] = new SelectList(_context.rack, "id", "codigo", almacen.id_rack);  //add
            return View(almacen);
        }

        // POST: almacen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_item,id_rack,lote,serie,fecha_venc,ubicacion,cantidad,estado,observacion,created_at,updated_at")] almacen almacen)
        {
            if (id != almacen.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(almacen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!almacenExists(almacen.id))
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
            ViewData["id_item"] = new SelectList(_context.item, "id", "codigo", almacen.id_item);
            ViewData["id_rack"] = new SelectList(_context.item, "id", "codigo", almacen.id_rack);  //add
            return View(almacen);
        }

        // GET: almacen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.almacen == null)
            {
                return NotFound();
            }

            var almacen = await _context.almacen
                .Include(a => a.item)
                .FirstOrDefaultAsync(m => m.id == id);
            if (almacen == null)
            {
                return NotFound();
            }

            return View(almacen);
        }

        // POST: almacen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.almacen == null)
            {
                return Problem("Entity set 'DBContext.almacen'  is null.");
            }
            var almacen = await _context.almacen.FindAsync(id);
            if (almacen != null)
            {
                _context.almacen.Remove(almacen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool almacenExists(int id)
        {
          return (_context.almacen?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
