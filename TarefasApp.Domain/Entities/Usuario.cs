using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Enums;

namespace TarefasApp.Domain.Entities
{
    public class Usuario
    {
        public required Guid Id { get; set; }

        public required string NomeUsuario { get; set; }

        public required string Email { get; set; }

        public required string Senha { get; set; }

        public required NivelAcesso NivelAcesso { get; set; }

        #region Relacionamento

        public List<UsuarioProjeto>? UsuarioProjetos { get; set; } = new List<UsuarioProjeto>();

        public List<Tarefa>? Tarefas { get; set; } = new List<Tarefa>();

        public List<Comentario>? Comentarios { get; set; } = new List<Comentario>();

        #endregion
    }
}
