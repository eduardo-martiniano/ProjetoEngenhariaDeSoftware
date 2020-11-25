using EngSoftware.Contracts;
using EngSoftware.Database;
using EngSoftware.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Infra
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ProjetoContext _usuarioRepository;
        public UsuarioRepository(ProjetoContext usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public void Add(Pessoa usuario)
        {
            _usuarioRepository.Pessoas.Add(usuario);
            _usuarioRepository.SaveChanges();
        }

        public void Editar(int usuarioId)
        {
            
        }

        public void Excluir(int usuarioId)
        {
            _usuarioRepository.Pessoas.Remove(_usuarioRepository.Pessoas.Find(usuarioId));
            _usuarioRepository.SaveChanges();
        }

        public Pessoa GetId(int usuarioId)
        {
            Pessoa pessoa = _usuarioRepository.Pessoas.Include(p => p.PessoaProjetos).Where(p => p.Id == usuarioId)
                                             .FirstOrDefault();
            foreach(var pp in pessoa.PessoaProjetos)
            {
                pessoa.PessoaProjetos = _usuarioRepository.PessoaProjetos.Include(p => p.Projeto).Include(p => p.Pessoa).Where(a => a.Pessoa == pessoa).ToList();
            }
            foreach(var pp in pessoa.PessoaProjetos)
            {
                pp.Projeto = _usuarioRepository.Projetos.Include(p => p.Tarefas).Include(p => p.Responsavel).Where(p => p.Id == pp.ProjetoId).FirstOrDefault();
            }
            return pessoa;
        }

        public Pessoa GetNome(string nome)
        {
            return _usuarioRepository.Pessoas.Where(p => p.Nome == nome).FirstOrDefault();
        }

        public List<Pessoa> GetTodos()
        {
            List<Pessoa> list = _usuarioRepository.Pessoas.Include(p => p.PessoaProjetos)
                                             .ToList();
            foreach(var p in list)
            {
                p.PessoaProjetos = _usuarioRepository.PessoaProjetos.Include(p => p.Projeto).Include(p => p.Pessoa).Where(a => a.Pessoa == p).ToList();
            }
            
            return list;

        }

        public bool JaExiste(Pessoa usuario)
        {
            var pessoa = _usuarioRepository.Pessoas.Where(u => u.Email == usuario.Email).FirstOrDefault();
            
            if (pessoa == null) return false;

            return true;
        }

        public bool MaxCoordenadores()
        {
            var coordenadores = _usuarioRepository.Pessoas
                                                  .Where(p => p.Tipo == Models.Enums.TipoPessoa.Coodernador)
                                                  .ToList();
            if (coordenadores.Count > 1) return true;
            
            return false;
        }

        public void RemoveUsuarioCadastrado(int usuarioId)
        {
            var pessoa = GetId(usuarioId);
            // remove a pessoa de todos os projetos aos quais ela está associada
            // não ocorre caso a pessoa seja responsável por um projeto, esse caso é testado na view antes de chegar aqui

            List<Projeto> proj = new List<Projeto>();
            foreach(var pp in pessoa.PessoaProjetos)
            {
                proj.Add(pp.Projeto);
                _usuarioRepository.PessoaProjetos.Remove(pp);
            }
            _usuarioRepository.Pessoas.Remove(pessoa);
            _usuarioRepository.SaveChanges();
        }
    }
}
