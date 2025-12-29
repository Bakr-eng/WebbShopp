using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class namn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Storlekar",
                table: "Storlekar",
                newName: "Namn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Namn",
                table: "Storlekar",
                newName: "Storlekar");
        }
    }
}
