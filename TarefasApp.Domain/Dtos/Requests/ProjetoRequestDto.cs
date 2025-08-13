using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Dtos.Requests
{
    public class ProjetoRequestDto
    {
        [Required(ErrorMessage = "Por favor, informe um nome para o projeto!")]
        [MinLength(10, ErrorMessage = "O nome do projeto deve ter no mínimo {1} caracteres")]
        [MaxLength(120, ErrorMessage = "O nome do projeto deve ter no máximo {1} caracteres")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe uma descrição para o projeto!")]
        [MinLength(10, ErrorMessage = "A descrição do projeto deve ter no mínimo {1} caracteres")]
        [MaxLength(255, ErrorMessage = "A descrição do projeto deve ter no máximo {1} caracteres")]
        public string? Descricao { get; set; }
    }
}
