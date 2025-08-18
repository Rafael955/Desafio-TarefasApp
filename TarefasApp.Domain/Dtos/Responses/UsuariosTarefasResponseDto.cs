using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Enums;

namespace TarefasApp.Domain.Dtos.Responses
{
    public class UsuariosTarefasResponseDto
    {
        public string? Usuario { get; set; }

        public int? Tarefas { get; set; }
    }
}
