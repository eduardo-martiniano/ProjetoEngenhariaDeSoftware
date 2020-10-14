using EngSoftware.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Contracts
{
    public interface IUsuarioRepository
    {
        void Add(Pessoa usuario);
        void Editar(int usuarioId);
        void Excluir(int usuarioId);
        Pessoa GetId(int usuarioId);
        List<Pessoa> GetTodos();
        bool JaExiste(Pessoa usuario);
        Pessoa GetNome(string nome);
    }
}
