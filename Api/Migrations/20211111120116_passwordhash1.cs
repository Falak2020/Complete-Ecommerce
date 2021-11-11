using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class passwordhash1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "PasswordHashes",
                newName: "PasswordSalt");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "PasswordHashes",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "PasswordHashes",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "PasswordHashes");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "PasswordHashes",
                newName: "Salt");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "PasswordHashes",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");
        }
    }
}
