using EngSoftware.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ProjetoEngenhariaDeSoftware.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Database
{
    public class ProjetoContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<PessoaProjeto> PessoaProjetos { get; set; }

        public ProjetoContext(DbContextOptions<ProjetoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PessoaProjeto>().HasKey(pp => new {pp.PessoaId, pp.ProjetoId});
        }
    }
}
