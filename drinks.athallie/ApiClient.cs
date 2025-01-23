using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;

namespace drinks.athallie
{
    internal class ApiClient(HttpClient Client)
    {
        public void setHeader(string mediaType, string userAgentName)
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType)
            );
            Client.DefaultRequestHeaders.Add("User-Agent", userAgentName);
        }

        public async Task<T> ProcessRepositoryAsync<T>(string url)
        {
            await using Stream stream = await Client.GetStreamAsync(url);
            var repositories = await JsonSerializer.DeserializeAsync<T>(stream);
            return repositories;
        }
    }
}
