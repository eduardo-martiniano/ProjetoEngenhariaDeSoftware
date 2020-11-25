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
            ViewBag.TodosUsuarios = _usuarioRepository.GetTodos();
            return View();
        }

        [HttpGet]
        public IActionResult CadastroLogado()
        {
            ViewBag.MaxCoord = _usuarioRepository.MaxCoordenadores();
            return View();
        }
        
        [HttpPost]
        public IActionResult CadastroLogado([FromForm] Pessoa usuario)
        {
            ViewBag.MaxCoord = _usuarioRepository.MaxCoordenadores();


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

            if (EngSoftware.Models.Usuario.Usuario._usuario.Tipo == Models.Enums.TipoPessoa.ADMIN)
                return RedirectToAction("MenuAdministrador", "Menu");

            return RedirectToAction("TodosOsUsuarios", "Projeto", new { id = EngSoftware.Models.Usuario.Usuario.projetoId});
        }
    }
}
