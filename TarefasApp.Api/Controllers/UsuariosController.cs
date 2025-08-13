using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApp.Domain.Interfaces.Services;
using TarefasApp.Domain.Services;

namespace TarefasApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController (IUsuariosDomainService usuariosDomainService) : ControllerBase
    {
        [HttpGet("relatorio-usuarios-tarefas/{idUsuario}")]
        public IActionResult GetUsersAverageTasks_Last30Days(Guid? idUsuario)
        {
            try
            {
                var result = usuariosDomainService.UsuariosTarefasConcluidas_Ultimos30Dias(idUsuario);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }
    }
}
