using Microsoft.EntityFrameworkCore.Migrations;

namespace OOMAC.EF.Migrations
{
    public partial class UpdateForeignKeysInMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatConIdAId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "MatConIdBId",
                table: "Match");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatConIdAId",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatConIdBId",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
