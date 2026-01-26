using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class removedBetalningssätt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordrar_BetalningsSätt_BetalningsSättId",
                table: "Ordrar");

            migrationBuilder.DropTable(
                name: "BetalningsSätt");

            migrationBuilder.DropIndex(
                name: "IX_Ordrar_BetalningsSättId",
                table: "Ordrar");

            migrationBuilder.DropColumn(
                name: "BetalningsSättId",
                table: "Ordrar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BetalningsSättId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Ordrar_BetalningsSättId",
                table: "Ordrar",
                column: "BetalningsSättId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordrar_BetalningsSätt_BetalningsSättId",
                table: "Ordrar",
                column: "BetalningsSättId",
                principalTable: "BetalningsSätt",
                principalColumn: "Id");
        }
    }
}
