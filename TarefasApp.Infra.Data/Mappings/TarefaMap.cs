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
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("TAREFAS");


            builder.HasKey(t => t.Id);


            builder.Property(t => t.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(t => t.Titulo)
                .HasColumnName("TITULO")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.DataVencimento)
                .HasColumnName("DATA_VENCIMENTO")
                .IsRequired();

            builder.Property(t => t.Status)
                .HasColumnName("STATUS")
                .IsRequired();

            builder.Property(t => t.Prioridade)
               .HasColumnName("PRIORIDADE")
               .IsRequired();

            builder.Property(t => t.IdProjeto)
               .HasColumnName("ID_PROJETO")
               .IsRequired();

            builder.Property(t => t.IdUsuario)
               .HasColumnName("ID_USUARIO");


            builder.HasOne(t => t.Usuario)
                .WithMany(u => u.Tarefas)
                .HasForeignKey(t => t.IdUsuario);

            builder.HasOne(t => t.Projeto)
                .WithMany(p => p.Tarefas)
                .HasForeignKey(t => t.IdProjeto);
        }
    }
}
