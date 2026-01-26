using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class addedPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BetalningsSättId",
                table: "Ordrar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FraktId",
                table: "Ordrar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BetalningsSätt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetalningsSätt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frakter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pris = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frakter", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordrar_BetalningsSättId",
                table: "Ordrar",
                column: "BetalningsSättId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordrar_FraktId",
                table: "Ordrar",
                column: "FraktId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordrar_BetalningsSätt_BetalningsSättId",
                table: "Ordrar",
                column: "BetalningsSättId",
                principalTable: "BetalningsSätt",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordrar_Frakter_FraktId",
                table: "Ordrar",
                column: "FraktId",
                principalTable: "Frakter",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordrar_BetalningsSätt_BetalningsSättId",
                table: "Ordrar");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordrar_Frakter_FraktId",
                table: "Ordrar");

            migrationBuilder.DropTable(
                name: "BetalningsSätt");

            migrationBuilder.DropTable(
                name: "Frakter");

            migrationBuilder.DropIndex(
                name: "IX_Ordrar_BetalningsSättId",
                table: "Ordrar");

            migrationBuilder.DropIndex(
                name: "IX_Ordrar_FraktId",
                table: "Ordrar");

            migrationBuilder.DropColumn(
                name: "BetalningsSättId",
                table: "Ordrar");

            migrationBuilder.DropColumn(
                name: "FraktId",
                table: "Ordrar");
        }
    }
}
