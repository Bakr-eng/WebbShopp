using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop2.Migrations
{
    /// <inheritdoc />
    public partial class addedONDbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Kunder_KundId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRad_Order_OrderId",
                table: "OrderRad");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRad_Produkter_ProduktId",
                table: "OrderRad");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRad_Storlekar_StorlekId",
                table: "OrderRad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderRad",
                table: "OrderRad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "OrderRad",
                newName: "OrderRader");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Ordrar");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRad_StorlekId",
                table: "OrderRader",
                newName: "IX_OrderRader_StorlekId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRad_ProduktId",
                table: "OrderRader",
                newName: "IX_OrderRader_ProduktId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRad_OrderId",
                table: "OrderRader",
                newName: "IX_OrderRader_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_KundId",
                table: "Ordrar",
                newName: "IX_Ordrar_KundId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderRader",
                table: "OrderRader",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ordrar",
                table: "Ordrar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRader_Ordrar_OrderId",
                table: "OrderRader",
                column: "OrderId",
                principalTable: "Ordrar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRader_Produkter_ProduktId",
                table: "OrderRader",
                column: "ProduktId",
                principalTable: "Produkter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRader_Storlekar_StorlekId",
                table: "OrderRader",
                column: "StorlekId",
                principalTable: "Storlekar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordrar_Kunder_KundId",
                table: "Ordrar",
                column: "KundId",
                principalTable: "Kunder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRader_Ordrar_OrderId",
                table: "OrderRader");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRader_Produkter_ProduktId",
                table: "OrderRader");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRader_Storlekar_StorlekId",
                table: "OrderRader");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordrar_Kunder_KundId",
                table: "Ordrar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ordrar",
                table: "Ordrar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderRader",
                table: "OrderRader");

            migrationBuilder.RenameTable(
                name: "Ordrar",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderRader",
                newName: "OrderRad");

            migrationBuilder.RenameIndex(
                name: "IX_Ordrar_KundId",
                table: "Order",
                newName: "IX_Order_KundId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRader_StorlekId",
                table: "OrderRad",
                newName: "IX_OrderRad_StorlekId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRader_ProduktId",
                table: "OrderRad",
                newName: "IX_OrderRad_ProduktId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRader_OrderId",
                table: "OrderRad",
                newName: "IX_OrderRad_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderRad",
                table: "OrderRad",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Kunder_KundId",
                table: "Order",
                column: "KundId",
                principalTable: "Kunder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRad_Order_OrderId",
                table: "OrderRad",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRad_Produkter_ProduktId",
                table: "OrderRad",
                column: "ProduktId",
                principalTable: "Produkter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRad_Storlekar_StorlekId",
                table: "OrderRad",
                column: "StorlekId",
                principalTable: "Storlekar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
