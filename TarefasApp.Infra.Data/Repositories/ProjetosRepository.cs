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
    public class ProjetosRepository : IProjetosRepository
    {
        public void Add(Projeto projeto)
        {
            using (var context = new DataContext())
            {
                context.Add(projeto);
                context.SaveChanges();
            }
        }

        public void Update(Projeto projeto)
        {
            using (var context = new DataContext())
            {
                context.Update(projeto);
                context.SaveChanges();
            }
        }

        public void Delete(Projeto projeto)
        {
            using (var context = new DataContext())
            {
                context.Remove(projeto);
                context.SaveChanges();
            }
        }

        public List<Projeto>? GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Set<Projeto>()
                    .Include(x => x.Tarefas)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Projeto? GetById(Guid? Id)
        {
            using (var context = new DataContext())
            {
                return context.Set<Projeto>()
                    .Include(x => x.Tarefas)
                    .SingleOrDefault(x => x.Id == Id);
            }
        }

        public Projeto? GetByName(string nome)
        {
            using (var context = new DataContext())
            {
                return context.Set<Projeto>()
                    .FirstOrDefault(x => x.Nome.Equals(nome));
            }
        }

        public bool VerifyLimitOfTasks(Guid? Id)
        {
            using (var context = new DataContext())
            {
                var result = context.Set<Projeto>()
                    .Include(x => x.Tarefas)
                    .SingleOrDefault(x => x.Id == Id);

                return result.Tarefas?.Count() == 20;
            }
        }
    }
}
