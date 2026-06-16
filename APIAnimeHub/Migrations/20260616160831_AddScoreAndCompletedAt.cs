using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAnimeHub.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreAndCompletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "AnimeList",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "AnimeList",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "AnimeList");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AnimeList");
        }
    }
}
