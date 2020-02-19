using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.Filmes.WebApi.Domains;
using senai.Filmes.WebApi.Interfaces;
using senai.Filmes.WebApi.Repositories;

namespace senai.Filmes.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos filmes
    /// </summary>

    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato domínio/api/NomeController
    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]
    public class FilmesController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _filmeRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IFilmeRepository _filmeRepository { get; set; }

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public FilmesController()
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// Lista todos os filmes
        /// </summary>
        /// <returns>Retorna uma lista de filmes</returns>
        /// dominio/api/Generos
        [HttpGet]
        public IEnumerable<FilmeDomain> Get()
        {
            // Faz a chamada para o metodo .Listar();
            return _filmeRepository.Listar();
        }
    }
}