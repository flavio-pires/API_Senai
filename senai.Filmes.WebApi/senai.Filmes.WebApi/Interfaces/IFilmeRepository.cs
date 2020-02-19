using senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Filmes.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositorio Filme
    /// </summary>
    interface IFilmeRepository
    {
        /// <summary>
        /// Lista todos os filmes
        /// </summary>
        /// <returns>Retorna uma lista de filmes cadastrados</returns>
        List<FilmeDomain> Listar();


    }
}
