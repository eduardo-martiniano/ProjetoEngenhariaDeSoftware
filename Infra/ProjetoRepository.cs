using EngSoftware.Contracts;
using EngSoftware.Database;
using EngSoftware.Models.Entities;
using EngSoftware.Models.Enums;
using System;
using System.Collections.Generic;
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

        public void Cancelar(int projetoId)
        {
            var projeto = _projetoRepository.Projetos.Find(projetoId);
            projeto.Status = ProjetoStatus.CANCELADO;
            _projetoRepository.SaveChanges();
        }

        public void Editar(int projetoId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<Projeto> ObterPorStatus(ProjetoStatus status)
        {
            return _projetoRepository.Projetos.Where(p => p.Status == status).ToList();
        }

        public List<Projeto> ObterTodos()
        {
            return _projetoRepository.Projetos.ToList();
        }
    }
}
