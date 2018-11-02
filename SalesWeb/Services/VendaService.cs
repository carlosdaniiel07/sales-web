using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Services
{
    public class VendaService
    {
        private readonly SalesWebContext _context;
        
        public VendaService (SalesWebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtem todas as vendas armazenadas no BD
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Venda>> ConsultaAsync ()
        {
            return await _context.Venda.
                Include(obj => obj.Vendedor).
                Include(obj => obj.Vendedor.Departamento).
                OrderBy(obj => obj.Data).ToListAsync();
        }

        /// <summary>
        /// Obtem uma venda específica no BD
        /// </summary>
        /// <returns></returns>
        public async Task<Venda> ConsultaAsync(int id)
        {
            return await _context.Venda.FindAsync(id);
        }

        /// <summary>
        /// Insere uma venda no BD
        /// </summary>
        /// <param name="venda">A venda a ser inserida</param>
        /// <returns></returns>
        public int Insere (Venda venda)
        {
            _context.Venda.Add(venda);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Atualiza o status de uma venda para "Pago"
        /// </summary>
        /// <param name="venda">A venda a ser paga</param>
        /// <returns></returns>
        public int Pagar (Venda venda)
        {
            if (_context.Venda.Any(x => x.Id.Equals(venda.Id)))
            {
                try
                {
                    _context.Venda.Update(venda);
                    return _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            }
            else
                throw new NotFoundException("Venda não encontrada!");
        }
    }
}
