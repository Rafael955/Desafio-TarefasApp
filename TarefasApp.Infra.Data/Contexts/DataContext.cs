using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.Data.Mappings;

namespace TarefasApp.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "BDTarefasApp");
            optionsBuilder.UseSqlServer("Data Source=localhost, 1435;Initial Catalog=master;User ID=sa;Password=Desafio@2025;Encrypt=False");
            //optionsBuilder.UseSqlServer("Data Source=sqlserver,1433;Initial Catalog=master;User ID=sa;Password=Desafio@2025;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ProjetoMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
            modelBuilder.ApplyConfiguration(new ComentarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioProjetoMap());
        }
    }
}
