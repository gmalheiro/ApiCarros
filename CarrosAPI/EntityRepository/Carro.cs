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

        public Carro()
        {
                
        }

        public Carro(string modelo, string descricao, int categoriaId)
        {
            Modelo = modelo;
            Descricao = descricao;
            CategoriaId = categoriaId;
            if (CategoriaId == 1)
            {
                Categoria Categoria = new Categoria() { CategoriaId = 4, NomeCategoria = "Hatch" };
            }
            else if (CategoriaId == 2)
            {
                Categoria Categoria = new Categoria() { CategoriaId = 4, NomeCategoria = "Sedan" };
            }
            else if (CategoriaId == 3)
            {
                Categoria Categoria = new Categoria() { CategoriaId = 4, NomeCategoria = "SUV" };
            }
            else
            {
                Categoria Categoria = new Categoria() { CategoriaId = 4, NomeCategoria = "Esportivo" };
            }
        }

    }
}
