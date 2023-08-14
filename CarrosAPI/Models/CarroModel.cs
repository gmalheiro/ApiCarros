namespace CarrosAPI.Models
{
    public class CarroModel
    {

        public int CarroId { get; set; }
        public string? Modelo { get; set; }
        public string? Descricao { get; set; }
        public int CategoriaId { get; set; }

        public CarroModel(int carroId, string modelo, string descricao, int categoriaId)
        {
            CarroId = carroId;
            Modelo = modelo;
            Descricao = descricao;
            CategoriaId = categoriaId;
        }

    }
}
