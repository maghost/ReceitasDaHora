using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ReceitasDaHora.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Linq;

[assembly: Dependency(typeof(ReceitasDaHora.Services.ReceitasDaHoraApiService))]
namespace ReceitasDaHora.Services
{
    public class ReceitasDaHoraApiService : IReceitasDaHoraApiService
    {
        private const string BaseUrl = "https://maratona-xamarin-matheus.azurewebsites.net/tables/";
        private HttpClient httpClient;

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            generateHttpClient();
            var response = await httpClient.GetAsync($"{BaseUrl}categoria").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<Categoria>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }

        public async Task<List<Receita>> GetReceitasByCategoriaIdAsync(string categoriaId)
        {
            generateHttpClient();
            var response = await httpClient.GetAsync($"{BaseUrl}receita?$filter=(categoria eq {categoriaId})").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<Receita>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }

        public async Task<List<Receita>> GetReceitasByFilterAsync(string filter)
        {
            generateHttpClient();
            var response = await httpClient.GetAsync($"{BaseUrl}receita").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    List<Receita> Receitas = JsonConvert.DeserializeObject<List<Receita>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));

                    return Receitas.Where(r => r.Titulo.ToLower().Contains(Uri.EscapeUriString(filter).ToLower())).ToList();
                }
            }

            return null;
        }

        public async Task<Receita> GetReceitaByIdAsync(string receitaId)
        {
            generateHttpClient();
            var response = await httpClient.GetAsync($"{BaseUrl}receita?$filter=(id eq {receitaId})").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    List<Receita> Receitas = JsonConvert.DeserializeObject<List<Receita>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));

                    return Receitas.ElementAtOrDefault(0);
                }
            }

            return null;
        }

        private void generateHttpClient()
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
            }
        }
    }
}
