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
        public required Guid? Id { get; set; }

        public required string Titulo { get; set; }

        public required string Descricao { get; set; }

        public required DateTime DataVencimento { get; set; }

        public required int Status { get; set; }

        public required int Prioridade { get; set; }

        public required dynamic Projeto { get; set; }

        public required dynamic Usuario { get; set; }

        public List<ComentarioResponseDto>? Comentarios { get; set; }
    }
}
