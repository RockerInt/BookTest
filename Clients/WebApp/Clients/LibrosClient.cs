using Books.Models;
using WebApp.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utilities;
using Microsoft.Extensions.Options;

namespace WebApp.Clients
{
    public class LibrosClient
    {
        private readonly UrlsConfig _urls;

        public LibrosClient(IOptions<UrlsConfig> urls)
        {
            _urls = urls.Value;
        }

        public async Task<IEnumerable<Libros>> Get()
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Get, _urls.BooksService, UrlsConfig.LibrosOperations.Get(), null);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntityListSimple<Libros>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Libros> GetById(int id)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Get, _urls.BooksService, UrlsConfig.LibrosOperations.GetById(id), null);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Libros>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<IEnumerable<Libros>> GetByEditorial(int id)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Get, _urls.BooksService, UrlsConfig.LibrosOperations.GetByEditorial(id), null);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntityListSimple<Libros>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Libros> Create(Libros libro)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.LibrosOperations.Create(), libro);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Libros>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Libros> Update(Libros libro)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.LibrosOperations.Update(), libro);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Libros>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.LibrosOperations.Delete(id), null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }
    }
}
