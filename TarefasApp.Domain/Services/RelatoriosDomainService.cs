using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class RelatoriosDomainService(IUsuariosRepository usuariosRepository, IRelatoriosRepository relatoriosRepository) : IRelatoriosDomainService
    {
        public List<UsuariosTarefasResponseDto>? UsuariosTarefasConcluidas_Ultimos30Dias(Guid? IdUsuario)
        {
            #region Regra de Negócio: Os relatórios devem ser acessíveis apenas por usuários com uma função específica de "gerente".

            if (!usuariosRepository.IsRoleManager(IdUsuario))
                throw new ApplicationException("Apenas usuários 'GERENTES' podem acessar relatórios.");

            #endregion

            return relatoriosRepository.GetUsersTasksAverage_Last30Days();
        }

    }
}
