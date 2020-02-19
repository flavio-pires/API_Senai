using senai.Filmes.WebApi.Domains;
using senai.Filmes.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Filmes.WebApi.Repositories
{
    /// <summary>
    /// Repositorio dos filmes
    /// </summary>
    public class FilmeRepository : IFilmeRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros
        /// Data Source = Nome do servidor identificado no banco de dados
        /// initial catalog = Nome do banco de dados
        /// user Id=sa; pwd=sa@132 - Faz a autenticação com um usuário específico, passando o logon e a senha
        /// </summary>
        /// //private string StringConexao = "Data Source=DESKTOP-NJ6LHN1\\SQLDEVELOPER; initial catalog=Filmes; integrated security=true";
        private string stringConexao = "Data Source=N-1S-DEV-12\\SQLEXPRESS; initial catalog=Filmes_manha; user Id=sa; pwd=sa@132";

        /// <summary>
        /// Metodo para listar todos os filmes
        /// </summary>
        /// <returns></returns>
        public List<FilmeDomain> Listar()
        {
            // Criar um objeto filmes do tipo lista onde serão armazenados todos os filmes
            List<FilmeDomain> filmes = new List<FilmeDomain>();

            // Estabelecer a conexão com o banco de dados, passando a string de conexão declarada acima
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declarar a instrução que será executada
                string queryListFilmes = "SELECT IdFilme, Titulo, Generos.Nome FROM Filmes INNER JOIN Generos ON Filmes.IdGenero = Generos.IdGenero";

                // Abrir conexão com o banco de dados
                con.Open();

                // Declarar o SqlDataReader para fazer a leitura da tabela do banco
                SqlDataReader rdr;

                // Declarar o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryListFilmes, con))
                {
                    // Armazena no objeto rdr as informações que foram lidas da tabela
                    rdr = cmd.ExecuteReader();

                    // Laço que faz a leitura dos registros dentro do objeto rdr
                    while (rdr.Read())
                    {
                        // Cria um objeto filme do tipo FilmeDomain para armazenar os dados que serão mostrados

                        FilmeDomain filme = new FilmeDomain()

                        {
                            // Atribui a variavel IdFilme os dados da coluna IdFilme na tabela Filmes
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),

                            //Atribui a variavel Titulo os dados da coluna Titulo na tabela Filmes
                            Titulo = rdr["Titulo"].ToString(),

                            //Atribui a variavel IdGenero os dados da coluna IdGenero na tabela Filmes
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            Genero = rdr[GeneroRepository].ToString

                        };

                        // Adiciona o filme criado a tabela filmes
                        filmes.Add(filme);
                    }
                }
            }

            //retorna a lista de filmes
            return filmes;
        }

        public FilmeDomain BuscarPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdFilme, Titulo, IdGenero FROM Filmes WHERE IdFilme = @ID";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader fazer a leitura no banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Caso a o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Cria um objeto filme
                        FilmeDomain filme = new FilmeDomain
                        {
                            // Atribui à propriedade IdFilme o valor da coluna "IdFilme" da tabela do banco
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),

                            // Atribui à propriedade titulo o valor da coluna "Titulo" da tabela do banco
                            Titulo = rdr["Titulo"].ToString()

                        };

                        // Retorna o genero com os dados obtidos
                        return filme;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastra um novo filme
        /// </summary>
        /// <param name="filme">Objeto filme que será cadastrado</param>
        public void Cadastrar(FilmeDomain filme)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                // string queryInsert = "INSERT INTO Generos(Nome) VALUES ('" + genero.Nome + "')";
                // Não usar dessa forma pois pode causar o efeito Joana D'arc
                // Além de permitir SQL Injection
                // Por exemplo
                // "nome" : "')DROP TABLE Filmes--'"
                // Ao tentar cadastrar o comando acima, irá deletar a tabela Filmes do banco de dados
                // https://www.devmedia.com.br/sql-injection/6102

                // Declara a query que será executada passando o valor como parâmetro, evitando assim os problemas acima
                string queryInsert = "INSERT INTO Filmes(Titulo, IdGenero) VALUES (@Titulo, @IdGenero)";

                // Declara o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(queryInsert, con);

                // Passa o valor do parâmetro
                cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);
                cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                // Abre a conexão com o banco de dados
                con.Open();

                // Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deleta um filme através do seu ID
        /// </summary>
        /// <param name="id">ID do filme que será deletado</param>
        public void Deletar(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada passando o valor como parâmetro
                string queryDelete = "DELETE FROM Filmes WHERE IdFilme = @ID";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando... comando sem conculta, não quero mostrar nada, só enviar 
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Atualiza um filme passando o ID pelo corpo da requisição
        /// </summary>
        /// <param name="filme">Objeto filme que será atualizado</param>
        public void AtualizarIdCorpo(FilmeDomain filme)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryUpdate = "UPDATE Filmes SET Titulo = @Titulo WHERE IdFilme = @ID";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", filme.IdFilme);
                    cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
