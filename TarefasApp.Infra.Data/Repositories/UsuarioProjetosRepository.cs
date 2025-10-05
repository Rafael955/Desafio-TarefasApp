using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Infra.Data.Contexts;

namespace TarefasApp.Infra.Data.Repositories
{
    public class UsuarioProjetosRepository(DataContext context) : IUsuarioProjetosRepository
    {
        public void AddUserToProject(UsuarioProjeto usuarioProjeto)
        {
            context.Add(usuarioProjeto);
            context.SaveChanges();
        }
    }
}
