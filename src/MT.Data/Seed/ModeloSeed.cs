using Microsoft.EntityFrameworkCore;
using MT.Domain.Entities;
using System;

namespace MT.Data.Seed
{
    public static class ModeloSeed
    {
        public static void ModeloSeedBuilder(this ModelBuilder modelBuilder)
        {
            DateTime datacriacao = new DateTime(2019, 9, 11, 00, 00, 00, 000, DateTimeKind.Local);

            modelBuilder.Entity<Modelo>().HasData(
                new Modelo { Id = Guid.NewGuid(), CreateAt = datacriacao, Descricao = "FH" },
                new Modelo { Id = Guid.NewGuid(), CreateAt = datacriacao, Descricao = "FM" },
                new Modelo { Id = Guid.NewGuid(), CreateAt = datacriacao, Descricao = "FT" }
               );


        }
    }
}
