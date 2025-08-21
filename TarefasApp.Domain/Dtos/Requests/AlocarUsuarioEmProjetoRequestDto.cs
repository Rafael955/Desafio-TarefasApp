using System.ComponentModel.DataAnnotations;

namespace TarefasApp.Domain.Dtos.Requests
{
    public class AlocarUsuarioEmProjetoRequestDto
    {
        [Required(ErrorMessage = "Por favor, informe o Id do usuário a ser alocado no projeto!")]
        public required Guid IdUsuario { get; set; }
    }
}
