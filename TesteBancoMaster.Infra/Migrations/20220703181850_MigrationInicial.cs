using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteBancoMaster.Infra.Migrations
{
    public partial class MigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "viagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origem = table.Column<string>(type: "varchar(100)", nullable: false),
                    Destino = table.Column<string>(type: "varchar(100)", nullable: false),
                    ValorPassagem = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viagem", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "viagem",
                columns: new[] { "Id", "Destino", "Origem", "ValorPassagem" },
                values: new object[,]
                {
                    { 1, "BRC", "GRU", 10m },
                    { 2, "SCL", "BRC", 5m },
                    { 3, "CDG", "GRU", 75m },
                    { 4, "SCL", "GRU", 20m },
                    { 5, "ORL", "GRU", 56m },
                    { 6, "CDG", "ORL", 5m },
                    { 7, "ORL", "SCL", 20m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "viagem");
        }
    }
}
