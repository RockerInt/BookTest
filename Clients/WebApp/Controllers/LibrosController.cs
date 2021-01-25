using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Models;
using WebApp.Clients;

namespace WebApp.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibrosClient _client;
        private readonly EditorialesClient _editorialesClient;

        public LibrosController(LibrosClient client, EditorialesClient editorialesClient)
        {
            _client = client;
            _editorialesClient = editorialesClient;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            return View(await _client.Get());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _client.GetById(id.Value);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["EditorialesId"] = new SelectList(_editorialesClient.Get().Result, "Id", "Nombre");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Isbn,EditorialesId,Titulo,Sinopsis,NPaginas")] Libros libro)
        {
            if (ModelState.IsValid)
            {
                await _client.Create(libro);
                return RedirectToAction(nameof(Index));
            }

            ViewData["EditorialesId"] = new SelectList(await _editorialesClient.Get(), "Id", "Nombre", libro.EditorialesId);
            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _client.GetById(id.Value);

            if (libro == null)
            {
                return NotFound();
            }

            ViewData["EditorialesId"] = new SelectList(await _editorialesClient.Get(), "Id", "Nombre", libro.EditorialesId);
            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Isbn,EditorialesId,Titulo,Sinopsis,NPaginas")] Libros libro)
        {
            if (id != libro.Isbn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _client.Update(libro);
                }
                catch
                {
                    if (!LibrosExists(libro.Isbn))
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

            ViewData["EditorialesId"] = new SelectList(await _editorialesClient.Get(), "Id", "Nombre", libro.EditorialesId);
            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _client.GetById(id.Value);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool LibrosExists(int id)
        {
            return _client.GetById(id).Result != null;
        }
    }
}
