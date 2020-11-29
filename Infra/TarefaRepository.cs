using EngSoftware.Contracts;
using EngSoftware.Database;
using EngSoftware.Models.Entities;
using EngSoftware.Models.Enums;
using Microsoft.EntityFrameworkCore;
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

        public void Editar(Tarefa tarefa)
        {   
            var _tarefa = ObterPorId(tarefa.Id);
            _tarefa.Nome = tarefa.Nome;
            _tarefa.Status = tarefa.Status;
            _tarefa.Descricao = tarefa.Descricao;
            _tarefa.DataFim = tarefa.DataFim;
            _tarefa.DataInicio = _tarefa.DataInicio;
            _tarefaRepository.Tarefas.Update(_tarefa);
            _tarefaRepository.SaveChanges();
            
        }

        public void Excluir(int tarefaId)
        {
            _tarefaRepository.Remove(_tarefaRepository.Tarefas.Find(tarefaId));
            _tarefaRepository.SaveChanges();
        }

        public Tarefa ObterPorId(int tarefaId)
        {
            return _tarefaRepository.Tarefas.Include(t => t.Pessoa)
                                            .Where(a => a.Id == tarefaId)
                                            .FirstOrDefault();
        }

        public List<Tarefa> ObterPorProjeto(int projetoId)
        {
            return _tarefaRepository.Tarefas.Include(t => t.Pessoa)
                                            .Where(t => t.ProjetoId == projetoId)
                                            .ToList();
        }

        public List<Tarefa> ObterPorStatus(int projetoId, TarefaStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
