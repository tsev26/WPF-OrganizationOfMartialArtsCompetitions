using Microsoft.EntityFrameworkCore.Migrations;

namespace OOMAC.EF.Migrations
{
    public partial class UpdatesToMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatScoreBId",
                table: "Match",
                newName: "MatScoreB");

            migrationBuilder.RenameColumn(
                name: "MatScoreAId",
                table: "Match",
                newName: "MatScoreA");

            migrationBuilder.AddColumn<int>(
                name: "MatScoreAInt",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatScoreAInt",
                table: "Match");

            migrationBuilder.RenameColumn(
                name: "MatScoreB",
                table: "Match",
                newName: "MatScoreBId");

            migrationBuilder.RenameColumn(
                name: "MatScoreA",
                table: "Match",
                newName: "MatScoreAId");
        }
    }
}
