using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using SalesWeb.Models;
using SalesWeb.Models.ViewModel;
using SalesWeb.Services;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Controllers
{
    public class VendasController : Controller
    {
        private readonly VendaService _vendaService;
        private readonly VendedorService _vendedorService;

        public VendasController (VendaService vendaService, VendedorService vendedorService)
        {
            _vendaService = vendaService;
            _vendedorService = vendedorService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _vendaService.ConsultaAsync());
        }

        public async Task<IActionResult> New ()
        {
            var viewModel = new VendaFormViewModel { Vendedores = await _vendedorService.ConsultaAsync() };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New (Venda venda)
        {
            venda.Status = StatusVenda.Pendente;

            if (ModelState.IsValid)
            {
                _vendaService.Insere(venda);
                return RedirectToAction(nameof(Index));
            }
            else
                return View(new VendaFormViewModel { Venda = venda, Vendedores = await _vendedorService.ConsultaAsync() });

        }

        public async Task<IActionResult> Cancel (int? id)
        {
            if (id.HasValue)
                return View(await _vendaService.ConsultaAsync(id.Value));
            else
                return RedirectToAction(nameof(Error), new { message = "Venda deve ser informada" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel (int id)
        {
            try
            {
                // Recupera a venda através do Id
                var venda = await _vendaService.ConsultaAsync(id);

                venda.Status = StatusVenda.Cancelada;
                _vendaService.Atualizar(venda);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e) { return RedirectToAction(nameof(Error), new { message = e.Message }); }
        }

        public async Task<IActionResult> Pay (int? id)
        {
            if (id.HasValue)
                return View(await _vendaService.ConsultaAsync(id.Value));
            else
                return RedirectToAction(nameof(Error), new { message = "Venda deve ser informada" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay (int id)
        {
            try
            {
                // Recupera a venda através do Id
                var venda = await _vendaService.ConsultaAsync(id);

                venda.Status = StatusVenda.Paga;
                _vendaService.Atualizar(venda);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e) { return RedirectToAction(nameof(Error), new { message = e.Message }); }
        }

        /// <summary>
        /// Retorna uma página de erro ao usuário
        /// </summary>
        /// <param name="message">A mensagem de erro a ser exibida</param>
        /// <returns></returns>
        public IActionResult Error(string message)
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