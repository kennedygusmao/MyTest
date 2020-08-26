﻿// <auto-generated />
using System;
using MT.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MT.Data.Migrations
{
    [DbContext(typeof(BContext))]
    [Migration("20200825230107_createInitial")]
    partial class createInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MT.Domain.Entities.Caminhao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnoFabricacao");

                    b.Property<int>("AnoModelo");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnName("DataCadastro");

                    b.Property<Guid>("ModeloId");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnName("DataAtualizacao");

                    b.HasKey("Id");

                    b.HasIndex("ModeloId");

                    b.ToTable("Caminhao");
                });

            modelBuilder.Entity("MT.Domain.Entities.Modelo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnName("DataCadastro");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnName("DataAtualizacao");

                    b.HasKey("Id");

                    b.ToTable("Modelo");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9ef87e24-e1bd-4002-ab6e-3927c8a47581"),
                            CreateAt = new DateTime(2019, 9, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            Descricao = "FH"
                        },
                        new
                        {
                            Id = new Guid("88b91d5e-b4b4-47ba-bf62-9e1e9fec3cf4"),
                            CreateAt = new DateTime(2019, 9, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            Descricao = "FM"
                        },
                        new
                        {
                            Id = new Guid("1170b743-a584-470a-bca5-af33138ecf13"),
                            CreateAt = new DateTime(2019, 9, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            Descricao = "FT"
                        });
                });

            modelBuilder.Entity("MT.Domain.Entities.Caminhao", b =>
                {
                    b.HasOne("MT.Domain.Entities.Modelo", "Modelo")
                        .WithMany()
                        .HasForeignKey("ModeloId");
                });
#pragma warning restore 612, 618
        }
    }
}
