using Microsoft.EntityFrameworkCore.Migrations;

namespace OOMAC.EF.Migrations
{
    public partial class RenameBracket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bracker_Tournament_BrcTrnId",
                table: "Bracker");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Bracker_MatBrcIdId",
                table: "Match");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bracker",
                table: "Bracker");

            migrationBuilder.RenameTable(
                name: "Bracker",
                newName: "Bracket");

            migrationBuilder.RenameIndex(
                name: "IX_Bracker_BrcTrnId",
                table: "Bracket",
                newName: "IX_Bracket_BrcTrnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bracket",
                table: "Bracket",
                column: "BrcId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bracket_Tournament_BrcTrnId",
                table: "Bracket",
                column: "BrcTrnId",
                principalTable: "Tournament",
                principalColumn: "TrnId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Bracket_MatBrcIdId",
                table: "Match",
                column: "MatBrcIdId",
                principalTable: "Bracket",
                principalColumn: "BrcId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bracket_Tournament_BrcTrnId",
                table: "Bracket");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Bracket_MatBrcIdId",
                table: "Match");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bracket",
                table: "Bracket");

            migrationBuilder.RenameTable(
                name: "Bracket",
                newName: "Bracker");

            migrationBuilder.RenameIndex(
                name: "IX_Bracket_BrcTrnId",
                table: "Bracker",
                newName: "IX_Bracker_BrcTrnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bracker",
                table: "Bracker",
                column: "BrcId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bracker_Tournament_BrcTrnId",
                table: "Bracker",
                column: "BrcTrnId",
                principalTable: "Tournament",
                principalColumn: "TrnId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Bracker_MatBrcIdId",
                table: "Match",
                column: "MatBrcIdId",
                principalTable: "Bracker",
                principalColumn: "BrcId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
