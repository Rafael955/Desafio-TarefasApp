using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Responses;

namespace TarefasApp.Domain.Interfaces.Services
{
    public interface IUsuariosDomainService
    {
        List<UsuariosTarefasResponseDto> UsuariosTarefasConcluidas_Ultimos30Dias(Guid? IdUsuario);

        UsuarioResponseDto? ObterUsuarioPorId(Guid? idUsuario);

        List<UsuarioResponseDto>? ListarUsuarios();
    }
}
