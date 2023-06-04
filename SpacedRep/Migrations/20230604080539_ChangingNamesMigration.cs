using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpacedRep.Migrations
{
    /// <inheritdoc />
    public partial class ChangingNamesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Repititions",
                table: "Repititions");

            migrationBuilder.RenameTable(
                name: "Repititions",
                newName: "Repetitions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repetitions",
                table: "Repetitions",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Repetitions",
                table: "Repetitions");

            migrationBuilder.RenameTable(
                name: "Repetitions",
                newName: "Repititions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repititions",
                table: "Repititions",
                column: "Id");
        }
    }
}
