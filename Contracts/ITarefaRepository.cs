using EngSoftware.Models.Entities;
using EngSoftware.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Contracts
{
    public interface ITarefaRepository
    {
        void Add(Tarefa tarefa);
        void Excluir(int tarefaId);
        void Editar(Tarefa tarefa);
        Tarefa ObterPorId(int tarefaId);
        List<Tarefa> ObterPorProjeto(int projetoId);
        List<Tarefa> ObterPorStatus(int projetoId, TarefaStatus status);
    }
}
