using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnePiece.Web.Migrations
{
    public partial class UpdateVolume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Seasons");

            migrationBuilder.AddColumn<string>(
                name: "EpisodeRange",
                table: "Volumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolumeType",
                table: "Volumes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Seasons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Seasons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolumeId",
                table: "Episodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_VolumeId",
                table: "Episodes",
                column: "VolumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Volumes_VolumeId",
                table: "Episodes",
                column: "VolumeId",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Volumes_VolumeId",
                table: "Episodes");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_VolumeId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "EpisodeRange",
                table: "Volumes");

            migrationBuilder.DropColumn(
                name: "VolumeType",
                table: "Volumes");

            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "VolumeId",
                table: "Episodes");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Seasons",
                nullable: true);
        }
    }
}
