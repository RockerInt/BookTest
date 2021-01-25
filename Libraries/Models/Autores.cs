using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }

        public virtual ICollection<AutoresHasLibros> AutoresHasLibros { get; set; }
    }
}
