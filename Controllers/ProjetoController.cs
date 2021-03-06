﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngSoftware.Contracts;
using EngSoftware.Models.Entities;
using EngSoftware.Models.Enums;
using EngSoftware.Models.Usuario;
using EngSoftware.Services;
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
            ViewBag.Projetos = _projetoRepository.CanceladosEnegados();
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
                _projetoRepository.AddUsuario(projeto.Id, pessoa);
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

        public IActionResult Concluir(int id)
        {
            var _projetoService = new ProjetoService(_projetoRepository);
            var resposta = _projetoService.ProjetoPodeSerConcluido(id);

            if (resposta)
            {
                _projetoRepository.Concluir(id);
            }

            var projeto = _projetoRepository.ObterPorId(id);

            if(projeto.Responsavel.Tipo == EngSoftware.Models.Enums.TipoPessoa.Pesquisador)
            {
                return RedirectToAction("MenuPesquisador", "Menu");
            }
            else return RedirectToAction("MenuCoordenador", "Menu");
        }

        public IActionResult Cancelar(int id)
        {
            var _projetoService = new ProjetoService(_projetoRepository);
            var resposta = _projetoService.ProjetoPodeSerConcluido(id);

            if (resposta)
            {
                _projetoRepository.Cancelar(id);
            }
            
            var projeto = _projetoRepository.ObterPorId(id);

            if(projeto.Responsavel.Tipo == EngSoftware.Models.Enums.TipoPessoa.Pesquisador)
            {
                return RedirectToAction("MenuPesquisador", "Menu");
            }
            else return RedirectToAction("MenuCoordenador", "Menu");
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

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var projeto = _projetoRepository.ObterPorId(id);

            ViewBag.Projeto = projeto;

            ViewBag.ProjetoId = projeto.Id;

            ViewBag.TodosUsuarios = _usuarioRepository.GetTodos();

            ViewBag.Pessoas = projeto.PessoaProjetos.Select(p => p.Pessoa).ToList();

            return View();
        }

        [HttpGet]
        public IActionResult TodosOsUsuarios(int id)
        {
            //Guardar em uma variavel estatica o projeto que estamos trabalhando
            Usuario.projetoId = id;
            ViewBag.TodosUsuarios = _usuarioRepository.GetTodos();
            var projeto = _projetoRepository.ObterPorId(id);
            ViewBag.ProjetoId = projeto.Id;
            return View();
        }

        public IActionResult AddUsuarioAoProjeto(int projetoId, int usuarioId)
        {
            var usuario = _usuarioRepository.GetId(usuarioId);
            _projetoRepository.AddUsuario(projetoId, usuario);
            return RedirectToAction("Detalhes", "Projeto", new { id = projetoId });
        }

        public IActionResult RemoveUsuarioDoProjeto(int projetoId, int usuarioId)
        {
            var projeto = _projetoRepository.ObterPorId(projetoId);
            var pessoa = _usuarioRepository.GetId(usuarioId);
            _projetoRepository.RemoveUsuarioRelacionadoAoProjeto(projeto, pessoa);
            return RedirectToAction("Detalhes", "Projeto", new { id = projetoId });
        }

        [HttpGet]
        public IActionResult ProjetosDoPesquisador(int id)
        {
            var pesquisador = _usuarioRepository.GetId(id);
            List<Projeto> p_temp = _projetoRepository.ProjetosRelacionadosAoUsuario(pesquisador);
            List<Projeto> p_final = new List<Projeto>();
            foreach(Projeto projeto in p_temp)
            {
                if(projeto.Status == EngSoftware.Models.Enums.ProjetoStatus.ACEITO) p_final.Add(projeto);
            }
            ViewBag.ProjetosDoPesquisador = p_final;
            ViewBag.Projetos = _projetoRepository.ObterPorStatus(ProjetoStatus.ACEITO);
            return View();
        }
    }
}
