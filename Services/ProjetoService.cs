using EngSoftware.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Services
{
    public class ProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoService(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public  bool ProjetoPodeSerConcluido(int projetoId)
        {
            var projeto = _projetoRepository.ObterPorId(projetoId);

            var tarefasPendentes = projeto
                .Tarefas
                .Where(t => t.Status == Models.Enums.TarefaStatus.Andamento
                || t.Status == Models.Enums.TarefaStatus.BackLogProduto 
                || t.Status == Models.Enums.TarefaStatus.BackLogSprint 
                || t.Status == Models.Enums.TarefaStatus.Revisao).ToList();

            if (tarefasPendentes.Count > 0) return false;

            return true;
        }
    }
}
