using EngSoftware.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EngSoftware.Models.Entities
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoPessoa Tipo { get; set; }
    }
}
