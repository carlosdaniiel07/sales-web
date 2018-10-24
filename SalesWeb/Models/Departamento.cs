using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWeb.Models
{
    public class Departamento
    {
        public int Id { get; set; }
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
