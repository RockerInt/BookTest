using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Books.Models
{
    public partial class Libros
    {
        public Libros()
        {
            AutoresHasLibros = new HashSet<AutoresHasLibros>();
        }


        [Display(Name = "ISBN")]
        public int Isbn { get; set; }

        [Display(Name = "Editorial")]
        public int? EditorialesId { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string NPaginas { get; set; }


        [Display(Name = "Editorial")]
        public virtual Editoriales Editoriales { get; set; }

        [Display(Name = "Autores")]
        public virtual ICollection<AutoresHasLibros> AutoresHasLibros { get; set; }
    }
}
