using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class ComentariosDomainService : IComentariosDomainService
    {
        public ComentarioTarefaResponseDto AdicionarComentario(ComentarioTarefaRequestDto request)
        {
            throw new NotImplementedException();
        }

        public ComentarioTarefaRequestDto AlterarComentario(Guid? idComentario, ComentarioTarefaRequestDto request)
        {
            throw new NotImplementedException();
        }

        public ComentarioTarefaResponseDto RemoverComentario(Guid? idComentario)
        {
            throw new NotImplementedException();
        }
    }
}
