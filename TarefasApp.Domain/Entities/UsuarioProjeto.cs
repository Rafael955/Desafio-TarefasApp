using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Entities
{
    public class UsuarioProjeto
    {
        public Guid? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        public Guid? IdProjeto { get; set; }
        public Projeto? Projeto { get; set; }
    }
}
