using EngSoftware.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EngSoftware.Models.Entities
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TarefaStatus Status { get; set; }
        public int? ProjetoId { get; set; }
        public int? PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
