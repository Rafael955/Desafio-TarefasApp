using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Enums;

namespace TarefasApp.Domain.Entities
{
    public class Tarefa
    {
        public required Guid Id { get; set; }

        public required string Titulo { get; set; }

        public required string Descricao { get; set; }

        public required DateTime DataVencimento { get; set; }

        public required Status Status { get; set; }

        public required Prioridade Prioridade { get; set; }

        #region Relacionamentos

        public Guid? IdProjeto { get; set; }
        public Projeto? Projeto { get; set; }

        public Guid? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        public List<Comentario>? Comentarios { get; set; }

        #endregion
    }
}
