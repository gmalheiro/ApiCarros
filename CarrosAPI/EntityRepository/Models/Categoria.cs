using System;
using System.Collections.Generic;
using CarrosAPI.EntityRepository.Models;

namespace CarrosAPI.EntityRepository
{
    public partial class Categoria
    {
        public Categoria()
        {
            Carros = new HashSet<Carro>();
        }

        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; } = null!;

        public virtual ICollection<Carro> Carros { get; set; }
    }
}
