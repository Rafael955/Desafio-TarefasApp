using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class UsuariosDomainService(IUsuariosRepository usuariosRepository) : IUsuariosDomainService
    {
        public UsuarioResponseDto? ObterUsuarioPorId(Guid? idUsuario)
        {
            var usuario = usuariosRepository.GetById(idUsuario);

            if (usuario == null)
                throw new ApplicationException("Usuario não foi encontrado.");

            return ToResponse(usuario);
        }

        public List<UsuarioResponseDto>? ListarUsuarios()
        {
            var usuarios = usuariosRepository.GetAll();

            if (usuarios == null)
                throw new ApplicationException("Nenhum usuario foi encontrado.");

            List<UsuarioResponseDto>? _usuarios = new List<UsuarioResponseDto>();

            foreach (var usuario in usuarios)
            {
                _usuarios.Add(ToResponse(usuario));
            }

            return _usuarios;
        }

        private UsuarioResponseDto ToResponse(Usuario usuario)
        {
            return new UsuarioResponseDto
            {

            };
        }
    }
}
