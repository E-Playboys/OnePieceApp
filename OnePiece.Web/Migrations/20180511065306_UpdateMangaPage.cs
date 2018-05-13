using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnePiece.Web.Migrations
{
    public partial class UpdateMangaPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "MangaPages");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "MangaPages");

            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Manga");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "MangaPages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "MangaPages",
                nullable: false,
                defaultValue: 0);
        }
    }
}
