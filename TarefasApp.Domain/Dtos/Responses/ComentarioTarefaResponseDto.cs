namespace TarefasApp.Domain.Dtos.Responses
{
    public class ComentarioTarefaResponseDto
    {
        public Guid? Id { get; set; }

        public string? Texto { get; set; }

        public Guid? IdTarefa { get; set; }

        public TarefaResponseDto? Tarefa { get; set; }

        public Guid? IdUsuario { get; set; }

        public UsuarioResponseDto? Usuario { get; set; }
    }
}
