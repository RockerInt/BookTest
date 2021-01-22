using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly BooksService _service;

        public BooksController(ILogger<BooksController> logger, BooksService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Autores>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Autores>>> TraerAutores()
        {
            return await _service.GetAutores();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Autores>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Autores>>> TraerAutoresTraerAutoresPorEditorial(int id)
        {
            return await _service.GetAutoresByEditorial(id);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GuardarEditorial(Editoriales editorial)
        {
            await _service.SaveEditorial(editorial);
            return Ok();
        }
    }
}
