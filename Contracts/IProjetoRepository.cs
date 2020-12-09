using EngSoftware.Models.Entities;
using EngSoftware.Models.Enums;
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
        void Editar(Projeto projeto);
        Projeto ObterPorId(int projetoId);
        List<Projeto> ObterTodos();
        List<Projeto> ObterPorStatus(ProjetoStatus status);
        void Aceitar(int projetoId);
        void Negar(int projetoId);
        void Cancelar(int projetoId);
        List<Projeto> ObterPorUsuario(Pessoa pessoa);
        void AddUsuario(int projetoId, Pessoa pessoa);
        List<Projeto> ProjetosRelacionadosAoUsuario(Pessoa pessoa);
        List<Pessoa> UsuariosRelacionadasAoProjeto(Projeto projeto);
        void RemoveUsuarioRelacionadoAoProjeto(Projeto projeto, Pessoa pessoa);
        void Concluir(int id);
    }
}
