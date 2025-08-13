using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;

namespace TarefasApp.Domain.Interfaces.Services
{
    public interface ITarefasDomainService
    {
        TarefaResponseDto CriarTarefa(TarefaRequestDto request);

        TarefaResponseDto AlterarTarefa(Guid? IdIdTarefa, TarefaRequestDto request);

        TarefaResponseDto ExcluirTarefa(Guid? IdTarefa);

        TarefaResponseDto ObterTarefaPorId(Guid? Id);

        List<TarefaResponseDto> ListarTarefas();
    }
}
