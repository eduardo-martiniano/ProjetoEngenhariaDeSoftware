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
        public IActionResult Cadastro(int id)
        {
            // ViewBag.Projetos = _projetoRepository.ObterTodos();
            ViewBag.Projeto = _projetoRepository.ObterPorId(id);
            ViewBag.Mensagem = "";
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
            ViewBag.Mensagem = "Campos invalidos!";
            return View();
        }

        [HttpGet]
        public IActionResult Todas(int id)
        {
            ViewBag.Tarefas = _tarefaRepository.ObterPorProjeto(id);
            var projeto = _projetoRepository.ObterPorId(id);
            ViewBag.Projeto = projeto;
            ViewBag.Equipe = _projetoRepository.UsuariosRelacionadasAoProjeto(projeto);
            return View();
        }

        public IActionResult Excluir(int id)
        {
            var tarefa = _tarefaRepository.ObterPorId(id);
            _tarefaRepository.Excluir(id);
            return RedirectToAction("Todas", "Tarefa", new { id = tarefa.ProjetoId });
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            ViewBag.Mensagem = "";
            // ViewBag.Projetos = _projetoRepository.ObterTodos();
            var tarefa = _tarefaRepository.ObterPorId(id);
            ViewBag.Projeto = _projetoRepository.ObterPorId(tarefa.ProjetoId);
            ViewBag.Tarefa = _tarefaRepository.ObterPorId(id);
            return View();
        }

        [HttpPost]
        public IActionResult Editar([FromForm] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _tarefaRepository.Editar(tarefa);
                return RedirectToAction("Todas", "Tarefa", new { id = tarefa.ProjetoId });
            }
            
            ViewBag.Tarefa = _tarefaRepository.ObterPorId(tarefa.Id);
            ViewBag.Projetos = _projetoRepository.ObterTodos();
            ViewBag.MensagemErro = "Preencha todos os campos!";
            return View();

        }
    }
}
