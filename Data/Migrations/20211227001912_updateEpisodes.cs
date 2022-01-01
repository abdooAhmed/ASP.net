using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace e_c_Project.Data.Migrations
{
    public partial class updateEpisodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "SeriesEpisodes",
                columns: table => new
                {
                    Episode_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Episode_num = table.Column<int>(type: "int", nullable: false),
                    SerieId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesEpisodes", x => x.Episode_Id);
                    table.ForeignKey(
                        name: "FK_SeriesEpisodes_series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "series",
                        principalColumn: "seriesId",
                        onDelete: ReferentialAction.Restrict);
                });



            migrationBuilder.CreateTable(
                name: "SeriesEpisodeServers",
                columns: table => new
                {
                    LinkId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    episodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    seriesId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesEpisodeServers", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_SeriesEpisodeServers_series_seriesId",
                        column: x => x.seriesId,
                        principalTable: "series",
                        principalColumn: "seriesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeriesEpisodeServers_SeriesEpisodes_episodeId",
                        column: x => x.episodeId,
                        principalTable: "SeriesEpisodes",
                        principalColumn: "Episode_Id",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_SeriesEpisodeServers_episodeId",
                table: "SeriesEpisodeServers",
                column: "episodeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesEpisodeServers_seriesId",
                table: "SeriesEpisodeServers",
                column: "seriesId");
        }

            
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
