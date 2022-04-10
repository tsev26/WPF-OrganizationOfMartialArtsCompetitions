using Microsoft.EntityFrameworkCore.Migrations;

namespace OOMAC.EF.Migrations
{
    public partial class UpdateForeignKeysInMatch2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Bracket_MatBrcIdId",
                table: "Match");

            migrationBuilder.RenameColumn(
                name: "MatBrcIdId",
                table: "Match",
                newName: "BracketId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_MatBrcIdId",
                table: "Match",
                newName: "IX_Match_BracketId");

            migrationBuilder.AlterColumn<int>(
                name: "BracketId",
                table: "Match",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Bracket_BracketId",
                table: "Match",
                column: "BracketId",
                principalTable: "Bracket",
                principalColumn: "BrcId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Bracket_BracketId",
                table: "Match");

            migrationBuilder.RenameColumn(
                name: "BracketId",
                table: "Match",
                newName: "MatBrcIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_BracketId",
                table: "Match",
                newName: "IX_Match_MatBrcIdId");

            migrationBuilder.AlterColumn<int>(
                name: "MatBrcIdId",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Bracket_MatBrcIdId",
                table: "Match",
                column: "MatBrcIdId",
                principalTable: "Bracket",
                principalColumn: "BrcId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
