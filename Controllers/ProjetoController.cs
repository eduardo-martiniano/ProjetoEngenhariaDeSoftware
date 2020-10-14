using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngSoftware.Contracts;
using EngSoftware.Models.Entities;
using EngSoftware.Models.Enums;
using EngSoftware.Models.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace EngSoftware.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public ProjetoController(IProjetoRepository projetoRepository, IUsuarioRepository usuarioRepository)
        {
            _projetoRepository = projetoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Todos()
        {
            ViewBag.Projetos = _projetoRepository.ObterTodos();
            return View();
        }

        public IActionResult TodosEmAndamento()
        {
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.ACEITO);
            return View();
        }
        public IActionResult TodosPendentes()
        {
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.AGUARDANDO);
            return View();
        }
        public IActionResult TodosCancelados()
        {
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.NEGADO);
            return View();
        }

        public IActionResult TodosConcluidos()
        {
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.CONCLUIDO);
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.MensagemErro = "";
            return View();
        }

        [HttpPost]
        public IActionResult Criar([FromForm] Projeto projeto)
        {
            projeto.Status = ProjetoStatus.AGUARDANDO;
            var pessoa = _usuarioRepository.GetId(Usuario._usuario.Id);
            projeto.Responsavel = pessoa;
            if (ModelState.IsValid)
            {
                _projetoRepository.Add(projeto);
                if(projeto.Responsavel.Tipo == TipoPessoa.Coodernador)
                    return RedirectToAction("MenuCoordenador", "Menu");
                else
                    return RedirectToAction("MenuPesquisador", "Menu");
            }
            ViewBag.MensagemErro = "Digite todos os dados!";
            return View();
        }

        public IActionResult Excluir(int id)
        {
            _projetoRepository.Excluir(id);
            return RedirectToAction("MenuCoordenador", "Menu");

        }

        public IActionResult Aprovar(int id)
        {
            _projetoRepository.Aceitar(id);

            return RedirectToAction("TodosPendentes", "Projeto");
        }

        public IActionResult Negar(int id)
        {
            _projetoRepository.Negar(id);

            return RedirectToAction("TodosPendentes", "Projeto");
        }
        
        [HttpGet]
        public IActionResult Editar(int id)
        {
            ViewBag.Projeto = _projetoRepository.ObterPorId(id);
            ViewBag.MensagemErro = "";
            return View();
        }

        [HttpPost]
        public IActionResult Editar([FromForm] Projeto projeto)
        {
            if(ModelState.IsValid)
            {
                _projetoRepository.Editar(projeto);

                if (Usuario._usuario.Tipo == TipoPessoa.Pesquisador)
                {
                    return RedirectToAction("MenuPesquisador", "Menu");
                }
                if (Usuario._usuario.Tipo == TipoPessoa.Coodernador)
                {
                    return RedirectToAction("MenuCoordenador", "Menu");
                }
            }
            ViewBag.Projeto = _projetoRepository.ObterPorId(projeto.Id);
            ViewBag.MensagemErro = "Preencha todos os campos!";
            return View();
            
        }
    }
}
