using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Enums;

namespace TarefasApp.Domain.Dtos.Requests
{
    public class TarefaRequestDto
    {
        [MinLength(10, ErrorMessage = "O título da tarefa deve ter no mínimo {1} caracteres")]
        [Required(ErrorMessage = "Por favor, informe o titulo da tarefa!")]
        public required string Titulo { get; set; }

        [MinLength(3, ErrorMessage = "A descrição da tarefa deve ter no mínimo {1} caracteres")]
        [Required(ErrorMessage = "Por favor, informe a descrição da tarefa!")]
        public required string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de vencimento da tarefa!")]
        public required DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "Por favor, informe o status da tarefa!")]
        [Range(1, 3, ErrorMessage = "O status deve ser (1)Pendente, (2)Em Andamento ou (3)Concluida")]
        public required int Status { get; set; }

        [Required(ErrorMessage = "Por favor, informe uma prioridade da tarefa!")]
        [Range(1,3, ErrorMessage = "A prioridade deve ser (1)Baixa, (2)Média ou (3)Alta")]
        public required int Prioridade { get; set; }

        [Required(ErrorMessage = "Por favor, informe um projeto associado a tarefa!")]
        public required Guid IdProjeto { get; set; }

        [Required(ErrorMessage = "Por favor, informe um usuario associado a tarefa!")]
        public required Guid? IdUsuario { get; set; }
    }
}
