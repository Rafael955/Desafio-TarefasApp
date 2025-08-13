using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController (ITarefasDomainService tarefasDomainService) : ControllerBase
    {
        [HttpPost("criar-tarefa")]
        [ProducesResponseType(typeof(TarefaResponseDto), StatusCodes.Status201Created)]
        public IActionResult Post(TarefaRequestDto request)
        {
            try
            {
                var result = tarefasDomainService.CriarTarefa(request);
                return StatusCode(StatusCodes.Status201Created, result);
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

        [HttpPut("atualizar-tarefa/{id}")]
        [ProducesResponseType(typeof(TarefaResponseDto), StatusCodes.Status200OK)]
        public IActionResult Put(Guid? id, TarefaRequestDto request)
        {
            try
            {
                var result = tarefasDomainService.AlterarTarefa(id, request);
                return StatusCode(StatusCodes.Status200OK, result);
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

        [HttpDelete("remover-tarefa/{id}")]
        [ProducesResponseType(typeof(TarefaResponseDto), StatusCodes.Status200OK)]
        public IActionResult Delete(Guid? id)
        {
            try
            {
                var result = tarefasDomainService.ExcluirTarefa(id);
                return StatusCode(StatusCodes.Status200OK, result);
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

        [HttpGet("listar-tarefas")]
        [ProducesResponseType(typeof(TarefaResponseDto), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            try
            {
                var result = tarefasDomainService.ListarTarefas();
                return StatusCode(StatusCodes.Status200OK, result);
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

        [HttpGet("obter-tarefa/{id}")]
        [ProducesResponseType(typeof(TarefaResponseDto), StatusCodes.Status200OK)]
        public IActionResult Get(Guid? id)
        {
            try
            {
                var result = tarefasDomainService.ObterTarefaPorId(id);
                return StatusCode(StatusCodes.Status200OK, result);
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
