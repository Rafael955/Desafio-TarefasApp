using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Domain.Dtos.Requests
{
    public class ComentarioTarefaRequestDto
    {
        [Required(ErrorMessage = "Por favor, informe um texto para o comentário!")]
        [MinLength(15, ErrorMessage = "Por favor, informe um texto para o comentário com um mínimo de {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe um texto para o comentário com no máximo {1} caracteres.")]
        public string? Texto { get; set; }

        [Required(ErrorMessage = "Por favor, informe uma tarefa associada ao comentário!")]
        public Guid? IdTarefa { get; set; }

        [Required(ErrorMessage = "Por favor, informe um usuário associado ao comentário!")]
        public Guid? IdUsuario { get; set; }
    }
}
