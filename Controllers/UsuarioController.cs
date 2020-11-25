using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngSoftware.Contracts;
using EngSoftware.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EngSoftware.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            ViewBag.Sucesso = "";
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro([FromForm] Pessoa usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Sucesso = "Digite todos os valores";
                return View();
            }

            if (_usuarioRepository.JaExiste(usuario))
            {
                ViewBag.Sucesso = "O usuario já existe!";
                return View();
            }

            _usuarioRepository.Add(usuario);
            ViewBag.Sucesso = "Cadastrado com sucesso!";
            return View();
            
        }

        [HttpGet]
        public IActionResult Todos()
        {
            return Ok(_usuarioRepository.GetTodos());
        }

        [HttpGet]
        public IActionResult CadastroLogado()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CadastroLogado([FromForm] Pessoa usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Sucesso = "Digite todos os valores";
                return View();
            }

            if (_usuarioRepository.JaExiste(usuario))
            {
                ViewBag.Sucesso = "O usuario já existe!";
                return View();
            }

            _usuarioRepository.Add(usuario);
            ViewBag.Sucesso = "Cadastrado com sucesso!";
            return RedirectToAction("TodosOsUsuarios", "Projeto", new { id = EngSoftware.Models.Usuario.Usuario.projetoId});
        }
    }
}
