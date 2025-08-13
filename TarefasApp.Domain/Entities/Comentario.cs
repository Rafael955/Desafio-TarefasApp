using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Entities
{
    public class Comentario
    {
        public Guid? Id { get; set; }

        public string? Texto { get; set; }

        #region Relacionamento

        public Guid? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        public Guid? IdTarefa { get; set; }
        public Tarefa? Tarefa { get; set; }

        #endregion
    }
}
