using System;
using System.Collections.Generic;

#nullable disable

namespace Books.Models
{
    public partial class AutoresHasLibros
    {
        public int AutoresId { get; set; }
        public int LibrosIsbn { get; set; }

        public virtual Autores Autores { get; set; }
        public virtual Libros Libros { get; set; }
    }
}
