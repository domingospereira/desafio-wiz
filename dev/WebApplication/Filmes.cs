using System;
using System.Collections.Generic;

namespace Desafio_Wiz
{
    public class FilmesResposta
    {
        public string Mensagem { get; set; }
        public IEnumerable<Filmes> Filmes { get; set; }
    }

    public class Filmes
    {
        public string Titulo { get; set; }

        public string Genero { get; set; }

        public DateTime DataLancamento { get; set; }

        public string Descricao { get; set; }
    }
}
