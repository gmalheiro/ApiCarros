using CarrosAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

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


    }
}
