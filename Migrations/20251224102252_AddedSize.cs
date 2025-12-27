using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class AddedSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorlekId",
                table: "Produkter",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Storlekar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storlekar", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkter_Storlekar_StorlekId",
                table: "Produkter");

            migrationBuilder.DropTable(
                name: "Storlekar");

            migrationBuilder.DropIndex(
                name: "IX_Produkter_StorlekId",
                table: "Produkter");

            migrationBuilder.DropColumn(
                name: "StorlekId",
                table: "Produkter");
        }
    }
}
