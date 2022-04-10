using Microsoft.EntityFrameworkCore.Migrations;

namespace OOMAC.EF.Migrations
{
    public partial class PoolsMergeIntoBrackets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Bracker_BracketsId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Contestant_MatWinId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Pool_PoolsId",
                table: "Match");

            migrationBuilder.DropTable(
                name: "MatchupEntry");

            migrationBuilder.DropTable(
                name: "Pool");

            migrationBuilder.DropIndex(
                name: "IX_Match_MatWinId",
                table: "Match");

            migrationBuilder.RenameColumn(
                name: "PoolsId",
                table: "Match",
                newName: "ContestantBId");

            migrationBuilder.RenameColumn(
                name: "MatWinId",
                table: "Match",
                newName: "MatConIdBId");

            migrationBuilder.RenameColumn(
                name: "BracketsId",
                table: "Match",
                newName: "ContestantAId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_PoolsId",
                table: "Match",
                newName: "IX_Match_ContestantBId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_BracketsId",
                table: "Match",
                newName: "IX_Match_ContestantAId");

            migrationBuilder.RenameColumn(
                name: "Round",
                table: "Bracker",
                newName: "BrcRound");

            migrationBuilder.AddColumn<int>(
                name: "MatBrcIdId",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatConIdAId",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MatScoreAId",
                table: "Match",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatScoreBId",
                table: "Match",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrcType",
                table: "Bracker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatBrcIdId",
                table: "Match",
                column: "MatBrcIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Bracker_MatBrcIdId",
                table: "Match",
                column: "MatBrcIdId",
                principalTable: "Bracker",
                principalColumn: "BrcId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Contestant_ContestantAId",
                table: "Match",
                column: "ContestantAId",
                principalTable: "Contestant",
                principalColumn: "ConId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Contestant_ContestantBId",
                table: "Match",
                column: "ContestantBId",
                principalTable: "Contestant",
                principalColumn: "ConId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Bracker_MatBrcIdId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Contestant_ContestantAId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Contestant_ContestantBId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_MatBrcIdId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "MatBrcIdId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "MatConIdAId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "MatScoreAId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "MatScoreBId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "BrcType",
                table: "Bracker");

            migrationBuilder.RenameColumn(
                name: "MatConIdBId",
                table: "Match",
                newName: "MatWinId");

            migrationBuilder.RenameColumn(
                name: "ContestantBId",
                table: "Match",
                newName: "PoolsId");

            migrationBuilder.RenameColumn(
                name: "ContestantAId",
                table: "Match",
                newName: "BracketsId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_ContestantBId",
                table: "Match",
                newName: "IX_Match_PoolsId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_ContestantAId",
                table: "Match",
                newName: "IX_Match_BracketsId");

            migrationBuilder.RenameColumn(
                name: "BrcRound",
                table: "Bracker",
                newName: "Round");

            migrationBuilder.CreateTable(
                name: "MatchupEntry",
                columns: table => new
                {
                    MaeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaeConId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: true),
                    MaeScore = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchupEntry", x => x.MaeId);
                    table.ForeignKey(
                        name: "FK_MatchupEntry_Contestant_MaeConId",
                        column: x => x.MaeConId,
                        principalTable: "Contestant",
                        principalColumn: "ConId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchupEntry_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pool",
                columns: table => new
                {
                    PolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolTrnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pool", x => x.PolId);
                    table.ForeignKey(
                        name: "FK_Pool_Tournament_PolTrnId",
                        column: x => x.PolTrnId,
                        principalTable: "Tournament",
                        principalColumn: "TrnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatWinId",
                table: "Match",
                column: "MatWinId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchupEntry_MaeConId",
                table: "MatchupEntry",
                column: "MaeConId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchupEntry_MatchId",
                table: "MatchupEntry",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Pool_PolTrnId",
                table: "Pool",
                column: "PolTrnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Bracker_BracketsId",
                table: "Match",
                column: "BracketsId",
                principalTable: "Bracker",
                principalColumn: "BrcId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Contestant_MatWinId",
                table: "Match",
                column: "MatWinId",
                principalTable: "Contestant",
                principalColumn: "ConId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Pool_PoolsId",
                table: "Match",
                column: "PoolsId",
                principalTable: "Pool",
                principalColumn: "PolId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
