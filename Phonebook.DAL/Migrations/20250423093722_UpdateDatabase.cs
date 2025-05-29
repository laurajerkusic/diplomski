using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_PhoneTypes_TypeId",
                table: "PhoneNumbers");

            migrationBuilder.DeleteData(
                table: "PhoneTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PhoneTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "PhoneNumbers",
                newName: "PhoneTypeId");

            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "PhoneNumbers",
                newName: "IsMain");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_TypeId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_PhoneTypeId");

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "PhoneTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "TypeName",
                value: "Mobile");

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "TypeName",
                value: "Home");

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "TypeName",
                value: "Work");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneTypes_TypeName",
                table: "PhoneTypes",
                column: "TypeName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_PhoneTypes_PhoneTypeId",
                table: "PhoneNumbers",
                column: "PhoneTypeId",
                principalTable: "PhoneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_PhoneTypes_PhoneTypeId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_PhoneTypes_TypeName",
                table: "PhoneTypes");

            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "PhoneTypes");

            migrationBuilder.RenameColumn(
                name: "PhoneTypeId",
                table: "PhoneNumbers",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "IsMain",
                table: "PhoneNumbers",
                newName: "IsDefault");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_PhoneTypeId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_TypeId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PhoneTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Mobile");

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Home");

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Work");

            migrationBuilder.InsertData(
                table: "PhoneTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Fax" });

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_PhoneTypes_TypeId",
                table: "PhoneNumbers",
                column: "TypeId",
                principalTable: "PhoneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
