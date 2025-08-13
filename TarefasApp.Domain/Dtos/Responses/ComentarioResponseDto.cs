namespace TarefasApp.Domain.Dtos.Responses
{
    public class ComentarioResponseDto
    {
        public required Guid Id { get; set; }

        public required string Texto { get; set; }
    }
}
