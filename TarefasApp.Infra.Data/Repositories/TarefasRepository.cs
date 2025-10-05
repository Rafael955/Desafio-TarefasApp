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
    public class TarefasRepository(DataContext context) : ITarefasRepository
    {
        public void Add(Tarefa tarefa)
        {
            context.Add(tarefa);
            context.SaveChanges();
        }

        public void Update(Tarefa tarefa)
        {
            context.Update(tarefa);
            context.SaveChanges();
        }

        public void Delete(Tarefa tarefa)
        {
            context.Remove(tarefa);
            context.SaveChanges();
        }

        public List<Tarefa>? GetAll()
        {
            return context.Set<Tarefa>()
                .AsNoTracking()
                .ToList();
        }

        public Tarefa? GetById(Guid? Id)
        {
            var result = context.Set<Tarefa>()
                .Include(x => x.Projeto)
                .Include(x => x.Usuario)
                .Include(x => x.Comentarios)
                .SingleOrDefault(x => x.Id == Id);

            return result;
        }

    }
}
