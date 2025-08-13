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
    public class ComentarioMap : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("COMENTARIOS");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(c => c.Texto)
                .HasColumnName("TEXTO")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(t => t.IdUsuario)
               .HasColumnName("ID_USUARIO")
               .IsRequired();

            builder.Property(t => t.IdTarefa)
               .HasColumnName("ID_TAREFA")
               .IsRequired();

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.IdUsuario);

            builder.HasOne(c => c.Tarefa)
               .WithMany(u => u.Comentarios)
               .HasForeignKey(c => c.IdTarefa);
        }
    }
}
