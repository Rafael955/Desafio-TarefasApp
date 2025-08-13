using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Dtos.Responses
{
    public class ProjetoResponseDto
    {
        public required Guid Id { get; set; }

        public required string Nome { get; set; }

        public string? Descricao { get; set; }

        public List<TarefaResponseDto>? Tarefas { get; set; }    
    }
}
