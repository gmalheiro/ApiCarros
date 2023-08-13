namespace CarrosAPI.Models
{
    public class CategoriaModel
    {
        public int CategoriaId{ get; set; }
        public string? NomeCategoria { get; set; }

        public CategoriaModel()
        {
                
        }

        public CategoriaModel(int categoriaId, string nomeCategoria)
        {
                CategoriaId = categoriaId;
                NomeCategoria= nomeCategoria;
        }
    }
}
