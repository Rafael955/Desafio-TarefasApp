using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;

namespace TarefasApp.Domain.Interfaces.Services
{
    public interface IComentariosDomainService
    {
        ComentarioTarefaResponseDto AdicionarComentario(ComentarioTarefaRequestDto request);

        ComentarioTarefaRequestDto AlterarComentario(Guid? idComentario, ComentarioTarefaRequestDto request);

        ComentarioTarefaResponseDto RemoverComentario(Guid? idComentario);
    }
}
