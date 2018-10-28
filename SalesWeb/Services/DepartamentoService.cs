using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SalesWeb.Data;
using SalesWeb.Models;

namespace SalesWeb.Services
{
    public class DepartamentoService
    {
        private readonly SalesWebContext _context;

        public DepartamentoService (SalesWebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtem todos os departamentos armazenados no BD
        /// </summary>
        /// <returns></returns>
        public ICollection<Departamento> Consulta ()
        {
            return _context.Departamento.OrderBy(x => x.Nome).ToList();
        }
    }
}
