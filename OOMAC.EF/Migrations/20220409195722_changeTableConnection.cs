using Microsoft.EntityFrameworkCore.Migrations;

namespace OOMAC.EF.Migrations
{
    public partial class changeTableConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BracketMatch");

            migrationBuilder.DropTable(
                name: "MatchPool");

            migrationBuilder.AddColumn<int>(
                name: "BracketsId",
                table: "Match",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PoolsId",
                table: "Match",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_BracketsId",
                table: "Match",
                column: "BracketsId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_PoolsId",
                table: "Match",
                column: "PoolsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Bracker_BracketsId",
                table: "Match",
                column: "BracketsId",
                principalTable: "Bracker",
                principalColumn: "BrcId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Pool_PoolsId",
                table: "Match",
                column: "PoolsId",
                principalTable: "Pool",
                principalColumn: "PolId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Bracker_BracketsId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Pool_PoolsId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_BracketsId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_PoolsId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "BracketsId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "PoolsId",
                table: "Match");

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
                name: "IX_BracketMatch_MatchesId",
                table: "BracketMatch",
                column: "MatchesId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPool_PoolsId",
                table: "MatchPool",
                column: "PoolsId");
        }
    }
}
