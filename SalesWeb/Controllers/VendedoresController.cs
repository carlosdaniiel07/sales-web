using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Models.ViewModel;
using SalesWeb.Services;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController (VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Vendedores/Index</returns>
        public IActionResult Index()
        {
            return View(_vendedorService.Consulta());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Vendedores/Create</returns>
        public IActionResult Create ()
        {
            // Obtem uma lista de departamentos
            var departamentos = _departamentoService.Consulta();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };

            return View(viewModel);
        }

        /// <summary>
        /// Insere um vendedor no BD
        /// </summary>
        /// <param name="vendedor">O vendedor a ser inserido</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Vendedor vendedor)
        {
            _vendedorService.Insere(vendedor);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Vendedores/Edit</returns>
        public IActionResult Edit (int? id)
        {
            if(id != null)
            {
                // Obtem o vendedor através do seu Id
                var viewModel = new VendedorFormViewModel
                {
                    Vendedor = _vendedorService.Consulta(id.Value),
                    Departamentos = _departamentoService.Consulta()
                };

                if (viewModel.Vendedor != null)
                    return View(viewModel);
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Atualiza os dados de um vendedor
        /// </summary>
        /// <param name="id">O Id do vendedor</param>
        /// <param name="vendedor">Os novos dados a serem salvos</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, Vendedor vendedor)
        {
            if (id.Equals(vendedor.Id))
            {
                try
                {
                    _vendedorService.Atualiza(vendedor);
                    return RedirectToAction(nameof(Index));

                }
                catch (DbConcurrencyException) { return BadRequest(); }
                catch (NotFoundException) { return NotFound(); }
            }
            else
                return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Vendedores/Details</returns>
        public IActionResult Details (int? id)
        {
            if (id != null)
            {
                // Obtem o vendedor através do seu Id
                var vendedor = _vendedorService.Consulta(id.Value);

                if (vendedor != null)
                    return View(vendedor);
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">O Id do vendedor a ser removido</param>
        /// <returns>Vendedores/Delete</returns>
        public IActionResult Delete (int? id)
        {
            if (id != null)
            {
                // Obtem o vendedor através do seu Id
                var vendedor = _vendedorService.Consulta(id.Value);

                if (vendedor != null)
                    return View(vendedor);
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Remove um vendedor do banco de dados
        /// </summary>
        /// <param name="id">O Id do vendedor a ser removido</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (int id)
        {
            _vendedorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
