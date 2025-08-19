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
    public class ComentariosRepository : IComentariosRepository
    {
        public void Add(Comentario comentario)
        {
            using (var context = new DataContext())
            {
                context.Add(comentario);
                context.SaveChanges();
            }
        }

        public void Update(Comentario comentario)
        {
            using (var context = new DataContext())
            {
                context.Update(comentario);
                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new DataContext())
            {
                context.Remove(id);
                context.SaveChanges();
            }
        }
    }
}
