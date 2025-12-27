using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class newproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produkter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pris = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KategoriId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produkter_Kategorier_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategorier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produkter_KategoriId",
                table: "Produkter",
                column: "KategoriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produkter");
        }
    }
}
