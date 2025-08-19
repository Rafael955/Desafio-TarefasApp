namespace TarefasApp.Domain.Dtos.Responses
{
    public class ComentarioTarefaResponseDto
    {
        public Guid? Id { get; set; }

        public string? Texto { get; set; }

        public TarefaResponseDto? Tarefa { get; set; }

        public UsuarioResponseDto? Usuario { get; set; }
    }
}
