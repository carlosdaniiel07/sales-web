using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWeb.Models
{
    public class Departamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O valor informado precisa estar entre {2} à {1} caracteres")]
        public string Nome { get; set; }

        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public Departamento() { }

        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void AddVendedor (Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }

        public double TotalVendas (DateTime inicio, DateTime fim)
        {
            return Vendedores.Sum(vendedor => vendedor.TotalVendas(inicio, fim));
        }
    }
}
