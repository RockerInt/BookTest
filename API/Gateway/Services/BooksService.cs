using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Services
{
    public class BooksService
    {
        private BooksContext Db { get; set; }

        public BooksService(BooksContext db)
        {
            Db = db;
        }

        public async Task<List<Autores>> GetAutores()
        {
            return await Task.FromResult(Db.Autores.ToList());
        }

        public async Task<List<Autores>> GetAutoresByEditorial(int editorialId)
        {
            return await Task.FromResult(Db.Autores.Where(x => x.AutoresHasLibros.Any(y => y.Libros.Editoriales.Id == editorialId)).ToList());
        }

        public async Task<bool> SaveEditorial(Editoriales editorial)
        {
            if (Db.Editoriales.Any(x => x.Id == editorial.Id))
            {
                Db.Editoriales.Update(editorial);
            }
            else
            {
                Db.Editoriales.Add(editorial);
            }

            return (await Db.SaveChangesAsync()) != 0;
        }

    }
}
