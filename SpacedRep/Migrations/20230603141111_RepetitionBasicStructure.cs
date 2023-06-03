using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpacedRep.Migrations
{
    /// <inheritdoc />
    public partial class RepetitionBasicStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Repititions",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedUntil",
                table: "Repititions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastReviewOn",
                table: "Repititions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stage",
                table: "Repititions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockedUntil",
                table: "Repititions");

            migrationBuilder.DropColumn(
                name: "LastReviewOn",
                table: "Repititions");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "Repititions");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Repititions",
                newName: "Created");
        }
    }
}
