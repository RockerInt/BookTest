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
    public class EditorialesController : Controller
    {
        private readonly EditorialesClient _client;

        public EditorialesController(EditorialesClient client)
        {
            _client = client;
        }

        // GET: Editoriales
        public async Task<IActionResult> Index()
        {
            return View(await _client.Get());
        }

        // GET: Editoriales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _client.GetById(id.Value);

            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // GET: Editoriales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Sede")] Editoriales editorial)
        {
            if (ModelState.IsValid)
            {
                await _client.Create(editorial);
                return RedirectToAction(nameof(Index));
            }

            return View(editorial);
        }

        // GET: Editoriales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _client.GetById(id.Value);

            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // POST: Editoriales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Sede")] Editoriales editorial)
        {
            if (id != editorial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _client.Update(editorial);
                }
                catch
                {
                    if (!EditorialesExists(editorial.Id))
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

            return View(editorial);
        }

        // GET: Editoriales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _client.GetById(id.Value);

            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // POST: Editoriales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool EditorialesExists(int id)
        {
            return _client.GetById(id).Result != null;
        }
    }
}
