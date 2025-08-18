namespace TarefasApp.Domain.Dtos.Responses
{
    public class UsuarioResponseDto
    {
        public Guid Id { get; set; }

        public string NomeUsuario { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string NivelAcesso { get; set; }

        #region Relacionamento

        public List<ProjetoResponseDto>? Projetos { get; set; }

        public List<TarefaResponseDto>? Tarefas { get; set; }

        public List<ComentarioResponseDto>? Comentarios { get; set; }

        #endregion
    }
}
