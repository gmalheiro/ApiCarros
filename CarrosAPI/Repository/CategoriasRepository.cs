using CarrosAPI.Models;
using Dapper;
using MySqlConnector;

namespace CarrosAPI.Repository
{
    public class CategoriasRepository
    {

        private readonly IConfiguration? _configuration;

        public CategoriasRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CategoriaModel> ListarCategorias()
        {
            try
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
                    return list!;
                }
                else
                {
                    return list;
                }
            }
            catch (Exception e)
            {
                List<CategoriaModel> list = new();
                Console.WriteLine(e.Message);
                return list;
            }
        }

    }
}
