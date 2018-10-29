using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using SalesWeb.Models;
using SalesWeb.Models.ViewModel;
using SalesWeb.Services;

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
        public async Task<IActionResult> Index()
        {
            return View(await _vendedorService.ConsultaAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Vendedores/Create</returns>
        public async Task<IActionResult> Create ()
        {
            // Obtem uma lista de departamentos
            var departamentos = await _departamentoService.ConsultaAsync();
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
        public async Task<IActionResult> Create (Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                _vendedorService.Insere(vendedor);
                return RedirectToAction(nameof(Index));
            }
            else
                return View(new VendedorFormViewModel { Departamentos = await _departamentoService.ConsultaAsync(), Vendedor = vendedor });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Vendedores/Edit</returns>
        public async Task<IActionResult> Edit (int? id)
        {
            if(id != null)
            {
                // Obtem o vendedor através do seu Id
                var viewModel = new VendedorFormViewModel
                {
                    Vendedor = await _vendedorService.ConsultaAsync(id.Value),
                    Departamentos = await _departamentoService.ConsultaAsync()
                };

                if (viewModel.Vendedor != null)
                    return View(viewModel);
                else
                    return RedirectToAction(nameof(Error), new { message = "Vendedor não localizado" });
            }
            else
                return RedirectToAction(nameof(Error), new { message = "Vendedor deve ser informado" });
        }

        /// <summary>
        /// Atualiza os dados de um vendedor
        /// </summary>
        /// <param name="id">O Id do vendedor</param>
        /// <param name="vendedor">Os novos dados a serem salvos</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, Vendedor vendedor)
        {
            if(ModelState.IsValid)
            {
                if (id.Equals(vendedor.Id))
                {
                    try
                    {
                        _vendedorService.Atualiza(vendedor);
                        return RedirectToAction(nameof(Index));

                    }
                    catch (ApplicationException e) { return RedirectToAction(nameof(Error), new { message = e.Message }); }
                }
                else
                    return RedirectToAction(nameof(Error), new { message = "Falha ao processar a sua requisição" });
            }
            else
                return View(new VendedorFormViewModel { Departamentos = await _departamentoService.ConsultaAsync(), Vendedor = vendedor });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Vendedores/Details</returns>
        public async Task<IActionResult> Details (int? id)
        {
            if (id != null)
            {
                // Obtem o vendedor através do seu Id
                var vendedor = await _vendedorService.ConsultaAsync(id.Value);

                if (vendedor != null)
                    return View(vendedor);
                else
                    return RedirectToAction(nameof(Error), new { message = "Vendedor não localizado" });
            }
            else
                return RedirectToAction(nameof(Error), new { message = "Vendedor deve ser informado" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">O Id do vendedor a ser removido</param>
        /// <returns>Vendedores/Delete</returns>
        public async Task<IActionResult> Delete (int? id)
        {
            if (id != null)
            {
                // Obtem o vendedor através do seu Id
                var vendedor = await _vendedorService.ConsultaAsync(id.Value);

                if (vendedor != null)
                    return View(vendedor);
                else
                    return RedirectToAction(nameof(Error), new { message = "Vendedor não localizado" });
            }
            else
                return RedirectToAction(nameof(Error), new { message = "Vendedor deve ser informado" });
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

        /// <summary>
        /// Retorna uma página de erro ao usuário
        /// </summary>
        /// <param name="message">A mensagem de erro a ser exibida</param>
        /// <returns></returns>
        public IActionResult Error (string message)
        {
            var errorViewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(errorViewModel);
        }
    }
}
