using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Enums;

namespace TarefasApp.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIOS");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(u => u.NomeUsuario)
                .HasColumnName("NOME_USUARIO")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Email)
               .HasColumnName("EMAIL")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(115)
                .IsRequired();

            builder.Property(u => u.NivelAcesso)
                .HasColumnName("NIVEL_ACESSO")
                .IsRequired();

            builder.HasData(
                new Usuario
                {
                    Id = Guid.Parse("C3610A1A-1FB1-44A6-A7A4-311D05C3C61D"),
                    NomeUsuario = "Admin",
                    Email = "admin@tarefasapp.com",
                    Senha = "e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7", //Admin@123
                    NivelAcesso = NivelAcesso.GERENTE
                },
                 new Usuario
                 {
                     Id = Guid.Parse("C0903EBD-9BB2-4308-BF25-54CAF722B754"),
                     NomeUsuario = "Colaborador",
                     Email = "colaborador@tarefasapp.com",
                     Senha = "a2cca92300eb94875c6e738c071eef0f2f7e492288789cb40fc8424eebcd876a", //Colab@123
                     NivelAcesso = NivelAcesso.COLABORADOR
                 }
            );
        }
    }
}
