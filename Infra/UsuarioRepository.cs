using EngSoftware.Contracts;
using EngSoftware.Database;
using EngSoftware.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Infra
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ProjetoContext _usuarioRepository;
        public UsuarioRepository(ProjetoContext usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public void Add(Pessoa usuario)
        {
            _usuarioRepository.Pessoas.Add(usuario);
            _usuarioRepository.SaveChanges();
        }

        public void Editar(int usuarioId)
        {
            
        }

        public void Excluir(int usuarioId)
        {
            _usuarioRepository.Pessoas.Remove(_usuarioRepository.Pessoas.Find(usuarioId));
            _usuarioRepository.SaveChanges();
        }

        public Pessoa GetId(int usuarioId)
        {
            return _usuarioRepository.Pessoas.Find(usuarioId);
        }

        public List<Pessoa> GetTodos()
        {
            return _usuarioRepository.Pessoas.ToList();
        }

        public bool JaExiste(Pessoa usuario)
        {
            var pessoa = _usuarioRepository.Pessoas.Where(u => u.Email == usuario.Email).FirstOrDefault();
            
            if (pessoa == null) return false;

            return true;
        }
    }
}
