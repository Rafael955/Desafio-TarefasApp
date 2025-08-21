using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;

namespace TarefasApp.Domain.Interfaces.Services
{
    public interface IProjetosDomainService
    {
        ProjetoResponseDto CriarProjeto(ProjetoRequestDto request);

        ProjetoResponseDto AlterarProjeto(Guid? idProjeto, ProjetoRequestDto request);

        ProjetoResponseDto RemoverProjeto(Guid? idProjeto);

        ProjetoResponseDto? ObterProjetoPorId(Guid? idProjeto);

        List<ProjetoResponseDto>? ListarProjetos();

        ProjetoResponseDto AlocarUsuarioEmProjeto(Guid idProjeto, AlocarUsuarioEmProjetoRequestDto request);
    }
}
