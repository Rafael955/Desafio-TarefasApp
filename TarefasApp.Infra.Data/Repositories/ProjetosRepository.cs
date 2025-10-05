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
    public class ProjetosRepository(DataContext context) : IProjetosRepository
    {
        public void Add(Projeto projeto)
        {
            context.Add(projeto);
            context.SaveChanges();
        }

        public void Update(Projeto projeto)
        {
            context.Update(projeto);
            context.SaveChanges();
        }

        public void Delete(Projeto projeto)
        {
            context.Remove(projeto);
            context.SaveChanges();
        }

        public List<Projeto>? GetAll()
        {
            return context.Set<Projeto>()
                .AsNoTracking()
                .ToList();
        }

        public Projeto? GetById(Guid? Id)
        {
            return context.Set<Projeto>()
                .Include(x => x.UsuariosProjeto)
                   .ThenInclude(x => x.Usuario)
                .Include(x => x.Tarefas)
                    .ThenInclude(x => x.Usuario)
                .SingleOrDefault(x => x.Id == Id);
        }

        public Projeto? GetByName(string nome)
        {
            return context.Set<Projeto>()
                .Include(x => x.UsuariosProjeto)
                   .ThenInclude(x => x.Usuario)
                .Include(x => x.Tarefas)
                    .ThenInclude(x => x.Usuario)
                .FirstOrDefault(x => x.Nome.Equals(nome));
        }

        public bool VerifyLimitOfTasks(Guid? Id)
        {
            var result = context.Set<Projeto>()
                .Include(x => x.Tarefas)
                .SingleOrDefault(x => x.Id == Id);

            return result == null ? false : result.Tarefas?.Count() == 20;
        }
    }
}
