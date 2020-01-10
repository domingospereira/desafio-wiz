using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Desafio_Wiz.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesLancamentoController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;



        public FilmesLancamentoController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<FilmesResposta> Get(int pagina = 0)
        {
            try
            {
                MovieDatabaseApi.Movies movies = new MovieDatabaseApi.Movies(_clientFactory.CreateClient());

                IEnumerable<MovieDatabaseApi.Result> r = await movies.BuscarLancamentos(pagina);

                if (!r.Any())
                    return new FilmesResposta() { Mensagem = "Não foi encontrado nenhuma lançamento." };

                return new FilmesResposta()
                {
                    Filmes = r.Select(n => new Filmes
                    {
                        Titulo = n.title,
                        DataLancamento = n.Date.GetValueOrDefault(),
                        Genero = string.Join(",", n.genres.Select(g => g.name)),
                        Descricao = n.overview
                    }).ToList()
                };
            }
            catch (Exception ex)
            {

                return new FilmesResposta() { Mensagem = "Ocorreu erro ao carregar os lançamentos: " + ex.Message };
            }

        }
    }
}
