using Microsoft.EntityFrameworkCore.Migrations;

namespace OOMAC.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contestant",
                columns: table => new
                {
                    ConId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConLName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConFName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConAge = table.Column<int>(type: "int", nullable: false),
                    ConTechnicalSkill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contestant", x => x.ConId);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    TrnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrnMinAge = table.Column<int>(type: "int", nullable: false),
                    TrnMaxAge = table.Column<int>(type: "int", nullable: false),
                    TrnMinTechSkill = table.Column<int>(type: "int", nullable: false),
                    TrnMaxTechSkill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.TrnId);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatWinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatId);
                    table.ForeignKey(
                        name: "FK_Match_Contestant_MatWinId",
                        column: x => x.MatWinId,
                        principalTable: "Contestant",
                        principalColumn: "ConId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bracker",
                columns: table => new
                {
                    BrcId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrcTrnId = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bracker", x => x.BrcId);
                    table.ForeignKey(
                        name: "FK_Bracker_Tournament_BrcTrnId",
                        column: x => x.BrcTrnId,
                        principalTable: "Tournament",
                        principalColumn: "TrnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContestantTournament",
                columns: table => new
                {
                    ContestansId = table.Column<int>(type: "int", nullable: false),
                    TournamentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestantTournament", x => new { x.ContestansId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_ContestantTournament_Contestant_ContestansId",
                        column: x => x.ContestansId,
                        principalTable: "Contestant",
                        principalColumn: "ConId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContestantTournament_Tournament_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournament",
                        principalColumn: "TrnId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "MatchupEntry",
                columns: table => new
                {
                    MaeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaeConId = table.Column<int>(type: "int", nullable: false),
                    MaeScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchId = table.Column<int>(type: "int", nullable: true)
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
                name: "BracketMatch",
                columns: table => new
                {
                    BracketsId = table.Column<int>(type: "int", nullable: false),
                    MatchesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BracketMatch", x => new { x.BracketsId, x.MatchesId });
                    table.ForeignKey(
                        name: "FK_BracketMatch_Bracker_BracketsId",
                        column: x => x.BracketsId,
                        principalTable: "Bracker",
                        principalColumn: "BrcId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BracketMatch_Match_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Match",
                        principalColumn: "MatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchPool",
                columns: table => new
                {
                    MatchesId = table.Column<int>(type: "int", nullable: false),
                    PoolsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPool", x => new { x.MatchesId, x.PoolsId });
                    table.ForeignKey(
                        name: "FK_MatchPool_Match_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Match",
                        principalColumn: "MatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPool_Pool_PoolsId",
                        column: x => x.PoolsId,
                        principalTable: "Pool",
                        principalColumn: "PolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bracker_BrcTrnId",
                table: "Bracker",
                column: "BrcTrnId");

            migrationBuilder.CreateIndex(
                name: "IX_BracketMatch_MatchesId",
                table: "BracketMatch",
                column: "MatchesId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestantTournament_TournamentsId",
                table: "ContestantTournament",
                column: "TournamentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatWinId",
                table: "Match",
                column: "MatWinId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPool_PoolsId",
                table: "MatchPool",
                column: "PoolsId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BracketMatch");

            migrationBuilder.DropTable(
                name: "ContestantTournament");

            migrationBuilder.DropTable(
                name: "MatchPool");

            migrationBuilder.DropTable(
                name: "MatchupEntry");

            migrationBuilder.DropTable(
                name: "Bracker");

            migrationBuilder.DropTable(
                name: "Pool");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Tournament");

            migrationBuilder.DropTable(
                name: "Contestant");
        }
    }
}
