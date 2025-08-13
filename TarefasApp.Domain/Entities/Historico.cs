using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Entities
{
    public class Historico
    {
        public required Guid Id { get; set; }

        public required string Descricao { get; set; }

        public required DateTime DataHoraModificacao { get; set; }

        #region Relacionamentos

        public Guid? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        #endregion
    }
}
