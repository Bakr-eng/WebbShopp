using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class neww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Varukorgar_ProduktId",
                table: "Varukorgar",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_Varukorgar_StorlekId",
                table: "Varukorgar",
                column: "StorlekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Varukorgar_Produkter_ProduktId",
                table: "Varukorgar",
                column: "ProduktId",
                principalTable: "Produkter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Varukorgar_Storlekar_StorlekId",
                table: "Varukorgar",
                column: "StorlekId",
                principalTable: "Storlekar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Varukorgar_Produkter_ProduktId",
                table: "Varukorgar");

            migrationBuilder.DropForeignKey(
                name: "FK_Varukorgar_Storlekar_StorlekId",
                table: "Varukorgar");

            migrationBuilder.DropIndex(
                name: "IX_Varukorgar_ProduktId",
                table: "Varukorgar");

            migrationBuilder.DropIndex(
                name: "IX_Varukorgar_StorlekId",
                table: "Varukorgar");
        }
    }
}
