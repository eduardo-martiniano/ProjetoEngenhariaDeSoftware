using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngSoftware.Contracts;
using EngSoftware.Models.Entities;
using EngSoftware.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EngSoftware.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly IProjetoRepository _projetoRepository;
        public ProjetoController(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Todos()
        {
            ViewBag.Projetos = _projetoRepository.ObterTodos();
            return View();
        }

        public IActionResult TodosEmAndamento()
        {
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.ACEITO);
            return View();
        }
        public IActionResult TodosPendentes()
        {
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.AGUARDANDO);
            return View();
        }
        public IActionResult TodosCancelados()
        {
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.NEGADO);
            return View();
        }

        public IActionResult TodosConcluidos()
        {
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.CONCLUIDO);
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([FromForm] Projeto projeto)
        {
            projeto.Status = ProjetoStatus.AGUARDANDO;
            if (ModelState.IsValid)
            {
                _projetoRepository.Add(projeto);
                return RedirectToAction("TodosPendentes", "Projeto");
            }

            return View();
        }

        public IActionResult Excluir(int id)
        {
            _projetoRepository.Excluir(id);
            return RedirectToAction("MenuCoordenador", "Menu");

        }

        public IActionResult Aprovar(int id)
        {
            _projetoRepository.Aceitar(id);

            return RedirectToAction("TodosPendentes", "Projeto");
        }

        public IActionResult Negar(int id)
        {
            _projetoRepository.Negar(id);

            return RedirectToAction("TodosPendentes", "Projeto");
        }
    }
}
