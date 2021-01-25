using Books.Models;
using Gateway.Clients;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Services
{
    public class BooksService
    {
        private readonly ILogger<BooksService> _logger;
        private readonly AutoresClient _autores;
        private readonly EditorialesClient _editoriales;
        private readonly LibrosClient _libros;

        public BooksService(ILogger<BooksService> logger, AutoresClient autores, EditorialesClient editoriales, LibrosClient libros)
        {
            _logger = logger;
            _autores = autores;
            _editoriales = editoriales;
            _libros = libros;
        }

        public async Task<IEnumerable<Autores>> GetAutores()
        {
            _logger.LogDebug("Autores client created, request = Get");
            var response = await _autores.Get();
            _logger.LogDebug("Autores response {@response}", response);

            return response;
        }

        public async Task<IEnumerable<Autores>> GetAutoresByEditorial(int id)
        {
            _logger.LogDebug("Autores client created, request = GetAutoresByEditorial{@id}", id);
            var response = await _autores.GetByEditorial(id);
            _logger.LogDebug("Autores response {@response}", response);

            return response;
        }

        public async Task<Editoriales> SaveEditorial(Editoriales editorial)
        {
            _logger.LogDebug("Editoriales client created, request = GetById{@id}", editorial.Id);
            var _editorial = await _editoriales.GetById(editorial.Id);
            _logger.LogDebug("Editoriales response {@response}", _editorial);

            if (_editorial is null)
            {
                _logger.LogDebug("Editoriales client created, request = Create{@editorial}", editorial);
                _editorial = await _editoriales.Create(editorial);
                _logger.LogDebug("Editoriales response {@response}", _editorial);
            }
            else
            {
                _logger.LogDebug("Editoriales client created, request = Update{@editorial}", editorial);
                _editorial = await _editoriales.Update(editorial);
                _logger.LogDebug("Editoriales response {@response}", _editorial);
            }

            return _editorial;
        }

    }
}
