using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class orderUserAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InvoiceAddressId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryAddressId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InvoiceAddressId",
                table: "Orders",
                column: "InvoiceAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAddresses_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAddresses_InvoiceAddressId",
                table: "Orders",
                column: "InvoiceAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserAddresses_DeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserAddresses_InvoiceAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_InvoiceAddressId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
