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
        private IProjetoRepository _projetoRepository;
        public TarefaController(ITarefaRepository tarefaRepository, IProjetoRepository projetoRepository)
        {
            _tarefaRepository = tarefaRepository;
            _projetoRepository = projetoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            ViewBag.Projetos = _projetoRepository.ObterTodos();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro([FromForm] Tarefa tarefa)
        {
            ViewBag.Projetos = _projetoRepository.ObterTodos();
            if (ModelState.IsValid)
            {
                _tarefaRepository.Add(tarefa);
                return RedirectToAction("Todas", "Tarefa", new {id = tarefa.ProjetoId});
            }

            return BadRequest("Algo deu errado");
        }

        [HttpGet]
        public IActionResult Todas(int id)
        {
            ViewBag.Tarefas = _tarefaRepository.ObterPorProjeto(id);
            return View();
        }

        public IActionResult Excluir(int id)
        {
            var tarefa = _tarefaRepository.ObterPorId(id);
            _tarefaRepository.Excluir(id);
            return RedirectToAction("Todas", "Tarefa", new { id = tarefa.ProjetoId });
        }
    }
}
