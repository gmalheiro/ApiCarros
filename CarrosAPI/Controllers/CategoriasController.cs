using CarrosAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;

namespace CarrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IConfiguration? _configuration;

        public CategoriasController(IConfiguration configuration)
        {
                _configuration = configuration;
        }

        [HttpGet]
        [Route("/ListarCategorias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CategoriaModel))]
        public IActionResult BuscarDapper()
        {
            string? dbConnection = _configuration.GetConnectionString("DbConn");

            MySqlConnection connection = new MySqlConnection(dbConnection);

            List<CategoriaModel> list = new();

            using (connection)
            {
                string sqlCommand = "SELECT * FROM Categorias";
                list = connection.Query<CategoriaModel>(sqlCommand).ToList();
            }

            if (list is null)
            {
                return NotFound("Categorias não encontradas");
            }
            else 
            {
                return Ok(list);
            }

        }

        [HttpGet]
        [Route("/ListarCategoria/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CategoriaModel))]
        public IActionResult BuscarDapperPorId(int id)
        {
            string? dbConnection = _configuration.GetConnectionString("DbConn");

            var parametrosConsulta = new DynamicParameters();
            parametrosConsulta.Add("@id",id);

            MySqlConnection connection = new MySqlConnection(dbConnection);

            string sqlCommand = "SELECT * FROM Categorias WHERE CategoriaId = @id";

            using (connection)
            {
                var categoria= connection.Query(sqlCommand,parametrosConsulta)
                                       ?.FirstOrDefault()
                                        ?? new CategoriaModel();
                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada");
                }
                else
                {
                    return Ok(categoria);
                }
            }

        }

        [HttpPost]
        [Route("/CadastrarCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CategoriaModel))]
        public IActionResult CadastrarCategoria(CategoriaModel categoria)
        {
            string? dbConnection = _configuration.GetConnectionString("DbConn");

            using (MySqlConnection connection = new MySqlConnection(dbConnection))
            {

                string sqlCommand = "INSERT INTO Categorias (NomeCategoria) VALUES (@NomeCategoria)";

                var NomeCategoria = new DynamicParameters();
                NomeCategoria.Add("@NomeCategoria", categoria.NomeCategoria);
                
                var categoriaCriada = connection.Execute(sqlCommand,NomeCategoria);

                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada");
                }
                else
                {
                    return Ok(categoriaCriada);
                }
            }

        }

        [HttpPut]
        [Route("/AtualizarCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CategoriaModel))]
        public IActionResult AtualizarCategoria(CategoriaModel categoria)
        {
            string? dbConnection = _configuration.GetConnectionString("DbConn");

            using (MySqlConnection connection = new MySqlConnection(dbConnection))
            {

                string sqlCommand = "UPDATE categorias SET NomeCategoria = @NomeCategoria WHERE CategoriaId = @CategoriaId";

                var CategoriaASerAtualizada = new DynamicParameters();
                CategoriaASerAtualizada.Add("@NomeCategoria", categoria.NomeCategoria);
                CategoriaASerAtualizada.Add("@CategoriaId",categoria.CategoriaId);


                var rowsAffected = connection.Execute(sqlCommand, CategoriaASerAtualizada);

                if ( rowsAffected != 1)
                {
                    return NotFound("Categoria não foi atualizada");
                }
                else
                {
                    return Ok(categoria);
                }
            }

        }


        [HttpDelete]
        [Route("/DeletarCategoria/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CategoriaModel))]
        public IActionResult DeletarCategoria(int id)
        {
            string? dbConnection = _configuration.GetConnectionString("DbConn");

            var parametrosConsulta = new DynamicParameters();
            parametrosConsulta.Add("@id", id);

            MySqlConnection connection = new MySqlConnection(dbConnection);

            string sqlCommand = " DELETE FROM Categorias WHERE CategoriaId = @id";

            using (connection)
            {
                var categoria = connection.Query("SELECT * FROM Categorias WHERE CategoriaId = @Id", parametrosConsulta)
                                       ?.FirstOrDefault()
                                        ?? new CategoriaModel();
                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada");
                }
                else
                {
                    var excluirCategoria = connection.Execute(sqlCommand, parametrosConsulta);
                    return Ok(categoria);
                }
            }

        }

        [HttpGet]
        [Route("/ListarCategoriaComCarros")]
        public IActionResult ListarCategoriaComCarros()
        {
            try
            {
                string? dbConnection = _configuration.GetConnectionString("DbConn");

                using (MySqlConnection connection = new MySqlConnection(dbConnection))
                {
                    string? SqlCommand = $@"SELECT Modelo,NomeCategoria
                                            FROM Carros AS CR
                                            INNER JOIN Categorias as CT
                                            ON CR.CategoriaId = CT.CategoriaID;";

                    var list = connection.Query(SqlCommand).ToList();
                    return Ok(list);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                 "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        }

    }

