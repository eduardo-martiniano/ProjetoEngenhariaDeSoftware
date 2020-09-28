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
    public class TarefaRepository : ITarefaRepository
    {
        private ProjetoContext _tarefaRepository;
        public TarefaRepository(ProjetoContext tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public void Add(Tarefa tarefa)
        {
            _tarefaRepository.Tarefas.Add(tarefa);
            _tarefaRepository.SaveChanges();
        }

        public void Editar(int tarefaId)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int tarefaId)
        {
            throw new NotImplementedException();
        }

        public Tarefa ObterPorId(int tarefaId)
        {
            throw new NotImplementedException();
        }

        public List<Tarefa> ObterPorProjeto(int projetoId)
        {
            return _tarefaRepository.Tarefas.ToList();
        }

        public List<Tarefa> ObterPorStatus(int projetoId, TarefaStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
