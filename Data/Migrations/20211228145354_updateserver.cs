using Microsoft.EntityFrameworkCore.Migrations;

namespace e_c_Project.Data.Migrations
{
    public partial class updateserver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeriesEpisodeServers_series_seriesId",
                table: "SeriesEpisodeServers");

            migrationBuilder.DropIndex(
                name: "IX_SeriesEpisodeServers_seriesId",
                table: "SeriesEpisodeServers");

            migrationBuilder.DropColumn(
                name: "seriesId",
                table: "SeriesEpisodeServers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "seriesId",
                table: "SeriesEpisodeServers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeriesEpisodeServers_seriesId",
                table: "SeriesEpisodeServers",
                column: "seriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesEpisodeServers_series_seriesId",
                table: "SeriesEpisodeServers",
                column: "seriesId",
                principalTable: "series",
                principalColumn: "seriesId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
