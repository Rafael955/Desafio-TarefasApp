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
    public class RelatoriosRepository(DataContext context) : IRelatoriosRepository
    {
        public List<UsuariosTarefasResponseDto>? GetUsersTasksAverage_Last30Days()
        {
            return context.Set<Usuario>()
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
