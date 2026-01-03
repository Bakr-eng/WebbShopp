using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class ClassForProduktStorlek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktStorlek");

            migrationBuilder.DropColumn(
                name: "EnheterILager",
                table: "Produkter");

            migrationBuilder.CreateTable(
                name: "ProduktStorlekar",
                columns: table => new
                {
                    ProduktId = table.Column<int>(type: "int", nullable: false),
                    StorlekId = table.Column<int>(type: "int", nullable: false),
                    EnheterIlager = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktStorlekar", x => new { x.ProduktId, x.StorlekId });
                    table.ForeignKey(
                        name: "FK_ProduktStorlekar_Produkter_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduktStorlekar_Storlekar_StorlekId",
                        column: x => x.StorlekId,
                        principalTable: "Storlekar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktStorlekar_StorlekId",
                table: "ProduktStorlekar",
                column: "StorlekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktStorlekar");

            migrationBuilder.AddColumn<int>(
                name: "EnheterILager",
                table: "Produkter",
                type: "int",
                nullable: true);

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
    }
}
