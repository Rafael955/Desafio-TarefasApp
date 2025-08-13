using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Interfaces.Services;
using TarefasApp.Domain.Services;

namespace TarefasApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController (IProjetosDomainService projetosDomainService) : ControllerBase
    {
        [HttpPost("criar-projeto")]
        [ProducesResponseType(typeof(ProjetoResponseDto), StatusCodes.Status201Created)]
        public IActionResult Post(ProjetoRequestDto request)
        {
            try
            {
                var result = projetosDomainService.CriarProjeto(request);
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

        [HttpPut("alterar-projeto/{Id}")]
        [ProducesResponseType(typeof(ProjetoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Update(Guid Id, ProjetoRequestDto request)
        {
            try
            {
                var result = projetosDomainService.AlterarProjeto(Id, request);
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

        [HttpDelete("remover-projeto/{Id}")]
        [ProducesResponseType(typeof(ProjetoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                var result = projetosDomainService.RemoverProjeto(Id);
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

        [HttpGet("obter-projeto/{Id}")]
        [ProducesResponseType(typeof(ProjetoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Get(Guid Id)
        {
            try
            {
                var result = projetosDomainService.ObterProjetoPorId(Id);
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
    
        [HttpGet("listar-projetos")]
        [ProducesResponseType(typeof(ProjetoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            try
            {
                var result = projetosDomainService.ListarProjetos();
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
