using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngSoftware.Contracts;
using EngSoftware.Models.Entities;
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

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([FromForm] Projeto projeto)
        {
            _projetoRepository.Add(projeto);

            return RedirectToAction("Todos", "Projeto");
        
        }

        public IActionResult Excluir(int id)
        {
            _projetoRepository.Excluir(id);
            return RedirectToAction("Todos", "Projeto");

        }
    }
}
