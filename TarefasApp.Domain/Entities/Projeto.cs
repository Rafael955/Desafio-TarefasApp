using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Entities
{
    public class Projeto
    {
        public required Guid Id { get; set; }

        public required string Nome { get; set; }

        public string? Descricao { get; set; }

        #region Relacionamento

        public List<UsuarioProjeto>? UsuariosProjeto { get; set; } = new List<UsuarioProjeto>();

        public List<Tarefa>? Tarefas { get; set; } = new List<Tarefa>();

        #endregion
    }
}
