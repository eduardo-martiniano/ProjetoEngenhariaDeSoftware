using EngSoftware.Models.Entities;

namespace ProjetoEngenhariaDeSoftware.Models.Entities
{
    public class PessoaProjeto
    {
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
    }
}