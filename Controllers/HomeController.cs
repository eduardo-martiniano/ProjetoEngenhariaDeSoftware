﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EngSoftware.Models;
using EngSoftware.Models.Entities;
using EngSoftware.Database;
using EngSoftware.Contracts;
using EngSoftware.Models.Enums;
using EngSoftware.Models.Usuario;

namespace EngSoftware.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUsuarioRepository usuarioRepository, ILogger<HomeController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] Pessoa pessoa)
        {
            if(pessoa.Email == "admin@admin" && pessoa.Senha == "admin")
            {
                Usuario._usuario = new Pessoa { Tipo = TipoPessoa.ADMIN };
                return RedirectToAction("MenuAdministrador", "Menu");
            }
            var usuarios = _usuarioRepository.GetTodos();
            foreach (var item in usuarios)
            {
                if (item.Email == pessoa.Email && item.Senha == pessoa.Senha && item.Tipo == TipoPessoa.Coodernador)
                {
                    Usuario._usuario = item;
                    return RedirectToAction("MenuCoordenador", "Menu");
                }

                if (item.Email == pessoa.Email && item.Senha == pessoa.Senha && item.Tipo == TipoPessoa.Aluno)
                {
                    Usuario._usuario = item;

                    return RedirectToAction("MenuAluno", "Menu", new { id = item.Id });
                }

                if(item.Email == pessoa.Email && item.Senha == pessoa.Senha && item.Tipo == TipoPessoa.Pesquisador)
                {
                    Usuario._usuario = item;

                    return RedirectToAction("MenuPesquisador", "Menu");
                }
            }
            @ViewBag.Mensagem = "Usuario Invalido!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
