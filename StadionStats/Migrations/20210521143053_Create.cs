using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StadionStats.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ArticleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    TeaserText = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ExternalLink = table.Column<string>(nullable: true),
                    ReadmoreLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleID);
                });

            migrationBuilder.CreateTable(
                name: "Land",
                columns: table => new
                {
                    LandID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Landenavn = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Land", x => x.LandID);
                });

            migrationBuilder.CreateTable(
                name: "Stadion",
                columns: table => new
                {
                    StadionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Navn = table.Column<string>(nullable: true),
                    AttendanceCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadion", x => x.StadionID);
                });

            migrationBuilder.CreateTable(
                name: "Liga",
                columns: table => new
                {
                    LigaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Navn = table.Column<string>(maxLength: 40, nullable: false),
                    Logo = table.Column<string>(nullable: true),
                    LandID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liga", x => x.LigaID);
                    table.ForeignKey(
                        name: "FK_Liga_Land_LandID",
                        column: x => x.LandID,
                        principalTable: "Land",
                        principalColumn: "LandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team2s",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StadionID = table.Column<int>(nullable: false),
                    Seasontickets = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Sponsortext = table.Column<string>(nullable: true),
                    LigaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team2s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team2s_Liga_LigaID",
                        column: x => x.LigaID,
                        principalTable: "Liga",
                        principalColumn: "LigaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team2s_Stadion_StadionID",
                        column: x => x.StadionID,
                        principalTable: "Stadion",
                        principalColumn: "StadionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    HomeTeamId = table.Column<int>(nullable: true),
                    AwayTeamId = table.Column<int>(nullable: true),
                    HomeScore = table.Column<float>(nullable: false),
                    GuestScore = table.Column<float>(nullable: false),
                    Attendance = table.Column<int>(nullable: false),
                    Tv = table.Column<int>(nullable: false),
                    StadionID = table.Column<int>(nullable: false),
                    LigaID = table.Column<int>(nullable: false),
                    IsCorona = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Team2s_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Team2s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Team2s_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Team2s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Liga_LigaID",
                        column: x => x.LigaID,
                        principalTable: "Liga",
                        principalColumn: "LigaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Stadion_StadionID",
                        column: x => x.StadionID,
                        principalTable: "Stadion",
                        principalColumn: "StadionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    SponsorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    SponsorText = table.Column<string>(nullable: false),
                    Team2ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.SponsorID);
                    table.ForeignKey(
                        name: "FK_Sponsor_Team2s_Team2ID",
                        column: x => x.Team2ID,
                        principalTable: "Team2s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_LigaID",
                table: "Games",
                column: "LigaID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_StadionID",
                table: "Games",
                column: "StadionID");

            migrationBuilder.CreateIndex(
                name: "IX_Liga_LandID",
                table: "Liga",
                column: "LandID");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsor_Team2ID",
                table: "Sponsor",
                column: "Team2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Team2s_LigaID",
                table: "Team2s",
                column: "LigaID");

            migrationBuilder.CreateIndex(
                name: "IX_Team2s_StadionID",
                table: "Team2s",
                column: "StadionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Sponsor");

            migrationBuilder.DropTable(
                name: "Team2s");

            migrationBuilder.DropTable(
                name: "Liga");

            migrationBuilder.DropTable(
                name: "Stadion");

            migrationBuilder.DropTable(
                name: "Land");
        }
    }
}
