using EngSoftware.Models.Enums;
using System.ComponentModel.DataAnnotations;

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
    }
}
