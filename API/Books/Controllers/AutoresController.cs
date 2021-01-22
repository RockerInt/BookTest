using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Models;
using Books.Data;
using System.Net;

namespace Books.Controllers
{
    public class AutoresController : Controller
    {
        private readonly BooksContext _context;

        public AutoresController(BooksContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(IEnumerable<Autores>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var autores = await _context.Autores.ToListAsync();

            return Ok(autores);
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Autores), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var autor = await _context.Autores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody] Autores autor)
        {
            if (ModelState.IsValid)
            {
                var _autor = new Autores()
                {
                    Nombre = autor.Nombre,
                    Apellidos = autor.Apellidos
                };

                _context.Add(_autor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByIdAsync), new { id = _autor.Id }, null);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Update")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateAsync([FromBody] Autores autor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _autor = await _context.Autores.SingleOrDefaultAsync(i => i.Id == autor.Id);

                    if (_autor == null)
                    {
                        return NotFound();
                    }

                    _context.Update(_autor);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetByIdAsync), new { id = _autor.Id }, null);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoresExists(autor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Delete/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var autor = await _context.Autores.FindAsync(id);
            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutoresExists(int id)
        {
            return _context.Autores.Any(e => e.Id == id);
        }
    }
}
