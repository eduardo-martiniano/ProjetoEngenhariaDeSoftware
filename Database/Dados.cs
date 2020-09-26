using EngSoftware.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Database
{
    public class Dados
    {
        List<Pessoa> pessoas = new List<Pessoa>();
        public List<Pessoa> GetPessoas()
        {
            pessoas.Add(new Pessoa() 
            {
                Email = "eduardo@gmail.com",
                Nome = "Eduardo",
                Senha = "123",
                Tipo = Models.Enums.TipoPessoa.Coodernador,
                Id = 1
            });
            return pessoas;
        }
        public void AddPessoa(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
        }

    }
}
