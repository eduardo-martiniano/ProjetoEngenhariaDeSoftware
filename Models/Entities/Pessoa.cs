using EngSoftware.Models.Enums;
using ProjetoEngenhariaDeSoftware.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngSoftware.Models.Entities
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public TipoPessoa Tipo { get; set; }
        public ICollection<PessoaProjeto> PessoaProjetos { get; set; }
    }
}
