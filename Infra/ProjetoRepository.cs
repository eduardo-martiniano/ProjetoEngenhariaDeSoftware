using EngSoftware.Contracts;
using EngSoftware.Database;
using EngSoftware.Models.Entities;
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

        public void Add(Projeto projeto)
        {
            _projetoRepository.Projetos.Add(projeto);
            _projetoRepository.SaveChanges();
        }

        public void Editar(int projetoId)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int projetoId)
        {
            throw new NotImplementedException();
        }

        public Projeto ObterPorId(int projetoId)
        {
            throw new NotImplementedException();
        }

        public List<Projeto> ObterTodos()
        {
            return _projetoRepository.Projetos.ToList();
        }
    }
}
