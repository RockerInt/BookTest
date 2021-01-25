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
    public class AutoresController : Controller
    {
        private readonly AutoresClient _client;

        public AutoresController(AutoresClient client)
        {
            this._client = client;
        }

        // GET: Autores
        public async Task<IActionResult> Index()
        {
            return View(await _client.Get());
        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await _client.GetById(id.Value);

            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellidos")] Autores autores)
        {
            if (ModelState.IsValid)
            {
                await _client.Create(autores);
                return RedirectToAction(nameof(Index));
            }

            return View(autores);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await _client.GetById(id.Value);

            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // POST: Autores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellidos")] Autores autores)
        {
            if (id != autores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _client.Update(autores);
                }
                catch 
                {
                    if (!AutoresExists(autores.Id))
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

            return View(autores);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await _client.GetById(id.Value);

            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool AutoresExists(int id)
        {
            return _client.GetById(id).Result != null;
        }
    }
}
