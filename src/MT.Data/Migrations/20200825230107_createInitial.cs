using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.Data.Migrations
{
    public partial class createInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modelo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: true),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caminhao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: true),
                    AnoFabricacao = table.Column<int>(nullable: false),
                    AnoModelo = table.Column<int>(nullable: false),
                    ModeloId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caminhao_Modelo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Modelo",
                columns: new[] { "Id", "DataCadastro", "Descricao", "DataAtualizacao" },
                values: new object[] { new Guid("9ef87e24-e1bd-4002-ab6e-3927c8a47581"), new DateTime(2019, 9, 11, 0, 0, 0, 0, DateTimeKind.Local), "FH", null });

            migrationBuilder.InsertData(
                table: "Modelo",
                columns: new[] { "Id", "DataCadastro", "Descricao", "DataAtualizacao" },
                values: new object[] { new Guid("88b91d5e-b4b4-47ba-bf62-9e1e9fec3cf4"), new DateTime(2019, 9, 11, 0, 0, 0, 0, DateTimeKind.Local), "FM", null });

            migrationBuilder.InsertData(
                table: "Modelo",
                columns: new[] { "Id", "DataCadastro", "Descricao", "DataAtualizacao" },
                values: new object[] { new Guid("1170b743-a584-470a-bca5-af33138ecf13"), new DateTime(2019, 9, 11, 0, 0, 0, 0, DateTimeKind.Local), "FT", null });

            migrationBuilder.CreateIndex(
                name: "IX_Caminhao_ModeloId",
                table: "Caminhao",
                column: "ModeloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caminhao");

            migrationBuilder.DropTable(
                name: "Modelo");
        }
    }
}
