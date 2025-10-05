using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Infra.Data.Contexts;

namespace TarefasApp.Infra.Data.Repositories
{
    public class ComentariosRepository(DataContext context) : IComentariosRepository
    {
        public void Add(Comentario comentario)
        {
            context.Add(comentario);
            context.SaveChanges();
        }

        public void Update(Comentario comentario)
        {
            context.Update(comentario);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            context.Remove(id);
            context.SaveChanges();
        }

        public List<Comentario>? GetAll()
        {
            return context.Set<Comentario>()
                .AsNoTracking()
                .ToList();
        }

        public Comentario? GetById(Guid? Id)
        {
            return context.Set<Comentario>()
                .Include(c => c.Usuario)
                .Include(c => c.Tarefa)
                .SingleOrDefault(c => c.Id == Id);
        }
    }
}
