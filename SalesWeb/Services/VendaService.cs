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
            return await _context.Venda
                .Include(obj => obj.Vendedor)
                .Include(obj => obj.Vendedor.Departamento)
                .OrderBy(obj => obj.Data).ToListAsync();
        }


        /// <summary>
        /// Obtem todas as vendas armazenadas no BD de acordo com o período informado
        /// </summary>
        /// <param name="dataMinima">Data mínima</param>
        /// <param name="dataLimite">Data limite</param>
        /// <returns></returns>
        public async Task<ICollection<Venda>> ConsultaAsync(DateTime dataMinima, DateTime dataLimite)
        {
            return await _context.Venda
                .Include(obj => obj.Vendedor)
                .Include(obj => obj.Vendedor.Departamento)
                .OrderBy(obj => obj.Data)
                .Where(x => x.Data >= dataMinima && x.Data <= dataLimite)
                .OrderBy(x => x.Data).ToListAsync();
        }

        /// <summary>
        /// Obtem todas as vendas armazenadas no BD de acordo com o vendedor informado
        /// </summary>
        /// <param name="vendedor">O vendedor</param>
        /// <returns></returns>
        public async Task<ICollection<Venda>> ConsultaAsync(Vendedor vendedor)
        {
            return await _context.Venda
                .Include(obj => obj.Vendedor)
                .Include(obj => obj.Vendedor.Departamento)
                .OrderBy(obj => obj.Data)
                .Where(x => x.Vendedor.Id.Equals(vendedor.Id))
                .OrderBy(x => x.Data).ToListAsync();
        }

        /// <summary>
        /// Obtem todas as vendas armazenadas no BD de acordo com o departamento
        /// </summary>
        /// <param name="departamento">O departamento</param>
        /// <returns></returns>
        public async Task<ICollection<Venda>> ConsultaAsync(Departamento departamento)
        {
            return await _context.Venda
                .Include(obj => obj.Vendedor)
                .Include(obj => obj.Vendedor.Departamento)
                .OrderBy(obj => obj.Data)
                .Where(x => x.Vendedor.Departamento.Equals(departamento.Id))
                .OrderBy(x => x.Data).ToListAsync();
        }

        /// <summary>
        /// Obtem uma venda específica no BD
        /// </summary>
        /// <returns></returns>
        public async Task<Venda> ConsultaAsync(int id)
        {
            return await _context.Venda
                .Include(obj => obj.Vendedor)
                .Include(obj => obj.Vendedor.Departamento)
                .Where(x => x.Id.Equals(id)).FirstAsync();
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
        /// Atualiza os dados de uma venda
        /// </summary>
        /// <param name="venda">A venda a ser atualizada</param>
        /// <returns></returns>
        public int Atualizar (Venda venda)
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
                throw new NotFoundException("Venda não encontrada");
        }
    }
}
