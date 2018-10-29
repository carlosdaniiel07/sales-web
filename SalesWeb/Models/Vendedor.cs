using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWeb.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O valor informado precisa estar entre {2} à {1} caracteres")]
        public string Nome { get; set; }

        [StringLength(80, MinimumLength = 3, ErrorMessage = "O valor informado precisa estar entre {2} à {1} caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Salario { get; set; }

        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }
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
