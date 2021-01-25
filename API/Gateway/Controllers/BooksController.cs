using Books.Models;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _service;

        public BooksController(BooksService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Autores>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TraerAutores()
        {
            var autores = await _service.GetAutores();

            return Ok(autores);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Autores>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TraerAutoresTraerAutoresPorEditorial(int id)
        {
            var autores = await _service.GetAutoresByEditorial(id);

            return Ok(autores);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Editoriales), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GuardarEditorial(Editoriales editorial)
        {
            var _editorial = await _service.SaveEditorial(editorial);

            return Ok(_editorial);
        }
    }
}
