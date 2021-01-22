using System;
using System.Collections.Generic;

#nullable disable

namespace Books.Models
{
    public partial class Autores
    {
        public Autores()
        {
            AutoresHasLibros = new HashSet<AutoresHasLibros>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        public virtual ICollection<AutoresHasLibros> AutoresHasLibros { get; set; }
    }
}
