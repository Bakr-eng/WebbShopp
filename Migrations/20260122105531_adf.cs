using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class adf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Varukorgar_KundId",
                table: "Varukorgar",
                column: "KundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Varukorgar_Kunder_KundId",
                table: "Varukorgar",
                column: "KundId",
                principalTable: "Kunder",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Varukorgar_Kunder_KundId",
                table: "Varukorgar");

            migrationBuilder.DropIndex(
                name: "IX_Varukorgar_KundId",
                table: "Varukorgar");
        }
    }
}
