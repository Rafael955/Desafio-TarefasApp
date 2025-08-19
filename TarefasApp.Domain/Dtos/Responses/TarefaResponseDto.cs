using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Dtos.Responses
{
    public class TarefaResponseDto
    {
        public Guid? Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public DateTime DataVencimento { get; set; }

        public dynamic? Status { get; set; }

        public dynamic? Prioridade { get; set; }

        public Guid? IdProjeto { get; set; }

        public ProjetoResponseDto? Projeto { get; set; }

        public Guid? IdUsuario { get; set; }

        public UsuarioResponseDto? Usuario { get; set; }

        public List<ComentarioTarefaResponseDto>? Comentarios { get; set; }
    }
}
