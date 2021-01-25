using Books.Models;
using WebApp.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using Microsoft.Extensions.Options;

namespace WebApp.Clients
{
    public class EditorialesClient
    {
        private readonly UrlsConfig _urls;

        public EditorialesClient(IOptions<UrlsConfig> urls)
        {
            _urls = urls.Value;
        }

        public async Task<IEnumerable<Editoriales>> Get()
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Get, _urls.BooksService, UrlsConfig.EditorialesOperations.Get(), null);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntityListSimple<Editoriales>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Editoriales> GetById(int id)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Get, _urls.BooksService, UrlsConfig.EditorialesOperations.GetById(id), null);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Editoriales>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Editoriales> Create(Editoriales editorial)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.EditorialesOperations.Create(), editorial);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Editoriales>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<Editoriales> Update(Editoriales editorial)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.EditorialesOperations.Update(), editorial);

            if (response.IsSuccessStatusCode)
            {
                return WebUtilities.ValidateContent(response).ToEntitySimple<Editoriales>();
            }
            else
            {
                throw new Exception($"HttpException: {Environment.NewLine} StatusCode: {Convert.ToInt16(response.StatusCode)}, {Environment.NewLine} Messege: {WebUtilities.ValidateContent(response)}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            var response = await WebUtilities.ConectAsync(WebUtilities.Method.Post, _urls.BooksService, UrlsConfig.EditorialesOperations.Delete(id), null);

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
