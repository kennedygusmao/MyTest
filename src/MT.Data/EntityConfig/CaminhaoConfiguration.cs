using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MT.Data.EntityConfig
{
    public class CaminhaoConfiguration : IEntityTypeConfiguration<Caminhao>
    {
        public void Configure(EntityTypeBuilder<Caminhao> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.AnoFabricacao)
                .IsRequired();
                

            builder.Property(c => c.AnoModelo)
                .IsRequired();


            builder.Property(c => c.CreateAt)
                .HasColumnName("DataCadastro");

            builder.Property(c => c.UpdateAt)
               .HasColumnName("DataAtualizacao");



            builder.ToTable("Caminhao");
        }
    }
}