using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Interfaces.Services;
using TarefasApp.Domain.Services;

namespace TarefasApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController (IUsuariosDomainService usuariosDomainService) : ControllerBase
    {
        [HttpGet("obter-usuario/{id}")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        public IActionResult Get(Guid? idUsuario)
        {
            throw new NotImplementedException();
        }

        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}
