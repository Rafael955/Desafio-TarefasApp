using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Enums;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Infra.Data.Contexts;

namespace TarefasApp.Infra.Data.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        public bool IsRoleManager(Guid? IdUsuario)
        {
            using (var context = new DataContext())
            {
                var usuarioSelecionado = context.Set<Usuario>()
                    .SingleOrDefault(x => x.Id == IdUsuario);

                return usuarioSelecionado?.NivelAcesso == NivelAcesso.GERENTE ? true : false;
            }
        }

        public List<UsuariosTarefasResponseDto>? GetUsersTasksAverage_Last30Days()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Usuario>()
                    .Include(u => u.Tarefas)
                    .GroupBy(u => u.NomeUsuario)
                    .Select(g => new UsuariosTarefasResponseDto
                    {
                        Usuario = g.Key,
                        Tarefas = Convert.ToInt32(g.Average(p => p.Tarefas.Count(x => x.Status == Status.CONCLUIDA)))
                    })
                    .ToList();
            }
        }
    }
}
