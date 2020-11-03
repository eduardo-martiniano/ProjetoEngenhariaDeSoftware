using EngSoftware.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Models.Entities
{
    public class Projeto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataFim { get; set; }
        [Required]
        public ProjetoStatus Status { get; set; }
        public Pessoa Responsavel { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
        public ICollection<Pessoa> Pessoas { get; set; }
    }
}
