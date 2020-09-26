using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EngSoftware.Models;
using EngSoftware.Models.Entities;
using EngSoftware.Database;

namespace EngSoftware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Mensagem = "";
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] Pessoa pessoa)
        {
            var dados = new Dados();
            var pessoas = dados.GetPessoas();
            foreach (var item in pessoas)
            {
                if (pessoa.Email == item.Email && pessoa.Senha == item.Senha)
                {
                    return RedirectToAction("Index", "Menu");
                }
            }
            ViewBag.Mensagem = "senha invalida";
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
