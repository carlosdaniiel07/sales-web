using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using SalesWeb.Models;
using SalesWeb.Services;
using SalesWeb.Services.Exceptions;
using SalesWeb.Data;


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
        public IActionResult Index ()
        {
            return View(_departamentoService.Consulta());
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
            _departamentoService.Insere(departamento);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Departamentos/Edit</returns>
        public IActionResult Edit (int? id)
        {
            if (id != null)
            {
                // Obtem o departamento
                var departamento = _departamentoService.Consulta(id.Value);

                if (departamento != null)
                    return View(departamento);
                else
                    return NotFound();
            }
            else
                return NotFound();
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
            if (id.Equals(departamento.Id))
            {
                try
                {
                    _departamentoService.Atualiza(departamento);
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
        /// <param name="id"></param>
        /// <returns>Departamentos/Details</returns>
        public IActionResult Details (int? id)
        {
            if (id != null)
            {
                // Obtem o departamento
                var departamento = _departamentoService.Consulta(id.Value);

                if (departamento != null)
                    return View(departamento);
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        public IActionResult Delete (int? id)
        {
            if (id != null)
            {
                // Obtem o departamento
                var departamento = _departamentoService.Consulta(id.Value);

                if (departamento != null)
                    return View(departamento);
                else
                    return NotFound();
            }
            else
                return NotFound();
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
    }
}
