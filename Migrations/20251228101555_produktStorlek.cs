using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class produktStorlek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkter_Storlekar_StorlekId",
                table: "Produkter");

            migrationBuilder.DropIndex(
                name: "IX_Produkter_StorlekId",
                table: "Produkter");

            migrationBuilder.DropColumn(
                name: "StorlekId",
                table: "Produkter");

            migrationBuilder.CreateTable(
                name: "ProduktStorlek",
                columns: table => new
                {
                    ProdukterId = table.Column<int>(type: "int", nullable: false),
                    StorlekarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktStorlek", x => new { x.ProdukterId, x.StorlekarId });
                    table.ForeignKey(
                        name: "FK_ProduktStorlek_Produkter_ProdukterId",
                        column: x => x.ProdukterId,
                        principalTable: "Produkter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduktStorlek_Storlekar_StorlekarId",
                        column: x => x.StorlekarId,
                        principalTable: "Storlekar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktStorlek_StorlekarId",
                table: "ProduktStorlek",
                column: "StorlekarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktStorlek");

            migrationBuilder.AddColumn<int>(
                name: "StorlekId",
                table: "Produkter",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produkter_StorlekId",
                table: "Produkter",
                column: "StorlekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produkter_Storlekar_StorlekId",
                table: "Produkter",
                column: "StorlekId",
                principalTable: "Storlekar",
                principalColumn: "Id");
        }
    }
}
