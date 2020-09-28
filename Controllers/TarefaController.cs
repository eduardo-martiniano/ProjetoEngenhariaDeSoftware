using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngSoftware.Contracts;
using EngSoftware.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EngSoftware.Controllers
{
    public class TarefaController : Controller
    {
        private ITarefaRepository _tarefaRepository;
        public TarefaController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
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
        public IActionResult Cadastro([FromForm] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _tarefaRepository.Add(tarefa);
                return Ok("Tarefa criada com sucesso!");
            }

            return BadRequest("Algo deu errado");
        }
        [HttpGet]
        public IActionResult Todas()
        { 
            ViewBag.Tarefas = _tarefaRepository.ObterPorProjeto(1);
            return View();
        }
    }
}
