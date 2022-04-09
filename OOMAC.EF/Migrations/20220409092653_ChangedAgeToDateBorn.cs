using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OOMAC.EF.Migrations
{
    public partial class ChangedAgeToDateBorn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConAge",
                table: "Contestant");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConDateBorn",
                table: "Contestant",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConDateBorn",
                table: "Contestant");

            migrationBuilder.AddColumn<int>(
                name: "ConAge",
                table: "Contestant",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
