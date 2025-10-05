using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Enums;
using TarefasApp.Infra.Data.Contexts;

namespace TarefasApp.Tests.Helpers
{
    public static class TestDataHelper
    {
        // Método que irá popular o banco de dados
        public static void SeedData(DataContext context)
        {
            // Limpa os dados antes de popular
            context.Set<Usuario>().RemoveRange(context.Set<Usuario>());
            
            context.SaveChanges();

            if (!context.Set<Usuario>().Any(u => u.Id == Guid.Parse("C3610A1A-1FB1-44A6-A7A4-311D05C3C61D")))
            {
                // 1. Crie o Usuario GERENTE
                var usuario = new Usuario
                {
                    Id = Guid.Parse("C3610A1A-1FB1-44A6-A7A4-311D05C3C61D"),
                    NomeUsuario = "Admin",
                    Email = "admin@tarefasapp.com",
                    Senha = "e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7", //Admin@123
                    NivelAcesso = NivelAcesso.GERENTE
                };

                // 2. Adicione as entidades ao contexto
                context.Set<Usuario>().Add(usuario);

                // 3. Salve as mudanças
                context.SaveChanges();
            }

            if (!context.Set<Usuario>().Any(u => u.Id == Guid.Parse("C0903EBD-9BB2-4308-BF25-54CAF722B754")))
            {
                // 4. Crie o Usuario COLABORADOR
                var usuario = new Usuario
                {
                    Id = Guid.Parse("C0903EBD-9BB2-4308-BF25-54CAF722B754"),
                    NomeUsuario = "Colaborador",
                    Email = "colaborador@tarefasapp.com",
                    Senha = "a2cca92300eb94875c6e738c071eef0f2f7e492288789cb40fc8424eebcd876a", //Colab@123
                    NivelAcesso = NivelAcesso.COLABORADOR
                };

                // 5. Adicione as entidades ao contexto
                context.Set<Usuario>().Add(usuario);

                // 6. Salve as mudanças
                context.SaveChanges();
            }
        }
    }
}
