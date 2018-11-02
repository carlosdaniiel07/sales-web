using System.Collections.Generic;

namespace SalesWeb.Models.ViewModel
{
    public class VendaFormViewModel
    {
        public Venda Venda { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; }
    }
}
