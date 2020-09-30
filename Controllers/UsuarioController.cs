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
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro([FromForm] Pessoa usuario)
        {
            _usuarioRepository.Add(usuario);
            return Ok("Usuario Cadastrado com sucesso!");
            
        }

        [HttpGet]
        public IActionResult Todos()
        {
            return Ok(_usuarioRepository.GetTodos());
        }
    }
}
