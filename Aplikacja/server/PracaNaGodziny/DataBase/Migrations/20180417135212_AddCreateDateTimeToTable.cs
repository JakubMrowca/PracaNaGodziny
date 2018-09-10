using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataBase.Migrations
{
    public partial class AddCreateDateTimeToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Employers_EmplyerId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Clients_ClientId1",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Clients_EmplyerId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EmplyerId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "ClientId1",
                table: "Locations",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ClientId1",
                table: "Locations",
                newName: "IX_Locations_ClientId");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Workers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Locations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Employers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Clients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployerId",
                table: "Clients",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_EmployerId",
                table: "Clients",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Employers_EmployerId",
                table: "Clients",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Clients_ClientId",
                table: "Locations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Employers_EmployerId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Clients_ClientId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Clients_EmployerId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Locations",
                newName: "ClientId1");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ClientId",
                table: "Locations",
                newName: "IX_Locations_ClientId1");

            migrationBuilder.AddColumn<Guid>(
                name: "EmplyerId",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_EmplyerId",
                table: "Clients",
                column: "EmplyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Employers_EmplyerId",
                table: "Clients",
                column: "EmplyerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Clients_ClientId1",
                table: "Locations",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
