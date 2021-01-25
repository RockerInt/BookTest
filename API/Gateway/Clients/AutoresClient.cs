using Books.Models;
using Gateway.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Gateway.Clients
{
    public class AutoresClient
    {
        private readonly UrlsConfig _urls;

        public AutoresClient(IOptions<UrlsConfig> urls)
        {
            _urls = urls.Value;
        }

        public async Task<IEnumerable<Autores>> Get()
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Get, _urls.BooksService, UrlsConfig.AutoresOperations.Get(), null);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntityListSimple<Autores>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Autores> GetById(int id)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Get, _urls.BooksService, UrlsConfig.AutoresOperations.GetById(id), null);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Autores>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<IEnumerable<Autores>> GetByEditorial(int id)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Get, _urls.BooksService, UrlsConfig.AutoresOperations.GetByEditorial(id), null);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntityListSimple<Autores>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Autores> Create(Autores autor)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.AutoresOperations.Create(), autor);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Autores>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Autores> Update(Autores autor)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.AutoresOperations.Update(), autor);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Autores>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.AutoresOperations.Delete(id), null);

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
