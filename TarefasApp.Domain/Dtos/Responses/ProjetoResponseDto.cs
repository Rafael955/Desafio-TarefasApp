using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Dtos.Responses
{
    public class ProjetoResponseDto
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public List<UsuarioResponseDto>? Usuarios { get; set; }

        public List<TarefaResponseDto>? Tarefas { get; set; }    
    }
}
