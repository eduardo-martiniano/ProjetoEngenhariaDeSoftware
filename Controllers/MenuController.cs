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
        private readonly IUsuarioRepository _usuarioRepository;
        public MenuController(IProjetoRepository projetoRepository, IUsuarioRepository usuarioRepository)
        {
            _projetoRepository = projetoRepository;
            _usuarioRepository = usuarioRepository;
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

        [HttpGet]
        public IActionResult MenuAluno(int id)
        {
            var pessoa = _usuarioRepository.GetId(id);
            var projetos = _projetoRepository.ProjetosRelacionadosAoUsuario(pessoa);
            ViewBag.Projetos = projetos;
            return View();
        }
    }
}
