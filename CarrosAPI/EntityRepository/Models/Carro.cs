using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CarrosAPI.EntityRepository
{
    public partial class Carro
    {
        public int CarroId { get; set; }
        public string Modelo { get; set; } = null!;
        public string? Descricao { get; set; }
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; } = null!;
    }
}
