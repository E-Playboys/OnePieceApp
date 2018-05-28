using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnePiece.Web.Migrations
{
    public partial class AddVolumne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolumeId",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChapterRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VolumeNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_VolumeId",
                table: "Mangas",
                column: "VolumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Volumes_VolumeId",
                table: "Mangas",
                column: "VolumeId",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Volumes_VolumeId",
                table: "Mangas");

            migrationBuilder.DropTable(
                name: "Volumes");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_VolumeId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "VolumeId",
                table: "Mangas");
        }
    }
}
