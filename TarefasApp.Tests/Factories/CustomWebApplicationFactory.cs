using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TarefasApp.Domain.Entities; // Importe o namespace das suas entidades de domínio
using TarefasApp.Infra.Data.Contexts;
using TarefasApp.Tests.Helpers; // Importe o namespace do seu DbContext

namespace TarefasApp.Tests.Factories
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Remover o DbContext atual para usar um novo
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Adicionar o DbContext InMemory para o teste
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Obter o service provider e o DbContext
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<DataContext>();

                    // Garantir que o banco de dados seja criado (e limpo) a cada teste
                    dbContext.Database.EnsureCreated();

                    // Chamar o método para popular o banco de dados
                    TestDataHelper.SeedData(dbContext);
                }
            });
        }
    }
}