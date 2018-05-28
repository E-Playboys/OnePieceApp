using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnePiece.Web.Migrations
{
    public partial class ChangeMangaTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manga_Seasons_SeasonId",
                table: "Manga");

            migrationBuilder.DropForeignKey(
                name: "FK_MangaPages_Manga_MangaId",
                table: "MangaPages");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Manga_MangaId",
                table: "Medias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manga",
                table: "Manga");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.RenameTable(
                name: "Manga",
                newName: "Mangas");

            migrationBuilder.RenameTable(
                name: "Article",
                newName: "Articles");

            migrationBuilder.RenameIndex(
                name: "IX_Manga_SeasonId",
                table: "Mangas",
                newName: "IX_Mangas_SeasonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mangas",
                table: "Mangas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MangaPages_Mangas_MangaId",
                table: "MangaPages",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Seasons_SeasonId",
                table: "Mangas",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Mangas_MangaId",
                table: "Medias",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MangaPages_Mangas_MangaId",
                table: "MangaPages");

            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Seasons_SeasonId",
                table: "Mangas");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Mangas_MangaId",
                table: "Medias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mangas",
                table: "Mangas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "Mangas",
                newName: "Manga");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "Article");

            migrationBuilder.RenameIndex(
                name: "IX_Mangas_SeasonId",
                table: "Manga",
                newName: "IX_Manga_SeasonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manga",
                table: "Manga",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Manga_Seasons_SeasonId",
                table: "Manga",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MangaPages_Manga_MangaId",
                table: "MangaPages",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Manga_MangaId",
                table: "Medias",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
