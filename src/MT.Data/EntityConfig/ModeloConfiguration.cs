using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MT.Domain.Entities;


namespace MT.Data.EntityConfig
{
    public class ModeloConfiguration : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            
            builder.Property(c => c.CreateAt)
                .HasColumnName("DataCadastro");

            builder.Property(c => c.UpdateAt)
               .HasColumnName("DataAtualizacao");



            builder.ToTable("Modelo");
        }
    }
    
}
