using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCatalog.Data.Migrations
{
    public partial class TitlesTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EndYear",
                table: "Titles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalTitle",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RunTimeMinutes",
                table: "Titles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartYear",
                table: "Titles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TitleType",
                table: "Titles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_TitleType",
                table: "Titles",
                column: "TitleType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Titles_TitleType",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "OriginalTitle",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "RunTimeMinutes",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "StartYear",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "TitleType",
                table: "Titles");
        }
    }
}
