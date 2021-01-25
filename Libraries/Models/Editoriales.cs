using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Books.Models
{
    public partial class Editoriales
    {
        public Editoriales()
        {
            Libros = new HashSet<Libros>();
        }

        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Sede { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}
