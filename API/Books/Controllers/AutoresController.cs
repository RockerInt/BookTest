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
    public class AutoresController : ControllerBase
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

            var autores = await Task.FromResult(_context.Autores
                .Include(l => l.AutoresHasLibros).ThenInclude(a => a.Libros).ThenInclude(l => l.Editoriales)
                .Where(x => x.AutoresHasLibros.Any(y => y.Libros.Editoriales.Id == id)).ToList());

            if (autores == null)
            {
                return NotFound();
            }

            return Ok(autores);
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

                return StatusCode((int)HttpStatusCode.Created, _autor);
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

                    _autor.Nombre = autor.Nombre;
                    _autor.Apellidos = autor.Apellidos;

                    _context.Update(_autor);
                    await _context.SaveChangesAsync();

                    return Ok(_autor);
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

            if (autor is null)
            {
                return NotFound();
            }

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
