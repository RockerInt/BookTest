using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Models;

namespace WebApp.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BooksContext _context;

        public LibrosController(BooksContext context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            var booksContext = _context.Libros.Include(l => l.Editoriales);
            return View(await booksContext.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libros
                .Include(l => l.Editoriales)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["EditorialesId"] = new SelectList(_context.Editoriales, "Id", "Nombre");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Isbn,EditorialesId,Titulo,Sinopsis,NPaginas")] Libros libros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EditorialesId"] = new SelectList(_context.Editoriales, "Id", "Nombre", libros.EditorialesId);
            return View(libros);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libros.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }
            ViewData["EditorialesId"] = new SelectList(_context.Editoriales, "Id", "Nombre", libros.EditorialesId);
            return View(libros);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Isbn,EditorialesId,Titulo,Sinopsis,NPaginas")] Libros libros)
        {
            if (id != libros.Isbn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrosExists(libros.Isbn))
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
            ViewData["EditorialesId"] = new SelectList(_context.Editoriales, "Id", "Nombre", libros.EditorialesId);
            return View(libros);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libros
                .Include(l => l.Editoriales)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libros = await _context.Libros.FindAsync(id);
            _context.Libros.Remove(libros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrosExists(int id)
        {
            return _context.Libros.Any(e => e.Isbn == id);
        }
    }
}
