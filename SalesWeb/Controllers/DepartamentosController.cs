using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

using SalesWeb.Models;
using SalesWeb.Models.ViewModel;
using SalesWeb.Services;

namespace SalesWeb.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly DepartamentoService _departamentoService;

        public DepartamentosController(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Departamentos/Index</returns>
        public async Task<IActionResult> Index ()
        {
            return View(await _departamentoService.ConsultaAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Departamentos/Create</returns>
        public IActionResult Create ()
        {
            return View();
        }

        /// <summary>
        /// Insere um departamento no BD
        /// </summary>
        /// <param name="departamento">O departamento a ser inserido</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _departamentoService.Insere(departamento);
                return RedirectToAction(nameof(Index));
            }
            else
                return View(departamento);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Departamentos/Edit</returns>
        public async Task<IActionResult> Edit (int? id)
        {
            if (id != null)
            {
                // Obtem o departamento
                var departamento = await _departamentoService.ConsultaAsync(id.Value);

                if (departamento != null)
                    return View(departamento);
                else
                    return RedirectToAction(nameof(Error), new { message = "Departamento não encontrado" });
            }
            else
                return RedirectToAction(nameof(Error), new { message = "Departamento deve ser informado" });
        }

        /// <summary>
        /// Atualiza os dados de um departamento
        /// </summary>
        /// <param name="id">O Id do departamento</param>
        /// <param name="departamento">Os novos dados do departamento</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                if (id.Equals(departamento.Id))
                {
                    try
                    {
                        _departamentoService.Atualiza(departamento);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (ApplicationException e) { return RedirectToAction(nameof(Error), new { message = e.Message }); }
                }
                else
                    return RedirectToAction(nameof(Error), new { message = "Falha ao processar a sua requisição" });
            }
            else
                return View(departamento);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Departamentos/Details</returns>
        public async Task<IActionResult> Details (int? id)
        {
            if (id != null)
            {
                // Obtem o departamento
                var departamento = await _departamentoService.ConsultaAsync(id.Value);

                if (departamento != null)
                    return View(departamento);
                else
                    return RedirectToAction(nameof(Error), new { message = "Departamento não encontrado" });
            }
            else
                return RedirectToAction(nameof(Error), new { message = "Departamento deve ser informado" });
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id != null)
            {
                // Obtem o departamento
                var departamento = await _departamentoService.ConsultaAsync(id.Value);

                if (departamento != null)
                    return View(departamento);
                else
                    return RedirectToAction(nameof(Error), new { message = "Departamento não encontrado" });
            }
            else
                return RedirectToAction(nameof(Error), new { message = "Departamento deve ser informado" });
        }

        /// <summary>
        /// Remove um departamento do BD
        /// </summary>
        /// <param name="id">O Id do departamento a ser removido</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (int id)
        {
            _departamentoService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Exibe uma página de erro au usuário
        /// </summary>
        /// <param name="message"></param>
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
