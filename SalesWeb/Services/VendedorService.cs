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
    public class VendedorService
    {
        private readonly SalesWebContext _context;

        public VendedorService(SalesWebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtem todos os vendedores armazenados no BD
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Vendedor>> ConsultaAsync ()
        {
            return await _context.Vendedor.Include(x => x.Departamento).ToListAsync();
        }

        /// <summary>
        /// Obtem um vendedor através do seu Id
        /// </summary>
        /// <returns></returns>
        public async Task<Vendedor> ConsultaAsync (int id)
        {
            return await _context.Vendedor.Include(x => x.Departamento).FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Insere um vendedor no BD
        /// </summary>
        /// <param name="vendedor">O vendedor a ser inserido</param>
        public int Insere (Vendedor vendedor)
        {
            _context.Vendedor.Add(vendedor);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Atualiza os dados de um vendedor
        /// </summary>
        /// <param name="vendedor">O vendedor a ser atualizado</param>
        /// <returns></returns>
        public int Atualiza (Vendedor vendedor)
        {
            if (_context.Vendedor.Any(x => x.Id.Equals(vendedor.Id)))
            {
                try
                {
                    _context.Vendedor.Update(vendedor);
                    return _context.SaveChanges();

                } catch (DbUpdateConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            }
            else
                throw new NotFoundException("Vendedor não encontrado!");
        }

        /// <summary>
        /// Remove um vendedor do BD
        /// </summary>
        /// <param name="id">O vendedor a ser removido</param>
        /// <returns></returns>
        public int Remove (int id)
        {
            var vendedor = _context.Vendedor.Find(id);
            _context.Vendedor.Remove(vendedor);

            return _context.SaveChanges();
        }
    }
}
