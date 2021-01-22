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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BooksContext _context;

        public LibrosController(BooksContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(IEnumerable<Libros>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var libros = await _context.Libros.Include(l => l.Editoriales).ToListAsync();

            return Ok(libros);
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Libros), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var libro = await _context.Libros
                .Include(l => l.Editoriales)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (libro == null)
            {
                return NotFound();
            }

            return Ok(libro);
        }

        [HttpGet]
        [Route("GetByEditorial/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Libros>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByEditorialAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var libros = await Task.FromResult(_context.Libros
                .Include(l => l.Editoriales)
                .Where(x => x.AutoresHasLibros.Any(y => y.Libros.Editoriales.Id == id)).ToList());
            if (libros == null)
            {
                return NotFound();
            }

            return Ok(libros);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody] Libros libro)
        {
            if (ModelState.IsValid)
            {
                var _libro = new Libros()
                {
                    Titulo = libro.Titulo,
                    Sinopsis = libro.Sinopsis,
                    NPaginas = libro.NPaginas,
                    EditorialesId = libro.EditorialesId
                };

                _context.Add(_libro);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByIdAsync), new { id = _libro.Isbn }, null);
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
        public async Task<IActionResult> UpdateAsync([FromBody] Libros libro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _libro = await _context.Libros.SingleOrDefaultAsync(i => i.Isbn == libro.Isbn);

                    if (_libro == null)
                    {
                        return NotFound();
                    }

                    _context.Update(_libro);
                    await _context.SaveChangesAsync();
                    
                    return CreatedAtAction(nameof(GetByIdAsync), new { id = _libro.Isbn }, null);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Isbn))
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

            var libro = await _context.Libros.FindAsync(id);
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Isbn == id);
        }
    }
}
