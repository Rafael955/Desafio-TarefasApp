using Microsoft.EntityFrameworkCore;
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

        public Usuario? GetById(Guid? id)
        {
            using (var context = new DataContext())
            {
                return context.Set<Usuario>()
                    .Include(u => u.UsuarioProjetos) //Incluindo tabela intermediaria
                        .ThenInclude(u => u.Projeto) // Então pegando os projetos associados ao usuário
                    .Include(u => u.Tarefas)
                        .ThenInclude(u => u.Projeto) // Incluindo o projeto associado à tarefa
                    .Include(u => u.Comentarios)
                    .SingleOrDefault(u => u.Id == id);
            }
        }

        public List<Usuario>? GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Set<Usuario>()
                    .AsNoTracking()
                    .ToList();
            }
        }
    }
}
