# desafio-wiz
1 - Foi criado uma solução chamada DesafioWiz onde foi inculuido dois projetos, um WebApplication Rest Api e um projeto de Class Library.

2 - O projeto Class Library se chama MovieDataBaseApi onde é feito a implementação do endpoint que consulta os filmes a serem lançados. E foi criado uma class chamada Movies e nela foi feito encapsulamento da class HttpClient e criado um metodo chamado BuscarLancamentos que recebe como parâmetro um campo inteiro para ser informado a número da página que será consultado na API (https://developers.themoviedb.org/3).

3 - Nos dois projetos foi adicionado a biblioteca externa Newtonsoft.Json que é utilizada para fazer a desserialização do json retornado na consulta dos filmes que serão lançados.

4 - No projeto WebApplication Rest Api foi criado um controller chamado FilmesLancamento onde tem como injeção de independência da class HttpClient. No FilmesLancamento é implementado o metodo get e terá como parametro um campo inteiro chamado pagina com valor padrão zero, e será retornado uma lista de filmes lançados com inforamções dos filmes como nome, descrição, data lançamento e gênero. Nesse projeto tem referência do projeto MovieDataBaseApi.

O retorno da API criada terá as definições das classes abaixo:
    
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
    
Onde FilmesReposta será o retorno final da API, onde a propriedade Mensagem será usada para informar se ocorreu algum erro na requisição da API e Filmes será uma lista dos lançamentos e terá como base a class Filmes.

5 - Ao fazer uma consulta na API e não for informado um número de página, então consultado como padrão a pagina 1. Como a API https://developers.themoviedb.org/ retorna 20 itens por página, então foi implementado no projeto MovieDataBaseApi para que seja consultado 5 páginas, ou seja, a cada página consultada na API FilmesLancamento será consultada 5 no endpoint implementado e com isso provavelmente o resultado da API criada terá mais de 20 itens.

6 - Como na consulta dos filmes lançados só retorna os ids do gêneros da API https://developers.themoviedb.org/, então foi feito um request que retorna as informações dos gêneros dos filmes no endpoint genres. E com isso será retornado as descrições dos gêneros no campo Genero da class Filmes.

7 - E por fim foi feito um filtro nos lançamentos consultados para que retorne apenas os itens que tem data de lançamento igual ou maior a data atual.
   
