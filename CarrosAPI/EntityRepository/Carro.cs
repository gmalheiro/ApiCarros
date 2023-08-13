using System;
using System.Collections.Generic;

namespace CarrosAPI.EntityRepository
{
    public partial class Carro
    {
        public int CarroId { get; set; }
        public string Modelo { get; set; } = null!;
        public string? Descricao { get; set; }
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; } = null!;
    }
}
