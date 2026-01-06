using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class AddedKunderAdresser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatuAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Land = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anvandarnamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Losenord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kunder_Adresser_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kunder_AdressId",
                table: "Kunder",
                column: "AdressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kunder");

            migrationBuilder.DropTable(
                name: "Adresser");
        }
    }
}
