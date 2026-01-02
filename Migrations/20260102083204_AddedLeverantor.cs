using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class AddedLeverantor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeverantorId",
                table: "Produkter",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Leverantorer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leverantorer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produkter_LeverantorId",
                table: "Produkter",
                column: "LeverantorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produkter_Leverantorer_LeverantorId",
                table: "Produkter",
                column: "LeverantorId",
                principalTable: "Leverantorer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkter_Leverantorer_LeverantorId",
                table: "Produkter");

            migrationBuilder.DropTable(
                name: "Leverantorer");

            migrationBuilder.DropIndex(
                name: "IX_Produkter_LeverantorId",
                table: "Produkter");

            migrationBuilder.DropColumn(
                name: "LeverantorId",
                table: "Produkter");
        }
    }
}
