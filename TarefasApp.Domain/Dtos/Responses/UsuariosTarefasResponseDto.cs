using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Dtos.Responses
{
    public class UsuariosTarefasResponseDto
    {
        public string? Usuario { get; set; }

        public int? Tarefas { get; set; }
    }
}
