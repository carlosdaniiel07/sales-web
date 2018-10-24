using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWeb.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }
        public double Salario { get; set; }

        public Departamento Departamento { get; set; }
        public ICollection<Vendas> Vendas { get; set; } = new List<Vendas>();

        public Vendedor() { }

        public Vendedor(int id, string nome, string email, DateTime nascimento, double salario, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Nascimento = nascimento;
            Salario = salario;
            Departamento = departamento;
        }

        public void AddVenda (Vendas venda)
        {
            Vendas.Add(venda);
        }

        public void RemoveVenda (Vendas venda)
        {
            Vendas.Remove(venda);
        }

        public double TotalVendas (DateTime inicio, DateTime fim)
        {
            return Vendas.Where(venda => venda.Data >= inicio && venda.Data <= fim && venda.Status.Equals(StatusVenda.Paga)).Sum(venda => venda.Valor);
        }
    }
}
