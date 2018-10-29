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
        public async Task<ICollection<Departamento>> ConsultaAsync ()
        {
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync();
        }

        /// <summary>
        /// Obtem um departamento através do seu Id
        /// </summary>
        /// <param name="id">O departamento a ser procurado</param>
        /// <returns></returns>
        public async Task<Departamento> ConsultaAsync (int id)
        {
            return await _context.Departamento.FindAsync(id);
        }

        /// <summary>
        /// Insere um novo departamento no BD
        /// </summary>
        /// <param name="departamento">O departamento a ser inserido</param>
        /// <returns></returns>
        public int Insere (Departamento departamento)
        {
            _context.Departamento.Add(departamento);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Atualiza os dados de um departmaento
        /// </summary>
        /// <param name="departamento">O departamento a ser atualizado</param>
        /// <returns></returns>
        public int Atualiza (Departamento departamento)
        {
            if (_context.Departamento.Any(x => x.Id.Equals(departamento.Id)))
            {
                try
                {
                    _context.Departamento.Update(departamento);
                    return _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            }
            else
                throw new NotFoundException("Departamento não encontrado!");
        }

        /// <summary>
        /// Remove um departamento do BD
        /// </summary>
        /// <param name="id">O Id do departamento a ser removido</param>
        /// <returns></returns>
        public int Remove (int id)
        {
            var departamento = _context.Departamento.Find(id);
            _context.Departamento.Remove(departamento);

            return _context.SaveChanges();
        }
    }
}
