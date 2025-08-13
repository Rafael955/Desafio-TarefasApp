using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Infra.Data.Mappings
{
    public class UsuarioProjetoMap : IEntityTypeConfiguration<UsuarioProjeto>
    {
        public void Configure(EntityTypeBuilder<UsuarioProjeto> builder)
        {
            builder.ToTable("USUARIO_PROJETO");

            builder.HasKey(up => new { up.IdProjeto, up.IdUsuario });

            builder.Property(up => up.IdUsuario)
                .HasColumnName("ID_USUARIO")
                .IsRequired();

            builder.Property(up => up.IdProjeto)
                .HasColumnName("ID_PROJETO")
                .IsRequired();

            builder.HasOne(up => up.Usuario)
                .WithMany(u => u.Projetos)
                .HasForeignKey(up => up.IdUsuario);

            builder.HasOne(up => up.Projeto)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(up => up.IdProjeto);

        }
    }
}
