using EngSoftware.Contracts;
using EngSoftware.Database;
using EngSoftware.Models.Entities;
using EngSoftware.Models.Enums;
using EngSoftware.Models.Usuario;
using Microsoft.EntityFrameworkCore;
using ProjetoEngenhariaDeSoftware.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Infra
{
    public class ProjetoRepository : IProjetoRepository
    {
        private ProjetoContext _projetoRepository;
        public ProjetoRepository(ProjetoContext projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public void Aceitar(int projetoId)
        {
            var projeto = _projetoRepository.Projetos.Find(projetoId);
            projeto.Status = ProjetoStatus.ACEITO;
            _projetoRepository.SaveChanges();
        }

        public void Add(Projeto projeto)
        {
            _projetoRepository.Projetos.Add(projeto);
            _projetoRepository.SaveChanges();
        }

        public void AddUsuario(int projetoId, Pessoa pessoa)
        {
            var projeto = ObterPorId(projetoId);

            var relacao = _projetoRepository.PessoaProjetos.Where(p => p.PessoaId == pessoa.Id && p.ProjetoId == projetoId).FirstOrDefault();

            if (relacao == null)
            {
                var pp = new PessoaProjeto() { PessoaId = pessoa.Id, ProjetoId = projetoId };
                if (projeto.PessoaProjetos == null)
                {
                    projeto.PessoaProjetos = new Collection<PessoaProjeto>();
                    projeto.PessoaProjetos.Add(pp);
                    _projetoRepository.SaveChanges();
                    return;
                }
                projeto.PessoaProjetos.Add(pp);
                _projetoRepository.SaveChanges();
            }
        }

        public void Cancelar(int projetoId)
        {
            var projeto = _projetoRepository.Projetos.Find(projetoId);
            projeto.Status = ProjetoStatus.CANCELADO;
            _projetoRepository.SaveChanges();
        }

        public void Editar(Projeto projeto)
        {
            _projetoRepository.Projetos.Update(projeto);
            _projetoRepository.SaveChanges();
        }

        public void Excluir(int projetoId)
        {
            var projeto = _projetoRepository.Projetos.Find(projetoId);
            _projetoRepository.Projetos.Remove(projeto);
            _projetoRepository.SaveChanges();
        }

        public void Negar(int projetoId)
        {
            var projeto = _projetoRepository.Projetos.Find(projetoId);
            projeto.Status = ProjetoStatus.NEGADO;
            _projetoRepository.SaveChanges();
        }

        public Projeto ObterPorId(int projetoId)
        {
            return _projetoRepository.Projetos
                                     .Include(p => p.Responsavel)
                                     .Include(p => p.PessoaProjetos)
                .Where(p => p.Id == projetoId).FirstOrDefault();
        }

        public List<Projeto> ObterPorStatus(ProjetoStatus status)
        {
            return _projetoRepository.Projetos.Include(p => p.Responsavel).Where(p => p.Status == status).ToList();
        }

        public List<Projeto> ObterPorUsuario(Pessoa pessoa)
        {
            return _projetoRepository.Projetos.Where(p => p.PessoaProjetos == pessoa).ToList();
        }

        public List<Projeto> ObterTodos()
        {
            return _projetoRepository.Projetos.ToList();
        }

        public List<Projeto> ProjetosRelacionadosAoUsuario(Pessoa pessoa)
        {
            return _projetoRepository.Projetos
                                     .Include(p => p.Responsavel)
                                     .Include(p => p.PessoaProjetos)
                                     .Where(p => p.PessoaProjetos.Select(a => a.PessoaId)
                                     .Contains(pessoa.Id))
                                     .ToList();
        }

        public List<Pessoa> UsuariosRelacionadasAoProjeto(Projeto projeto)
        {
            return _projetoRepository.Pessoas.Include(p => p.PessoaProjetos)
                                             .Where(p => p.PessoaProjetos.Select(a => a.ProjetoId)
                                             .Contains(projeto.Id))
                                             .ToList();
        }
    }
}
