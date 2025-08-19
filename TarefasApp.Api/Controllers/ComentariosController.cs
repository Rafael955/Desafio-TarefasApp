using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController(IComentariosDomainService comentariosDomainService) : ControllerBase
    {
        [HttpPost("adicionar-comentario")]
        [ProducesResponseType(typeof(ComentarioTarefaRequestDto), StatusCodes.Status201Created)]
        public IActionResult Post(ComentarioTarefaRequestDto request)
        {
            try
            {
                var response = comentariosDomainService.AdicionarComentario(request);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
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

        [HttpPut("alterar-comentario/{id}")]
        [ProducesResponseType(typeof(ComentarioTarefaRequestDto), StatusCodes.Status200OK)]
        public IActionResult Update(Guid id, ComentarioTarefaRequestDto request)
        {
            try
            {
                var response = comentariosDomainService.AlterarComentario(id, request);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
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

        [HttpDelete("apagar-comentario/{id}")]
        [ProducesResponseType(typeof(ComentarioTarefaRequestDto), StatusCodes.Status200OK)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var response = comentariosDomainService.RemoverComentario(id);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
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
