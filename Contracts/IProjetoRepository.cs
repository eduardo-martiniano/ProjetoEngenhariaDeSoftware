using EngSoftware.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Contracts
{
    public interface IProjetoRepository
    {
        void Add(Projeto projeto);
        void Excluir(int projetoId);
        void Editar(int projetoId);
        Projeto ObterPorId(int projetoId);
        List<Projeto> ObterTodos();
    }
}
