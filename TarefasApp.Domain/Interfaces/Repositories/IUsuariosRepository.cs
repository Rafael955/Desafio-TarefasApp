using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Domain.Interfaces.Repositories
{
    public interface IUsuariosRepository
    {
        bool IsRoleManager(Guid? IdUsuario);

        List<UsuariosTarefasResponseDto>? GetUsersTasksAverage_Last30Days();

        Usuario? GetById(Guid? id);

        List<Usuario>? GetAll();
    }
}
