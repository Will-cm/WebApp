﻿using System;
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
    public class rolsController : Controller
    {
        private readonly DBContext _context;

        public rolsController(DBContext context)
        {
            _context = context;
        }

        // GET: rols
        public async Task<IActionResult> Index()
        {
              return _context.rol != null ? 
                          View(await _context.rol.ToListAsync()) :
                          Problem("Entity set 'DBContext.rol'  is null.");
        }

        // GET: rols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.rol == null)
            {
                return NotFound();
            }

            var rol = await _context.rol
                .FirstOrDefaultAsync(m => m.id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // GET: rols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: rols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,descripcion")] rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // GET: rols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.rol == null)
            {
                return NotFound();
            }

            var rol = await _context.rol.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: rols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,descripcion")] rol rol)
        {
            if (id != rol.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!rolExists(rol.id))
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
            return View(rol);
        }

        // GET: rols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.rol == null)
            {
                return NotFound();
            }

            var rol = await _context.rol
                .FirstOrDefaultAsync(m => m.id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // POST: rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.rol == null)
            {
                return Problem("Entity set 'DBContext.rol'  is null.");
            }
            var rol = await _context.rol.FindAsync(id);
            if (rol != null)
            {
                _context.rol.Remove(rol);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool rolExists(int id)
        {
          return (_context.rol?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
