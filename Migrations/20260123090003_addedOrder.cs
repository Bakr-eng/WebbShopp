using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class addedOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundId = table.Column<int>(type: "int", nullable: false),
                    Datom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPris = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Kunder_KundId",
                        column: x => x.KundId,
                        principalTable: "Kunder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProduktId = table.Column<int>(type: "int", nullable: false),
                    StorlekId = table.Column<int>(type: "int", nullable: false),
                    Antal = table.Column<int>(type: "int", nullable: false),
                    PrisVidKop = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderRad_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRad_Produkter_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRad_Storlekar_StorlekId",
                        column: x => x.StorlekId,
                        principalTable: "Storlekar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_KundId",
                table: "Order",
                column: "KundId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRad_OrderId",
                table: "OrderRad",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRad_ProduktId",
                table: "OrderRad",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRad_StorlekId",
                table: "OrderRad",
                column: "StorlekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRad");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
