using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Config
{
    public class UrlsConfig
    {
        public string BooksService { get; set; }

        public class LibrosOperations
        {
            public static string Get() => $"/api/v1/Libros/Get";

            public static string GetById(int id) => $"/api/v1/Libros/Get/{id}";

            public static string GetByEditorial(int id) => $"/api/v1/Libros/GetByEditorial/{id}";

            public static string Create() => $"/api/v1/Libros/Create";

            public static string Update() => $"/api/v1/Libros/Update";

            public static string Delete(int id) => $"/api/v1/Libros/Delete/{id}";
        }

        public class AutoresOperations
        {
            public static string Get() => $"/api/v1/Autores/Get";

            public static string GetById(int id) => $"/api/v1/Autores/Get/{id}";

            public static string GetByEditorial(int id) => $"/api/v1/Autores/GetByEditorial/{id}";

            public static string Create() => $"/api/v1/Autores/Create";

            public static string Update() => $"/api/v1/Autores/Update";

            public static string Delete(int id) => $"/api/v1/Autores/Delete/{id}";
        }

        public class EditorialesOperations
        {
            public static string Get() => $"/api/v1/Editoriales/Get";

            public static string GetById(int id) => $"/api/v1/Editoriales/Get/{id}";

            public static string Create() => $"/api/v1/Editoriales/Create";

            public static string Update() => $"/api/v1/Editoriales/Update";

            public static string Delete(int id) => $"/api/v1/Editoriales/Delete/{id}";
        }
    }

}
