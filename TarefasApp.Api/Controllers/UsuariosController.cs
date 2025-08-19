using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Services;
using TarefasApp.Domain.Services;

namespace TarefasApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController (IUsuariosDomainService usuariosDomainService) : ControllerBase
    {
        [HttpGet("obter-usuario/{idUsuario}")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        public IActionResult Get(Guid? idUsuario)
        {
            try
            {
                var response = usuariosDomainService.ObterUsuarioPorId(idUsuario);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    errorMessage = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    errorMessage = ex.Message,
                });
            }
        }
        
        [HttpGet("listar-usuarios")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            try
            {
                var response = usuariosDomainService.ListarUsuarios();

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    errorMessage = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    errorMessage = ex.Message,
                });
            }
        }
    }
}
