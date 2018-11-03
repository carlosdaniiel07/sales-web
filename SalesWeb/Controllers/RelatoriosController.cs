using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using SalesWeb.Services;
using SalesWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesWeb.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly VendaService _vendasService;
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public RelatoriosController (VendaService vendaService, VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendasService = vendaService;
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> VendasGerais()
        {
            return View(await _vendasService.ConsultaAsync());
        }

        public async Task<IActionResult> VendasPorPeriodo (DateTime? dataMinima, DateTime? dataLimite)
        {
            // Lista de dados
            ICollection<Venda> dados = new List<Venda>();

            if (dataMinima.HasValue && dataLimite.HasValue)
            {
                dados = await _vendasService.ConsultaAsync(dataMinima.Value, dataLimite.Value);

                ViewData["dataMinima"] = dataMinima.Value.ToShortDateString();
                ViewData["dataLimite"] = dataLimite.Value.ToShortDateString();
            }

            return View(dados);
        }

        public async Task<IActionResult> VendasPorVendedor (int? Vendedor)
        {
            ICollection<Venda> dados = new List<Venda>();
            
            // Carrega select com os vendedores
            ViewBag.Vendedores = new SelectList(await _vendedorService.ConsultaAsync(), "Id", "Nome");

            if(Vendedor.HasValue)
            {
                // Obtem o vendedor através do seu Id
                var vendedor = await _vendedorService.ConsultaAsync(Vendedor.Value);

                if (vendedor != null)
                    dados = await _vendasService.ConsultaAsync(vendedor);
            }

            return View(dados);   
        }

        public async Task<IActionResult> VendasPorDepartamento (int? Departamento)
        {
            ICollection<Venda> dados = new List<Venda>();

            // Carrega select com os departamentos
            ViewBag.Departamentos = new SelectList(await _departamentoService.ConsultaAsync(), "Id", "Nome");

            if (Departamento.HasValue)
            {
                // Obtem o departamento através do seu Id
                var departamento = await _departamentoService.ConsultaAsync(Departamento.Value);

                if (departamento != null)
                    dados = await _vendasService.ConsultaAsync(departamento);
            }

            return View(dados);
        }
    }
}