using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngSoftware.Contracts;
using EngSoftware.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EngSoftware.Controllers
{
    public class MenuController : Controller
    {
        private readonly IProjetoRepository _projetoRepository;
        public MenuController(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MenuCoordenador()
        {
            return View();
        }

        public IActionResult MenuPesquisador()
        {
            return View();
        }

        public IActionResult MenuAluno(int alunoId)
        {
            var projetos = _projetoRepository.ObterPorUsuario(alunoId);
            ViewBag.Projetos = projetos;
            return View();
        }
    }
}
