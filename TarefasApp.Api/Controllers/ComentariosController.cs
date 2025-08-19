using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApp.Domain.Dtos.Requests;

namespace TarefasApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        [HttpPost("criar-comentario")]
        [ProducesResponseType(typeof(ComentarioTarefaRequestDto), StatusCodes.Status201Created)]
        public IActionResult Post(ComentarioTarefaRequestDto request)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut]
        public IActionResult Update()
        {

        }

        [HttpDelete]
        public IActionResult Delete()
        {

        }
    }
}
