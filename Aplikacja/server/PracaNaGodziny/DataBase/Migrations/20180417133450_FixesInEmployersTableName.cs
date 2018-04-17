using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataBase.Migrations
{
    public partial class FixesInEmployersTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Emplyers_EmplyerId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Emplyers");

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Arch = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LocationCount = table.Column<int>(nullable: false),
                    ModDateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    WorkerCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Employers_EmplyerId",
                table: "Clients",
                column: "EmplyerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Employers_EmplyerId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.CreateTable(
                name: "Emplyers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Arch = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LocationCount = table.Column<int>(nullable: false),
                    ModDateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    WorkerCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplyers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Emplyers_EmplyerId",
                table: "Clients",
                column: "EmplyerId",
                principalTable: "Emplyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
