using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace MovieDatabaseApi
{
    public class Movies
    {
        private readonly HttpClient _httpClient;
        public Movies(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Result>> BuscarLancamentos(int pagina = 0)
        {
            List<Result> listaResult = new List<Result>();

            string url = "https://api.themoviedb.org/3/";

            //System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

            if (pagina > 0)
            {
                pagina = ((pagina - 1) * 5) + 1;
            }
            else
            {
                pagina = 1;
            }

            _httpClient.BaseAddress = new Uri(url);

            string result = await _httpClient.GetStringAsync("movie/upcoming?api_key=c5e9cae2e4b8dc81a5e9337ae04906a7&language=pt-BR&page=" + pagina);

            if (!string.IsNullOrWhiteSpace(result))
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieResult>(result);

                listaResult.AddRange(obj.results.Where(n => n.Date >= DateTime.Today));

                for (int i = 0; i < 5; i++)
                {
                    if (i == 0) continue;

                    result = await _httpClient.GetStringAsync("movie/upcoming?api_key=c5e9cae2e4b8dc81a5e9337ae04906a7&language=pt-BR&page=" + (pagina + i));

                    obj = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieResult>(result);

                    listaResult.AddRange(obj.results.Where(n => n.Date >= DateTime.Today));
                }

                string genres = await _httpClient.GetStringAsync("genre/movie/list?api_key=c5e9cae2e4b8dc81a5e9337ae04906a7&language=pt-BR");

                var objGenres = Newtonsoft.Json.JsonConvert.DeserializeObject<GenreResult>(genres);

                listaResult.ForEach(n => n.genres = objGenres.genres.Where(g => n.genre_ids.Contains(g.id)).ToList());

                //return listaResults;
            }

            return listaResult;
        }
    }
}
