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
    public class EditorialesController : ControllerBase
    {
        private readonly BooksContext _context;

        public EditorialesController(BooksContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(IEnumerable<Editoriales>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var editoriales = await _context.Editoriales.ToListAsync();

            return Ok(editoriales);
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Editoriales), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var editorial = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editorial == null)
            {
                return NotFound();
            }

            return Ok(editorial);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody] Editoriales editorial)
        {
            if (ModelState.IsValid)
            {
                var _editorial = new Editoriales()
                {
                    Nombre = editorial.Nombre,
                    Sede = editorial.Sede
                };

                _context.Add(_editorial);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByIdAsync), new { id = _editorial.Id }, null);
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
        public async Task<IActionResult> UpdateAsync([FromBody] Editoriales editorial)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _editorial = await _context.Editoriales.SingleOrDefaultAsync(i => i.Id == editorial.Id);

                    if (_editorial == null)
                    {
                        return NotFound();
                    }

                    _context.Update(_editorial);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetByIdAsync), new { id = _editorial.Id }, null);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorialExists(editorial.Id))
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

            var editorial = await _context.Editoriales.FindAsync(id);
            _context.Editoriales.Remove(editorial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EditorialExists(int id)
        {
            return _context.Editoriales.Any(e => e.Id == id);
        }
    }
}
